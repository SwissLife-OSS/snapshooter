using System;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace Snapshooter.Extensions
{
    /// <summary>
    /// The class <see cref="HashExtensions"/> has function
    /// to create hashes out of string values.
    /// </summary>
    public static class HashExtensions
    {
        /// <summary>
        /// Creates a SHA256 hash out of the JToken value.
        /// </summary>
        /// <param name="jtoken">The JToken value to hash.</param>
        /// <returns>The SHA256 hash.</returns>
        public static string ToSHA256(this JToken jtoken)
        {
            var value = jtoken.ConvertToValueString();

            if (value.IsBase64())
            {
                byte[] binaryValue = Convert.FromBase64String(value);

                return binaryValue.ToSHA256();
            }

            var hash = value.ToSHA256();

            return hash;
        }
        
        /// <summary>
        /// Creates a SHA256 hash out of the string value.
        /// </summary>
        /// <param name="value">The string value to hash.</param>
        /// <returns>The SHA256 hash.</returns>
        public static string ToSHA256(this string value)
        {
            return ToSHA256(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Creates a SHA256 hash out of the byte array value.
        /// </summary>
        /// <param name="value">The string value to hash.</param>
        /// <returns>The SHA256 hash.</returns>
        public static string ToSHA256(this byte[] value)
        {
            using var sha256 = SHA256.Create();

            return Convert.ToBase64String(
                sha256.ComputeHash(value));
        }
    }
}
