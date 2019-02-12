using System;

namespace Snapshooter
{
    public interface ISnapshotAssert
    {
        void AssertSnapshot(object currentResult,
                            string snapshotFileName,
							SnapshotNameExtension snapshotNameExtension,
                            Func<MatchOptions, MatchOptions> matchOptions);
    }
}