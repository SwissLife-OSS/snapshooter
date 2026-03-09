
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Extensions;
using TUnit.Core;

namespace Snapshooter.TUnit
{
    /// <summary>
    /// A TUnit snapshot full name reader is responsible to get the information  
    /// for the snapshot file from a TUnit test.
    /// </summary>
    public class TUnitSnapshotFullNameReader : ISnapshotFullNameReader
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
                if (IsTUnitTestMethod(method))
                {
                    snapshotFullName = new SnapshotFullName(
                        GetCurrentSnapshotName(),
                        stackFrame.GetFileName().GetDirectoryName());

                    break;
                }

                MethodBase asyncMethod = EvaluateAsynchronousMethodBase(method);
                if (IsTUnitTestMethod(asyncMethod))
                {
                    snapshotFullName = new SnapshotFullName(
                        GetCurrentSnapshotName(),
                        stackFrame.GetFileName().GetDirectoryName());

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

            snapshotFullName = LiveUnitTestingDirectoryResolver
                                    .CheckForSession(snapshotFullName);

            return snapshotFullName;
        }

        private static bool IsTUnitTestMethod(MemberInfo method)
        {
            return method?.GetCustomAttributes(typeof(TestAttribute)).Any() ?? false;
        }

        private static MethodBase EvaluateAsynchronousMethodBase(MemberInfo method)
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

        private static string GetCurrentSnapshotName()
        {
            TestContext currentTestContext = TestContext.Current!;

            var typeName = currentTestContext.Metadata.TestDetails.ClassType.Name;
            var methodName = currentTestContext.Metadata.TestDetails.TestName;
            var parameters = SnapshotNameExtension.Create(currentTestContext.Metadata.TestDetails.TestMethodArguments).ToParamsString();

            return $"{typeName}.{methodName}{parameters}";
        }
    }
}
