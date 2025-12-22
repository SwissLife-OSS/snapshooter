using Snapshooter.Extensions;
using Xunit;

namespace Snapshooter.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("C:\\normal\\path\\with\\filename", "C:\\normal\\path\\with")]
        [InlineData("C:/normal/path/with/slash/filename", "C:/normal/path/with/slash")]
        [InlineData("/normal/path/with/slash/filename", "/normal/path/with/slash")]
        [InlineData("filename", "")]
        public void RemoveFilename_RemoveFilenameFromDifferentPath_Success(
            string absoluteFilename, string result)
        {
            Assert.Equal(result, absoluteFilename.RemoveFilename());
        }
    }
}
