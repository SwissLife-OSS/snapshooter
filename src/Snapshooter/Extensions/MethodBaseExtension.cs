using System.Reflection;

namespace Snapshooter.Extensions
{
    /// <summary>
    /// The method base extension is used to add more functionality
    /// to the class <see cref="MethodBase"/>
    /// </summary>
    public static class MethodBaseExtension
    {
        /// <summary>
        /// Creates the name of the method with class name.
        /// </summary>
        /// <param name="methodBase">The used method name to get the name.</param>
        public static string ToName(this MethodBase methodBase)
        {
            var fullName = string.Concat(
                methodBase.ReflectedType.Name, ".", methodBase.Name);

            return fullName;
        }
    }
}
