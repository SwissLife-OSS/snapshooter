using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
            catch
            {
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

            #if NETCOREAPP3_1_OR_GREATER
                Span<byte> buffer = new Span<byte>(new byte[base64String.Length]);
                return Convert.TryFromBase64String(base64String, buffer, out int bytesParsed);
            #else
                return IsBase64String(base64String);
            #endif
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

        /// <summary>
        /// Gets the directory path of a filename. The Path.GetDirectoryName
        /// returns an empty string if it runs on wsl ubuntu if a windows file path
        /// is given. This method has a fallback if this is the case. If
        /// Path.GetDirectoryName returns an empty string, then this method tries
        /// to get the folder path manually.
        /// and thus with c:\\dir1\\dir\\filename.cs (as example)
        /// </summary>
        /// <param name="filenameFullPath">The filenameFullPath string.</param>
        /// <returns>The folder path.</returns>
        public static string GetDirectoryName(this string filenameFullPath)
        {
            var folderPath = Path.GetDirectoryName(filenameFullPath);

            if (string.IsNullOrEmpty(folderPath))
            {
                folderPath = filenameFullPath.RemoveFilename();
            }

            return folderPath;
        }

        /// <summary>
        /// Removes the last part of a file path after the folder separator
        /// if any. This is the filename and its extension, if any.
        /// This is meant as a fallback for the Path.GetDirectoryName when executed
        /// in WSL but with a directory from xUnit (run in Windows subsystem)
        /// and thus with c:\\dir1\\dir\\filename.cs (as example)
        /// </summary>
        /// <param name="filenameFullPath">The filenameFullPath string.</param>
        /// <returns>The text string without the filename.</returns>
        public static string RemoveFilename(this string filenameFullPath)
        {
            int index = filenameFullPath.LastIndexOf('\\');

            if (index < 0)
            {
                index = filenameFullPath.LastIndexOf('/');

                if(index < 0)
                {
                    return string.Empty;
                }
            }

            return filenameFullPath.Remove(index);
        }
    }
}
