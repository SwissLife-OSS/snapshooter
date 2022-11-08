using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snapshooter.Xunit.Tests.Helpers
{
    internal static class SnapshotDefaultNameResolver
    {
        public static string ResolveSnapshotDefaultName()
        {
            var snapshotFullNameResolver =
                new SnapshotFullNameResolver(
                    new XunitSnapshotFullNameReader());

            SnapshotFullName snapshotFullName =
                snapshotFullNameResolver.ResolveSnapshotFullName();

            string snapshotFileName = Path.Combine(
                snapshotFullName.FolderPath,
                FileNames.SnapshotFolderName,
                snapshotFullName.Filename);

            return snapshotFileName;
        }
    }
}
