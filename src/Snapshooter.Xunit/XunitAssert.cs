using System.Collections.Generic;
using System.Text;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using Snapshooter.Core;
using Xunit.Sdk;
using x = Xunit;

namespace Snapshooter.Xunit;

/// <summary>
/// The XunitAssert instance compares two strings with the XUnit assert utility.
/// </summary>
public class XunitAssert : IAssert
{
    /// <summary>
    /// Asserts the expected snapshot and the actual snapshot 
    /// with the XUnit assert utility.
    /// </summary>
    /// <param name="expectedSnapshot">The expected snapshot.</param>
    /// <param name="actualSnapshot">The actual snapshot.</param>
    public void Assert(string expectedSnapshot, string actualSnapshot)
    {
        try
        {
            x.Assert.Equal(expectedSnapshot, actualSnapshot);
        }
        catch(EqualException ex)
        {
            string snapshotDiff = GetSnapshotsDiff(expectedSnapshot, actualSnapshot);

            snapshotDiff = snapshotDiff + "\r\n" + "New Snapshot: " + "file:///C:/Work/Github-OSS/snapshooter/test/Snapshooter.Xunit.Tests/__snapshots__/__mismatch__/SnapshotTests.Match_FactMatchSingleSnapshot_OneFieldNotEqual.snap"; 

            throw new XunitException(snapshotDiff)
            {
                HelpLink = "helplink"
                //UserMessage = "dfe"
            };
        }
    }

    private static string GetSnapshotsDiff(
        string expectedSnapshot,
        string actualSnapshot)
    {
        DiffPaneModel diff = InlineDiffBuilder
            .Diff(expectedSnapshot, actualSnapshot);

        var output = new StringBuilder();

        if (diff.HasDifferences)
        {
        
            output.AppendLine("Snapshot mismatch:");

            foreach (DiffPiece line in diff.Lines)
            {
                switch (line.Type)
                {
                    case ChangeType.Inserted:
                        output.Append("+++ ");
                        break;

                    case ChangeType.Deleted:
                        output.Append("--- ");
                        break;

                    default:
                        output.Append("    ");
                        break;
                }

                output.AppendLine(line.Text);
            }            
        }

        return output.ToString();
    }
}
