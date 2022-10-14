using System;
using System.IO;
using System.Text;
using Snapshooter.Exceptions;

#nullable enable

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
        /// <param name="snapshotFullName">The snapshot name and location.</param>
        /// <param name="snapshotData">The current snapshot data to store.</param>
        /// <returns>The file path of the stored snapshot.</returns>
        public string SaveNewSnapshot(
            SnapshotFullName snapshotFullName, string snapshotData)
        {
            if (snapshotFullName == null)
            {
                throw new ArgumentNullException(nameof(snapshotFullName));
            }

            if (string.IsNullOrWhiteSpace(snapshotData))
            {
                throw new ArgumentException("Must not be empty", nameof(snapshotData));
            }

            string snapshotSubfolderPath = EnsureSnapshotFolder(snapshotFullName);

            string fullSnapshotFilename = Path.Combine(
                snapshotSubfolderPath, snapshotFullName.Filename);

            File.WriteAllText(fullSnapshotFilename, snapshotData, Encoding.UTF8);

            return fullSnapshotFilename;
        }

        /// <summary>
        /// Saves a mismatching snapshot as a snapshot *.snap file
        /// in the __snapshots__/__mismatch__ folder.
        /// </summary>
        /// <param name="snapshotFullName">The snapshot name and location.</param>
        /// <param name="snapshotData">The current snapshot data to store.</param>
        /// <returns>The file path of the stored snapshot.</returns>
        public string SaveMismatchSnapshot(
            SnapshotFullName snapshotFullName, string snapshotData)
        {
            if (snapshotFullName == null)
            {
                throw new ArgumentNullException(nameof(snapshotFullName));
            }

            if (string.IsNullOrWhiteSpace(snapshotData))
            {
                throw new ArgumentException("Must not be empty", nameof(snapshotData));
            }

            string snapshotSubfolderPath = EnsureSnapshotSubfolder(
                snapshotFullName, FileNames.MismatchFolderName);

            string fullSnapshotFilename = Path.Combine(
                snapshotSubfolderPath, snapshotFullName.Filename);

            File.WriteAllText(fullSnapshotFilename, snapshotData, Encoding.UTF8);

            return fullSnapshotFilename;
        }

        /// <summary>
        /// Reads the current snapshot from the __snapshots__ folder.
        /// If the snapshot does not exists, an exception is thrown.
        /// </summary>
        /// <param name="snapshotFullName">The full name of the snapshot.</param>
        /// <returns>The expected snapshot.</returns>
        public string ReadSnapshot(SnapshotFullName snapshotFullName)
        {
            if (TryReadSnapshot(snapshotFullName, out string? snapshotData))

            {
                return snapshotData!;
            }

            throw new SnapshotNotFoundException(
                $"Snapshot '{snapshotFullName.Filename}' could not be found.");
        }

        /// <summary>
        /// Reads the current snapshot from the __snapshots__ folder.
        /// </summary>
        /// <param name="snapshotFullName">The full name of the snapshot.</param>
        /// <param name="snapshotData">The loaded snapshot data.</param>
        /// <returns>True if the snapshot could be found.</returns>
        public bool TryReadSnapshot(SnapshotFullName snapshotFullName, out string snapshotData)
        {
            if (snapshotFullName == null)
            {
                throw new ArgumentNullException(nameof(snapshotFullName));
            }

            snapshotData = string.Empty;

            string snapshotFolderPath = EnsureSnapshotFolder(snapshotFullName);

            string fullSnapshotName = Path.Combine(snapshotFolderPath, snapshotFullName.Filename);

            if (File.Exists(fullSnapshotName))
            {
                snapshotData = File.ReadAllText(fullSnapshotName, Encoding.UTF8);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes the current snapshot if exists from the __snapshots__ folder.
        /// </summary>
        /// <param name="snapshotFullName">The full name of the snapshot.</param>
        public void DeleteSnapshot(SnapshotFullName snapshotFullName)
        {
            if (snapshotFullName == null)
            {
                throw new ArgumentNullException(nameof(snapshotFullName));
            }

            string snapshotFolderPath =
                EnsureSnapshotFolder(snapshotFullName);

            string fullSnapshotName =
                Path.Combine(snapshotFolderPath, snapshotFullName.Filename);

            if (File.Exists(fullSnapshotName))
            {
                File.Delete(fullSnapshotName);
            }
        }

        /// <summary>
        /// Deletes the given subfolder of the __snapshots__ folder of the current snapshot test.
        /// </summary>
        /// <param name="snapshotFullName">The location of the running snapshot test.</param>
        /// <param name="subfolderName">The subfolder to delete.</param>
        public void DeleteSnapshotSubfolder(
            SnapshotFullName snapshotFullName, string subfolderName)
        {
            if (snapshotFullName == null)
            {
                throw new ArgumentNullException(nameof(snapshotFullName));
            }

            if (string.IsNullOrEmpty(subfolderName))
            {
                throw new ArgumentException($"{nameof(subfolderName)} must be set");
            }

            var snapshotSubFolderPath = GetSnapshotSubfolderPath(snapshotFullName, subfolderName);

            DeleteFolderIfExist(snapshotSubFolderPath);
        }

        private string EnsureSnapshotFolder(SnapshotFullName snapshotFullName)
        {
            var snapshotFolderPath = GetSnapshotFolderPath(snapshotFullName);

            CreateFolderIfNotExist(snapshotFolderPath);

            return snapshotFolderPath;
        }

        private string EnsureSnapshotSubfolder(
            SnapshotFullName snapshotFullName, string subfolder)
        {
            var snapshotSubFolderPath = GetSnapshotSubfolderPath(snapshotFullName, subfolder);

            CreateFolderIfNotExist(snapshotSubFolderPath);

            return snapshotSubFolderPath;
        }

        private static string GetSnapshotFolderPath(SnapshotFullName snapshotFullName)
        {
            return Path.Combine(
                snapshotFullName.FolderPath, FileNames.SnapshotFolderName);
        }

        private static string GetSnapshotSubfolderPath(
            SnapshotFullName snapshotFullName, string subfolder)
        {
            var snapshotFolderPath = GetSnapshotFolderPath(snapshotFullName);

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
