namespace Snapshooter.Core
{
    /// <summary>
    /// This class is responsible to get all full namermation for the snapshot to store.
    /// </summary>
    public interface ISnapshotFullNameReader
    {
        /// <summary>
        /// Reads all full name information for the snapshot to store.
        /// </summary>
        /// <returns></returns>
        SnapshotFullName ReadSnapshotFullName();
    }
}