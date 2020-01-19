using System;

namespace Snapshooter.Core
{
    /// <summary>
    /// The snapshot comparer is responsible to compare the actual snapshot with the
    /// existing one and also include the field match options checks.
    /// </summary>
    public interface ISnapshotComparer
    {
        /// <summary>
        /// Compares the current snapshot with the expected snapshot and applies 
        /// the compare rules of the compare actions.
        /// </summary>
        /// <param name="matchOptions">
        /// The compare actions, which will be used for special comparison.
        /// </param>
        /// <param name="expectedSnapshot">
        /// The original snapshot of the current result.
        /// </param>
        /// <param name="actualSnapshot">
        /// The actual (modifiable) snapshot of the current result.
        /// </param>    
        void CompareSnapshots(string expectedSnapshot, string actualSnapshot, 
                              Func<MatchOptions, MatchOptions> matchOptions);
    }
}
