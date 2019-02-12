using Snapshooter.Core;

namespace Snapshooter
{
    public interface ISnapshotFileInfoResolver
    {
        ISnapshotFileInfo ResolveSnapshotFileInfo();
        ISnapshotFileInfo ResolveSnapshotFileInfo(string snapshotName);
        ISnapshotFileInfo ResolveSnapshotFileInfo(string snapshotName, string nameExtension);
    }
}