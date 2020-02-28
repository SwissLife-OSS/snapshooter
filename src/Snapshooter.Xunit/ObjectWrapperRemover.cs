using System;
using System.Reflection;

namespace Snapshooter.Xunit
{
    public static class ObjectWrapperRemover
    {
        public static object RemoveUnwantedWrappers(this object objectToRemoveWrappers)
        {
            object cleanedObject = objectToRemoveWrappers.RemoveFluentAssertionWrapper();

            return cleanedObject;
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

        // TODO: Remove
        private static object RemoveFluentAssertionWrapper2(this object objectToRemoveWrappers)
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
