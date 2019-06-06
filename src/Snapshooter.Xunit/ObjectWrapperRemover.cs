using System;
using System.Reflection;

namespace Snapshooter.Xunit
{
    public static class ObjectWrapperRemover
    {
        public static object RemoveUnwantedWrappers(this object objectToRemoveWrappers)
        {
            objectToRemoveWrappers.RemoveFluentAssertionWrapper();

            return objectToRemoveWrappers;
        }

        private static object RemoveFluentAssertionWrapper(this object objectToRemoveWrappers)
        {
            Type resultType = objectToRemoveWrappers.GetType();

            if (resultType.Namespace.Equals("FluentAssertions.Primitives")
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
