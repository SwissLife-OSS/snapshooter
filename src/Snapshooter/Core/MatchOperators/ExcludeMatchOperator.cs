using Newtonsoft.Json.Linq;

namespace Snapshooter.Core;

public class ExcludeMatchOperator : FieldMatchOperator
{
    private readonly string _fieldsPath;

    public ExcludeMatchOperator(string fieldsPath)
    {
        _fieldsPath = fieldsPath;
    }

    public override bool HasFormatAction() => true;

    public override void FormatFields(JToken snapshotData)
    {
        FieldOption fieldOption = new FieldOption(snapshotData);

        foreach (JToken fieldToken in fieldOption.FindFieldTokens(_fieldsPath))
        {
            FormatField(fieldToken);
        }
    }

    public override FieldOption ExecuteMatch(
        JToken snapshotData,
        JToken expectedSnapshotData)
    {
        return new FieldOption(snapshotData);
    }

    private JToken FormatField(JToken field)
    {
        if (field.Parent is { } parent)
        {
            parent.Remove();
        }

        return field;
    }
}

