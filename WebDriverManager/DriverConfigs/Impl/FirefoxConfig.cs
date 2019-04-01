using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using WebDriverManager.Helpers;

namespace WebDriverManager.DriverConfigs.Impl
{
    public class FirefoxConfig : IDriverConfig
    {
        public virtual string GetName()
        {
            return "Firefox";
        }

        public virtual string GetUrl32()
        {
            return
                "https://github.com/mozilla/geckodriver/releases/download/v<version>/geckodriver-v<version>-win32.zip";
        }

        public virtual string GetUrl64()
        {
            return
                "https://github.com/mozilla/geckodriver/releases/download/v<version>/geckodriver-v<version>-win64.zip";
        }

        public virtual string GetBinaryName()
        {
            return "geckodriver.exe";
        }

        /// <summary>
        /// The selenium driver list URL
        /// </summary>
        private const string _geckDriverListURL = "https://api.github.com";

        private string GetLatestVersion()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_geckDriverListURL);
                    client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync("repos/mozilla/geckodriver/releases/latest").Result;
                    response.EnsureSuccessStatusCode();
                    var latestVersionJSON = response.Content.ReadAsStringAsync().Result;

                    JObject jsonObject = JObject.Parse(latestVersionJSON);

                    string versionTag = (string)jsonObject["tag_name"];

                    return CompatibilityHelper.GetVersionSubString(versionTag);
                }
            }
            catch (Exception ex)
            {
                //Add Logging
                throw new ArgumentException("Unable to get 'driverVersion'");
            }
        }        

        public string GetDriverVersion(string browserVersion)
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

                    return CompatibilityHelper.GetLatestStoredVersion(BrowserName.Firefox);
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
                    //get matching version 
                    driverVersion = CompatibilityHelper.GetCompatibleStoredVersion(BrowserName.Firefox, browserVersion);                  
                    
                    if (!string.IsNullOrWhiteSpace(driverVersion))
                    {
                        return driverVersion;
                    }

                    //if browser version is newer than examples stored in compatbility dictionary 
                    //try using the most recent version of gecko driver
                    driverVersion = CompatibilityHelper.GetLatestStoredVersion(BrowserName.Firefox);

                    if(!string.IsNullOrWhiteSpace(driverVersion))
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
    }
}
