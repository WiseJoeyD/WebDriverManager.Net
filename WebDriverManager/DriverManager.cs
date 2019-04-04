using System;
using System.Collections.Generic;
using System.IO;
using WebDriverManager.DriverConfigs;
using WebDriverManager.Helpers;
using WebDriverManager.Services;
using WebDriverManager.Services.Impl;

namespace WebDriverManager
{
    public class DriverManager
    {
        private readonly PathVariableService _variableService;        

        public DriverManager()
        {
            _variableService = new PathVariableService();
        }

        public DriverManager(PathVariableService variableService)
        {
            _variableService = variableService;
        }                
        

        public void SetupLatestDriver(string workingDirectory, IDriverConfig browserConfig,
                                Architecture architecture = Architecture.Auto)
        {
            SetupCompatibleDriver(workingDirectory, browserConfig, "Latest", architecture);
        }


        public void SetupCompatibleDriver(string workingDirectory, IDriverConfig browserConfig, string browserVersion,
                                        Architecture architecture = Architecture.Auto)
        {                        
            var driverArchitecture = architecture.Equals(Architecture.Auto) ? ArchitectureHelper.GetArchitecture() : architecture;                     

            List<Tuple<string, string>> driverInfo;

            driverInfo = GetDriverInfo(browserConfig, driverArchitecture, browserVersion);

            //these are all the variables we need to be setup
            var url = driverInfo[0].Item1;            
            var driverLocation = Path.Combine(workingDirectory, driverInfo[0].Item2);
            

            if (File.Exists(driverLocation))
            {
                SetUpLocalDriver(driverLocation);
            }
            else
            {
                var tempDirectory = FileHelper.GetTempDirectoryJoey();
                var driverName = browserConfig.GetBinaryName();
                var downloadedFileName = FileHelper.GetFileNameFromUrl(url);                

                var zipLocation = ZipHelper.DownloadZipJoey(url, tempDirectory, downloadedFileName);

                FileHelper.CreateDestinationDirectory(driverLocation);

                ZipHelper.UnZipToDriverLocation(zipLocation, driverLocation, driverName);

                ZipHelper.DeleteZipJoey(zipLocation);

                SetUpLocalDriver(driverLocation);
            }
        }

        /// <summary>
        /// Adds a web driver exe to the Process Environment Path to be used by Selenium
        /// </summary>
        /// <param name="pathToDriverExecutable">path including filename</param>
        public void SetUpLocalDriver(string pathToDriverExecutable)
        {
            _variableService.AddDriverToEnvironmentPathVariable(pathToDriverExecutable);
        }


        public List<Tuple<string, string>> GetDriverInfo(IDriverConfig browserConfig, Architecture architecture, string browserVersion)
        {
            var baseUrlForSelectedArchitecture = architecture.Equals(Architecture.X32) ? browserConfig.GetUrl32() : browserConfig.GetUrl64();

            browserVersion = browserVersion.Equals("Latest", StringComparison.InvariantCultureIgnoreCase) ? browserVersion : CompatibilityHelper.GetMajorVersion(browserVersion);

            var driverVersion = browserConfig.GetDriverVersion(browserVersion);

            var url = UrlHelper.BuildUrl(baseUrlForSelectedArchitecture, driverVersion);

            var fileAndFolderPath = FileHelper.GetDriverDestination(browserConfig.GetName(), driverVersion, architecture, browserConfig.GetBinaryName());

            List<Tuple<string, string>> driverInfo = new List<Tuple<string, string>>();

            driverInfo.Add(new Tuple<string, string>(url, fileAndFolderPath));

            return driverInfo;
        }

    }
}