using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebDriverManager.Helpers
{
    static class CompatibilityHelper
    {
        private static Dictionary<string, string> VersionList;

        public static void Initialise(BrowserName name)
        {
            switch (name)
            {
                case BrowserName.Chrome:
                    SetupChromeDictionary();
                    break;
                case BrowserName.Firefox:
                    SetupFirefoxDictionary();
                    break;
                case BrowserName.InternetExplorer:
                    SetupInternetExplorerDictionary();
                    break;
                case BrowserName.Edge:
                    SetupEdgeDictionary();
                    break;
                case BrowserName.Opera:
                    SetupOperaDictionary();
                    break;
                case BrowserName.PhantomJS:
                    SetupPhantomJSDictionary();
                    break;
                default:
                    break;
            }                 
        }

        public static string GetMajorVersion(string browserVersion)
        {
            var match = Regex.Match(browserVersion, @"^\d*").Value;

            if (!string.IsNullOrWhiteSpace(match))
            {
                return match;
            }
            else
            {
                throw new ArgumentException("No Major Version Found in 'browserVersion'");
            }
        }

        public static string GetVersionSubString(string versionString)
        {
            var match = Regex.Match(versionString, @"[^v.]\d*.\d*.\d").Value;

            if (!string.IsNullOrWhiteSpace(match))
            {
                return match;
            }
            else
            {
                throw new ArgumentException("No Version Found in 'versionString'");
            }
        }

        public static string GetLatestStoredVersion(BrowserName browser)
        {
            CompatibilityHelper.Initialise(browser);

            try
            {
                var latestStoredVersion = CompatibilityHelper.VersionList.OrderByDescending(x => x.Key).First().Value;

                return latestStoredVersion;
            }
            catch (Exception)
            {
                return String.Empty;
            }
            
        }

        public static string GetCompatibleStoredVersion(BrowserName browser, string versionNumber)
        {
            CompatibilityHelper.Initialise(browser);

            try
            {
                return CompatibilityHelper.VersionList[versionNumber];
            }
            catch (Exception)
            {
                return String.Empty;
            }
            
        }

        // Populate dictionaries       

        private static void SetupInternetExplorerDictionary()
        {
            VersionList = new Dictionary<string, string>();

            //Latest version compatible with all previous versions
            //updated March 2019
            //3.141.59
            VersionList.Add("Latest", "3.141.5");
        }

        private static void SetupPhantomJSDictionary()
        {
            VersionList = new Dictionary<string, string>();

            //Latest version compatible with all previous versions
            //updated March 2019
            //2.1.1
            VersionList.Add("Latest", "2.1.1");
        }

        private static void SetupChromeDictionary()
        {
            VersionList = new Dictionary<string, string>();

            //updated March 2019
            //source: https://chromedriver.storage.googleapis.com/2.46/notes.txt
            VersionList.Add("73", "2.46");
            VersionList.Add("72", "2.46");
            VersionList.Add("71", "2.46");
            VersionList.Add("70", "2.45");
            VersionList.Add("69", "2.44");
            VersionList.Add("68", "2.42");
            VersionList.Add("67", "2.41");
            VersionList.Add("66", "2.40");
            VersionList.Add("65", "2.38");
            VersionList.Add("64", "2.37");
            VersionList.Add("63", "2.36");
            VersionList.Add("62", "2.35");
            VersionList.Add("61", "2.34");
            VersionList.Add("60", "2.33");
            VersionList.Add("59", "2.32");
            VersionList.Add("58", "2.31");
            VersionList.Add("57", "2.29");
            VersionList.Add("56", "2.29");
            VersionList.Add("55", "2.28");
            VersionList.Add("54", "2.27");
            VersionList.Add("53", "2.26");
            VersionList.Add("52", "2.25");
            VersionList.Add("51", "2.23");
            VersionList.Add("50", "2.23");
            VersionList.Add("49", "2.22");
            VersionList.Add("48", "2.21");
            VersionList.Add("47", "2.21");
            VersionList.Add("46", "2.21");
            VersionList.Add("45", "2.20");
            VersionList.Add("44", "2.20");
            VersionList.Add("43", "2.20");
            VersionList.Add("42", "2.17");
            VersionList.Add("41", "2.15");
            VersionList.Add("40", "2.14");
            VersionList.Add("39", "2.14");
            VersionList.Add("38", "2.13");
            VersionList.Add("37", "2.12");
            VersionList.Add("36", "2.12");
        }

        private static void SetupFirefoxDictionary()
        {
            VersionList = new Dictionary<string, string>();

            //updated March 2019
            //source: https://firefox-source-docs.mozilla.org/testing/geckodriver/Support.html
            VersionList.Add("68", "0.24.0");
            VersionList.Add("67", "0.24.0");
            VersionList.Add("66", "0.24.0");
            VersionList.Add("65", "0.24.0");
            VersionList.Add("64", "0.24.0");
            VersionList.Add("63", "0.24.0");
            VersionList.Add("62", "0.24.0");
            VersionList.Add("61", "0.24.0");
            VersionList.Add("60", "0.24.0");
            VersionList.Add("59", "0.24.0");
            VersionList.Add("58", "0.24.0");
            VersionList.Add("57", "0.24.0");
            VersionList.Add("56", "0.20.1");
            VersionList.Add("55", "0.20.1");
            VersionList.Add("54", "0.18.0");
            VersionList.Add("53", "0.18.0");
            VersionList.Add("52", "0.17.0");
        }

        private static void SetupOperaDictionary()
        {
            VersionList = new Dictionary<string, string>();

            //updated March 2019
            //source: https://github.com/operasoftware/operachromiumdriver/releases
            VersionList.Add("58", "2.42");
            VersionList.Add("57", "2.41");
            VersionList.Add("56", "2.40");
            VersionList.Add("55", "2.38");
            VersionList.Add("54", "2.37");
            VersionList.Add("53", "2.36");
            VersionList.Add("52", "2.35");
            VersionList.Add("50", "2.33");
            VersionList.Add("49", "2.32");
            VersionList.Add("48", "2.30");
            VersionList.Add("46", "2.29");
        }

        private static void SetupEdgeDictionary()
        {
            VersionList = new Dictionary<string, string>();

            //updated March 2019
            //source: https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/
            //unlike other drivers, edge driver name is the same, and the path differes by a GUID not a version
            //storing like a version as it is combined with eh url in the same way as the other browser URLs
            //Version numbers represent either Edge brower and Edge HTML - due to way Microsoft display both versions
            //both are shown, so including both to ensure users don't get an error if using either version

            VersionList.Add("42", "F/8/A/F8AF50AB-3C3A-4BC4-8773-DC27B32988DD");
            VersionList.Add("17", "F/8/A/F8AF50AB-3C3A-4BC4-8773-DC27B32988DD");
            VersionList.Add("41", "D/4/1/D417998A-58EE-4EFE-A7CC-39EF9E020768");
            VersionList.Add("16", "D/4/1/D417998A-58EE-4EFE-A7CC-39EF9E020768");
            VersionList.Add("40", "3/4/2/342316D7-EBE0-4F10-ABA2-AE8E0CDF36DD");
            VersionList.Add("15", "3/4/2/342316D7-EBE0-4F10-ABA2-AE8E0CDF36DD");
            VersionList.Add("38", "3/2/D/32D3E464-F2EF-490F-841B-05D53C848D15");
            VersionList.Add("14", "3/2/D/32D3E464-F2EF-490F-841B-05D53C848D15");
            VersionList.Add("25", "C/0/7/C07EBF21-5305-4EC8-83B1-A6FCC8F93F45");
            VersionList.Add("13", "C/0/7/C07EBF21-5305-4EC8-83B1-A6FCC8F93F45");
            VersionList.Add("20", "8/D/0/8D0D08CF-790D-4586-B726-C6469A9ED49C");
            VersionList.Add("12", "8/D/0/8D0D08CF-790D-4586-B726-C6469A9ED49C");
        }
    }
}

