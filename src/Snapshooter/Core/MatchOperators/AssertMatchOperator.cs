using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json.Linq;
using Snapshooter.Exceptions;
using Snapshooter.Extensions;

#nullable enable

namespace Snapshooter.Core
{
    public class AssertMatchOperator : FieldMatchOperator
    {        
        private readonly Action<FieldOption> _fieldOption;

        public AssertMatchOperator(Action<FieldOption> fieldOption)
        {
            _fieldOption = fieldOption;
        }

        public override bool HasFormatAction() => false;

        public override JToken FormatField(JToken field)
        {
            return field;
        }

        public override IEnumerable<JToken> GetFieldTokens(JToken snapshotData)
        {
            return Array.Empty<JToken>();
        }

        public override FieldOption GetFieldOption(JToken snapshotData)
        {
            return new FieldOption(snapshotData);
        }

        public override FieldOption ExecuteMatch(
            JToken snapshotData,
            JToken expectedSnapshotData)
        {
            FieldOption fieldOption = new FieldOption(snapshotData);

            _fieldOption(fieldOption);

            return fieldOption;
        }
    }
}
