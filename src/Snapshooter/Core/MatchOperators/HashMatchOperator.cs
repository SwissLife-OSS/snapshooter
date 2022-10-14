using System;
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

            fieldOption.Fields<object>(_fieldsPath);

            return fieldOption;
        }

        public override FieldOption ExecuteMatch(JToken snapshotData, JToken expectedSnapshotData)
        {
            var actualFieldOption = new FieldOption(snapshotData);
            var actualValues = actualFieldOption.Fields<string>(_fieldsPath);

            var expectedFieldOption = new FieldOption(expectedSnapshotData);
            var expectedHashes = expectedFieldOption.Fields<string>(_fieldsPath);

            if (actualValues.Length != expectedHashes.Length)
            {
                throw new SnapshotCompareException(
                    $"The hashed field(s) '{_fieldsPath}' does not match with snapshot.");
            }

            for (var i = 0; i < actualValues.Length; i++)
            {
                var actualHash = actualValues[i].ToHashSHA256();
                if (!string.Equals(actualHash, expectedHashes[i], StringComparison.Ordinal))
                {
                    throw new SnapshotCompareException(
                        $"The hashed field(s) '{_fieldsPath}' does not match with snapshot.");
                }
            }

            return GetFieldOption(snapshotData);
        }
    }
}
