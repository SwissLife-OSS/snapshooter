using System;
using System.Linq;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Xunit;

namespace Snapshooter.Json.Tests.MatchOptions.IsTypeField
{
    public class IsTypeFieldTests
    {
        [Fact]
        public void Match_IsTypeScalarFieldDateTime_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_IsTypeScalarFieldDateTime_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<DateTime>("CreationDate"));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNotDateTime_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_IsTypeScalarFieldNotDateTime_ThrowsSnapshotFieldException);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<DateTime>("Size")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNullDateTime_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_IsTypeScalarFieldNullDateTime_ThrowsSnapshotFieldException);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson, snapshotName,
                    matchOptions => matchOptions.IsTypeField<DateTime>("Age")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldGuid_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_IsTypeScalarFieldGuid_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<Guid>("Id"));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNotGuid_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_IsTypeScalarFieldNotGuid_ThrowsSnapshotFieldException);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<Guid>("Size")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNullGuid_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_IsTypeScalarFieldNullGuid_ThrowsSnapshotFieldException);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson, snapshotName,
                    matchOptions => matchOptions.IsTypeField<Guid>("Age")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldInt_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_IsTypeScalarFieldInt_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<int>("Age"));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNotInt_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_IsTypeScalarFieldNotInt_ThrowsSnapshotFieldException);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<int>("Size")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNullInt_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeScalarFieldNullInt_ThrowsSnapshotFieldException);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson, snapshotName,
                    matchOptions => matchOptions.IsTypeField<int>("Age")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldDecimal_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeScalarFieldDecimal_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<decimal>("Size"));
        }

        [Fact]
        public void Match_IsTypeScalarFieldDecimalBoxed_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeScalarFieldDecimalBoxed_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<decimal>("Age"));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNotDecimal_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeScalarFieldNotDecimal_ThrowsSnapshotFieldException);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<decimal>("Firstname")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldNullDecimal_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeScalarFieldNullDecimal_ThrowsSnapshotFieldException);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider().Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(() =>
                Snapshot.Match(testPerson, snapshotName,
                    matchOptions => matchOptions.IsTypeField<int>("Age")));
        }

        [Fact]
        public void Match_IsTypeScalarFieldInAllWays_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeScalarFieldInAllWays_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .WithSize(1.5m).Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<DateTime>("CreationDate"));
            Snapshot.Match(testPerson, snapshotName, matchOptions =>
                matchOptions.IsType(option => option.Field<DateTime>("CreationDate")));
        }

        [Fact]
        public void Match_IsTypeComplexObjectField_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeComplexObjectField_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeField<TestAddress>("Address"));
        }

        [Fact]
        public void Match_IsTypeWildcardScalarFieldsArray_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeWildcardScalarFieldsArray_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Children.ElementAt(0).DateOfBirth = DateTime.Parse("2015-08-17");
            testPerson.Children.ElementAt(1).DateOfBirth = DateTime.Parse("2017-08-17");
            testPerson.Children.ElementAt(2).DateOfBirth = DateTime.Parse("2018-08-17");

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeFields<DateTime>("Children[*].DateOfBirth"));
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsType(
                    option => option.Fields<DateTime>("Children[*].DateOfBirth")));
        }

        [Fact]
        public void Match_IsTypeWildcardScalarFieldsOneEntry_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeWildcardScalarFieldsOneEntry_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Children.ElementAt(0).DateOfBirth = DateTime.Parse("2015-08-17");

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeFields<DateTime>("Children[*].DateOfBirth"));
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsType(
                    option => option.Fields<DateTime>("Children[*].DateOfBirth")));
        }

        [Fact]
        public void Match_IsTypeWildcardComplexFieldsArray_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeWildcardComplexFieldsArray_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(TestDataBuilder.TestChildJames().Build())
                .Build();

            testPerson.Children.ElementAt(0).Name = "newName1x";
            testPerson.Children.ElementAt(1).Name = "newName2x";
            testPerson.Children.ElementAt(2).Name = "newName3x";

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsTypeFields<TestChild>("Children[*]"));
            Snapshot.Match(testPerson, snapshotName,
                matchOptions => matchOptions.IsType(
                    option => option.Fields<TestChild>("Children[*]")));
        }

        [Fact]
        public void Match_IsTypeArrayFields_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeArrayFields_SuccessfulMatch);

            object[] testPersons = new object[]
            {
                TestDataBuilder.TestPersonMarkWalton().Build(),
                TestDataBuilder.TestPersonSandraSchneider().Build(),
                TestDataBuilder.TestPersonMarkWalton().Build()
            };

            // act & assert
            Snapshot.Match(testPersons, snapshotName,
                matchOptions => matchOptions.IsTypeFields<TestPerson>("[*]"));
        }

        [Fact]
        public void Match_IsTypeArrayFieldsPerson_SuccessfulIgnored()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_IsTypeArrayFieldsPerson_SuccessfulIgnored);

            object[] testPersons = new object[]
            {
                TestDataBuilder.TestPersonMarkWalton().Build(),
                TestDataBuilder.TestPersonSandraSchneider().Build(),
                TestDataBuilder.TestPersonMarkWalton().Build()
            };

            // act & assert
            Snapshot.Match(testPersons, snapshotName,
                matchOptions => matchOptions.IsTypeFields<DateTime>("[*].DateOfBirth"));
        }
    }
}
