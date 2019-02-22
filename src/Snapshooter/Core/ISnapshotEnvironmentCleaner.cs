namespace Snapshooter.Core
{
    /// <summary>
    /// The snapshot environment cleaner is responsible to clean up the environment
    /// of the snapshot unit test. Deleting file and folders.
    /// </summary>
    public interface ISnapshotEnvironmentCleaner
    {
        /// <summary>
        /// Cleans up the snapshot unit test environment.
        /// </summary>
        /// <param name="snapshotFullName">
        /// The file name and folder path of the current snapshot.
        /// </param>
        void Cleanup(SnapshotFullName snapshotFullName);
    }
}