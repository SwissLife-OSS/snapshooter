using System;
using System.Collections.Generic;
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

        #endregion

        #region Match Snapshot - Multiple Objects Tests

        [Test]
        public void Match_MultipleObjectsSnapshot_SuccessfulMatch()
        {
            // arrange
            TestPerson markWalton = TestDataBuilder.TestPersonMarkWalton().Build();
            TestPerson sandraSchneider = TestDataBuilder.TestPersonSandraSchneider().Build();
            TestChild hanna = TestDataBuilder.TestChildHanna().Build();

            // act & assert
            Snapshot.Match(new { markWalton, sandraSchneider, hanna });
        }

        [Test]
        public void Match_ObjectsArraySnapshot_SuccessfulMatch()
        {
            // arrange
            TestPerson markWalton = TestDataBuilder.TestPersonMarkWalton().Build();
            TestPerson sandraSchneider = TestDataBuilder.TestPersonSandraSchneider().Build();
            TestChild hanna = TestDataBuilder.TestChildHanna().Build();

            // act & assert
            Snapshot.Match(new object[] { markWalton, sandraSchneider, hanna });
        }

        [Test]
        public void Match_ObjectsListsSnapshot_SuccessfulMatch()
        {
            // arrange
            TestPerson markWalton = TestDataBuilder.TestPersonMarkWalton().Build();
            TestPerson sandraSchneider = TestDataBuilder.TestPersonSandraSchneider().Build();
            TestChild hanna = TestDataBuilder.TestChildHanna().Build();

            // act & assert
            Snapshot.Match(new List<object>() { markWalton, sandraSchneider, hanna });
        }

        #endregion

        #region Match Snapshots - Ignore Fields Tests

        [Test]
        public void Match_IgnoreScalarField_SuccessfulIgnored()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .WithSize(0.5m)
                .Build();

            // act & assert
            Snapshot.Match(
                testPerson, matchOptions => matchOptions.IgnoreField("Size"));
        }

        [Test]
        public void Match_IgnoreScalarNullIntField_SuccessfulIgnored()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .Build();

            // act & assert
            Snapshot.Match(
                testPerson, matchOptions => matchOptions.IgnoreField<int?>("Age"));
        }

        [Test]
        public void Match_IgnoreScalarNullStringField_SuccessfulIgnored()
        {
            // arrange
            TestChild testChild = TestDataBuilder.TestChildNull()
                .Build();

            // act & assert
            Snapshot.Match(
                testChild, matchOptions => matchOptions.IgnoreField<string>("Name"));
        }

        [Test]
        public void Match_IgnoreScalarFieldNullConvertError_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .Build();

            // act & assert
            string message = Assert.Throws<SnapshotFieldException>(() => Snapshot
                .Match(testPerson, matchOptions => matchOptions.IgnoreField<int>("Age")))
                .Message;

            StringAssert.Contains(
                "The field 'Age' of the compare context caused an error.", message);
        }

        [Test]
        public void Match_IgnoreScalarFieldPathNotExist_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            // act & assert
            string message = Assert.Throws<SnapshotFieldException>(() => Snapshot
                .Match(testPerson, matchOptions => matchOptions.IgnoreField<decimal>("alt")))
                .Message;

            StringAssert.Contains(
                "The field 'alt' of the compare context caused an error.", message);
        }

        [Test]
        public void Match_IgnoreComplexObjectField_SuccessfulIgnored()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            testPerson.Address = null;

            // act & assert
            Snapshot.Match(
                testPerson, matchOptions => matchOptions.IgnoreField<object>("Address"));
        }

        [Test]
        public void Match_IgnoreScalarFieldInAllWays_SuccessfulIgnored()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .WithSize(1.5m)
                .Build();

            // act & assert
            Snapshot.Match(
                testPerson, matchOptions => matchOptions.IgnoreField("Size"));
            Snapshot.Match(
                testPerson, matchOptions => matchOptions.IgnoreField<decimal>("Size"));
            Snapshot.Match(
                testPerson, matchOptions => matchOptions.Ignore(option => option.Field<decimal>("Size")));
        }

        [Test]
        public void Match_IgnoreSeveralSingleFields_SuccessfulIgnored()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Id = Guid.NewGuid();
            testPerson.CreationDate = DateTime.UtcNow;
            testPerson.Address.StreetNumber = -58;
            testPerson.Address.Country = null;
            testPerson.Relatives[0].Address.Plz = null;

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions
                    .IgnoreField<Guid>("Id")
                    .IgnoreField<DateTime>("CreationDate")
                    .IgnoreField<int>("Address.StreetNumber")
                    .IgnoreField<TestChild>("Children[3]")
                    .IgnoreField<TestCountry>("Address.Country")
                    .IgnoreField<TestCountry>("Relatives[0].Address.Plz"));
        }

        [Test]
        public void Match_IgnoreWildcardScalarFieldsArray_SuccessfulIgnored()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Children.ElementAt(0).Name = "newName1";
            testPerson.Children.ElementAt(1).Name = "newName2";
            testPerson.Children.ElementAt(2).Name = "newName3";

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IgnoreFields("Children[*].Name"));
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IgnoreFields<string>("Children[*].Name"));
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.Ignore(option => option.Fields<string>("Children[*].Name")));
        }

        [Test]
        public void Match_IgnoreWildcardComplexFieldsArray_SuccessfulIgnored()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Children.ElementAt(0).Name = "newName1x";
            testPerson.Children.ElementAt(1).Name = "newName2x";
            testPerson.Children.ElementAt(2).Name = "newName3x";

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IgnoreFields("Children[*]"));
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IgnoreFields<TestChild>("Children[*]"));
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.Ignore(option => option.Fields<TestChild>("Children[*]")));
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IgnoreField("Children"));
        }

        [Test]
        public void Match_IgnoreArrayFields_SuccessfulIgnored()
        {
            // arrange
            object[] testPersons = new object[]
            {
                TestDataBuilder.TestPersonMarkWalton().Build(),
                TestDataBuilder.TestPersonSandraSchneider().Build(),
                TestDataBuilder.TestPersonMarkWalton().Build(),
                TestDataBuilder.TestChildJames().Build(),
                TestDataBuilder.TestChildHanna().Build(),
                TestDataBuilder.TestCountrySwitzerland().Build()
            };

            // act & assert
            Snapshot.Match(
                testPersons, matchOptions => matchOptions.IgnoreFields<object>("[*]"));
        }

        [Test]
        public void Match_IgnoreArrayFieldsPersonFirstname_SuccessfulIgnored()
        {
            // arrange
            object[] testPersons = new object[]
            {
                TestDataBuilder.TestPersonMarkWalton().Build(),
                TestDataBuilder.TestPersonSandraSchneider().Build(),
                TestDataBuilder.TestPersonMarkWalton().Build()
            };

            // act & assert
            Snapshot.Match(
                testPersons, matchOptions => matchOptions.IgnoreFields<object>("[*].Firstname"));
        }

        [Test]
        public void Match_IgnoreFieldFailsWithinFirstSnapshotCreation_ThrowsSnapshotFieldException()
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

            Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", false.ToString());

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .WithSize(0.5m)
                .Build();

            // act
            Assert.Throws<SnapshotFieldException>(() => Snapshot.Match(
                testPerson, matchOptions => matchOptions.IgnoreField<int>("Size")));

            // assert
            Assert.True(File.Exists(snapshotFileName));
        }

        #endregion

    }
}
