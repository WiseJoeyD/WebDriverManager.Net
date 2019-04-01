using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using WebDriverManager.Helpers;

namespace WebDriverManager.DriverConfigs.Impl
{
    public class InternetExplorerConfig : IDriverConfig
    {
        public virtual string GetName()
        {
            return "InternetExplorer";
        }

        public virtual string GetUrl32()
        {
            return "http://selenium-release.storage.googleapis.com/<release>/IEDriverServer_Win32_<version>.zip";
        }

        public virtual string GetUrl64()
        {
            return "http://selenium-release.storage.googleapis.com/<release>/IEDriverServer_x64_<version>.zip";
        }

        public virtual string GetBinaryName()
        {
            return "IEDriverServer.exe";
        }

        /// <summary>
        /// The selenium driver list URL
        /// </summary>
        private const string _seleniumDriverListURL = "http://selenium-release.storage.googleapis.com";

        /// <summary>
        /// Gets the latest version.
        /// </summary>
        /// <returns></returns>
        public virtual string GetDriverVersion(string browserVersion)
        {
            try
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        var xmlhtml = client.DownloadString(_seleniumDriverListURL);

                        var versionXml = new XmlDocument();
                        versionXml.LoadXml(xmlhtml);

                        //using xpath to find only contents with 'IEDriverServer' string 
                        XmlNodeList xPathIEDriverNodeList = versionXml.SelectNodes("//*[contains(text(), 'IEDriverServer_Win32')]/..");

                        // Find the most recent version number based on Modifed date
                        string mostRecentVersion = GetMostRecentVersionNumber(xPathIEDriverNodeList);

                        return mostRecentVersion;
                    }
                }
                catch
                {
                    //Add Logging
                }

                //if code has reached this point then online lookup was unsuccessful
                /// look at dictoinary and get latest version stored

                CompatibilityHelper.Initialise(BrowserName.InternetExplorer);

                var latestStoredVersion = CompatibilityHelper.GetLatestStoredVersion(BrowserName.InternetExplorer);

                return latestStoredVersion;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to get 'driverVersion'", ex.InnerException);
            }
        }


        /// <summary>
        /// Gets the most recent version number based on the date in the node
        /// </summary>
        /// <param name="xPathIEDriverNodeList">The x path ie driver node list.</param>
        /// <returns>The latest driver version number</returns>
        private static string GetMostRecentVersionNumber(XmlNodeList xPathIEDriverNodeList)
        {
            var versionRegEx = new Regex(@"(?<=_)(\d{1}(\.\d+)+)");
            var mostRecentVersion = String.Empty;
            var mostRecentDate = DateTime.MinValue;

            foreach (XmlNode ieDriverContentNode in xPathIEDriverNodeList)
            {
                var currentNodeVersion = String.Empty;

                foreach (XmlNode node in ieDriverContentNode.ChildNodes)
                {
                    if (node.Name == "Key")
                    {
                        // Strip the version number from the Key, eg 2.39/IEDriverServer_Win32_2.39.0.zip
                        Match match = versionRegEx.Match(node.InnerText);
                        if (match.Success)
                        {
                            // Store the version number
                            currentNodeVersion = match.Value;
                        }
                    }

                    if (node.Name == "LastModified")
                    {
                        // Inner text holds the date, eg 2014-01-13T22:17:40.327Z
                        if (DateTime.TryParse(node.InnerText, out DateTime currentNodeDate))
                        {
                            // If the date on this node is after the stored date then overwrite the stored date and version number
                            if (currentNodeDate > mostRecentDate)
                            {
                                mostRecentDate = currentNodeDate;
                                mostRecentVersion = currentNodeVersion;
                                break;
                            }
                        }
                    }
                }
            }

            return mostRecentVersion;
        }


        /// <summary>
        /// Gets the latest version.
        /// </summary>
        /// <returns></returns>
        private string GetLatestVersion()
        {
            using (var client = new WebClient())
            {
                var xmlhtml = client.DownloadString(_seleniumDriverListURL);

                var versionXml = new XmlDocument();
                versionXml.LoadXml(xmlhtml);

                //using xpath to find only contents with 'IEDriverServer' string 
                XmlNodeList xPathIEDriverNodeList = versionXml.SelectNodes("//*[contains(text(), 'IEDriverServer_Win32')]/..");

                // Find the most recent version number based on Modifed date
                string mostRecentVersion = GetMostRecentVersionNumber(xPathIEDriverNodeList);

                return mostRecentVersion;
            }
        }

    }
}