using System;
using System.Linq;
using Snapshooter.Core;

using x = Xunit;

namespace Snapshooter.Xunit
{
    /// <summary>
    /// The XunitAssert instance compares two strings with the XUnit assert utility.
    /// </summary>
    public class XunitAssert : IAssert
    {
        /// <summary>
        /// Asserts the expected snapshot and the actual snapshot
        /// with the XUnit assert utility.
        /// </summary>
        /// <param name="expectedSnapshot">The expected snapshot.</param>
        /// <param name="actualSnapshot">The actual snapshot.</param>
        public void Assert(string expectedSnapshot, string actualSnapshot)
        {
            string[] expectedData = expectedSnapshot.Split('\n');
            string[] actualData = actualSnapshot.Split('\n');

            for (int i = 0; i < Math.Max(expectedData.Length, actualData.Length); i++)
            {
                string expected = expectedData.ElementAtOrDefault(i);
                string actual = actualData.ElementAtOrDefault(i);
                x.Assert.True(expected.Equals(actual),
                    $"Missmatch at line {i + 1}\n\t\tExpected:\t{expected}\n\t\tActual:\t\t{actual}");
            }
        }
    }
}
