using Snapshooter.Core;
using Snapshooter.Exceptions;

namespace Snapshooter
{
    /// <summary>
    /// Resolves the full snapshot name for a unit test. The full snapshot name
    /// consists of the snapshot file name and the folder path.
    /// </summary>
    public class SnapshotFullNameResolver : ISnapshotFullNameResolver
    {
        private readonly ISnapshotFullNameReader _snapshotFullNameReader;

        /// <summary>
        /// Constructor of the class <see cref="SnapshotFullNameResolver"/> 
        /// initializes a new instance.
        /// </summary>
        /// <param name="snapshotFullNameReader">The filename.</param>
        public SnapshotFullNameResolver(
            ISnapshotFullNameReader snapshotFullNameReader)
        {
            _snapshotFullNameReader = snapshotFullNameReader;
        }

        /// <summary>
        /// Resolves automatically the snapshot name for the running unit test.
        /// </summary>
        /// <returns>The full name of a snapshot.</returns>
        public SnapshotFullName ResolveSnapshotFullName()
        {
            return ResolveSnapshotFullName(null);
        }

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
        public SnapshotFullName ResolveSnapshotFullName(
            string snapshotName)
        {
            return ResolveSnapshotFullName(snapshotName, null);
        }

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
        /// <returns>The full name of a snapshot.</returns>
        public SnapshotFullName ResolveSnapshotFullName(
            string snapshotName, string snapshotNameExtension)
        {
            SnapshotFullName snapshotFullName = 
                _snapshotFullNameReader.ReadSnapshotFullName();

            if (snapshotFullName == null)
            {
                throw new SnapshotTestException(
                    "The snapshot full name could not be evaluated.");
            }

            if(string.IsNullOrWhiteSpace(snapshotFullName.Filename) && 
               string.IsNullOrWhiteSpace(snapshotName))
            {
                throw new SnapshotTestException(
                    "No snapshot name could be resolved.");
            }

            if (string.IsNullOrWhiteSpace(snapshotName))
            {
                snapshotName = snapshotFullName.Filename;
            }

            snapshotName = AddSnapshotNameExtension(snapshotName, snapshotNameExtension);
            snapshotName = EnsureSnapshotFileExtension(snapshotName);
                                   
            return new SnapshotFullName(snapshotName, snapshotFullName.FolderPath);
        }

        private string EnsureSnapshotFileExtension(string snapshotName)
        {
            if (!snapshotName.EndsWith(FileNames.SnapshotFileExtension))
            {
                snapshotName = string.Concat(
                    snapshotName, FileNames.SnapshotFileExtension);
            }

            return snapshotName;
        }

        private string AddSnapshotNameExtension(
            string snapshotName, string snapshotNameExtension)
        {
            if (snapshotNameExtension != null)
            {
                snapshotName = string.Concat(snapshotName, snapshotNameExtension);
            }

            return snapshotName;
        }
    }
}