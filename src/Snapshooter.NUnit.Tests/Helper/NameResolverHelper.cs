using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snapshooter.NUnit.Tests.Helper
{
    public class NameResolverHelper
    {
        public static async Task<SnapshotFullName> AsyncResolveFullNameMethodInHelper()
        {
            await Task.Delay(1);

            var snapshotFullNameResolver = new NUnitSnapshotFullNameReader();

            SnapshotFullName snapshotFullName = snapshotFullNameResolver.ReadSnapshotFullName();

            await Task.Delay(1);

            return snapshotFullName;
        }
    }
}
