using Xunit;
using Snapshooter.Core;
using System.Threading.Tasks;

namespace Snapshooter.Xunit.Tests
{
    public class XunitSnapshotFileInfoReaderTests
    {
        [Fact]
        public void ReadSnapshotFileInfo_ResolveSnapshotFileName_ResolvedSuccessfully()
        {
            // arrange
            var snapshotFileInfoResolver = new XunitSnapshotFileInfoReader();

            // act
            SnapshotFullName snapshotFileInfo = snapshotFileInfoResolver.ReadSnapshotFileInfo();

            // assert
            Assert.Equal(
                $"{nameof(XunitSnapshotFileInfoReaderTests)}." +
                $"{nameof(ReadSnapshotFileInfo_ResolveSnapshotFileName_ResolvedSuccessfully)}",
                snapshotFileInfo.Filename);
        }
        
        [Fact]
        public async Task ReadSnapshotFileInfo_ResolveFactSnapshotNameAsync_ResolvedSuccessfully()
        {
            // arrange
            var snapshotFileInfoResolver = new XunitSnapshotFileInfoReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFileInfo = snapshotFileInfoResolver.ReadSnapshotFileInfo();

            // assert
            await Task.Delay(1);
            Assert.Equal(
                $"{nameof(XunitSnapshotFileInfoReaderTests)}." +
                $"{nameof(ReadSnapshotFileInfo_ResolveFactSnapshotNameAsync_ResolvedSuccessfully)}",
                snapshotFileInfo.Filename);
        }

        #pragma warning disable xUnit1026 // Theory methods should use all of their parameters

        [Theory]
        [InlineData("testString1", 5)]
        [InlineData("testString2", 6)]
        [InlineData("testString3", 7)]
        public void ReadSnapshotFileInfo_ResolveTheorySnapshotName_NameResolvedWithoutInlineDataParameters(
            string param1, int param2)
        {
            // arrange
            var snapshotFileInfoResolver = new XunitSnapshotFileInfoReader();

            // act
            SnapshotFullName snapshotFileInfo = snapshotFileInfoResolver.ReadSnapshotFileInfo();

            // assert
            Assert.Equal(
                $"{nameof(XunitSnapshotFileInfoReaderTests)}." +
                $"{nameof(ReadSnapshotFileInfo_ResolveTheorySnapshotName_NameResolvedWithoutInlineDataParameters)}",
                snapshotFileInfo.Filename);
        }

        [Theory]
        [InlineData("testString1", 5)]
        [InlineData("testString2", 6)]
        [InlineData("testString3", 7)]
        public async Task ReadSnapshotFileInfo_ResolveTheorySnapshotNameAsync_NameResolvedWithoutInlineDataParameters(
           string param1, int param2)
        {
            // arrange
            var snapshotFileInfoResolver = new XunitSnapshotFileInfoReader();
            await Task.Delay(1);

            // act
            SnapshotFullName snapshotFileInfo = snapshotFileInfoResolver.ReadSnapshotFileInfo();

            // assert
            await Task.Delay(1);
            Assert.Equal(
                $"{nameof(XunitSnapshotFileInfoReaderTests)}." +
                $"{nameof(ReadSnapshotFileInfo_ResolveTheorySnapshotNameAsync_NameResolvedWithoutInlineDataParameters)}",
                snapshotFileInfo.Filename);
        }

        #pragma warning restore xUnit1026 // Theory methods should use all of their parameters
    }
}
