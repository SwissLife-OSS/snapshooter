using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
# nullable enable

namespace Snapshooter.Extensions
{
    /// <summary>
    /// Some string extensions to support the snapshot testing.
    /// </summary>
    internal static class StringExtension
    {
        /// <summary>
        /// Ensures that the given string ends with a line ending '\n'.
        /// </summary>
        /// <param name="text">The text string.</param>
        /// <returns>The text string with line ending.</returns>
        public static string EnsureLineEnding(this string text)
        {
            if (text.Last() == '\n')
            {
                return text;
            }

            return text + "\n";
        }

        /// <summary>
        /// Normalizes the line endings of a text string. 
        /// (Removes the carriage returns)
        /// </summary>
        /// <param name="text">The text string.</param>
        /// <returns>The normalized line ending string.</returns>
        public static string NormalizeLineEndings(this string text)
        {
            string normalisedText = text
                .Replace("\r\n", "\n")
                .Replace("\n\r", "\n")
                .Replace("\r", "\n");
            
            return normalisedText;
        }

        /// <summary>
        /// Verifies if the given string is in the correct json format.
        /// Returns true if the string is in a valid json format, otherwise false.
        /// </summary>
        /// <param name="input">The input string to verify.</param>
        /// <param name="jsonLoadSettings">The optional json load settings</param>
        /// <returns>
        /// True if the string is in a valid json format, otherwise false.
        /// </returns>
        public static bool IsValidJsonFormat(
            this string input,
            JsonLoadSettings? jsonLoadSettings = null)
        {
            try
            {
                JToken.Parse(input, jsonLoadSettings);
                return true;
            }
            catch (JsonReaderException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// Verifies if the given string is in correct base64 format.
        /// Returns true if the string is in a valid base64 format, otherwise false.
        /// </summary>
        /// <param name="base64String">The input string to verify.</param>
        /// <returns>
        /// True if the string is in a valid base64 format, otherwise false.
        /// </returns>
        public static bool IsBase64String(this string base64String)
        {
            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }            
        }

        /// <summary>
        /// Verifies if the given string is in correct base64 format.
        /// Returns true if the string is in a valid base64 format, otherwise false.
        /// </summary>
        /// <param name="base64String">The input string to verify.</param>
        /// <returns>
        /// True if the string is in a valid base64 format, otherwise false.
        /// </returns>
        public static bool IsBase64(this string base64String)
        {
            if (string.IsNullOrEmpty(base64String) ||
                base64String.Length % 4 != 0 ||
                base64String.Contains(" ") ||
                base64String.Contains("\t") ||
                base64String.Contains("\r") ||
                base64String.Contains("\n") ||
                base64String.Contains("null"))
            {
                return false;
            }

            Span<byte> buffer = new Span<byte>(new byte[base64String.Length]);
            return Convert.TryFromBase64String(base64String, buffer, out int bytesParsed);
        }

        /// <summary>
        /// Checks if the fields path starts with '**.' and if yes then it
        /// returns true and the fieldName is set.
        /// </summary>
        /// <param name="fieldsPath">The fields json path.</param>
        /// <param name="fieldName">The fields name.</param>
        /// <returns>True if the fields path starts with '**.' .</returns>
        public static bool TryFindFieldsByName(this string fieldsPath, out string fieldName)
        {
            if (fieldsPath.StartsWith(
                Wellknown.FindByNamePrefix,
                ignoreCase: true,
                CultureInfo.InvariantCulture))
            {
                fieldName = fieldsPath.Remove(0, 3);
                return true;
            }

            fieldName = fieldsPath;
            return false;
        }
    }
}
