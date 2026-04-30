using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

namespace RunnerTray
{
    internal sealed class TrayApplicationContext : ApplicationContext
    {
        private readonly NotifyIcon _trayIcon;
        private readonly ContextMenuStrip _trayMenu;
        private readonly ToolStripMenuItem _startItem;
        private readonly ToolStripMenuItem _stopItem;
        private readonly ToolStripMenuItem _settingsItem;
        private readonly ToolStripMenuItem _exitItem;

        private Process _process;

        public TrayApplicationContext()
        {
            _startItem = new ToolStripMenuItem("Start", null, OnStartClicked);
            _stopItem = new ToolStripMenuItem("Stop", null, OnStopClicked);
            _settingsItem = new ToolStripMenuItem("Settings", null, OnSettingsClicked);
            _exitItem = new ToolStripMenuItem("Exit", null, OnExitClicked);

            _trayMenu = new ContextMenuStrip();
            _trayMenu.Items.AddRange(new ToolStripItem[]
            {
                _startItem,
                _stopItem,
                _settingsItem,
                _exitItem
            });

            _trayIcon = new NotifyIcon
            {
                Icon = LoadTrayIcon(),
                Visible = true,
                Text = "RunnerTray",
                ContextMenuStrip = _trayMenu
            };

            // Automatically start the configured command when the application starts
            StartCommand();

            UpdateMenuItems();
        }

        private bool IsRunning => _process != null && !_process.HasExited;
        public static bool IsRunningAsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void StartCommand()
        {
            if (IsRunning)
            {
                ShowBalloon("Process is already running.");
                UpdateMenuItems();
                return;
            }

            var command = AppSettings.Current.Command;
            var runAsAdmin = AppSettings.Current.RunAsAdmin;
            var isRunningAsAdmin = IsRunningAsAdmin();
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C " + command,
                    UseShellExecute = isRunningAsAdmin,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                if (runAsAdmin && !isRunningAsAdmin)
                {
                    psi.Verb = "runas";
                }
                
                _process = new Process { StartInfo = psi, EnableRaisingEvents = true };
                _process.Exited += (s, e) =>
                {
                    ShowBalloon("Process exited.");
                    UpdateMenuItems();
                };

                _process.Start();
                ShowBalloon("Started: " + command);
                UpdateMenuItems();
            }
            catch (Exception ex)
            {
                ShowBalloon("Failed to start process: " + ex.Message);
                _process = null;
            }
        }

        private void StopCommand()
        {
            if (!IsRunning)
            {
                ShowBalloon("Process is not running.");
                UpdateMenuItems();
                return;
            }

            try
            {
                _process.Kill();
                _process.WaitForExit(2000);
            }
            catch (Exception ex)
            {
                ShowBalloon("Failed to stop process: " + ex.Message);
            }
            finally
            {
                _process.Dispose();
                _process = null;
                UpdateMenuItems();
            }
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            StartCommand();
        }

        private void OnStopClicked(object sender, EventArgs e)
        {
            StopCommand();
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            using (var dlg = new SettingsForm())
            {
                dlg.ShowDialog();
            }
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            ExitThread();
        }

        protected override void ExitThreadCore()
        {
            _trayIcon.Visible = false;
            if (IsRunning)
            {
                try
                {
                    _process.Kill();
                }
                catch
                {
                }
            }

            base.ExitThreadCore();
        }

        private void ShowBalloon(string message)
        {
            _trayIcon.BalloonTipTitle = "RunnerTray";
            _trayIcon.BalloonTipText = message;
            _trayIcon.ShowBalloonTip(3000);
        }

        private void UpdateMenuItems()
        {
            var running = IsRunning;
            _startItem.Enabled = !running;
            _stopItem.Enabled = running;
        }

        private static Icon LoadTrayIcon()
        {
            try
            {
                // Use the application's own icon (embedded in the executable/resources)
                var exePath = Process.GetCurrentProcess().MainModule.FileName;
                var associated = Icon.ExtractAssociatedIcon(exePath);
                if (associated != null)
                {
                    return associated;
                }
            }
            catch
            {
                // fall back below
            }

            // Fallback to a standard icon if anything goes wrong
            return SystemIcons.Application;
        }
    }
}

