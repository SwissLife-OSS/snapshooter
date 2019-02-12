using System;

namespace Snapshooter.Core
{
    public interface ISnapshotComparer
    {
        void CompareSnapshots(string expectedSnapshot, string actualSnapshot, 
							  Func<MatchOptions, MatchOptions> matchOptions);
    }
}
