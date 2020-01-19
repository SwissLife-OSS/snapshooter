using Snapshooter.Core;

namespace Snapshooter
{
    /// <summary>
    /// Resolves the full snapshot name for a unit test. The full snapshot name
    /// consists of the snapshot file name and the folder path.
    /// </summary>
    public interface ISnapshotFullNameResolver
    {
        /// <summary>
        /// Resolves automatically the snapshot name for the running unit test.
        /// </summary>
        /// <returns>The full name of a snapshot.</returns>
        SnapshotFullName ResolveSnapshotFullName();

        /// <summary>
        /// Resolves the snapshot name for the running unit test.
        /// The default generated snapshot name can be overwritten
        /// by the given snapshot name.
        /// </summary>
        /// <param name="snapshotName">
        /// The snapshot name given by the user. This snapshot name will overwrite
        /// the automatically generated snapshot name.
        /// </param>
        /// <returns>The full name of a snapshot.</returns>
        SnapshotFullName ResolveSnapshotFullName(string snapshotName);

        /// <summary>
        /// Resolves the snapshot name for the running unit test.
        /// The default generated snapshot name can either be overwritten
        /// with a given snapshot name, or can be extended by the snapshot name extensions,
        /// or both.
        /// </summary>
        /// <param name="snapshotName">
        /// The snapshot name given by the user, this snapshot name will overwrite
        /// the automatically generated snapshot name.
        /// </param>
        /// <param name="snapshotNameExtension">
        /// The snapshot name extension will extend the snapshot name with
        /// this given extensions. It can be used to make a snapshot name even more
        /// specific.
        /// </param>
        /// <returns>The full name of a snapshot.</returns>
        SnapshotFullName ResolveSnapshotFullName(string snapshotName, string snapshotNameExtension);
    }
}
