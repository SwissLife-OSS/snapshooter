using Snapshooter.Tests.Data;
using Xunit;

namespace Snapshooter.Xunit3.Tests.MatchOptions.IncludeField;

public class IncludeFieldTests
{
    [Fact]
    public void IncludeField_StringScalarField_IncludedOnlyField()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .IncludeField("Firstname"));
    }

    [Fact]
    public void IncludeField_StringScalarFields_IncludedOnlyFields()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .IncludeField("Firstname")
            .IncludeField("Age")
            .IncludeField("CreationDate"));
    }

    [Fact]
    public void IncludeField_StringDuplicatedScalarFields_IncludedOnlyFields()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .IncludeField("Age")
            .IncludeField("Lastname")
            .IncludeField("Age")
            .IncludeField("**.Lastname"));
    }
    
    [Fact]
    public void IncludeField_ComplexObjectField_IncludedOnlyField()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .IncludeField("Address"));
    }

    [Fact]
    public void IncludeField_IncludeTwiceInPath_RightFieldIncluded()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .IncludeField("Address")
            .IncludeField("Address.Country"));
    }

    [Fact]
    public void IncludeField_IncludeTwoDifferentFields_RightFieldIncluded()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .IncludeField("Children")
            .IncludeField("Relatives"));
    }

    [Fact]
    public void IncludeField_IncludeFieldsByName_IncludedOnlyFields()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .IncludeField("**.Address")
            .IncludeField("**.Name")
            .IncludeField("**.Country")
            .IncludeField("**.Lastname"));
    }

    [Fact]
    public void IncludeField_IncludeArrayFields_IncludedOnlyFields()
    {
        // arrange
        TestPerson testPerson = TestDataBuilder
            .TestPersonMarkWalton()
            .Build();

        // act & assert
        Snapshot.Match(testPerson, options => options
            .IncludeField("Children[2]")
            .IncludeField("Relatives[0]"));
    }
}
