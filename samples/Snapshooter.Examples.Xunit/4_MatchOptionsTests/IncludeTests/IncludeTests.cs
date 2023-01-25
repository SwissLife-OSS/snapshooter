using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using Xunit;

namespace Snapshooter.Examples.Xunit.MatchOptionsTests.IncludeTests;

public class IncludeTests
{
    [Fact]
    public void IncludeField_IncludeOneField_RightSnapshot()
    {
        // arrange
        var serviceClient = new ServiceClient();

        // act
        TestPerson person = serviceClient.CreatePerson();

        // assert
        Snapshot.Match(person);
    }

    [Fact]
    public void IncludeField_IncludeFieldsByName_RightSnapshot()
    {
        // arrange
        var serviceClient = new ServiceClient();

        // act
        TestPerson person = serviceClient.CreatePerson();

        // assert
        Snapshot.Match(person);
    }
}
