using Newtonsoft.Json.Linq;

namespace Snapshooter.Core;

/// <summary>
/// This field match operator can exclude some specific
/// fields from a snapshot. The fields will completely be
/// removed from the snapshot. These excluded fields will
/// not appear in the snapshot then.
/// </summary>
public class ExcludeMatchOperator : FieldMatchOperator
{
    private readonly string _fieldsPath;

    /// <summary>
    /// Creates a new instance of the <see cref="ExcludeMatchOperator"/>
    /// </summary>
    /// <param name="fieldsPath">The path of the field to exclude.</param>
    public ExcludeMatchOperator(string fieldsPath)
    {
        _fieldsPath = fieldsPath;
    }

    /// <inheritdoc/>
    public override bool HasFormatAction() => true;

    /// <inheritdoc/>
    public override void FormatFields(JToken snapshotData)
    {
        FieldOption fieldOption = new FieldOption(snapshotData);

        foreach (JToken fieldToken in fieldOption.FindFieldTokens(_fieldsPath))
        {
            FormatField(fieldToken);
        }
    }

    /// <inheritdoc/>
    public override FieldOption ExecuteMatch(
        JToken snapshotData,
        JToken expectedSnapshotData)
    {
        return new FieldOption(snapshotData);
    }

    private static void FormatField(JToken field)
    {
        if (field.Parent is JArray array)
        {
            array.Remove(field);
        }
        else if (field.Parent is { } parent)
        {
            parent.Remove();
        }
    }
}

