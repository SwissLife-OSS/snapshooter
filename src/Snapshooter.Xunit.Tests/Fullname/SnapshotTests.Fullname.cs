using Xunit;

namespace Snapshooter.Xunit.Tests
{
    public partial class SnapshotTests
    {        
        #region FullName Snapshot - Read Snapshot Fullname Tests

        [Fact]
        public void FullName_ReadAutomaticallyFactTestFullName_ReturnsValidSnapshotFullname()
        {
            // arrange
            string folderPathEnding = "Snapshooter.Xunit.Tests\\Fullname";
            string filename = "SnapshotTests.FullName_ReadAutomaticallyFactTestFullName_" +
                              "ReturnsValidSnapshotFullname.snap";
            
            // act
            SnapshotFullName fullName = Snapshot.FullName();

            // assert
            Assert.Equal(filename, fullName.Filename);
            Assert.EndsWith(folderPathEnding, fullName.FolderPath);
        }

        [Fact]
        public void FullName_ReadAutomaticallyFactTestFullNameAndAddNameExtension_ReturnsValidSnapshotFullname()
        {
            // arrange
            string folderPathEnding = "Snapshooter.Xunit.Tests\\Fullname";
            string filename = "SnapshotTests.FullName_ReadAutomaticallyFactTest" +
                              "FullNameAndAddNameExtension_" +
                              "ReturnsValidSnapshotFullname_with_1_extension.snap";

            // act
            SnapshotFullName fullName = 
                Snapshot.FullName(SnapshotNameExtension.Create("with", 1, "extension"));

            // assert
            Assert.Equal(filename, fullName.Filename);
            Assert.EndsWith(folderPathEnding, fullName.FolderPath);
        }

        [Fact]
        public void FullName_OverwriteFactTestFullName_ReturnsValidSnapshotFullname()
        {
            // arrange
            string folderPathEnding = "Snapshooter.Xunit.Tests\\Fullname";
            string filename = "Fact_OverwrittenSnapshotFullname.snap";

            // act
            SnapshotFullName fullName = Snapshot.FullName("Fact_OverwrittenSnapshotFullname");

            // assert
            Assert.Equal(filename, fullName.Filename);
            Assert.EndsWith(folderPathEnding, fullName.FolderPath);
        }

        [Fact]
        public void FullName_OverwriteFactTestFullNameAndAddNameExtension_ReturnsValidSnapshotFullname()
        {
            // arrange
            string folderPathEnding = "Snapshooter.Xunit.Tests\\Fullname";
            string filename = "Fact_OverwrittenSnapshotFullname_with_1_extension.snap";

            // act
            SnapshotFullName fullName = Snapshot.FullName(
                "Fact_OverwrittenSnapshotFullname",
                SnapshotNameExtension.Create("with", 1, "extension"));

            // assert
            Assert.Equal(filename, fullName.Filename);
            Assert.EndsWith(folderPathEnding, fullName.FolderPath);
        }

        [Theory]
        [InlineData(36, 189.45)]
        public void FullName_ReadAutomaticallyTheoryTestFullName_ReturnsValidSnapshotFullname(
            int age, int size)
        {
            // arrange
            string folderPathEnding = "Snapshooter.Xunit.Tests\\Fullname";
            string filename = "SnapshotTests.FullName_ReadAutomaticallyTheoryTestFullName_" +
                              "ReturnsValidSnapshotFullname.snap";

            // act
            SnapshotFullName fullName = Snapshot.FullName();

            // assert
            Assert.Equal(filename, fullName.Filename);
            Assert.EndsWith(folderPathEnding, fullName.FolderPath);
        }

        [Theory]
        [InlineData(36, 189.45)]
        public void FullName_ReadAutomaticallyTheoryTestFullNameAndAddNameExtensions_ReturnsValidSnapshotFullname(
            int age, decimal size)
        {
            // arrange
            string folderPathEnding = "Snapshooter.Xunit.Tests\\Fullname";
            string filename = "SnapshotTests.FullName_ReadAutomaticallyTheory" +
                              "TestFullNameAndAddNameExtensions_" +
                              "ReturnsValidSnapshotFullname_36_189.45.snap";

            // act
            SnapshotFullName fullName = 
                Snapshot.FullName(SnapshotNameExtension.Create(age, size));

            // assert
            Assert.Equal(filename, fullName.Filename);
            Assert.EndsWith(folderPathEnding, fullName.FolderPath);
        }

        [Theory]
        [InlineData(36, 189.45)]
        public void FullName_OverwriteTheoryTestFullName_ReturnsValidSnapshotFullname(
            int age, int size)
        {
            // arrange
            string folderPathEnding = "Snapshooter.Xunit.Tests\\Fullname";
            string filename = "Theory_OverwrittenSnapshotFullname.snap";

            // act
            SnapshotFullName fullName = 
                Snapshot.FullName("Theory_OverwrittenSnapshotFullname");

            // assert
            Assert.Equal(filename, fullName.Filename);
            Assert.EndsWith(folderPathEnding, fullName.FolderPath);
        }

        [Theory]
        [InlineData(36, 87189.45)]
        public void FullName_OverwriteTheoryTestFullNameAndAddNameExtensions_ReturnsValidSnapshotFullname(
            int age, decimal size)
        {
            // arrange
            string folderPathEnding = "Snapshooter.Xunit.Tests\\Fullname";
            string filename = "Theory_OverwrittenSnapshotFullname_36_87189.45.snap";

            // act
            SnapshotFullName fullName =
                Snapshot.FullName(
                    "Theory_OverwrittenSnapshotFullname",
                    SnapshotNameExtension.Create(age, size));

            // assert
            Assert.Equal(filename, fullName.Filename);
            Assert.EndsWith(folderPathEnding, fullName.FolderPath);
        }
        
        #endregion
    }
}
