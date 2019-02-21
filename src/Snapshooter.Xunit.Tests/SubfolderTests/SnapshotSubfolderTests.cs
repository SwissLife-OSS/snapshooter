using Snapshooter.Core;
using Snapshooter.Tests.Data;
using System;
using System.IO;
using Xunit;
using Xunit.Sdk;

namespace Snapshooter.Xunit.Tests.Subfolder
{
    public class SnapshotSubfolderTests
    {
        [Fact]
        public void Match_SubfolderSnapshotGeneration_GoodCase()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match<TestPerson>(testPerson);
        }

        [Fact]
        public void Match_SubfolderFactMatchSingleSnapshot_OneFieldNotEqual()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().WithAge(5).Build();

            // act
            Action match = () => Snapshot.Match<TestPerson>(testPerson);

            // assert
            Assert.Throws<EqualException>(match);
        }

        [Fact]
        public void Match_SubfolderFactMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated()
        {
            // arrange
            var snapshotFileInfoResolver = new SnapshotFileInfoResolver(
                new XunitSnapshotFileInfoReader(), new SnapshotFileNameBuilder());

            SnapshotFullName snapshotFileInfo =
                snapshotFileInfoResolver.ResolveSnapshotFileInfo();

            string snapshotFileName = Path.Combine(
                snapshotFileInfo.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFileInfo.Filename);

            File.Delete(snapshotFileName);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act 
            Snapshot.Match<TestPerson>(testPerson);

            // assert
            Assert.True(File.Exists(snapshotFileName));
        }
    }
}
