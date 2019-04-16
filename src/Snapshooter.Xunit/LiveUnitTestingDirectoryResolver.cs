using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Snapshooter.Exceptions;

namespace Snapshooter.Xunit
{
    /// <summary>
    /// A class to help resolving the directory in which the test is executed when running
    /// in a Visual Studio Live Unit Testing session
    /// </summary>
    internal static class LiveUnitTestingDirectoryResolver
    {
        internal static SnapshotFullName TryResolveName(string testname)
        {
            var currentDir = Directory.GetCurrentDirectory();
            if (currentDir.ToLower().Contains("liveunittest"))
            {
                var srcPath = currentDir.Substring(0, currentDir.IndexOf(".vs") - 1);
                var filename = testname.Split('.').FirstOrDefault() + ".cs";
                var files = Directory.GetFiles(srcPath, filename, SearchOption.AllDirectories);
                if (files.Length == 1)
                {
                    return new SnapshotFullName(testname, Path.GetDirectoryName(files[0]));
                }
                else if (files.Length > 1)
                {
                    throw new SnapshotTestException(
                            $"Multiple files found with '{filename}' this can happen " +
                             "in Visual Studio Live Unit session while trying to resolve " +
                             "the directory name. " +
                             "To solve this issue either stop Live Unit Testing or " +
                             "make sure testfile names are unique. " +
                            $"Duplicate files are:{Environment.NewLine}{string.Join(Environment.NewLine, files)}");
                }
            }
            return null;
        }

        internal static SnapshotFullName CheckForSession(SnapshotFullName snapshotFullName)
        {
            if (string.IsNullOrEmpty(snapshotFullName.FolderPath))
            {
                snapshotFullName = TryResolveName(snapshotFullName.Filename);

                if (string.IsNullOrEmpty(snapshotFullName?.FolderPath))
                {
                    throw new SnapshotTestException("Could not resolve directory for SnapShot");
                }
            }
            return snapshotFullName;
        }
    }
}
