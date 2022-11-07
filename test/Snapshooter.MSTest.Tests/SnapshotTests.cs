using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snapshooter.Tests.Data;

namespace Snapshooter.MSTest.Tests
{
    [TestClass]
    public partial class SnapshotTests
    {
        #region Match Snapshot - Simple Snapshot Tests

        [TestMethod]
        public void Match_TestMatchSingleSnapshot_GoodCase()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson);
        }

        [TestMethod]
        public void Match_TestMatchSingleSnapshot_OneFieldNotEqual()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().WithAge(5).Build();

            // act & assert
            Assert.ThrowsException<AssertFailedException>(() => Snapshot.Match(testPerson));
        }

        [TestMethod]
        public void Match_TestMatchSingleSnapshot_FieldNotExistInSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.ThrowsException<AssertFailedException>(() => Snapshot.Match(testPerson));
        }

        [TestMethod]
        public void Match_TestMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated()
        {
            // arrange
            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                new MSTestSnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName();

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            File.Delete(snapshotFileName);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Snapshot.Match(testPerson);

            // assert
            Assert.IsTrue(File.Exists(snapshotFileName));
        }

        [DataTestMethod]
        [DataRow(36, 189.45)]
        [DataRow(42, 173.16)]
        [DataRow(19, 193.02)]
        public void Match_TestCaseMatchSingleSnapshot_GoodCase(int age, double size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize((decimal)size).Build();

            // act & assert
            Snapshot.Match(testPerson);
        }

        [DataTestMethod]
        [DataRow(34, 175)]
        [DataRow(36, 177)]
        [DataRow(37, 178)]
        public void Match_TestCaseMatchSingleSnapshot_OneFieldNotEqual(int age, int size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            testPerson.Address.Country.CountryCode = CountryCode.DE;

            // act & assert
            Assert.ThrowsException<AssertFailedException>(() => Snapshot.Match(testPerson));
        }

        [DataTestMethod]
        [DataRow(22, 160)]
        [DataRow(23, 164)]
        public void Match_TestCaseMatchSingleSnapshot_FieldNotExistInSnapshot(int age, int size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act & assert
            Assert.ThrowsException<AssertFailedException>(() => Snapshot.Match(testPerson));
        }

        [DataRow(19, 162.3)]
        [DataRow(17, 112.3)]
        public void Match_TestCaseMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated(int age, double size)
        {
            // arrange
            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                new MSTestSnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName();

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            File.Delete(snapshotFileName);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize((decimal)size).Build();

            // act
            Snapshot.Match(testPerson);

            // assert
            Assert.IsTrue(File.Exists(snapshotFileName));
        }

        #endregion
    }
}
