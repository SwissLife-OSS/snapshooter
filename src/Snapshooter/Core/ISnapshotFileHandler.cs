#nullable enable

namespace Snapshooter.Core
{
    /// <summary>
    /// The <see cref="SnapshotFileHandler"/> is responsible to handle all snapshots  
    /// interactions on the file system. It can read, save and delete snapshots or 
    /// their folders.
    /// </summary>
    public interface ISnapshotFileHandler
    {
        /// <summary>
        /// Saves a new snapshot as a snapshot *.snap file in the __snapshots__ folder.
        /// </summary>
        /// <param name="snapshotFullName">The snapshot name and location.</param>
        /// <param name="snapshotData">The current snapshot data to store.</param>
        /// <returns>The file path of the stored snapshot.</returns>
        string SaveNewSnapshot(
            SnapshotFullName snapshotFullName, string snapshotData);

        /// <summary>
        /// Saves a mismatching snapshot as a snapshot *.snap file 
        /// in the __snapshots__/__mismatch__ folder.
        /// </summary>
        /// <param name="snapshotFullName">The snapshot name and location.</param>
        /// <param name="snapshotData">The current snapshot data to store.</param>
        /// <returns>The file path of the stored snapshot.</returns>
        string SaveMismatchSnapshot(
            SnapshotFullName snapshotFullName, string snapshotData);

        /// <summary>
        /// Reads the current snapshot from the __snapshots__ folder.
        /// If the snapshot does not exists, an exception is thrown.
        /// </summary>
        /// <param name="snapshotFullName">The full name of the snapshot.</param> 
        /// <returns>The expected snapshot.</returns>
        string ReadSnapshot(SnapshotFullName snapshotFullName);

        /// <summary>
        /// Tries to read the current snapshot from the __snapshots__ folder.
        /// </summary>
        /// <param name="snapshotFullName">The full name of the snapshot.</param>
        /// <param name="snapshotData">The loaded snapshot data.</param>
        /// <returns>True if the snapshot could be found.</returns>
        bool TryReadSnapshot(SnapshotFullName snapshotFullName, out string? snapshotData);

        /// <summary>
        /// Deletes the current snapshot if exists from the __snapshots__ folder.
        /// </summary>
        /// <param name="snapshotFullName">The full name of the snapshot.</param>
        void DeleteSnapshot(SnapshotFullName snapshotFullName);

        /// <summary>
        /// Deletes the given subfolder of the __snapshots__ folder of the current snapshot test.
        /// </summary>
        /// <param name="snapshotFullName">The location of the running snapshot test.</param>
        /// <param name="subfolderName">The subfolder to delete.</param>
        void DeleteSnapshotSubfolder(
            SnapshotFullName snapshotFullName, string subfolderName);

    }
}
