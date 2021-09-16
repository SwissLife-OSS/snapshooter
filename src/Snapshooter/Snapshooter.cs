using System;
using Snapshooter.Core;

namespace Snapshooter
{
    /// <summary>
    /// The <see cref="Snapshooter"/> can be used to compare a given object
    /// against a snapshot. If no snapshot exists, a new snapshot will be created from
    /// the current object and saved on the file system.
    /// Snapshooter can also be used to resolve the name of a snapshot within a unit test.
    /// </summary>
    public class Snapshooter
    {
        private readonly ISnapshotAssert _snapshotAssert;
        private readonly ISnapshotFileHandler _snapshotFileHandler;
        private readonly ISnapshotFullNameResolver _snapshotFullNameResolver;

        /// <summary>
        /// Constructor of the class <see cref="Snapshooter"/>
        /// initializes a new instance.
        /// </summary>
        /// <param name="snapshotAssert">The snapshot asserter.</param>
        /// <param name="snapshotFileHandler">The snapshot file manager.</param>
        /// <param name="snapshotFullNameResolver">The snapshot full name resolver.</param>
        public Snapshooter(
            ISnapshotAssert snapshotAssert,
            ISnapshotFileHandler snapshotFileHandler,
            ISnapshotFullNameResolver snapshotFullNameResolver)
        {
            _snapshotAssert = snapshotAssert;
            _snapshotFileHandler = snapshotFileHandler;
            _snapshotFullNameResolver = snapshotFullNameResolver;
        }

        /// <summary>
        /// Creates a snapshot of the given object and compares it with the
        /// already existing snapshot of the test.
        /// If no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, given by the snapshot full name.
        /// </summary>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotFullName">
        /// The full name of a snapshot with folder and file name.</param>
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the snapshot comparison.
        /// </param>
        public void AssertSnapshot(
            object currentResult,
            SnapshotFullName snapshotFullName,
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            if (currentResult == null)
            {
                throw new ArgumentNullException(nameof(currentResult));
            }

            if (snapshotFullName == null)
            {
                throw new ArgumentNullException(nameof(snapshotFullName));
            }

            _snapshotAssert.AssertSnapshot(
                currentResult, snapshotFullName, matchOptions);
        }

        /// <summary>
        /// Deletes the given snapshot file.
        /// If no snapshot file exists, nothing will happen.
        /// </summary>
        /// <param name="snapshotFullName">
        /// The full name of a snapshot with folder and file name.
        /// </param>
        public void DeleteSnapshot(SnapshotFullName snapshotFullName)
        {
            if (snapshotFullName == null)
            {
                throw new ArgumentNullException(nameof(snapshotFullName));
            }

            _snapshotFileHandler.DeleteSnapshot(snapshotFullName);
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
        /// Example:
        /// Snapshot name = 'NumberAdditionTest'
        /// Snapshot name extension = '5', '6', 'Result', '11'
        /// Result: 'NumberAdditionTest_5_6_Result_11'
        /// </param>
        /// <returns>The full name of a snapshot.</returns>
        public SnapshotFullName ResolveSnapshotFullName(
            string snapshotName = null,
            SnapshotNameExtension snapshotNameExtension = null)
        {
            SnapshotFullName snapshotFullName = _snapshotFullNameResolver
                .ResolveSnapshotFullName(
                    snapshotName, snapshotNameExtension?.ToParamsString());

            return snapshotFullName;
        }
    }
}
