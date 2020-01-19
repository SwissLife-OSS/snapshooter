using System.Collections.Generic;

namespace Snapshooter.Core
{
    /// <summary>
    /// The snapshot environment cleaner is responsible to clean up the environment
    /// of the snapshot unit test. Deleting file and folders.
    /// </summary>
    public class SnapshotEnvironmentCleaner : ISnapshotEnvironmentCleaner
    {
        private static readonly object _lockObject = new object ();
        private static readonly HashSet<string> _mismatchFolders = new HashSet<string>();

        private readonly ISnapshotFileHandler _snapshotFileHandler;

        /// <summary>
        /// Constructor of the <see cref="SnapshotEnvironmentCleaner"/> class to create
        /// a new instance.
        /// </summary>
        /// <param name="snapshotFileHandler"></param>
        public SnapshotEnvironmentCleaner(ISnapshotFileHandler snapshotFileHandler)
        {
            _snapshotFileHandler = snapshotFileHandler;
        }

        /// <summary>
        /// Cleans up the snapshot unit test environment.
        /// </summary>
        /// <param name="snapshotFullName">
        /// The file and folder path of the actual snapshot.
        /// </param>
        public void Cleanup(SnapshotFullName snapshotFullName)
        {
            CleanupMismatchFolder(snapshotFullName);
        }

        private void CleanupMismatchFolder(SnapshotFullName snapshotFullName)
        {
            if (!_mismatchFolders.Contains(snapshotFullName.FolderPath))
            {
                lock (_lockObject)
                {
                    if (!_mismatchFolders.Contains(snapshotFullName.FolderPath))
                    {
                        _snapshotFileHandler.DeleteSnapshotSubfolder(
                            snapshotFullName, FileNames.MismatchFolderName);

                        _mismatchFolders.Add(snapshotFullName.FolderPath);
                    }
                }
            }
        }

    }
}
