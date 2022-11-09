using System;
using System.IO;
using System.Linq;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Xunit;

namespace Snapshooter.Xunit.Tests.MatchOptions.IsTypeField
{
    public class IsTypeFieldTests
    {
        [Fact]
        public void Match_IsTypeScalarFieldDateTime_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeField<DateTime>("CreationDate"));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNotDateTime_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeField<DateTime>("Size")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNullDateTime_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson,
                    matchOptions => matchOptions.IsTypeField<DateTime>("Age")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldGuid_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeField<Guid>("Id"));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNotGuid_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeField<Guid>("Size")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNullGuid_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson,
                    matchOptions => matchOptions.IsTypeField<Guid>("Age")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldInt_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeField<int>("Age"));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNotInt_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeField<int>("Size")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNullInt_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson,
                    matchOptions => matchOptions.IsTypeField<int>("Age")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldDecimal_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeField<decimal>("Size"));
        }

        [Fact]
        public void Match_IsTypeScalarFieldDecimalBoxed_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeField<decimal>("Age"));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNotDecimal_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeField<decimal>("Firstname")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNullDecimal_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson,
                    matchOptions => matchOptions.IsTypeField<int>("Age")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldInAllWays_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .WithSize(1.5m).Build();

            // act & assert
            Snapshot.Match(
                testPerson, matchOptions =>
                    matchOptions.IsTypeField<DateTime>("CreationDate"));
            Snapshot.Match(
                testPerson, matchOptions =>
                    matchOptions.IsType(option => option.Field<DateTime>("CreationDate")));
        }

        [Fact]
        public void Match_IsTypeComplexObjectField_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeField<TestAddress>("Address"));
        }

        [Fact]
        public void Match_IsTypeWildcardScalarFieldsArray_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Children.ElementAt(0).DateOfBirth = DateTime.Parse("2015-08-17");
            testPerson.Children.ElementAt(1).DateOfBirth = DateTime.Parse("2017-08-17");
            testPerson.Children.ElementAt(2).DateOfBirth = DateTime.Parse("2018-08-17");

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeFields<DateTime>("Children[*].DateOfBirth"));
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsType(
                    option => option.Fields<DateTime>("Children[*].DateOfBirth")));
        }

        [Fact]
        public void Match_IsTypeWildcardScalarFieldsOneEntry_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Children.ElementAt(0).DateOfBirth = DateTime.Parse("2015-08-17");

            // act & assert
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsTypeFields<DateTime>("Children[*].DateOfBirth"));
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsType(
                    option => option.Fields<DateTime>("Children[*].DateOfBirth")));
        }

        [Fact]
        public void Match_IsTypeWildcardComplexFieldsArray_SuccessfulMatch()
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
                matchOptions => matchOptions.IsTypeFields<TestChild>("Children[*]"));
            Snapshot.Match(testPerson,
                matchOptions => matchOptions.IsType(
                    option => option.Fields<TestChild>("Children[*]")));
        }

        [Fact]
        public void Match_IsTypeArrayFields_SuccessfulMatch()
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
                testPersons, matchOptions => matchOptions.IsTypeFields<TestPerson>("[*]"));
        }

        [Fact]
        public void Match_IsTypeArrayFieldsPerson_SuccessfulIgnored()
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
                testPersons, matchOptions =>
                    matchOptions.IsTypeFields<DateTime>("[*].DateOfBirth"));
        }

        [Fact]
        public void Match_IsTypeIntFailsWithinFirstSnapshotCreation_ThrowsSnapshotFieldException()
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

            File.Delete(snapshotFileName);

            Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", false.ToString());

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .WithSize(0.5m)
                .Build();

            // act
            Action action = () => Snapshot.Match(
                testPerson, matchOptions => matchOptions.IsTypeField<int>("Size"));

            // assert
            Assert.Throws<SnapshotFieldException>(action);
            Assert.False(File.Exists(snapshotFileName));
        }
    }
}
