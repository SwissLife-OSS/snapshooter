using Newtonsoft.Json.Linq;

#nullable enable

namespace Snapshooter.Core;

public abstract class FieldMatchOperator
{
    public abstract bool HasFormatAction();

    public abstract void FormatFields(JToken snapshotData);

    public abstract FieldOption ExecuteMatch(
        JToken snapshotData, JToken expectedSnapshotData);
    
}

