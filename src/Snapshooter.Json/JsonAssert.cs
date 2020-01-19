using System;
using Snapshooter.Core;
using Snapshooter.Exceptions;

namespace Snapshooter.Json
{
    /// <summary>
    /// The JsonAssert instance compares two strings with the .Net string compare.
    /// </summary>
    public class JsonAssert : IAssert
    {
        /// <summary>
        /// Asserts the expected snapshot and the actual snapshot
        /// with the .Net string compare.
        /// </summary>
        /// <param name="expectedSnapshot">The expected snapshot.</param>
        /// <param name="actualSnapshot">The actual snapshot.</param>
        public virtual void Assert(string expectedSnapshot, string actualSnapshot)
        {
            int snapshotCompare = string.Compare(
                expectedSnapshot, actualSnapshot,
                StringComparison.InvariantCultureIgnoreCase);

            if (snapshotCompare != 0)
            {
                throw new SnapshotCompareException($"Snapshots are not equal: " +
                    $"expected: {expectedSnapshot}; " +
                    $"actual: {actualSnapshot}");
            }
        }
    }
}
