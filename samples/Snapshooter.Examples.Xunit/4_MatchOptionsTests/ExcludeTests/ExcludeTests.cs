using System;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using Xunit;

namespace Snapshooter.Examples.Xunit.MatchOptionsTests.ExcludeTests;

public class ExcludeTests
{
    [Fact]
    public void ExcludeField_ExcludeOneField_RightSnapshot()
    {
        // arrange
        var serviceClient = new ServiceClient();

        // act
        TestPerson person = serviceClient
            .CreatePerson(id: Guid.NewGuid(), "David", "Mustermann");

        // assert
        Snapshot.Match(person);
    }

    [Fact]
    public void ExcludeField_ExcludeFieldsByName_RightSnapshot()
    {
        // arrange
        var serviceClient = new ServiceClient();

        // act
        TestPerson person = serviceClient
            .CreatePerson(id: Guid.NewGuid(), "David", "Mustermann");

        // assert
        Snapshot.Match(person);
    }
}
