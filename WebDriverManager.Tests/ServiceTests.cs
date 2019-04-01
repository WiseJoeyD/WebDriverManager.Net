using System;
using System.IO;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Services.Impl;
using Xunit;

namespace WebDriverManager.Tests
{
    public class ServiceTests
    {
        private readonly BinaryService _customBinaryService;
        private readonly PathVariableService _customVariableService;
        private readonly ChromeConfig _chromeConfig;

        public ServiceTests()
        {
            _customBinaryService = new BinaryService();
            _customVariableService = new PathVariableService();
            _chromeConfig = new ChromeConfig();
        }

        [Fact]
        public void CustomServiceTest()
        {
            new DriverManager(_customBinaryService, _customVariableService).SetupLatestDriver(Directory.GetCurrentDirectory(),_chromeConfig);
            var pathVariable = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
            Assert.NotNull(pathVariable);
            Assert.Contains(_chromeConfig.GetName(), pathVariable);
        }
    }
}