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
                case BrowserName.EdgeChromium:
                    SetupEdgeChromiumDictionary();
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
            Initialise(browser);

            try
            {
                var latestStoredVersion = VersionList.OrderByDescending(x => x.Key).First().Value;

                return latestStoredVersion;
            }
            catch (Exception)
            {
                return String.Empty;
            }
            
        }

        public static string GetCompatibleStoredVersion(BrowserName browser, string versionNumber)
        {
            Initialise(browser);

            try
            {
                return VersionList[versionNumber];
            }
            catch (Exception)
            {
                return String.Empty;
            }
            
        }

        // Populate dictionaries       

        private static void SetupInternetExplorerDictionary()
        {
            VersionList = new Dictionary<string, string>
            {
                //Latest version compatible with all previous versions
                //updated June 2019
                //3.141.59
                { "Latest", "3.141.59" }
            };
        }

        private static void SetupPhantomJSDictionary()
        {
            VersionList = new Dictionary<string, string>
            {
                //2.1.1 - last version
                { "Latest", "2.1.1" }
            };
        }

        private static void SetupChromeDictionary()
        {
            VersionList = new Dictionary<string, string>
            {
                //updated June 2019
                // 74+ versions changed version formats
                //source: https://chromedriver.storage.googleapis.com/index.html
                //compatbility source: https://chromedriver.storage.googleapis.com/2.46/notes.txt
                { "73", "2.46" },
                { "72", "2.46" },
                { "71", "2.46" },
                { "70", "2.45" },
                { "69", "2.44" },
                { "68", "2.42" },
                { "67", "2.41" },
                { "66", "2.40" },
                { "65", "2.38" },
                { "64", "2.37" },
                { "63", "2.36" },
                { "62", "2.35" },
                { "61", "2.34" },
                { "60", "2.33" },
                { "59", "2.32" },
                { "58", "2.31" },
                { "57", "2.29" },
                { "56", "2.29" },
                { "55", "2.28" },
                { "54", "2.27" },
                { "53", "2.26" },
                { "52", "2.25" },
                { "51", "2.23" },
                { "50", "2.23" },
                { "49", "2.22" },
                { "48", "2.21" },
                { "47", "2.21" },
                { "46", "2.21" },
                { "45", "2.20" },
                { "44", "2.20" },
                { "43", "2.20" },
                { "42", "2.17" },
                { "41", "2.15" },
                { "40", "2.14" },
                { "39", "2.14" },
                { "38", "2.13" },
                { "37", "2.12" },
                { "36", "2.12" }
            };
        }

        private static void SetupFirefoxDictionary()
        {
            VersionList = new Dictionary<string, string>
            {
                //updated June 2019
                // Version 57 maintains open ended compatibility
                //source: https://firefox-source-docs.mozilla.org/testing/geckodriver/Support.html
                { "57", "0.24.0" },
                { "56", "0.20.1" },
                { "55", "0.20.1" },
                { "54", "0.18.0" },
                { "53", "0.18.0" },
                { "52", "0.17.0" }
            };
        }

        private static void SetupOperaDictionary()
        {
            VersionList = new Dictionary<string, string>
            {
                //updated March 2019
                //source: https://github.com/operasoftware/operachromiumdriver/releases
                { "60", "2.45" },
                { "59", "2.42" },
                { "58", "2.42" },
                { "57", "2.41" },
                { "56", "2.40" },
                { "55", "2.38" },
                { "54", "2.37" },
                { "53", "2.36" },
                { "52", "2.35" },
                { "50", "2.33" },
                { "49", "2.32" },
                { "48", "2.30" },
                { "46", "2.29" }
            };
        }

        private static void SetupEdgeDictionary()
        {
            VersionList = new Dictionary<string, string>
            {
                //updated June 2019
                //source: https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/
                //unlike other drivers, edge driver name is the same, and the path differes by a GUID not a version
                //storing like a version as it is combined with eh url in the same way as the other browser URLs
                //Version numbers represent either Edge brower and Edge HTML - due to way Microsoft display both versions
                //both are shown, so including both to ensure users don't get an error if using either version

                { "42", "F/8/A/F8AF50AB-3C3A-4BC4-8773-DC27B32988DD" },
                { "17", "F/8/A/F8AF50AB-3C3A-4BC4-8773-DC27B32988DD" },
                { "41", "D/4/1/D417998A-58EE-4EFE-A7CC-39EF9E020768" },
                { "16", "D/4/1/D417998A-58EE-4EFE-A7CC-39EF9E020768" },
                { "40", "3/4/2/342316D7-EBE0-4F10-ABA2-AE8E0CDF36DD" },
                { "15", "3/4/2/342316D7-EBE0-4F10-ABA2-AE8E0CDF36DD" },
                { "38", "3/2/D/32D3E464-F2EF-490F-841B-05D53C848D15" },
                { "14", "3/2/D/32D3E464-F2EF-490F-841B-05D53C848D15" },
                { "25", "C/0/7/C07EBF21-5305-4EC8-83B1-A6FCC8F93F45" },
                { "13", "C/0/7/C07EBF21-5305-4EC8-83B1-A6FCC8F93F45" },
                { "20", "8/D/0/8D0D08CF-790D-4586-B726-C6469A9ED49C" },
                { "12", "8/D/0/8D0D08CF-790D-4586-B726-C6469A9ED49C" }
            };
        }

        private static void SetupEdgeChromiumDictionary()
        {
            VersionList = new Dictionary<string, string>
            {
                //updated June 2019
                //source: https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/
                //unlike other drivers, edge driver name is the same, and the path differes by a GUID not a version
                //storing like a version as it is combined with eh url in the same way as the other browser URLs
                //Version numbers represent either Edge brower and Edge HTML - due to way Microsoft display both versions
                //both are shown, so including both to ensure users don't get an error if using either version

                { "76", "76.0.168.0" }
            };
        }
    }
}

