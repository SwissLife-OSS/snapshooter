using System;
using NUnit.Framework;

namespace Snapshooter.NUnit.Tests
{
    public class NUnitAssertTests
    {
        [Test]
        public void Assert_AssertEqualText_AssertSuccessful()
        {
            // arrange
            var snapshotAssert = new NUnitAssert();

            // act & assert
            snapshotAssert.Assert("{Same}", "{Same}");
        }

        [Test]
        public void Assert_AssertUnequalText_ThrowsEqualException()
        {
            // arrange
            var snapshotAssert = new NUnitAssert();

            // act 
            Action action = () => snapshotAssert.Assert("{Same}", "{Sme}");

            // assert
            Assert.That(action, Throws.TypeOf<AssertionException>());
        }
    }
}
