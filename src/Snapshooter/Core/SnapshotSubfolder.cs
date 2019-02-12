namespace Snapshooter.Core
{
	/// <summary>
    /// The snapshot subfolders
    /// </summary>
    public enum SnapshotSubfolder
    {
		/// <summary>
        /// The subfolder to store new snapshots
        /// </summary>
		New = 1,

		/// <summary>
        /// The subfolder to store not matching snapshots.		
        /// </summary>
		Mismatch = 2,
    }
}
