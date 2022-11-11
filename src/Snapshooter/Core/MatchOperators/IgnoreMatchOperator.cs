using System;
using Newtonsoft.Json.Linq;

#nullable enable

namespace Snapshooter.Core
{
    public class IgnoreMatchOperator<T> : FieldMatchOperator
    {
        private readonly string _fieldPath;
        private readonly Func<FieldOption, T>? _fieldOption;

        public IgnoreMatchOperator(string fieldPath)
        {
            _fieldPath = fieldPath;
        }

        [Obsolete("Use the other constructor with the field path string.")]
        public IgnoreMatchOperator(Func<FieldOption, T> fieldOption)
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

            if (_fieldOption is { })
            {
                _fieldOption(fieldOption);
            }
            else
            {
                fieldOption.FindAllFields<T>(_fieldPath);
            }

            return fieldOption;
        }
    }
}
