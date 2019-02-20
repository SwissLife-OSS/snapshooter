using System;
using System.IO;

namespace Snapshooter.Core
{
    /// <summary>
    /// The <see cref="SnapshotFileHandler"/> is responsible to handle all snapshots  
    /// interactions on the file system. It can read, save and delete snapshots or 
    /// their folders.
    /// </summary>
    public class SnapshotFileHandler : ISnapshotFileHandler
    {
        /// <summary>
        /// Saves a new snapshot as a snapshot *.snap file in the __snapshots__ folder.
        /// </summary>
        /// <param name="snapshotFileInfo">The snapshot name and location.</param>
        /// <param name="snapshotData">The current snapshot data to store.</param>
        /// <returns>The file path of the stored snapshot.</returns>
        public string SaveNewSnapshot(
            ISnapshotFileInfo snapshotFileInfo, string snapshotData)
        {
            if (snapshotFileInfo == null)
            {
                throw new ArgumentNullException(nameof(snapshotFileInfo));
            }
                
            if (string.IsNullOrWhiteSpace(snapshotData))
            {
                throw new ArgumentException("Must not be empty", nameof(snapshotData));
            }
                
            string snapshotSubfolderPath = EnsureSnapshotFolder(snapshotFileInfo);

            string fullSnapshotFilename = Path.Combine(
                snapshotSubfolderPath, snapshotFileInfo.Filename);

            File.WriteAllText(fullSnapshotFilename, snapshotData);

            return fullSnapshotFilename;
        }

        /// <summary>
        /// Saves a mismatching snapshot as a snapshot *.snap file 
        /// in the __snapshots__/__mismatch__ folder.
        /// </summary>
        /// <param name="snapshotFileInfo">The snapshot name and location.</param>
        /// <param name="snapshotData">The current snapshot data to store.</param>
        /// <returns>The file path of the stored snapshot.</returns>
        public string SaveMismatchSnapshot(
            ISnapshotFileInfo snapshotFileInfo, string snapshotData)
        {
            if (snapshotFileInfo == null)
            {
                throw new ArgumentNullException(nameof(snapshotFileInfo));
            }

            if (string.IsNullOrWhiteSpace(snapshotData))
            {
                throw new ArgumentException("Must not be empty", nameof(snapshotData));
            }

            string snapshotSubfolderPath = EnsureSnapshotSubfolder(
                snapshotFileInfo, FileNames.MismatchFolderName);

            string fullSnapshotFilename = Path.Combine(
                snapshotSubfolderPath, snapshotFileInfo.Filename);

            File.WriteAllText(fullSnapshotFilename, snapshotData);

            return fullSnapshotFilename;
        }

        /// <summary>
        /// Reads the current snapshot from the __snapshots__ folder.
        /// </summary>
        /// <param name="snapshotFileInfo">The file info of the snapshot.</param> 
        /// <returns>The expected snapshot.</returns>
        public string ReadSnapshot(ISnapshotFileInfo snapshotFileInfo)
        {
            if (snapshotFileInfo == null)
            {
                throw new ArgumentNullException(nameof(snapshotFileInfo));
            }
                
            string snapshotFolderPath = EnsureSnapshotFolder(snapshotFileInfo);

            string fullSnapshotName = Path.Combine(snapshotFolderPath, snapshotFileInfo.Filename);

            string snapshotData = null;

            if (File.Exists(fullSnapshotName))
            {
                snapshotData = File.ReadAllText(fullSnapshotName);
            }

            return snapshotData;
        }
                
        /// <summary>
        /// Deletes the given subfolder of the __snapshots__ folder of the current snapshot test.
        /// </summary>
        /// <param name="snapshotFileInfo">The location of the running snapshot test.</param>
        /// <param name="subfolderName">The subfolder to delete.</param>
        public void DeleteSnapshotSubfolder(
            ISnapshotFileInfo snapshotFileInfo, string subfolderName)
        {
            if (snapshotFileInfo == null)
            {
                throw new ArgumentNullException(nameof(snapshotFileInfo));
            }

            if (string.IsNullOrEmpty(subfolderName))
            {
                throw new ArgumentException($"{nameof(subfolderName)} must be set");
            }

            var snapshotSubFolderPath = GetSnapshotSubfolderPath(snapshotFileInfo, subfolderName);

            DeleteFolderIfExist(snapshotSubFolderPath);
        }
        
        private string EnsureSnapshotFolder(ISnapshotFileInfo snapshotFileInfo)
        {
            var snapshotFolderPath = GetSnapshotFolderPath(snapshotFileInfo);

            CreateFolderIfNotExist(snapshotFolderPath);

            return snapshotFolderPath;
        }

        private string EnsureSnapshotSubfolder(
            ISnapshotFileInfo snapshotFileInfo, string subfolder)
        {
            var snapshotSubFolderPath = GetSnapshotSubfolderPath(snapshotFileInfo, subfolder);

            CreateFolderIfNotExist(snapshotSubFolderPath);

            return snapshotSubFolderPath;
        }
        
        private static string GetSnapshotFolderPath(ISnapshotFileInfo snapshotFileInfo)
        {
            return Path.Combine(
                snapshotFileInfo.FolderPath, FileNames.SnapshotFolderName);
        }

        private static string GetSnapshotSubfolderPath(
            ISnapshotFileInfo snapshotFileInfo, string subfolder)
        {
            var snapshotFolderPath = GetSnapshotFolderPath(snapshotFileInfo);

            return Path.Combine(snapshotFolderPath, subfolder);
        }

        private static void CreateFolderIfNotExist(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private static void DeleteFolderIfExist(string snapshotSubFolderPath)
        {
            if (Directory.Exists(snapshotSubFolderPath))
            {
                Directory.Delete(snapshotSubFolderPath, true);
            }
        }
    }
}
