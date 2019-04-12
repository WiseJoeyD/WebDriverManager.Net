using System;
using System.IO;

namespace WebDriverManager.Helpers
{
    public static class FileHelper
    {
        public static string GetTempDirectory()
        {
            var tempDirectory = Path.GetTempPath();
            return tempDirectory;
        }

        public static void CreateDestinationDirectory(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (directory != null) Directory.CreateDirectory(directory);
        }

        public static string GetDriverDestination(string driverName, string version, Architecture architecture, string binName)
        {
            return Path.Combine(driverName, version, architecture.ToString(), binName);
        }

        public static string GetFileNameFromUrl(string url)
        {
            var zipName = Path.GetFileName(url);
            return zipName;
        }
        
    }
}