using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;

namespace Snapshooter.NUnit.Tests
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
        public void Match_TestMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated()
        {
            // arrange
            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                new NUnitSnapshotFullNameReader());

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
            Assert.True(File.Exists(snapshotFileName));
        }

        [TestCase(36, 189.45)]
        [TestCase(42, 173.16)]
        [TestCase(19, 193.02)]
        public void Match_TestCaseMatchSingleSnapshot_GoodCase(int age, decimal size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act & assert
            Snapshot.Match(testPerson);
        }

        [TestCase(34, 175)]
        [TestCase(36, 177)]
        [TestCase(37, 178)]
        public void Match_TestCaseMatchSingleSnapshot_OneFieldNotEqual(int age, decimal size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            testPerson.Address.Country.CountryCode = CountryCode.DE;

            // act & assert
            Assert.Throws<AssertionException>(() => Snapshot.Match(testPerson));
        }

        [TestCase(22, 160)]
        [TestCase(23, 164)]
        public void Match_TestCaseMatchSingleSnapshot_FieldNotExistInSnapshot(int age, decimal size)
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(age).WithSize(size).Build();

            // act & assert
            Assert.Throws<AssertionException>(() => Snapshot.Match(testPerson));
        }

        [TestCase(19, 162.3)]
        [TestCase(17, 112.3)]
        public void Match_TestCaseMatchNewSingleSnapshot_ExpectedSnapshotHasBeenCreated(int age, decimal size)
        {
            // arrange
            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                new NUnitSnapshotFullNameReader());

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
            Assert.True(File.Exists(snapshotFileName));
        }

        [Test]
        public void Match_WithLambdaExpression_Works()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(new Random().Next()).Build();

            // act & assert
            Snapshot.Match(testPerson, o => o.AcceptField(t => t.Age));
        }

        #endregion
    }
}
