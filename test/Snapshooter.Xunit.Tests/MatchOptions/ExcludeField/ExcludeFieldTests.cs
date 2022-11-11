using System;
using System.IO;
using FluentAssertions;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit.Tests.Helpers;
using Xunit;
using Xunit.Sdk;

namespace Snapshooter.Xunit.Tests.MatchOptions.ExcludeField;

public class ExcludeFieldsTests
{
    [Fact]
    public void ExcludeField_StringScalarField_FieldIsExcluded()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options.ExcludeField("Firstname"));
    }

    [Fact]
    public void ExcludeField_ExcludedFieldNotExcludedAnymore_ThrowsException()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act
        Action act = () => Snapshot
            .Match(testPerson, options => options.ExcludeField("Firstname"));

        // assert
        act.Should().Throw<EqualException>()
            .Which.Message.Should().Contain("Firstname");
    }

    [Fact]
    public void ExcludeField_NewExcludeMultipleFieldsSnapshot_CorrectFormatted()
    {
        // arrange
        string snapshotFileName =
            SnapshotDefaultNameResolver.ResolveSnapshotDefaultName();

        File.Delete(snapshotFileName);

        string expectedSnapshot =
            File.ReadAllText(snapshotFileName + ".original");

        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act
        Snapshot.Match(testPerson, options => options
            .ExcludeField("Firstname")
            .ExcludeField("DateOfBirth")           
            .ExcludeField("Size")           
            .ExcludeField("Address.Country.CountryCode")           
            .ExcludeField("Children")
            .ExcludeField("Relatives[*].Relatives")
            .ExcludeField("Relatives[*].Address"));

        // assert
        Assert.True(File.Exists(snapshotFileName));
        Snapshot.Match(expectedSnapshot);
    }

    [Fact]
    public void ExcludeField_ExcludeMultipleFieldsSnapshot_SuccessfullyCompared()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .ExcludeField("Firstname")
            .ExcludeField("DateOfBirth")
            .ExcludeField("Size")
            .ExcludeField("Address.Country.CountryCode")
            .ExcludeField("Children")
            .ExcludeField("Relatives[*].Relatives")
            .ExcludeField("Relatives[*].Address"));
    }

    [Fact]
    public void ExcludeField_ExcludeEntireArrayFieldsSnapshot_SuccessfullyCompared()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .ExcludeField("Children"));
    }

    [Fact]
    public void ExcludeField_ExcludeSignleArrayFieldsSnapshot_SuccessfullyCompared()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .ExcludeField("Children[*].Name"));
    }

    [Fact]
    public void ExcludeField_ExcludeComplexObject_SuccessfullyCompared()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .ExcludeField("Address.Country"));
    }

    [Fact]
    public void ExcludeField_ExcludeAllFieldsByName_SuccessfullyCompared()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .ExcludeField("**.DateOfBirth")
            .ExcludeField("**.CountryCode"));
    }

    [Fact]
    public void ExcludeField_ExcludeAllFieldsModified_Mismatch()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act 
        Action act = () => Snapshot.Match(testPerson, options => options
            .ExcludeField("**.DateOfBirth")
            .ExcludeField("**.CountryCode"));

        // assert
        act.Should().Throw<EqualException>()
            .Which.Message.Should().Contain("CountryCode");
    }
}
