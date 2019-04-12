using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

namespace WebDriverManager.Helpers
{
    public static class ZipHelper
    {
        public static string DownloadZip(string url, string tempDirectory, string fileName)
        {
            try
            {
                var zipFileLocation = Path.Combine(tempDirectory, fileName);

                if (!File.Exists(zipFileLocation))
                {
                    using (var webClient = new WebClient())
                    {
                        webClient.DownloadFile(new Uri(url), zipFileLocation);

                        if (!File.Exists(zipFileLocation))
                            throw new FileNotFoundException(fileName + " was not downloaded to " + tempDirectory, fileName);
                    }
                }

                return zipFileLocation;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to download Zip", ex.InnerException);
            }
        }

        public static bool UnZipToDriverLocation(string zipLocation, string driverLocation, string driverName)
        {
            try
            {
                if (!Path.GetExtension(zipLocation).Equals(".zip", StringComparison.CurrentCultureIgnoreCase))
                {
                    File.Copy(zipLocation, driverLocation);
                }
                else
                {
                    int NumberOfRetries = 3;
                    int DelayOnRetry = new Random().Next(10, 1500);

                    for (int i = 1; i <= NumberOfRetries; ++i)
                    {
                        try
                        {
                            var zipArchive = ZipFile.OpenRead(zipLocation);

                            foreach (var entry in zipArchive.Entries)
                            {
                                if (entry.Name == driverName)
                                {
                                    entry.ExtractToFile(driverLocation, true);
                                }
                            }

                            zipArchive.Dispose();

                            break; // When done we can break loop
                        }
                        catch (IOException ioEx) when (i <= NumberOfRetries)
                        {
                            // You may check error code to filter some exceptions, not every error
                            // can be recovered.
                            Thread.Sleep(DelayOnRetry);
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



        public static void DeleteZip(string path)
        {
            int NumberOfRetries = 3;
            int DelayOnRetry = new Random().Next(10, 1500);

            for (int i = 1; i <= NumberOfRetries; ++i)
            {
                try
                {
                    File.Delete(path);

                    
                }
                catch (IOException ioEx) when (i <= NumberOfRetries)
                {
                    Thread.Sleep(DelayOnRetry);
                }
            }

        }
    }
}