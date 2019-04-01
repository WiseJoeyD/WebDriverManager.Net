using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using WebDriverManager.Helpers;

namespace WebDriverManager.DriverConfigs.Impl
{
    public class ChromeConfig : IDriverConfig
    {
        public virtual string GetName()
        {
            return "Chrome";
        }

        public virtual string GetUrl32()
        {
            return "https://chromedriver.storage.googleapis.com/<version>/chromedriver_win32.zip";
        }

        public virtual string GetUrl64()
        {
            return GetUrl32();
        }

        public virtual string GetBinaryName()
        {
            return "chromedriver.exe";
        }

        public virtual string GetDriverVersion(string browserVersion)
        {
            var driverVersion = String.Empty;

            if (browserVersion.Equals("Latest", StringComparison.InvariantCultureIgnoreCase))
            {
                // look online
                bool onlineLookupSuccessful;

                try
                {
                    driverVersion = GetLatestVersion();
                    onlineLookupSuccessful = true;
                }
                catch (Exception)
                {
                    onlineLookupSuccessful = false;
                    //Add Logging
                }


                //was it successful?
                if (!onlineLookupSuccessful)
                {
                    //no
                    /// look at dictoinary and get latest version stored

                    return CompatibilityHelper.GetLatestStoredVersion(BrowserName.Chrome);
                }
                else
                {
                    return driverVersion;
                }

            }
            else
            {
                //search 
                try
                {
                    Int32.TryParse(browserVersion, out int j);

                    if (j > 73)
                    {
                        driverVersion = GetLatestVersion(browserVersion);
                    }
                    else
                    {
                        driverVersion = CompatibilityHelper.GetCompatibleStoredVersion(BrowserName.Chrome, browserVersion);
                    }

                    if (!string.IsNullOrWhiteSpace(driverVersion))
                    {
                        return driverVersion;
                    }
                    else
                    {
                        throw new ArgumentException("Unable to get 'driverVersion'");
                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }                

            }
        }

        public string GetLatestVersion(string specificVersion = "")
        {

            if (!String.IsNullOrWhiteSpace(specificVersion))
            {
                specificVersion = "_" + specificVersion;
            }

            var uri = new Uri("https://chromedriver.storage.googleapis.com/LATEST_RELEASE" + specificVersion);
            var webRequest = WebRequest.Create(uri);
            using (var response = webRequest.GetResponse())
            {
                using (var content = response.GetResponseStream())
                {
                    if (content == null) throw new ArgumentNullException($"Can't get content from URL: {uri}");
                    using (var reader = new StreamReader(content))
                    {
                        var version = reader.ReadToEnd().Trim();
                        return version;
                    }
                }
            }
        }
        
    }
}