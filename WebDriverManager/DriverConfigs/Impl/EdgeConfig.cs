using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebDriverManager.Helpers;

namespace WebDriverManager.DriverConfigs.Impl
{
    public class EdgeConfig : IDriverConfig
    {
        public virtual string GetName()
        {
            return "Edge";
        }

        public virtual string GetUrl32()
        {
            return "https://download.microsoft.com/download/<version>/MicrosoftWebDriver.exe";
        }

        public virtual string GetUrl64()
        {
            return GetUrl32();
        }

        public virtual string GetBinaryName()
        {
            return "MicrosoftWebDriver.exe";
        }

        /// <summary>
        /// Gets the latest version.
        /// </summary>
        /// <returns></returns>
        public virtual string GetDriverVersion(string browserVersion)
        {
            try
            {
                if (browserVersion.Equals("Latest", StringComparison.InvariantCultureIgnoreCase))
                {
                    // look at dictoinary and get latest version stored
                    return CompatibilityHelper.GetLatestStoredVersion(BrowserName.Edge);
                }
                else
                {
                    // look at dictoinary and get matching version
                    return CompatibilityHelper.GetCompatibleStoredVersion(BrowserName.Edge, browserVersion);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to get 'driverVersion'", ex.InnerException);
            }

        }

    }
}
