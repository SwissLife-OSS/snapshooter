using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Snapshooter.Extensions
{
    /// <summary>
    /// Some string extensions to support the snapshot testing.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Removes the carriage return in string.
        /// </summary>
        /// <param name="snapshotData">The snapshot data string.</param>
        /// <returns>The normalized line ending string.</returns>
        public static string NormalizeLineEndings(this string snapshotData)
        {
            string snapshotText = snapshotData
                .Replace("\\r", string.Empty)
                .Replace("\r", string.Empty);

            if (snapshotText.Last() == '\n')
            {
                return snapshotText;
            }

            return snapshotText + "\n";
        }

        /// <summary>
        /// Verifies if the given string is in the correct json format.
        /// Returns true if the string is in a valid json format, otherwise false.
        /// </summary>
        /// <param name="input">The input string to verify.</param>
        /// <returns>
        /// True if the string is in a valid json format, otherwise false.
        /// </returns>
        public static bool IsValidJsonFormat(this string input)
        {
            input = input.Trim();
            if ((input.StartsWith("{") && input.EndsWith("}")) ||
                (input.StartsWith("[") && input.EndsWith("]")) ||
                (input.StartsWith("\"") && input.EndsWith("\"")))
            {
                try
                {
                    JToken.Parse(input);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
