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
        /// <param name="snapshotFileInfo">
        /// The file and folder path of the running snapshot unit test
        /// </param>
        public void Cleanup(ISnapshotFileInfo snapshotFileInfo)
        {
            CleanupMismatchFolder(snapshotFileInfo);
        }

        private void CleanupMismatchFolder(ISnapshotFileInfo snapshotFileInfo)
        {
            if (!_mismatchFolders.Contains(snapshotFileInfo.FolderPath))
            {
                lock (_lockObject)
                {
                    if (!_mismatchFolders.Contains(snapshotFileInfo.FolderPath))
                    {
                        _snapshotFileHandler.DeleteSnapshotSubfolder(
                            snapshotFileInfo, FileNames.MismatchFolderName);

                        _mismatchFolders.Add(snapshotFileInfo.FolderPath);
                    }
                }
            }
        }

    }
}
