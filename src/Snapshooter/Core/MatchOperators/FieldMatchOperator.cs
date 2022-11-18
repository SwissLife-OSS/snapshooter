using Newtonsoft.Json.Linq;

#nullable enable

namespace Snapshooter.Core;

/// <summary>
/// Base class of the match operators.
/// </summary>
public abstract class FieldMatchOperator
{
    /// <summary>
    /// Defines if the field match operator will format
    /// the snapshot.
    /// </summary>
    /// <returns>True if the snapshot will be formatted, otherwise False</returns>
    public abstract bool HasFormatAction();

    /// <summary>
    /// Formats the specified fields of the snapshot.
    /// </summary>
    /// <param name="snapshotData">The entire snapshot.</param>
    public abstract void FormatFields(JToken snapshotData);

    /// <summary>
    /// Compares the specified match fields of the
    /// current snapshot with the original snapshot.
    /// </summary>
    /// <param name="snapshotData">The current snapshot.</param>
    /// <param name="expectedSnapshotData">The original snapshot.</param>
    /// <returns></returns>
    public abstract FieldOption ExecuteMatch(
        JToken snapshotData, JToken expectedSnapshotData);
    
}

