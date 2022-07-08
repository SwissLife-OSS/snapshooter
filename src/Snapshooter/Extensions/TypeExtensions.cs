using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable

namespace Snapshooter.Extensions
{
    /// <summary>
    /// Some extensions for Type, to support snapshot testing.
    /// </summary>
    public static class TypeExtensions
    {
        static Dictionary<Type, string> _typeAlias = new Dictionary<Type, string>
        {
            { typeof(bool), "Boolean" },
            { typeof(byte), "Byte" },
            { typeof(sbyte), "SByte" },
            { typeof(char), "Char" },
            { typeof(decimal), "Decimal" },
            { typeof(double), "Double" },
            { typeof(float), "Float" },
            { typeof(int), "Integer" },
            { typeof(uint), "UInteger" },
            { typeof(long), "Long" },
            { typeof(ulong), "ULong" },
            { typeof(short), "Short" },
            { typeof(ushort), "UShort" },
            { typeof(object), "Object" },
            { typeof(string), "String" },
        };

        public static string GetAliasName(this Type type)
        {
            if (_typeAlias.TryGetValue(type, out string alias))
            {
                return alias;
            }

            Type nullbase = Nullable.GetUnderlyingType(type);
            if (nullbase != null)
            {
                return GetAliasName(nullbase) + "?";
            }

            if (type.IsArray)
            {
                return type.GetElementType().GetAliasName() + "[]";
            }

            string friendlyName = type.Name;
            if (type.IsGenericType)
            {
                int backtick = friendlyName.IndexOf('`');
                if (backtick > 0)
                {
                    friendlyName = friendlyName.Remove(backtick);
                }

                friendlyName = new StringBuilder(friendlyName)
                    .Append("<")
                    .Append(string
                        .Join(", ", type.GetGenericArguments()
                        .Select(argument => argument.GetAliasName())))
                    .Append(">")
                    .ToString();
            }

            return friendlyName;
        }

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

        /// <summary>
        /// Checks if the type T is nullable or not.
        /// </summary>
        /// <typeparam name="T">
        /// The type to find out if nullable or not.
        /// </typeparam>
        /// <returns>True if nullable, false if not.</returns>
        public static bool IsNullable(this Type type)
        {
            if (!type.IsValueType)
            {
                return true;
            }

            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true;
            }
                
            return false;
        }
    }
}
