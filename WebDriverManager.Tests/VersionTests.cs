using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;

namespace WebDriverManager.Tests
{
    public class VersionData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {new ChromeConfig(), @"^\d+\.\d+\.*\d*\.*\d*$"},
            new object[] {new EdgeChromiumConfig(), @"^\d+\.\d+\.*\d*\.*\d*$"},
            new object[] {new EdgeConfig(), @"^[A-Z0-9-/]*$"},
            new object[] {new FirefoxConfig(), @"^\d+\.\d+\.\d+$"},
            new object[] {new InternetExplorerConfig(), @"^\d+\.\d+\.\d+$"},
            new object[] {new OperaConfig(), @"^\d+\.\d+.*\d*.*\d*$"},
            new object[] {new PhantomConfig(), @"^\d+\.\d+\.\d+$"}
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class VersionTests
    {
        [Theory, ClassData(typeof(VersionData))]
        protected void VersionTest(IDriverConfig driverConfig, string pattern)
        {
            var version = driverConfig.GetDriverVersion("Latest");
            var regex = new Regex(pattern);
            var match = regex.Match(version);
            Assert.NotEmpty(version);
            Assert.True(match.Success);
        }

        [Fact]
        protected void FirefoxVersionListTest()
        {
            var browserConfig = new FirefoxConfig();
            var version = browserConfig.GetDriverVersion("58");

            Assert.NotEmpty(version);
            Assert.Equal("0.25.0", version);
        }

        [Fact]
        protected void ChromeVersionListTest()
        {
            var browserConfig = new ChromeConfig();
            var version = browserConfig.GetDriverVersion("73");

            Assert.NotEmpty(version);
            Assert.Equal("2.46", version);
        }

        [Fact]
        protected void InternetExplorerVersionListTest()
        {
            var browserConfig = new InternetExplorerConfig();
            var version = browserConfig.GetDriverVersion("11.973");

            Assert.NotEmpty(version);
            Assert.Equal("3.150.1", version);
        }

        [Fact]
        protected void EdgeVersionListTest()
        {
            var browserConfig = new EdgeConfig();
            var version = browserConfig.GetDriverVersion("16");

            Assert.NotEmpty(version);
            Assert.Equal("D/4/1/D417998A-58EE-4EFE-A7CC-39EF9E020768", version);
        }
        
    }
}