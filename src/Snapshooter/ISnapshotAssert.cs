using System;

namespace Snapshooter
{
    /// <summary>
    /// The class <see cref="SnapshotAssert"/> can be used to compare a given object
    /// against a snapshot. If no snapshot exists, a new snapshot will be created from
    /// the current object and saved on the file system.
    /// </summary>
    public interface ISnapshotAssert
    {
        /// <summary>
        /// Compares the snapshot against the given result object. If the snapshot is
        /// new then it will be saved directly. If the snapshot is not matching with the
        /// given result object, then the given result object will be snapshot and saved
        /// in the given folder.
        /// </summary>
        /// <param name="currentResult">
        /// The object to compare.
        /// </param>
        /// <param name="snapshotFullName">
        /// The name and folder of the snapshot.
        /// </param>
        /// <param name="matchOptions">
        /// Additional match actions, which can be applied during the comparison
        /// </param>
        void AssertSnapshot(
            object currentResult,
            SnapshotFullName snapshotFullName,
            Func<MatchOptions, MatchOptions> matchOptions = null);
    }
}
