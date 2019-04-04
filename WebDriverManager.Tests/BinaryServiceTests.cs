using System.IO;
using WebDriverManager.Helpers;
using WebDriverManager.Services.Impl;
using Xunit;

namespace WebDriverManager.Tests
{
    public class ZipHelperTests
    {
        [Fact]
        public void DownloadZipResultNotEmpty()
        {
            const string url = "https://chromedriver.storage.googleapis.com/2.46/chromedriver_win32.zip";
            var tempDirectory = FileHelper.GetTempDirectoryJoey();
            var downloadFileName = FileHelper.GetFileNameFromUrl(url);

            var zipLocation = ZipHelper.DownloadZipJoey(url, tempDirectory, downloadFileName);

            Assert.NotEmpty(zipLocation);
            Assert.True(File.Exists(zipLocation));
        }

        [Fact]
        public void UnZipResultNotEmpty()
        {
            var zipPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "unzipable.zip");
            var subFolderDestination = FileHelper.GetDriverDestination("Files", "2.0.0", Architecture.X32, "file.txt");
            var fullDestinationPath = Path.Combine(Directory.GetCurrentDirectory(), subFolderDestination);

            FileHelper.CreateDestinationDirectory(fullDestinationPath);

            var result = ZipHelper.UnZipToDriverLocation(zipPath, fullDestinationPath, "file.txt");

            Assert.True(result);
            Assert.True(File.Exists(fullDestinationPath));
        }

        [Fact]
        public void RemoveZipTargetMissing()
        {
            var originalZipPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "removable.zip");
            var newZipPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "removableCopy.zip");

            File.Copy(originalZipPath, newZipPath, true);

            Assert.True(File.Exists(newZipPath));
            ZipHelper.DeleteZipJoey(newZipPath);
            Assert.False(File.Exists(newZipPath));
        }
    }
}