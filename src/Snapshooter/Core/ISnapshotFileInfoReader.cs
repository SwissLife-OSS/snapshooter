namespace Snapshooter.Core
{
    /// <summary>
    /// This class is responsible to get all file information for the snapshot to store.
    /// </summary>
    public interface ISnapshotFileInfoReader
    {
        /// <summary>
        /// Reads all file information for the snapshot to store.
        /// </summary>
        /// <returns></returns>
        SnapshotFileInfo ReadSnapshotFileInfo();
    }
}