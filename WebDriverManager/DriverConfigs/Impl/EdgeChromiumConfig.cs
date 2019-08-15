using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebDriverManager.Helpers;

namespace WebDriverManager.DriverConfigs.Impl
{
    public class EdgeChromiumConfig : IDriverConfig
    {
        public virtual string GetName()
        {
            return "EdgeChromium";
        }

        public virtual string GetUrl32()
        {
            return "https://msedgewebdriverstorage.blob.core.windows.net/edgewebdriver/<version>/edgedriver_win32.zip";
        }

        public virtual string GetUrl64()
        {
            return "https://msedgewebdriverstorage.blob.core.windows.net/edgewebdriver/<version>/edgedriver_win64.zip";
        }

        public virtual string GetBinaryName()
        {
            return "msedgedriver.exe";
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
                    return CompatibilityHelper.GetLatestStoredVersion(BrowserName.EdgeChromium);
                }
                else
                {
                    // look at dictoinary and get matching version
                    return CompatibilityHelper.GetCompatibleStoredVersion(BrowserName.EdgeChromium, browserVersion);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to get 'driverVersion'", ex.InnerException);
            }

        }

    }
}
