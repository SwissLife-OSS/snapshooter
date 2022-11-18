using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Snapshooter.Core;

public class IncludeMatchOperator : FieldMatchOperator
{
    private readonly List<string> _fieldsPaths;

    public IncludeMatchOperator()
    {
        _fieldsPaths = new List<string>();
    }

    public override bool HasFormatAction() => true;

    public void AddFieldPath(string fieldPath)
    {
        if(!_fieldsPaths.Contains(fieldPath))
        {
            _fieldsPaths.Add(fieldPath);
        }
    }

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

