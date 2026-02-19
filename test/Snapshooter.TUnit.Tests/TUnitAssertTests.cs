using System;
using System.Threading.Tasks;
using TUnit.Assertions.AssertConditions.Throws;
using TUnit.Assertions.Exceptions;

namespace Snapshooter.TUnit.Tests
{
    public class TUnitAssertTests
    {
        [Test]
        public void Assert_AssertEqualText_AssertSuccessful()
        {
            // arrange
            var snapshotAssert = new TUnitAssert();

            // act & assert
            snapshotAssert.Assert("{Same}", "{Same}");
        }

        [Test]
        public async Task Assert_AssertUnequalText_ThrowsEqualException()
        {
            // arrange
            var snapshotAssert = new TUnitAssert();

            // act 
            Action action = () => snapshotAssert.Assert("{Same}", "{Sme}");

            // assert
            await Assert.That(action).ThrowsExactly<AssertionException>();
        }
    }
}
