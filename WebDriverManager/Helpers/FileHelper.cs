using System;
using System.IO;

namespace WebDriverManager.Helpers
{
    public static class FileHelper
    {
        public static string GetTempDirectoryJoey()
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

        //***********************************

        public static string GetZipDestination(string url)
        {
            var tempDirectory = Path.GetTempPath();
            var zipName = Path.GetFileName(url);
            if (zipName == null) throw new ArgumentNullException($"Can't get zip name from URL: {url}");
            return Path.Combine(tempDirectory, zipName);
        }

        public static string GetBinDestination(string driverName, string version, Architecture architecture, string binName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            return Path.Combine(currentDirectory, driverName, version, architecture.ToString(), binName);
        }

        
    }
}