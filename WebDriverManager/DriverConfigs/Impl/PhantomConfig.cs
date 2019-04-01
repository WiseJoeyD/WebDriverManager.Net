using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebDriverManager.Helpers;

namespace WebDriverManager.DriverConfigs.Impl
{
    public class PhantomConfig : IDriverConfig
    {
        public virtual string GetName()
        {
            return "Phantom";
        }

        public virtual string GetUrl32()
        {
            return "https://bitbucket.org/ariya/phantomjs/downloads/phantomjs-<version>-windows.zip";
        }

        public virtual string GetUrl64()
        {
            return GetUrl32();
        }

        public virtual string GetBinaryName()
        {
            return "phantomjs.exe";
        }        

        public string GetDriverVersion(string browserVersion)
        {
            try
            {
                // look at dictionary and get latest version stored
                //PhantomJS has no more active development (as of March 2019) so simply return last version from dictionary
                return CompatibilityHelper.GetLatestStoredVersion(BrowserName.PhantomJS);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to get 'driverVersion'", ex.InnerException);
            }
        }
    }
}
