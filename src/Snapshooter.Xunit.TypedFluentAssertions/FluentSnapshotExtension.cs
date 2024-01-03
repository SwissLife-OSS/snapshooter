using FluentAssertions;
using System;

namespace Snapshooter.Xunit;

public static class FluentSnapshotExtension
{
    /// <summary>
    /// Creates a json snapshot of the given object and compares it with the
    /// already existing snapshot of the test.
    /// If no snapshot exists, a new snapshot will be created from the current result
    /// and saved under a certain file path, which will shown within the test message.
    /// </summary>
    /// <param name="currentResult">The object to match.</param>
    /// <param name="matchOptions">
    /// Additional compare actions, which can be applied during the snapshot comparison
    /// </param>
    public static void MatchSnapshot<TSubject>(
        this TypedAssertions<TSubject> currentResult,
        Func<MatchOptions<TSubject>, MatchOptions<TSubject>>? matchOptions = null)
    {
        var cleanedObject = currentResult.RemoveUnwantedWrappers();

        Func<MatchOptions, MatchOptions>? chainedMatchOptions =
            matchOptions != null ? m => matchOptions(new MatchOptions<TSubject>(m)) : null;

        Snapshot.Match(
            cleanedObject,
            chainedMatchOptions);
    }
}
