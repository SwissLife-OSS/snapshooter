using System;
using System.Linq;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace Snapshooter
{
    /// <summary>
    /// Class for generating a diff between two strings.
    /// </summary>
    public static class DiffGenerator
    {
        /// <summary>
        /// Generate a displayable diff between two strings.
        /// </summary>
        /// <param name="expectedSnapshot">The expected snapshot used in the assert.</param>
        /// <param name="actualSnapshot">The actual snapshot used in the assert.</param>
        /// <returns>A new string showing the added/removed lines between the two snapshots.</returns>
        public static string GenerateDiff(string expectedSnapshot, string actualSnapshot)
        {
            DiffPaneModel diff = InlineDiffBuilder.Diff(expectedSnapshot, actualSnapshot);
            string displayableDiff = string.Join(
                Environment.NewLine,
                diff.Lines.Select(line => line.Type switch
                    {
                        ChangeType.Inserted => $"+ {line.Text}",
                        ChangeType.Deleted => $"- {line.Text}",
                        _ => line.Text
                    }));
            return displayableDiff;
        }
    }
}
