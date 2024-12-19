using System;
using System.IO;
using Snapshooter.Exceptions;
using Snapshooter.StrictMode.Tests.Helpers;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using Xunit;

namespace Snapshooter.StrictMode.Tests
{
    [Collection(CollectionFixtureNames.SynchronExecutionFixture)]
    public class StrictModeTests : IClassFixture<EnvironmentCleanupFixture>
    {
        [Theory]
        [InlineData("on")]
        [InlineData("true")]
        public void Match_With_Environment_StrictMode_On_Snapshot_Missing(string value)
        {
            // arrange
            System.Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", value);
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Action action = () => Snapshot.Match(testPerson);

            //assert
            Assert.Throws<SnapshotNotFoundException>(action);
        }

        [Theory]
        [InlineData("on")]
        [InlineData("true")]
        public void Match_With_Environment_StrictMode_On_Snapshot_Exists(string value)
        {
            // arrange
            System.Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", value);
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson);
        }

        [Theory]
        [InlineData("off")]
        [InlineData("false")]
        public void Match_With_Environment_StrictMode_Off_Snapshot_Not_Exists(string value)
        {
            // arrange
            DeleteSnapshotFileIfExisting();
            System.Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", value);
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson);
            DeleteSnapshotFileIfExisting();
        }

        [Fact]
        public void Match_With_MatchOptions_StrictMode_On_Snapshot_Missing()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Action action = () => Snapshot.Match(testPerson, options => options.SetStrictMode(true));

            //assert
            Assert.Throws<SnapshotNotFoundException>(action);
        }

        [Fact]
        public void Match_With_MatchOptions_StrictMode_On_Snapshot_Exists()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson, options => options.SetStrictMode(true));
        }

        [Fact]
        public void Match_With_MatchOptions_StrictMode_Off_Snapshot_Not_Exists()
        {
            // arrange
            DeleteSnapshotFileIfExisting();
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson, options => options.SetStrictMode(false));
            DeleteSnapshotFileIfExisting();
        }

        [Fact]
        public void Match_With_Environment_StrictMode_Off_MatchOptions_StrictMode_On()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Action action = () => Snapshot.Match(testPerson, options => options.SetStrictMode(true));

            //assert
            Assert.Throws<SnapshotNotFoundException>(action);
        }

        [Fact]
        public void Match_With_Environment_StrictMode_On_MatchOptions_StrictMode_On()
        {
            // arrange
            System.Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", "on");
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Action action = () => Snapshot.Match(testPerson, options => options.SetStrictMode(true));

            //assert
            Assert.Throws<SnapshotNotFoundException>(action);
        }

        private void DeleteSnapshotFileIfExisting()
        {
            var snapshotFullNameResolver = new SnapshotFullNameResolver(new XunitSnapshotFullNameReader());
            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ResolveSnapshotFullName();
            var snapshotFileName = Path.Combine(snapshotFullName.FolderPath, FileNames.SnapshotFolderName, snapshotFullName.Filename);

            if (File.Exists(snapshotFileName))
            {
                File.Delete(snapshotFileName);
            }
        }
    }
}
