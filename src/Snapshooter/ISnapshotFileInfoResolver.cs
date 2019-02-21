using Snapshooter.Core;

namespace Snapshooter
{
    public interface ISnapshotFullNameResolver
    {
        SnapshotFullName ResolveSnapshotFullName();
        SnapshotFullName ResolveSnapshotFullName(string snapshotName);
        SnapshotFullName ResolveSnapshotFullName(string snapshotName, string nameExtension);
    }
}