
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Snapshooter.Core;
using Snapshooter.Exceptions;

namespace Snapshooter.NUnit
{
    /// <summary>
    /// A NUnit snapshot full name reader is responsible to get the information  
    /// for the snapshot file from a NUnit test.
    /// </summary>
    public class NUnitSnapshotFullNameReader : ISnapshotFullNameReader
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
                if (IsNUnitTestMethod(method))
                {
                    snapshotFullName = new SnapshotFullName(
                        GetCurrentSnapshotName(),
                        Path.GetDirectoryName(stackFrame.GetFileName()));

                    break;
                }

                MethodBase asyncMethod = EvaluateAsynchronMethodBase(method);
                if (IsNUnitTestMethod(asyncMethod))
                {
                    snapshotFullName = new SnapshotFullName(
                        GetCurrentSnapshotName(),
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

            snapshotFullName = LiveUnitTestingDirectoryResolver
                                    .CheckForSession(snapshotFullName);

            return snapshotFullName;
        }

        private static bool IsNUnitTestMethod(MemberInfo method)
        {
            bool isFactTest = IsTestMethod(method);
            bool isTheoryTest = IsTestCaseTestMethod(method);
            bool isTheoryDataTest = IsTestCaseSourceTestMethod(method);

            return isFactTest || isTheoryTest || isTheoryDataTest;
        }

        private static bool IsTestMethod(MemberInfo method)
        {
            return method?.GetCustomAttributes(typeof(TestAttribute))?.Any() ?? false;
        }

        private static bool IsTestCaseTestMethod(MemberInfo method)
        {
            return method?.GetCustomAttributes(typeof(TestCaseAttribute))?.Any() ?? false;
        }

        private static bool IsTestCaseSourceTestMethod(MemberInfo method)
        {
            return method?.GetCustomAttributes(typeof(TestCaseSourceAttribute))?.Any() ?? false;
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

        private static string GetCurrentSnapshotName()
        {
            TestContext currentTestContext = TestContext.CurrentContext;

            ITest test = typeof(TestContext.TestAdapter)
                .GetField("_test", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(currentTestContext.Test) as ITest;

            string snapshotName = string.Concat(
                test.TypeInfo.Name.ToString(CultureInfo.InvariantCulture), ".",
                currentTestContext.Test.MethodName.ToString(CultureInfo.InvariantCulture),
                SnapshotNameExtension.Create(currentTestContext.Test.Arguments).ToParamsString());

            return snapshotName;
        }
    }
}
