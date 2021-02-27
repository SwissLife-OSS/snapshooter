using System;
using System.Reflection;

namespace Snapshooter
{
    public static class ObjectWrapperRemover
    {
        public static object RemoveUnwantedWrappers(this object objectToRemoveWrappers)
        {
            if (objectToRemoveWrappers == null)
            {
                throw new ArgumentNullException(nameof(objectToRemoveWrappers));
            }

            return objectToRemoveWrappers.RemoveFluentAssertionsWrapper();
        }

        private static object RemoveFluentAssertionsWrapper(this object objectToRemoveWrappers)
        {
            Type resultType = objectToRemoveWrappers.GetType();

            if (resultType.Namespace == null || !resultType.Namespace.StartsWith("FluentAssertions."))
            {
                return objectToRemoveWrappers;
            }

            PropertyInfo prop = resultType.GetProperty("Subject");
            if (prop == null)
            {
                return objectToRemoveWrappers;
            }

            return prop.GetValue(objectToRemoveWrappers);
        }
    }
}
