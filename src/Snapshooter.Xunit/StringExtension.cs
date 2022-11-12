using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Snapshooter.Xunit;

/// <summary>
/// Some string extensions to support the snapshot testing.
/// </summary>
internal static class StringExtension
{
    /// <summary>
    /// Removes the last part of a file path after the folder separator
    /// if any. This is the filename and its extension, if any.
    /// This is meant as a fallback for the Path.GetDirectoryName when executed
    /// in WSL but with a directory from xUnit (run in Windows subsystem)
    /// and thus with c:\\dir1\\dir\\filename.cs (as example)
    /// </summary>
    /// <param name="filenameFullPath">The filenameFullPath string.</param>
    /// <returns>The text string without the filename.</returns>
    public static string GetDirectoryName(this string filenameFullPath)
    {
        string[] directorySections = filenameFullPath.Split('\\');
        IEnumerable<string>? minusLastSection = directorySections.Take(directorySections.Length - 1);
        string directoryName = string.Join("\\", minusLastSection);

        return directoryName;
    }
}
