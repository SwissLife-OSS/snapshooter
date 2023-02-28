using System.IO;

namespace Snapshooter.Xunit.Tests.Helpers
{
    internal static class SnapshotDefaultNameResolver
    {
        public static string ResolveSnapshotDefaultName()
        {
            SnapshotFullName snapshotFullName =
                ResolveSnapshotDefaultFullName();

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            return snapshotFileName;
        }

        public static SnapshotFullName ResolveSnapshotDefaultFullName()
        {
            var snapshotFullNameResolver =
                new SnapshotFullNameResolver(
                    new XunitSnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName();

            return snapshotFullName;
        }
    }
}
