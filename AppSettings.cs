using System;
using System.IO;

namespace RunnerTray
{
    internal static class AppSettings
    {
        private const string FileName = "RunnerTray.config";
        private const string DefaultCommand = @"C:\Runner\run.cmd";

        public static string LoadCommand()
        {
            try
            {
                var path = GetConfigPath();
                if (File.Exists(path))
                {
                    return File.ReadAllText(path);
                }
            }
            catch
            {
                // ignore and fall back to default
            }

            return DefaultCommand;
        }

        public static void SaveCommand(string command)
        {
            try
            {
                var path = GetConfigPath();
                File.WriteAllText(path, command ?? string.Empty);
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

