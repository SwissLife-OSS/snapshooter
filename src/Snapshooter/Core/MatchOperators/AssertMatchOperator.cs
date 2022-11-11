using System;
using Newtonsoft.Json.Linq;

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

        public override void FormatFields(JToken snapshotData)
        {
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
