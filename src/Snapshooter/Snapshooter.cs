using System;

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
        private readonly ISnapshotFileInfoResolver _snapshotFullNameResolver;

        /// <summary>
        /// Constructor of the class <see cref="Snapshooter"/> 
        /// initializes a new instance.
        /// </summary>
        /// <param name="snapshotAssert">The snapshot asserter.</param>
        /// <param name="snapshotFullNameResolver">The snapshot full name resolver.</param>
        public Snapshooter(ISnapshotAssert snapshotAssert, 
            ISnapshotFileInfoResolver snapshotFullNameResolver)
        {
            _snapshotAssert = snapshotAssert;
            _snapshotFullNameResolver = snapshotFullNameResolver;
        }

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

        public SnapshotFullName ResolveSnapshotFullName(
            string snapshotName = null,
            SnapshotNameExtension snapshotNameExtension = null)
        {
            SnapshotFullName snapshotFullName = _snapshotFullNameResolver
                .ResolveSnapshotFileInfo(
                    snapshotName, snapshotNameExtension?.ToParamsString());

            return snapshotFullName;
        }
    }
}
