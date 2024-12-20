using System.IO;
using System.Threading.Tasks;
using Snapshooter.Tests.Data;
using TUnit.Assertions.Exceptions;

namespace Snapshooter.TUnit.Tests
{
    public partial class SnapshotTests
    {
        #region Match Snapshot - Simple Snapshot Tests

        [Test]
        public void Match_TestMatchSingleSnapshot_GoodCase()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson);
        }

        [Test]
        public void Match_TestMatchSingleSnapshot_OneFieldNotEqual()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().WithAge(5).Build();

            // act & assert
            Assert.Throws<AssertionException>(() => Snapshot.Match(testPerson));
        }

        [Test]
        public void Match_TestMatchSingleSnapshot_FieldNotExistInSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<AssertionException>(() => Snapshot.Match(testPerson));
        }

        [Test]
        public async Task Match_TestMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated()
        {
            // arrange
            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                new TUnitSnapshotFullNameReader());

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
            await Assert.That(File.Exists(snapshotFileName)).IsTrue();
        }

        [Test]
        [Arguments(36, 189.45)]
        [Arguments(42, 173.16)]
        [Arguments(19, 193.02)]
        public void Match_TestCaseMatchSingleSnapshot_GoodCase(int age, decimal size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act & assert
            Snapshot.Match(testPerson);
        }

        [Test]
        [Arguments(34, 175)]
        [Arguments(36, 177)]
        [Arguments(37, 178)]
        public void Match_TestCaseMatchSingleSnapshot_OneFieldNotEqual(int age, decimal size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            testPerson.Address.Country.CountryCode = CountryCode.DE;

            // act & assert
            Assert.Throws<AssertionException>(() => Snapshot.Match(testPerson));
        }

        [Test]
        [Arguments(22, 160)]
        [Arguments(23, 164)]
        public void Match_TestCaseMatchSingleSnapshot_FieldNotExistInSnapshot(int age, decimal size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act & assert
            Assert.Throws<AssertionException>(() => Snapshot.Match(testPerson));
        }

        [Test]
        [Arguments(19, 162.3)]
        [Arguments(17, 112.3)]
        public async Task Match_TestCaseMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated(int age, decimal size)
        {
            // arrange
            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                new TUnitSnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName();

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            File.Delete(snapshotFileName);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act
            Snapshot.Match(testPerson);

            // assert
            await Assert.That(File.Exists(snapshotFileName)).IsTrue();
        }

        #endregion
    }
}
