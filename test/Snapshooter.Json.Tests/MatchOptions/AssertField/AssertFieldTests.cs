using System;
using FluentAssertions;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Xunit;

namespace Snapshooter.Json.Tests.MatchOptions.AssertField
{
    public class AssertFieldTests
    {
        [Fact]
        public void Match_AssertScalarGuidField_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_AssertScalarGuidField_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                        matchOption => matchOption.Assert(
                            fieldOption => Assert.Equal(fieldOption.Field<Guid>("Id"),
                                Guid.Parse("c78c698f-9ee5-4b4b-9a0e-ef729b1f8ec8"))));
        }

        [Fact]
        public void Match_AssertScalarGuidFieldNotMatch_ThrowsSnapshotCompareException()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_AssertScalarGuidFieldNotMatch_ThrowsSnapshotCompareException);

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<SnapshotCompareException>(
                () => Snapshot.Match(testPerson, snapshotName,
                    matchOption => matchOption.Assert(
                        fieldOption => Assert.Equal(fieldOption.Field<Guid>("Id"),
                            Guid.Parse("fcf04ca6-d8f2-4214-a3ff-d0ded5bad4de")))));
        }

        [Fact]
        public void Match_AssertScalarGuidNullField_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_AssertScalarGuidNullField_SuccessfulMatch);

            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .WithId(null)
                .Build();

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOption => matchOption.Assert(
                    fieldOption => Assert.Null(fieldOption.Field<Guid?>("Id"))));
        }

        [Fact]
        public void Match_AssertScalarStringField_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_AssertScalarStringField_SuccessfulMatch);

            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            // act & assert
            Snapshot.Match(testChild, snapshotName,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Equal("Hanna", fieldOption.Field<string>("Children[2].Name"))));
        }

        [Fact]
        public void Match_AssertScalarStringFieldUnequal_ThrowsSnapshotCompareException()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_AssertScalarStringFieldUnequal_ThrowsSnapshotCompareException);

            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            // act & assert
            Assert.Throws<SnapshotCompareException>(() => Snapshot.Match(
                testChild, snapshotName, matchOption => matchOption.Assert(fieldOption =>
                        Assert.Equal("Anna", fieldOption.Field<string>("Children[2].Name")))));
        }

        [Fact]
        public void Match_AssertScalarNullStringField_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_AssertScalarNullStringField_SuccessfulMatch);

            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            // act & assert
            Snapshot.Match(
                testChild, snapshotName, matchOption => matchOption.Assert(fieldOption =>
                    Assert.Null(fieldOption.Field<string>("Children[1].Name"))));
        }

        [Fact]
        public void Match_AssertScalarStringFieldToInteger_SuccessfulMatch()
        {
            // arrange
            string snapshotName = nameof(SnapshotTests) + "." +
                                  nameof(Match_AssertScalarStringFieldToInteger_SuccessfulMatch);

            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .WithFirstname(22.ToString())
                .Build();

            // act & assert
            Snapshot.Match(testChild, snapshotName,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Equal(22, fieldOption.Field<int>("Firstname"))));
        }

        [Fact]
        public void Match_AssertScalarStringFieldToIntegerParseFailure_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
                nameof(SnapshotTests) + "." +
                nameof(Match_AssertScalarStringFieldToIntegerParseFailure_ThrowsSnapshotFieldException);

            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(
                () => Snapshot.Match(testChild, snapshotName,
                    matchOption => matchOption.Assert(fieldOption =>
                        Assert.Equal(22, fieldOption.Field<int>("Firstname")))));
        }

        [Fact]
        public void Match_AssertScalarNullIntegerFieldWithNull_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_AssertScalarNullIntegerFieldWithNull_SuccessfulMatch);

            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(null)
                .Build();

            // act & assert
            Snapshot.Match(testChild, snapshotName,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Null(fieldOption.Field<int?>("Age"))));
        }

        [Fact]
        public void Match_AssertScalarNullIntegerFieldWithoutNull_ThrowsSnapshotFieldException()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_AssertScalarNullIntegerFieldWithoutNull_ThrowsSnapshotFieldException);

            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(null)
                .Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(
                () => Snapshot.Match(testChild, snapshotName,
                     matchOption => matchOption.Assert(fieldOption =>
                        Assert.Null(fieldOption.Field<int>("Age")))));
        }

        [Fact]
        public void Match_AssertSeveralFields_SuccessfulMatch()
        {
            // arrange
            string snapshotName =
               nameof(SnapshotTests) + "." +
               nameof(Match_AssertSeveralFields_SuccessfulMatch);

            TestChild testChild = TestDataBuilder.TestChildJames().Build();
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(testChild)
                .Build();

            testPerson.Id = Guid.NewGuid();
            testPerson.CreationDate = DateTime.UtcNow;
            testPerson.Address.StreetNumber = -58;
            testPerson.Address.Country = null;
            testPerson.Relatives[0].Address.Plz = null;

            // act & assert
            Snapshot.Match(testPerson, snapshotName,
                matchOption => matchOption
                    .Assert(fieldOption =>
                        Assert.NotEqual(Guid.Empty, fieldOption.Field<Guid>("Id")))
                    .Assert(fieldOption =>
                        Assert.NotEqual(DateTime.UtcNow.AddSeconds(5), fieldOption.Field<DateTime>("CreationDate")))
                    .Assert(fieldOption =>
                        Assert.Equal(-58, fieldOption.Field<int>("Address.StreetNumber")))
                    .Assert(fieldOption =>
                        testChild.Should().BeEquivalentTo(fieldOption.Field<TestChild>("Children[3]")))
                    .Assert(fieldOption =>
                        Assert.Null(fieldOption.Field<TestCountry>("Address.Country")))
                    .Assert(fieldOption =>
                        Assert.Null(fieldOption.Field<TestCountry>("Relatives[0].Address.Plz"))));
        }
    }
}
