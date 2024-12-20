using Xunit;
using System;
using Xunit3.Sdk;

namespace Snapshooter.Xunit3.Tests;

public class XunitAssertTests
{
    [Fact]
    public void Assert_AssertEqualText_AssertSuccessful()
    {
        // arrange
        var snapshotAssert = new XunitAssert();

        // act & assert
        snapshotAssert.Assert("{Same}", "{Same}");         
    }

    [Fact]
    public void Assert_AssertUnequalText_ThrowsEqualException()
    {
        // arrange
        var snapshotAssert = new XunitAssert();

        // act 
        Action action = () => snapshotAssert.Assert("{Same}", "{Sme}");

        // assert    
        Assert.Throws<EqualException>(action);
    }
}
