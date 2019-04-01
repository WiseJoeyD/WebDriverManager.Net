using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace WebDriverManager.Helpers
{
    public static class ZipHelper
    {       
        public static string DownloadZipJoey(string url, string tempDirectory, string fileName)
        {
            try
            {
                var zipFileLocation = Path.Combine(tempDirectory, fileName);

                if (!File.Exists(zipFileLocation))
                {
                    using (var webClient = new WebClient())
                    {
                        webClient.DownloadFile(new Uri(url), zipFileLocation);
                    }
                }

                return zipFileLocation;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to download Zip", ex.InnerException);
            }
        }

        public static bool UnZipToDriverLocation(string zipLocation, string driveLocation, string driverName)
        {
            try
            {
                if (!Path.GetExtension(zipLocation).Equals(".zip", StringComparison.CurrentCultureIgnoreCase))
                {
                    File.Copy(zipLocation, driveLocation);
                }
                else
                {
                    using (var zip = ZipFile.Open(zipLocation, ZipArchiveMode.Read))
                    {
                        foreach (var entry in zip.Entries)
                        {
                            if (entry.Name == driverName)
                            {
                                entry.ExtractToFile(driveLocation, true);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to unzip download", ex.InnerException);
            }
        }

        public static void DeleteZipJoey(string path)
        {
            File.Delete(path);
        }

    }
}