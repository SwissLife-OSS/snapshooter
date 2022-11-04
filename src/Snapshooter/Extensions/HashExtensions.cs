using System;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Globalization;

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
            var json = JsonConvert
                .SerializeObject(jtoken, Formatting.None, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                })
                .Trim('\"');
                        
            var hash = json.ToSHA256();

            return hash;
        }

        /// <summary>
        /// Creates a SHA256 hash out of the string value.
        /// </summary>
        /// <param name="value">The string value to hash.</param>
        /// <returns>The SHA256 hash.</returns>
        public static string ToSHA256(this string value)
        {
            using var sha256 = SHA256.Create();

            return Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}
