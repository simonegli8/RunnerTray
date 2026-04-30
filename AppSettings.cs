using System;
using System.IO;
using Newtonsoft.Json;

namespace RunnerTray
{
    public class AppSettings
    {
        private const string FileName = "RunnerTray.config";
        private const string DefaultCommand = @"C:\Runner\run.cmd";

        public string Command { get; set; } = DefaultCommand;
        public bool RunAsAdmin { get; set; } = false;

        public static AppSettings Current { get; private set; } = new AppSettings().Load();

        public AppSettings Load()
        {
            try
            {
                var path = GetConfigPath();
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    var settings = JsonConvert.DeserializeObject<AppSettings>(json);
                    Command = settings.Command;
                    RunAsAdmin = settings.RunAsAdmin;
                }
            }
            catch
            {
                // ignore and fall back to default
            }
            return this;
        }

        public void Save()
        {
            try
            {
                var path = GetConfigPath();
                File.WriteAllText(path, JsonConvert.SerializeObject(this));
            }
            catch
            {
                // ignore errors for now
            }
        }

        private static string GetConfigPath()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appFolder = Path.Combine(folder, "RunnerTray");
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            return Path.Combine(appFolder, FileName);
        }
    }
}

