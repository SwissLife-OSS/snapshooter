//using Xunit;
//using System.IO;
//using Snapshooter.Tests.Data;
//using Snapshooter.Core;

//namespace Snapshooter.Tests
//{
//    public class SnapshotFileStoreTests
//    {
//        [Fact]
//        public void SaveNewSnapshot_SavesStringAsSnapshotFile_SavedStringInFile()
//        {
//            // arrange
//            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();
//            string serializedTestPerson = new JsonSnapshotSerializer().Serialize(testPerson);
//			string arrangedSnapshotName = $"{GetType().Name}." +
//                $"{nameof(SaveNewSnapshot_SavesStringAsSnapshotFile_SavedStringInFile)}.snap";

//            ISnapshotFileHandler snapshotStore = new SnapshotFileHandler();

//            // act
//            string actualSnapshot = snapshotStore
//				.SaveNewSnapshot(serializedTestPerson, arrangedSnapshotName);

//            // assert
//            string expectedFilePath = Path.Combine("__snapshots__", "new", arrangedSnapshotName);
//            Assert.Equal(serializedTestPerson, File.ReadAllText(expectedFilePath));
//            Assert.EndsWith(expectedFilePath, actualSnapshot);
//        }


//        [Fact]
//        public void LoadSnapshot_ReadExpectedSnapshot_ReturnsSnapshot()
//        {
//            // arrange
//            TestPerson newTestPerson = TestDataBuilder.TestPersonSandraSchneider().Build();
//            string arrangedPerson = new JsonSnapshotSerializer().Serialize(newTestPerson);
//            string arrangedSnapshotName = $"{GetType().Name}." +
//                $"{nameof(LoadSnapshot_ReadExpectedSnapshot_ReturnsSnapshot)}.snap";

//            string testDirectory = "__snapshots__/";
//            Directory.CreateDirectory(testDirectory);

//            File.WriteAllText(Path.Combine(testDirectory, arrangedSnapshotName), arrangedPerson);

//            ISnapshotFileHandler snapshotStore = new SnapshotFileHandler();

//            // act
//            string actualPerson = snapshotStore.LoadSnapshot(arrangedSnapshotName);

//            // assert
//            Assert.Equal(arrangedPerson, actualPerson);
//        }
//    }
//}
