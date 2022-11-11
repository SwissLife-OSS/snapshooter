using System;
using Snapshooter.Core;
using Snapshooter.Exceptions;

#nullable enable

namespace Snapshooter;

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
    private readonly ISnapshotFormatter _snapshotFormatter;

    /// <summary>
    /// Initializes a new instance of the <see cref="SnapshotAssert"/> class.
    /// </summary>
    /// <param name="snapshotSerializer">The serializer of the snapshot object.</param>
    /// <param name="snapshotFileHandler">The snapshot file handler.</param>
    /// <param name="snapshotEnvironmentCleaner">The environment cleanup utility.</param>
    /// <param name="snapshotComparer">The snaspshot text comparer.</param>
    /// <param name="snapshotFormatter">Transforms the snapshot to the right output format.</param>
    public SnapshotAssert(
        ISnapshotSerializer snapshotSerializer,
        ISnapshotFileHandler snapshotFileHandler,
        ISnapshotEnvironmentCleaner snapshotEnvironmentCleaner,
        ISnapshotComparer snapshotComparer,
        ISnapshotFormatter snapshotFormatter)
    {
        _snapshotSerializer = snapshotSerializer;
        _snapshotFileHandler = snapshotFileHandler;
        _snapshotEnvironmentCleaner = snapshotEnvironmentCleaner;
        _snapshotComparer = snapshotComparer;
        _snapshotFormatter = snapshotFormatter;
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
        MatchOptions matchOptions)
    {
        if (currentResult == null)
        {
            throw new ArgumentNullException(nameof(currentResult));
        }

        if (snapshotFullName == null)
        {
            throw new ArgumentNullException(nameof(snapshotFullName));
        }

        if (matchOptions == null)
        {
            throw new ArgumentNullException(nameof(matchOptions));
        }

        _snapshotEnvironmentCleaner.Cleanup(snapshotFullName);

        string objectSnapshotSerialized = _snapshotSerializer
            .SerializeObject(currentResult);

        string actualSnapshotSerialized = _snapshotFormatter
            .FormatSnapshot(objectSnapshotSerialized, matchOptions);

        bool originalSnapshotExists = _snapshotFileHandler
            .TryReadSnapshot(snapshotFullName, out string? originalSnapshotSerialized);
                
        originalSnapshotSerialized ??= actualSnapshotSerialized;
        
        ExecuteSnapshotComparison(
            originalSnapshotExists,
            actualSnapshotSerialized,
            originalSnapshotSerialized,
            snapshotFullName,
            matchOptions);
    }        

    private void ExecuteSnapshotComparison(
        bool originalSnapshotExists,
        string actualSnapshotSerialized,
        string originalSnapshotSerialized,
        SnapshotFullName snapshotFullName,
        MatchOptions matchOptions)
    {
        try
        {                
            CheckStrictMode(originalSnapshotExists);
            
            _snapshotComparer.CompareSnapshots(
                originalSnapshotSerialized,
                actualSnapshotSerialized,
                matchOptions);

            if (!originalSnapshotExists)
            {
                _snapshotFileHandler.SaveNewSnapshot(
                    snapshotFullName,
                    actualSnapshotSerialized);
            }
        }
        catch (Exception)
        {
            _snapshotFileHandler.SaveMismatchSnapshot(
                snapshotFullName,
                actualSnapshotSerialized);

            throw;
        }
    }

    private void CheckStrictMode(bool originalSnapshotExists)
    {
        if (!originalSnapshotExists)
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
        }
    }
}
