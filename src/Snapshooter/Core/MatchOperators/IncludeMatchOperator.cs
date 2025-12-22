using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Snapshooter.Core;

/// <summary>
/// This field match operator can include some specific
/// fields of a snapshot. Only the specified fields will
/// be included within the snapshot. All other fields will
/// be removed/excluded from the snapshot.
/// </summary>
public class IncludeMatchOperator : FieldMatchOperator
{
    private readonly List<string> _fieldsPaths;

    /// <summary>
    /// Creates a new instance of the <see cref="IncludeMatchOperator"/>
    /// </summary>
    public IncludeMatchOperator()
    {
        _fieldsPaths = new List<string>();
    }

    /// <inheritdoc/>
    public override bool HasFormatAction() => true;

    /// <summary>
    /// Adds a path of a field to include.
    /// </summary>
    /// <param name="fieldPath">The path of a field to include.</param>
    public void AddFieldPath(string fieldPath)
    {
        if(!_fieldsPaths.Contains(fieldPath))
        {
            _fieldsPaths.Add(fieldPath);
        }
    }

    /// <inheritdoc/>
    public override void FormatFields(JToken snapshotData)
    {
        FieldOption fieldOption = new FieldOption(snapshotData);

        List<JToken> includedFields = new List<JToken>();

        foreach(string fieldPath in _fieldsPaths)
        {
            List<JToken> fieldsToInclude = fieldOption
                .FindFieldTokens(fieldPath)
                .ToList();

            includedFields.AddRange(fieldsToInclude);
        }
            
        FilterIncludedFieldsOnly(snapshotData, includedFields);
    }

    /// <inheritdoc/>
    public override FieldOption ExecuteMatch(
        JToken snapshotData,
        JToken expectedSnapshotData)
    {
        return new FieldOption(snapshotData);
    }

    private static void FilterIncludedFieldsOnly(
        JToken snapshotField,
        List<JToken> includedFields)
    {
        foreach (JToken child in snapshotField.Children().ToList())
        {
            if (child.HasValues)
            {
                if (!includedFields.Any(field =>
                    child.Path.StartsWith(field.Path) ||
                    field.Path.StartsWith(child.Path)))
                {
                    RemoveField(child);

                    continue;
                }

                FilterIncludedFieldsOnly(child, includedFields);
            }
        }
    }

    private static void RemoveField(JToken child)
    {
        if (child is JProperty property)
        {
            property.Remove();
        }
        else if (child.Parent is JArray array)
        {
            array.Remove(child);
        }
        else if (child is JValue value)
        {
            value.Parent?.Remove();
        }
    }
}

