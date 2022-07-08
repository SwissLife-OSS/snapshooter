using System;

#nullable enable

namespace Snapshooter.Core
{
    /// <summary>
    /// Formats the snapshot output according the match options. 
    /// Each match option has a specific format action (or no format action) and
    /// the snapshot fields will be formatted according to this format action of the
    /// match option.
    /// </summary>
    public interface ISnapshotFormatter
    {
        /// <summary>
        /// Formats the snapshot output according the match options. 
        /// Each match option has a specific format action (or no format action) and
        /// the snapshot fields will be formatted according to this format action of the
        /// match option.
        /// </summary>
        /// <param name="snapshot">
        /// The snapshot to format.
        /// </param>
        /// <param name="matchOptions">
        /// The match options, which contain the format actions.
        /// </param>
        /// <returns>The formatted snapshot.</returns>
        string FormatSnapshot(
            string snapshot,
            Func<MatchOptions, MatchOptions>? matchOptions = null);
    }
}