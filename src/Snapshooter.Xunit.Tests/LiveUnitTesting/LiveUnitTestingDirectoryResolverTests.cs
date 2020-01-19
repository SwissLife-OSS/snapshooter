using System;
using System.IO;
using FluentAssertions;
using Snapshooter.Exceptions;
using Xunit;

namespace Snapshooter.Xunit.Tests.LiveUnitTesting
{
    public class LiveUnitTestingDirectoryResolverTests
    {

        [Fact(Skip = "This one fails on macos")]
        public void TryResolveName_OneFile_FullNameCorrect()
        {
            // arrange
            var tempDir = ArrangeLiveUnitTestDirectory("Test1.cs");
            var testName = "Test1.Foo";

            // act
            SnapshotFullName fullName = LiveUnitTestingDirectoryResolver
                               .TryResolveName(testName);

            // assert
            fullName.Should().NotBeNull();
            fullName.FolderPath.Should().Be(Path.Combine(tempDir, "1"));
            fullName.Filename.Should().Be(testName);
        }

        [Fact]
        public void TryResolveName_TwoFiles_SnapshotTestException()
        {
            // arrange
            ArrangeLiveUnitTestDirectory("Test1.cs", "Test1.cs");
            var testName = "Test1.Foo";

            // act
            Func<SnapshotFullName> action =
                    () => LiveUnitTestingDirectoryResolver
                            .TryResolveName(testName);

            // assert
            action.Should().Throw<SnapshotTestException>();
        }

        [Fact]
        public void TryResolveName_NoFileMatch_ResultIsNull()
        {
            // arrange
            ArrangeLiveUnitTestDirectory("Test2.cs");
            var testName = "Test1.Foo";

            // act
            SnapshotFullName fullName = LiveUnitTestingDirectoryResolver
                               .TryResolveName(testName);

            // assert
            fullName.Should().BeNull();
        }

        [Fact]
        public void CheckForSession_PathSet_ReturnSame()
        {
            // arrange
            var snapshotFullName = new SnapshotFullName("filename", "dirname");

            // act
            SnapshotFullName fullNameResult = LiveUnitTestingDirectoryResolver
                                    .CheckForSession(snapshotFullName);

            // assert
            fullNameResult.Should().NotBeNull();
            fullNameResult.Filename.Should().Be(snapshotFullName.Filename);
            fullNameResult.FolderPath.Should().Be(snapshotFullName.FolderPath);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CheckForSession_PathIsNullOrEmpty_SnapshotTestException(string path)
        {
            // arrange
            var snapshotFullName = new SnapshotFullName("filename", path);

            // act
            Func<SnapshotFullName> action = () => LiveUnitTestingDirectoryResolver
                                                    .CheckForSession(snapshotFullName);

            // assert
            action.Should().Throw<SnapshotTestException>();
        }



        private string ArrangeLiveUnitTestDirectory(params string[] files)
        {
            var tempDir = Path.Combine(Path.GetTempPath(),
                                       "snapshooter",
                                       Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);
            int fileNr = 1;
            foreach (var file in files)
            {
                var fileDir = Path.Combine(tempDir, fileNr.ToString());
                Directory.CreateDirectory(fileDir);
                File.WriteAllText(Path.Combine(fileDir, file), file);
                fileNr++;
            }
            var liveUnitTestFake = Path.Combine(tempDir, ".vs", "some", "liveunittest");
            Directory.CreateDirectory(liveUnitTestFake);
            Directory.SetCurrentDirectory(liveUnitTestFake);
            return tempDir;
        }




    }
}
