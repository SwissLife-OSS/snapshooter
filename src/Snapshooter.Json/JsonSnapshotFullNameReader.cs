using System;
using Snapshooter.Core;

namespace Snapshooter.Json
{
    /// <summary>
    /// A json snapshot full name reader is responsible to get the information
    /// for the snapshot from a test.
    /// </summary>
    public class JsonSnapshotFullNameReader : ISnapshotFullNameReader
    {
        /// <summary>
        /// Evaluates the snapshot full name information.
        /// </summary>
        /// <returns>The full name for the snapshot.</returns>
        public SnapshotFullName ReadSnapshotFullName()
        {
            return new SnapshotFullName(
                null, AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
