using System;
using Snapshooter.Core;
using Snapshooter.Exceptions;

namespace Snapshooter
{
    /// <summary>
    /// The class <see cref="SnapshotAssert"/> can be used to compare a given object
    /// against a snapshot. If no snapshot exists, a new snapshot will be created from
    /// the current object and saved on the file system.
    /// </summary>
    public class SnapshotAssert : ISnapshotAssert
    {
        private readonly ISnapshotSerializer _snapshotSerializer;
        private readonly ISnapshotFileHandler _snapshotFileHandler;
        private readonly ISnapshotEnvironmentCleaner _snapshotEnvironmentCleaner;
        private readonly ISnapshotComparer _snapshotComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotAssert"/> class.
        /// </summary>
        /// <param name="snapshotSerializer">The serializer of the snapshot object.</param>
        /// <param name="snapshotFileHandler">The snapshot file handler.</param>
        /// <param name="snapshotEnvironmentCleaner">The environment cleanup utility.</param>
        /// <param name="snapshotComparer">The snaspshot text comparer.</param>
        public SnapshotAssert(ISnapshotSerializer snapshotSerializer,
                              ISnapshotFileHandler snapshotFileHandler,
                              ISnapshotEnvironmentCleaner snapshotEnvironmentCleaner,
                              ISnapshotComparer snapshotComparer)
        {
            _snapshotSerializer = snapshotSerializer
                ?? throw new ArgumentNullException(nameof(snapshotSerializer));
            _snapshotFileHandler = snapshotFileHandler
                ?? throw new ArgumentNullException(nameof(snapshotFileHandler));
            _snapshotEnvironmentCleaner = snapshotEnvironmentCleaner
                ?? throw new ArgumentNullException(nameof(snapshotEnvironmentCleaner));
            _snapshotComparer = snapshotComparer
                ?? throw new ArgumentNullException(nameof(snapshotComparer));
        }

        /// <summary>
        /// Compares the snapshot against the given result object. If the snapshot is
        /// new then it will be saved directly. If the snapshot is not matching with the
        /// given result object, then the given result object will be snapshot and saved
        /// in the given folder.
        /// </summary>
        /// <param name="currentResult">
        /// The object to compare.
        /// </param>
        /// <param name="snapshotFullName">
        /// The name and folder of the snapshot.
        /// </param>
        /// <param name="matchOptions">
        /// Additional match actions, which can be applied during the comparison
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

            _snapshotEnvironmentCleaner.Cleanup(snapshotFullName);

            string actualSnapshotSerialized = _snapshotSerializer.SerializeObject(currentResult);
            string savedSnapshotSerialized = _snapshotFileHandler.ReadSnapshot(snapshotFullName);

            if (savedSnapshotSerialized == null)
            {
                string value = Environment.GetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE");
                if (string.Equals(value, "on", StringComparison.Ordinal)
                    || (bool.TryParse(value, out bool b) && b))
                {
                    throw new SnapshotNotFoundException(
                        "Strict mode is enabled and no snapshot has been found " +
                        "for the current test. Create a new snapshot locally and " +
                        "rerun your tests.");
                }

                _snapshotFileHandler
                    .SaveNewSnapshot(snapshotFullName, actualSnapshotSerialized);
                return;
            }

            CompareSnapshots(actualSnapshotSerialized, savedSnapshotSerialized,
                    snapshotFullName, matchOptions);
        }

        private void CompareSnapshots(
            string actualSnapshotSerialized,
            string savedSnapshotSerialized,
            SnapshotFullName snapshotFullName,
            Func<MatchOptions, MatchOptions> matchOptions)
        {
            try

            {
                _snapshotComparer.CompareSnapshots(
                    savedSnapshotSerialized, actualSnapshotSerialized, matchOptions);
            }
            catch (Exception)
            {
                _snapshotFileHandler
                    .SaveMismatchSnapshot(snapshotFullName, actualSnapshotSerialized);

                throw;
            }
        }
    }
}
