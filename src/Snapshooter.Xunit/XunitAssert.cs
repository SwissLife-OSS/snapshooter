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
                    $"\nMissmatch at line {i + 1} column {GetMissmatchColumn(expected, actual)}\n"
                    + $"Expected:\t{expected}\n"
                    + $"Actual:\t\t{actual}\n");
            }
        }

        private int GetMissmatchColumn(string expected, string actual)
        {
            return expected.Zip(actual, (a, b) => a == b).TakeWhile(b => b).Count() + 1;
        }
    }
}
