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
            return snapshotData.Replace("\\r", string.Empty)
                               .Replace("\r", string.Empty);
        }
    }
}
