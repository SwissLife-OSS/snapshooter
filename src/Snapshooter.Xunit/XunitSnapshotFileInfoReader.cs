using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Snapshooter.Core;
using Snapshooter.Extensions;
using Xunit;

namespace Snapshooter.Xunit
{
	/// <summary>
    /// A xunit snapshot file info reader is responsible to get the information  
    /// for the snapshot file from a xunit test.
    /// </summary>
    public class XunitSnapshotFileInfoReader : ISnapshotFileInfoReader
    {
		/// <summary>
        /// Evaluates the snapshot file infos.
        /// </summary>
        /// <returns>The file infos for the snapshot.</returns>
        public SnapshotFileInfo ReadSnapshotFileInfo()
        {
            SnapshotFileInfo snapshotFileInfo = null;
            StackFrame[] stackFrames = new StackTrace(true).GetFrames();
            foreach (StackFrame stackFrame in stackFrames)
            {
                MethodBase method = stackFrame.GetMethod();
                if (IsXunitTestMethod(method))
                {
                    snapshotFileInfo = new SnapshotFileInfo()
                    {
                        FolderPath = Path.GetDirectoryName(stackFrame.GetFileName()),
                        Filename = method.ToName()
                    };
                    
                    break;                    
                }

                MethodBase asyncMethod = GetAsyncMethodBase(method);
                if (IsXunitTestMethod(asyncMethod))
                {
                    snapshotFileInfo = new SnapshotFileInfo()
                    {
                        FolderPath = Path.GetDirectoryName(stackFrame.GetFileName()),
                        Filename = asyncMethod.ToName()
                    };
                                      
                    break;
                }
            }
            
            return snapshotFileInfo;
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

        private static MethodBase GetAsyncMethodBase(MemberInfo method)
        {
            Type generatedType = method?.DeclaringType;
            Type originalClass = generatedType?.DeclaringType;
            if (originalClass == null)
            {
                return null;
            }

            IEnumerable<MethodInfo> matchingMethods =
                from methodInfo in originalClass.GetMethods()
                let attr = methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>()
                where attr != null && attr.StateMachineType == generatedType
                select methodInfo;

            return matchingMethods.SingleOrDefault();
        }       
    }
}
