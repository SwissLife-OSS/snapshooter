using System;
using System.IO;
using FluentAssertions;
using Snapshooter.Exceptions;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit3.Tests.Helpers;
using Xunit;

namespace Snapshooter.Xunit3.Tests.MatchOptions.AssertField
{
    public class AssertFieldTests
    {
        [Fact]
        public void Match_AssertScalarGuidField_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson,
                        matchOption => matchOption.Assert(
                            fieldOption => Assert.Equal(fieldOption.Field<Guid>("Id"),
                                Guid.Parse("c78c698f-9ee5-4b4b-9a0e-ef729b1f8ec8"))));
        }

        [Fact]
        public void Match_AssertScalarGuidFieldNotMatch_ThrowsSnapshotCompareException()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Assert.Throws<SnapshotCompareException>(
                () => Snapshot.Match(testPerson,
                    matchOption => matchOption.Assert(
                        fieldOption => Assert.Equal(fieldOption.Field<Guid>("Id"),
                            Guid.Parse("fcf04ca6-d8f2-4214-a3ff-d0ded5bad4de")))));
        }

        [Fact]
        public void Match_AssertScalarGuidNullField_SuccessfulMatch()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonSandraSchneider()
                .WithId(null)
                .Build();

            // act & assert
            Snapshot.Match(testPerson,
                matchOption => matchOption.Assert(
                    fieldOption => Assert.Null(fieldOption.Field<Guid?>("Id"))));
        }

        [Fact]
        public void Match_AssertScalarStringField_SuccessfulMatch()
        {
            // arrange
            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            // act & assert
            Snapshot.Match(testChild,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Equal("Hanna", fieldOption.Field<string>("Children[2].Name"))));
        }

        [Fact]
        public void Match_AssertScalarStringFieldUnequal_ThrowsSnapshotCompareException()
        {
            // arrange
            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            // act & assert
            Assert.Throws<SnapshotCompareException>(() =>
                Snapshot.Match(testChild,
                    matchOption => matchOption.Assert(fieldOption =>
                        Assert.Equal("Anna", fieldOption.Field<string>("Children[2].Name")))));
        }

        [Fact]
        public void Match_AssertScalarNullStringField_SuccessfulMatch()
        {
            // arrange
            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            // act & assert
            Snapshot.Match(testChild,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Null(fieldOption.Field<string>("Children[1].Name"))));
        }

        [Fact]
        public void Match_AssertArrayNotEmpty_SuccessfulMatch()
        {
            // arrange
            TestPerson testChild = TestDataBuilder
                .TestPersonSandraWalton()
                .Build();

            // act & assert
            Snapshot.Match(testChild,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.NotEmpty(fieldOption.Fields<TestPerson>("Relatives[*]"))));
        }

        [Fact]
        public void Match_AssertArrayEmpty_SuccessfulMatch()
        {
            // arrange
            TestPerson testChild = TestDataBuilder
                .TestPersonSandraSchneider()
                .Build();

            // act & assert
            Snapshot.Match(testChild,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Empty(fieldOption.Fields<TestChild>("Children[*]"))));
        }

        [Fact]
        public void Match_AssertScalarStringFieldToInteger_SuccessfulMatch()
        {
            // arrange
            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .WithFirstname(22.ToString())
                .Build();

            // act & assert
            Snapshot.Match(testChild,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Equal(22, fieldOption.Field<int>("Firstname"))));
        }

        [Fact]
        public void Match_AssertScalarStringFieldToIntegerParseFailure_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(
                () => Snapshot.Match(testChild,
                    matchOption => matchOption.Assert(fieldOption =>
                        Assert.Equal(22, fieldOption.Field<int>("Firstname")))));
        }

        [Fact]
        public void Match_AssertScalarNullIntegerFieldWithNull_SuccessfulMatch()
        {
            // arrange
            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(null)
                .Build();

            // act & assert
            Snapshot.Match(testChild,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Null(fieldOption.Field<int?>("Age"))));
        }

        [Fact]
        public void Match_AssertScalarNullIntegerFieldWithoutNull_ThrowsSnapshotFieldException()
        {
            // arrange
            TestPerson testChild = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(null)
                .Build();

            // act & assert
            Assert.Throws<SnapshotFieldException>(
                () => Snapshot.Match(testChild,
                     matchOption => matchOption.Assert(fieldOption =>
                        Assert.Null(fieldOption.Field<int>("Age")))));
        }

        [Fact]
        public void Match_AssertSeveralFields_SuccessfulMatch()
        {
            // arrange
            TestChild testChild = TestDataBuilder.TestChildJames().Build();
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .AddChild(testChild)
                .Build();

            testPerson.Id = Guid.NewGuid();
            testPerson.CreationDate = DateTime.UtcNow;
            testPerson.Address.StreetNumber = -58;
            testPerson.Address.Country = null!;
            testPerson.Relatives[0].Address.Plz = null;

            // act & assert
            Snapshot.Match(testPerson,
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

        [Fact]
        public void Match_AssertEqualGuidValueFailsWithinFirstSnapshotCreation_ThrowsSnapshotCompareException()
        {
            // arrange
            var snapshotFullNameResolver = new SnapshotFullNameResolver(
                new Xunit3SnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName();

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            File.Delete(snapshotFileName);

            Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", false.ToString());

            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act
            Action action = () => Snapshot.Match(testPerson,
                matchOption => matchOption.Assert(
                    fieldOption => Assert.Equal(fieldOption.Field<Guid>("Id"),
                        Guid.Parse("C24C7F55-2C96-442B-B9D5-35B642169E72"))));

            // assert
            Assert.Throws<SnapshotCompareException>(action);
            Assert.False(File.Exists(snapshotFileName));
        }

        [Fact]
        public void Match_AssertTwoFieldsAgainstEachOtherWithinSnapshot_SuccessfulAssert()
        {
            // arrange
            TestPerson testChild = TestDataBuilder
                .TestPersonMarkWalton()
                .Build();

            // act & assert
            Snapshot.Match(testChild,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Equal(
                        fieldOption.Field<string>("Address.Country.Name"),
                        fieldOption.Field<string>("Relatives[0].Address.Country.Name"))));
        }

        [Fact]
        public void Match_AssertTwoUnequalFieldsAgainstEachOtherWithinSnapshot_FailedAssert()
        {
            // arrange
            TestPerson testChild = TestDataBuilder
                .TestPersonMarkWalton()
                .Build();

            // act
            Action action = () => Snapshot.Match(testChild,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Equal(
                        fieldOption.Field<string>("Lastname"),
                        fieldOption.Field<string>("Relatives[0].Lastname"))));

            // assert
            Assert.Throws<SnapshotCompareException>(action);
        }

        [Fact]
        public void Match_AssertTwoRandomFieldsAgainstEachOtherWithinSnapshot_SuccessfulAssert()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder
                .TestPersonMarkWalton()                
                .Build();

            Guid id = Guid.NewGuid();

            testPerson.Id = id;
            testPerson.Relatives[0].Id = id;

            // act & assert
            Snapshot.Match(testPerson,
                matchOption => matchOption.Assert(fieldOption =>
                    Assert.Equal(
                        fieldOption.Field<string>("Id"),
                        fieldOption.Field<string>("Relatives[0].Id"))));
        }

        [Fact]
        public void Match_AssertMultipleTwoFieldCompares_Success()
        {
            // arrange
            string snapshotFileName =
                SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

            string expectedSnapshot =
                File.ReadAllText(snapshotFileName + ".original");

            // act & assert
            Snapshot.Match(expectedSnapshot, matchOptions => matchOptions
                    .Assert(fieldOption => Assert.Equal(
                        fieldOption.Field<Guid>("changeSets[0].DocumentInstanceId"),
                        fieldOption.Field<Guid>("docInstances[0].Id")))
                    .Assert(fieldOption => Assert.Equal(
                        fieldOption.Field<Guid>("audits[0].DocumentInstanceId"),
                        fieldOption.Field<Guid>("docInstances[0].Id")))
                    .Assert(fieldOption => Assert.Equal(
                        fieldOption.Field<Guid>("changeSets[0].UserId"),
                        fieldOption.Field<Guid>("users[0].UserId")))
                    .Assert(fieldOption => Assert.Equal(
                        fieldOption.Field<Guid>("users[0].UserId"),
                        fieldOption.Field<Guid>("audits[0].UserId")))
                    .IsTypeFields<Guid>("changeSets[*].UserId")
                    .IsTypeField<Guid>("changeSets[*].Id")
                    .IsTypeField<DateTime>("changeSets[*].ChangeDate")
                    .IsTypeField<Guid>("changeSets[*].DocumentInstanceId")
                    .IsTypeField<DateTime>("audits[*].TimeStamp")
                    .IsTypeField<Guid>("audits[*].Id")
                );
        }
    }
}
