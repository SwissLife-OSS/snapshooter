using System.IO;

namespace Snapshooter
{
    /// <summary>
    /// The snapshot full name instance contains the file name and the path
    /// of the snapshot to store on the file system.
    /// </summary>
    public class SnapshotFullName
    {
        /// <summary>
        /// Constructor of the class <see cref="SnapshotFullName"/> 
        /// initializes a new instance.
        /// </summary>
        /// <param name="fileName">The filename.</param>
        /// <param name="folderPath">The folder path.</param>
        public SnapshotFullName(string fileName, string folderPath)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();

            foreach (char invalidChar in invalidChars)
            {
                fileName = fileName.Replace(invalidChar, '_');
            }

            Filename = fileName;
            FolderPath = folderPath;
        }

        /// <summary>
        /// The file name of the snapshot.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        ///  The folder of the snapshot
        /// </summary>
        public string FolderPath { get; }
    }
}
