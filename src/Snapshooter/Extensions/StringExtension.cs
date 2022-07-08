using System;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
# nullable enable

namespace Snapshooter.Extensions
{
    /// <summary>
    /// Some string extensions to support the snapshot testing.
    /// </summary>
    public static class StringExtension
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
    }
}
