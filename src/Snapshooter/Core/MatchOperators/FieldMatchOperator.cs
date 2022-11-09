using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

#nullable enable

namespace Snapshooter.Core;

public abstract class FieldMatchOperator
{
    public abstract bool HasFormatAction();
    public abstract IEnumerable<JToken> GetFieldTokens(JToken snapshotData);
    public abstract FieldOption ExecuteMatch(JToken snapshotData, JToken expectedSnapshotData);
    public abstract JToken FormatField(JToken snapshotData);
}

