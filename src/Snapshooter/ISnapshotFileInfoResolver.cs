using Snapshooter.Core;

namespace Snapshooter
{
    public interface ISnapshotFileInfoResolver
    {
        SnapshotFullName ResolveSnapshotFileInfo();
        SnapshotFullName ResolveSnapshotFileInfo(string snapshotName);
        SnapshotFullName ResolveSnapshotFileInfo(string snapshotName, string nameExtension);
    }
}