using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Extensions;
using Xunit;

namespace Snapshooter.Xunit
{
    /// <summary>
    /// A xunit snapshot full name reader is responsible to get the information  
    /// for the snapshot file from a xunit test.
    /// </summary>
    public class XunitSnapshotFullNameReader : ISnapshotFullNameReader
    {
        /// <summary>
        /// Evaluates the snapshot full name information.
        /// </summary>
        /// <returns>The full name of the snapshot.</returns>
        public SnapshotFullName ReadSnapshotFullName()
        {
            SnapshotFullName snapshotFullName = null;
            StackFrame[] stackFrames = new StackTrace(true).GetFrames();
            foreach (StackFrame stackFrame in stackFrames)
            {
                MethodBase method = stackFrame.GetMethod();
                if (IsXunitTestMethod(method))
                {
                    snapshotFullName = new SnapshotFullName(
                        method.ToName(),
                        Path.GetDirectoryName(stackFrame.GetFileName()));

                    break;
                }

                MethodBase asyncMethod = EvaluateAsynchronMethodBase(method);
                if (IsXunitTestMethod(asyncMethod))
                {
                    snapshotFullName = new SnapshotFullName(
                        asyncMethod.ToName(),
                        Path.GetDirectoryName(stackFrame.GetFileName()));

                    break;
                }
            }

            if (snapshotFullName == null)
            {
                throw new SnapshotTestException(
                    "The snapshot full name could not be evaluated. " +
                    "This error can occur, if you use the snapshot match " +
                    "within a async test helper child method. To solve this issue, " +
                    "use the Snapshot.FullName directly in the unit test to " +
                    "get the snapshot name, then reach this name to your " +
                    "Snapshot.Match method.");
            }

            if (string.IsNullOrEmpty(snapshotFullName.FolderPath))
            {
                snapshotFullName = TryResolveNameInLiveUnitTestingSession(snapshotFullName.Filename);

                if (string.IsNullOrEmpty(snapshotFullName.FolderPath))
                {
                    throw new SnapshotTestException("Could not resove directory for SnapShot");
                }
            }

            return snapshotFullName;
        }


        private SnapshotFullName TryResolveNameInLiveUnitTestingSession(string testname)
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

        private static bool IsXunitTestMethod(MemberInfo method)
        {
            bool isFactTest = IsFactTestMethod(method);
            bool isTheoryTest = IsTheoryTestMethod(method);

            return isFactTest || isTheoryTest;
        }

        private static bool IsFactTestMethod(MemberInfo method)
        {
            return method?.GetCustomAttributes(typeof(FactAttribute)).Any() ?? false;
        }

        private static bool IsTheoryTestMethod(MemberInfo method)
        {
            return method?.GetCustomAttributes(typeof(TheoryAttribute)).Any() ?? false;
        }

        private static MethodBase EvaluateAsynchronMethodBase(MemberInfo method)
        {
            Type methodDeclaringType = method?.DeclaringType;
            Type classDeclaringType = methodDeclaringType?.DeclaringType;

            MethodInfo actualMethodInfo = null;
            if (classDeclaringType != null)
            {
                IEnumerable<MethodInfo> selectedMethodInfos =
                from methodInfo in classDeclaringType.GetMethods()
                let stateMachineAttribute = methodInfo
                    .GetCustomAttribute<AsyncStateMachineAttribute>()
                where stateMachineAttribute != null &&
                    stateMachineAttribute.StateMachineType == methodDeclaringType
                select methodInfo;

                actualMethodInfo = selectedMethodInfos.SingleOrDefault();
            }

            return actualMethodInfo;

        }
    }
}
