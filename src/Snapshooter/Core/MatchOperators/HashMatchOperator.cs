using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Snapshooter.Exceptions;
using Snapshooter.Extensions;

namespace Snapshooter.Core
{
    public class HashMatchOperator : FieldMatchOperator
    {
        private readonly string _fieldsPath;

        public HashMatchOperator(string fieldsPath)
        {
            _fieldsPath = fieldsPath;
        }

        public override bool IsFormatActionSet() => true;

        public override JToken FormatField(JToken field)
        {
            var fieldValue = field
                .ToString(Formatting.None)
                .Replace("\"", string.Empty);

            var hash = fieldValue.ToHashSHA256();

            field.Replace(new JValue(hash));

            return field;
        }

        public override FieldOption GetFieldOption(JToken snapshotData)
        {
            FieldOption fieldOption = new FieldOption(snapshotData);

            fieldOption.Field<object>(_fieldsPath);

            return fieldOption;
        }

        public override FieldOption ExecuteMatch(JToken snapshotData, JToken expectedSnapshotData)
        {
            var actualFieldOption = new FieldOption(snapshotData);
            var actualvalue = actualFieldOption.Field<string>(_fieldsPath);

            var actualHash = actualvalue.ToHashSHA256();

            var expectedFieldOption = new FieldOption(expectedSnapshotData);
            var expectedHash = expectedFieldOption.Field<string>(_fieldsPath);

            if (!string.Equals(actualHash, expectedHash, StringComparison.Ordinal))
            {
                throw new SnapshotCompareException($"The hash in field '{_fieldsPath}' does not match with snapshot.");
            }

            return GetFieldOption(snapshotData);
        }
    }
}
