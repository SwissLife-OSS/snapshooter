using System;
using System.IO;
using Snapshooter.Environment.Tests.Helpers;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using Xunit;

namespace Snapshooter.Environment.Tests
{
    [Collection(CollectionFixtureNames.SynchronExecutionFixture)]
    public class StrictModeTests : IClassFixture<EnvironmentCleanupFixture>
    {
        [Theory]
        [InlineData("on")]
        [InlineData("true")]
        public void Match_With_StrictMode_On_Snapshot_Missing(string value)
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
        public void Match_With_StrictMode_On_Snapshot_Exists(string value)
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
        public void Match_With_StrictMode_Off_Snapshot_Not_Exists(string value)
        {
            // arrange
            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                new XunitSnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName();

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            if (File.Exists(snapshotFileName))
            {
                File.Delete(snapshotFileName);
            }

            System.Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", value);
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson);
            File.Delete(snapshotFileName);
        }
    }
}
