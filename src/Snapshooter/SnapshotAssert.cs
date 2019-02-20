using System;
using Snapshooter.Core;

namespace Snapshooter
{
    /// <summary>
    /// The class <see cref="SnapshotAssert"/> can be used to compare a given object 
    /// against a snapshot. If no snapshot exists, a new snapshot will be created from
    /// the current object and saved under a certain file path, which will shown in the
    /// Assert exception.
    /// </summary>
    public class SnapshotAssert : ISnapshotAssert
    {
        private readonly ISnapshotSerializer _snapshotSerializer;
        private readonly ISnapshotFileInfoResolver _snapshotFileInfoResolver;
        private readonly ISnapshotFileHandler _snapshotFileHandler;
        private readonly ISnapshotEnvironmentCleaner _snapshotEnvironmentCleaner;
        private readonly ISnapshotComparer _snapshotComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotAssert"/> class.
        /// </summary>
        /// <param name="snapshotSerializer">The serializer of the snapshot object.</param>
        /// <param name="snapshotFileInfoResolver">The snapshot file information resolver.</param>
        /// <param name="snapshotFileHandler">The snapshot file handler.</param>
        /// <param name="snapshotEnvironmentCleaner">The environment cleanup utility.</param>
        /// <param name="snapshotComparer">The snaspshot text comparer.</param>
        public SnapshotAssert(ISnapshotSerializer snapshotSerializer,
                              ISnapshotFileInfoResolver snapshotFileInfoResolver,
                              ISnapshotFileHandler snapshotFileHandler,
                              ISnapshotEnvironmentCleaner snapshotEnvironmentCleaner,
                              ISnapshotComparer snapshotComparer)
        {
            _snapshotSerializer = snapshotSerializer 
                ?? throw new ArgumentNullException(nameof(snapshotSerializer));
            _snapshotFileInfoResolver = snapshotFileInfoResolver 
                ?? throw new ArgumentNullException(nameof(snapshotFileInfoResolver));
            _snapshotFileHandler = snapshotFileHandler 
                ?? throw new ArgumentNullException(nameof(snapshotFileHandler));
            _snapshotEnvironmentCleaner = snapshotEnvironmentCleaner
                ?? throw new ArgumentNullException(nameof(snapshotEnvironmentCleaner));
            _snapshotComparer = snapshotComparer 
                ?? throw new ArgumentNullException(nameof(snapshotComparer));
        }

        /// <summary>
        /// Compares the snapshot against the given result object.
        /// </summary>
        /// <param name="currentResult">
        /// The object to compare.
        /// </param>
        /// <param name="snapshotFileInfo">
        /// The file infos of the snapshot.
        /// </param> 
        /// <param name="matchOptions">
        /// Additional match actions, which can be applied during the comparison
        /// </param>
        public void AssertSnapshot(
            object currentResult, 
            string snapshotName = null, 
            SnapshotNameExtension snapshotNameExtension = null,
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            if (currentResult == null)
            {
                throw new ArgumentNullException(nameof(currentResult));
            }                
            
            ISnapshotFileInfo snapshotFileInfo = _snapshotFileInfoResolver
                .ResolveSnapshotFileInfo(snapshotName, snapshotNameExtension?.ToParamsString());

            _snapshotEnvironmentCleaner.Cleanup(snapshotFileInfo);

            string actualSnapshotSerialized = _snapshotSerializer.Serialize(currentResult);
            string savedSnapshotSerialized = _snapshotFileHandler.ReadSnapshot(snapshotFileInfo);
                       
            CompareSnapshots(actualSnapshotSerialized, savedSnapshotSerialized,
                snapshotFileInfo, matchOptions);
        }

        private void CompareSnapshots(
            string actualSnapshotSerialized,
            string savedSnapshotSerialized,
            ISnapshotFileInfo snapshotFileInfo,
            Func<MatchOptions, MatchOptions> matchOptions)
        {
            if (savedSnapshotSerialized == null)
            {
                _snapshotFileHandler
                    .SaveNewSnapshot(snapshotFileInfo, actualSnapshotSerialized);

                return;
            }

            try
            {
                _snapshotComparer.CompareSnapshots(
                    savedSnapshotSerialized, actualSnapshotSerialized, matchOptions);
            }
            catch (Exception)
            {
                _snapshotFileHandler
                    .SaveMismatchSnapshot(snapshotFileInfo, actualSnapshotSerialized);

                throw;
            }
        }
    }
}
