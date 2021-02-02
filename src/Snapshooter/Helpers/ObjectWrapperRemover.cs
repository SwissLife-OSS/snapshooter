using System;
using System.Reflection;

namespace Snapshooter
{
    public static class ObjectWrapperRemover
    {
        internal static object RemoveUnwantedWrappers(this object objectToRemoveWrappers)
        {
            if (objectToRemoveWrappers == null)
            {
                throw new ArgumentNullException(nameof(objectToRemoveWrappers));
            }

            return objectToRemoveWrappers.RemoveFluentAssertionWrapper();
        }

        private static object RemoveFluentAssertionWrapper(this object objectToRemoveWrappers)
        {
            Type resultType = objectToRemoveWrappers.GetType();

            if (resultType.Namespace != null
                && resultType.Name != null
                && resultType.Namespace.Equals("FluentAssertions.Primitives")
                && resultType.Name.Equals("ObjectAssertions"))
            {
                PropertyInfo prop = resultType.GetProperty("Subject");
                object actualvalue = prop.GetValue(objectToRemoveWrappers);

                return actualvalue;
            }

            return objectToRemoveWrappers;
        }
    }
}
