using System;
using System.IO;
using System.Linq;

namespace Snapshooter.Core
{
    /// <summary>
    /// The class <see cref="SnapshotFileHandler"/> is responsible to either to store object as
    /// snapshots on the file system or to retrieve the data of a snapshot from the file system.
    /// </summary>
    public class SnapshotFileHandler : ISnapshotFileHandler
    {
        /// <summary>
        /// Saves the current data as a snapshot on the file system.
        /// </summary>
        /// <param name="snapshotFileInfo">The file info of the snapshot.</param>
        /// <param name="subfolder"></param> 
        /// <param name="snapshotData">The current snapshot data to store.</param>
        /// <returns>The file path of the stored snapshot.</returns>
        public string SaveSnapshot(
			ISnapshotFileInfo snapshotFileInfo, SnapshotSubfolder subfolder, string snapshotData)
        {
            if (snapshotFileInfo == null)
                throw new ArgumentNullException(nameof(snapshotFileInfo));
            if (string.IsNullOrWhiteSpace(snapshotData))
                throw new ArgumentException("Must not be empty", nameof(snapshotData));

            string snapshotSubfolderPath = EnsureSnapshotSubfolder(snapshotFileInfo, subfolder);

            string fullSnapshotFilename = Path.Combine(
                snapshotSubfolderPath, snapshotFileInfo.Filename);

            File.WriteAllText(fullSnapshotFilename, snapshotData);

            return fullSnapshotFilename;
        }

        

        public void DeleteEmptySnapshotSubfolder(
			ISnapshotFileInfo snapshotFileInfo, SnapshotSubfolder subfolder)
        {
            if (snapshotFileInfo == null)
                throw new ArgumentNullException(nameof(snapshotFileInfo));

            var snapshotSubFolderPath = GetSnapshotSubfolderPath(snapshotFileInfo, subfolder);

            if (!Directory.Exists(snapshotSubFolderPath))
            {
                return;
            }

            if(!Directory.EnumerateFileSystemEntries(snapshotSubFolderPath).Any())
            {
                Directory.Delete(snapshotSubFolderPath);
            }
        }

        public void DeleteSnapshotSubfolderFile(
			ISnapshotFileInfo snapshotFileInfo, SnapshotSubfolder subfolder)
        {
            if (snapshotFileInfo == null)
                throw new ArgumentNullException(nameof(snapshotFileInfo));

            var snapshotSubfolderFilePath = 
				GetSnapshotSubfolderFilePath(snapshotFileInfo, subfolder);

            DeleteFileIfExist(snapshotSubfolderFilePath);
        }

        /// <summary>
        /// Loads the current snapshot from the file system.
        /// </summary>
        /// <param name="snapshotFileInfo">The file info of the snapshot.</param> 
        /// <returns>The expected snapshot.</returns>
        public string LoadSnapshot(ISnapshotFileInfo snapshotFileInfo)
        {
            if (snapshotFileInfo == null)
                throw new ArgumentNullException(nameof(snapshotFileInfo));

            string snapshotFolderPath = EnsureSnapshotFolder(snapshotFileInfo);
			
            string fullSnapshotName = Path.Combine(snapshotFolderPath, snapshotFileInfo.Filename);

            string snapshotData = null;

            if (File.Exists(fullSnapshotName))
            {
                snapshotData = File.ReadAllText(fullSnapshotName);
            }
						
            return snapshotData;
        }


        private string EnsureSnapshotFolder(ISnapshotFileInfo snapshotFileInfo)
        {
            var snapshotFolderPath = GetSnapshotFolderPath(snapshotFileInfo);

            CreateFolderIfNotExist(snapshotFolderPath);

            return snapshotFolderPath;
        }

        private string EnsureSnapshotSubfolder(
            ISnapshotFileInfo snapshotFileInfo, SnapshotSubfolder subfolder)
        {
            var snapshotSubFolderPath = GetSnapshotSubfolderPath(snapshotFileInfo, subfolder);

            CreateFolderIfNotExist(snapshotSubFolderPath);

            return snapshotSubFolderPath;
        }
		
        private static string GetSnapshotFolderPath(ISnapshotFileInfo snapshotFileInfo)
        {
            return Path.Combine(snapshotFileInfo.FolderPath, FileNames.SnapshotFolderName);
        }

        private static string GetSnapshotSubfolderPath(
			ISnapshotFileInfo snapshotFileInfo, SnapshotSubfolder subfolder)
        {
            var snapshotFolderPath = GetSnapshotFolderPath(snapshotFileInfo);

            return Path.Combine(snapshotFolderPath, 
				string.Concat("__", subfolder.ToString().ToLower(), "__"));
        }

        private static string GetSnapshotSubfolderFilePath(
            ISnapshotFileInfo snapshotFileInfo, SnapshotSubfolder subfolder)
        {
            var snapshotSubFolderPath = GetSnapshotSubfolderPath(snapshotFileInfo, subfolder);

            return Path.Combine(snapshotSubFolderPath, snapshotFileInfo.Filename);
        }

        private static void CreateFolderIfNotExist(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private static void DeleteFileIfExist(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
