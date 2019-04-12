using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;

namespace WebDriverManager.Tests
{
    public class DriverData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {new EdgeConfig()},
            new object[] {new FirefoxConfig()},
            new object[] {new InternetExplorerConfig()},
            new object[] {new OperaConfig()},
            new object[] {new PhantomConfig()},
            new object[] {new ChromeConfig()}
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

    public class DriverDownloadTests
    {
        [Theory, ClassData(typeof(DriverData))]
        protected void DriverDownloadTest(IDriverConfig driverConfig)
        {
            new DriverManager().SetupLatestDriver(Directory.GetCurrentDirectory(), driverConfig);
            var pathVariable = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
            Assert.NotNull(pathVariable);
            Assert.Contains(driverConfig.GetName(), pathVariable);
        }
    }
}