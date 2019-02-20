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
        /// <param name="snapshotFileInfo">
        /// The file and folder path of the running snapshot unit test
        /// </param>
        void Cleanup(ISnapshotFileInfo snapshotFileInfo);
    }
}