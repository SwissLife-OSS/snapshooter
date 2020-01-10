using System;
using System.Collections.Generic;

namespace Snapshooter.Core
{
    /// <summary>
    /// Some extensions for Type, to support snapshot testing.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns the list of inherited types.
        /// </summary>
        /// <param name="type">The current object type.</param>
        /// <returns>The list of all inherited types.</returns>
        public static IEnumerable<Type> BaseTypesAndSelf(this Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}
