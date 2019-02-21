using System;
using Snapshooter.Core;

namespace Snapshooter.Json
{
    /// <summary>
    /// A json snapshot file info reader is responsible to get the information  
    /// for the snapshot file from a test.
    /// </summary>
    public class JsonSnapshotFileInfoReader : ISnapshotFileInfoReader
    {
        /// <summary>
        /// Evaluates the snapshot file infos.
        /// </summary>
        /// <returns>The file infos for the snapshot.</returns>
        public SnapshotFullName ReadSnapshotFileInfo()
        {
            return new SnapshotFullName(
                null, AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
