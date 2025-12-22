using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Snapshooter.MSTest.Tests
{
    [TestClass]
    public class MSTestAssertTests
    {
        [TestMethod]
        public void Assert_AssertEqualText_AssertSuccessful()
        {
            // arrange
            var snapshotAssert = new MSTestAssert();

            // act & assert
            snapshotAssert.Assert("{Same}", "{Same}");
        }

        [TestMethod]
        public void Assert_AssertUnequalText_ThrowsEqualException()
        {
            // arrange
            var snapshotAssert = new MSTestAssert();

            // act 
            Action action = () => snapshotAssert.Assert("{Same}", "{Sme}");

            // assert
            Assert.ThrowsExactly<AssertFailedException>(action);
        }
    }
}
