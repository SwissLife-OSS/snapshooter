using System;
using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using Xunit;

namespace Snapshooter.Examples.Xunit.MatchOptionsTests.AcceptTests;

public class AcceptTests
{
    [Fact]
    public void AcceptField_AcceptOneField_RightSnapshot()
    {
        // arrange
        var serviceClient = new ServiceClient();

        // act
        TestPerson person = serviceClient
            .CreatePerson(Guid.NewGuid(), "David", "Mustermann");

        // assert
        Snapshot.Match(person,
            matchOptions => matchOptions.AcceptField<Guid>("Id"));
    }

    [Fact]
    public void AcceptField_AcceptFieldsByName_RightSnapshot()
    {
        // arrange
        var serviceClient = new ServiceClient();

        // act
        TestPerson person = serviceClient
            .CreatePerson(Guid.NewGuid(), "David", "Mustermann");

        // assert
        Snapshot.Match(person,
            matchOptions => matchOptions.AcceptField<Guid>("**.Id"));
    }
}
