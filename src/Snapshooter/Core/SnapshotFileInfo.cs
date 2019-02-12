namespace Snapshooter.Core
{
    /// <summary>
    /// The SnapshotFileInfo instance contains the file name and the path
    /// of the snapshot to store on the file system.
    /// </summary>
    public class SnapshotFileInfo : ISnapshotFileInfo
    {
		/// <summary>
        /// The file name of the snapshot.
        /// </summary>
		public string Filename { get; set; }

		/// <summary>
        ///  The folder of the snapshot
        /// </summary>
		public string FolderPath { get; set; }
    }
}
