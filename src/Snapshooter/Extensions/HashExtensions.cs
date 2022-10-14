using System;
using System.Text;
using System.Security.Cryptography;

namespace Snapshooter.Extensions
{
    /// <summary>
    /// The class <see cref="HashExtensions"/> has function
    /// to create hashes out of string values.
    /// </summary>
    public static class HashExtensions
    {
        /// <summary>
        /// Creates a SHA256 hash out of the string value.
        /// </summary>
        /// <param name="value">The string value to hash.</param>
        /// <returns>The SHA256 hash.</returns>
        public static string ToHashSHA256(this string value)
        {
            using var sha256 = SHA256.Create();

            return Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}
