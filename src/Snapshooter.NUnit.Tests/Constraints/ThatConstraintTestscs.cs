using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Snapshooter.NUnit.Tests.Constraints
{
    public class ThatConstraintTestscs
    {
        [Test]
        public void TestIfStoredSnapshotIsMatching()
        {
            var actual = "dd";

            Assert.That(actual, Matches.ToSnapshot);
        }
    }
}
