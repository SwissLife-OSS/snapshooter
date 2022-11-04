using System;
using System.Collections.Generic;
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

        public override bool HasFormatAction() => true;

        public override JToken FormatField(JToken field)
        {            
            var hash = field.ToSHA256();

            field.Replace(new JValue(hash));

            return field;
        }

        public override IEnumerable<JToken> GetFieldTokens(JToken snapshotData)
        {
            FieldOption fieldOption = new FieldOption(snapshotData);

            return fieldOption.FindFieldTokens(_fieldsPath);
        }

        public override FieldOption GetFieldOption(JToken snapshotData)
        {
            FieldOption fieldOption = new FieldOption(snapshotData);

            fieldOption.FindAllFields<object>(_fieldsPath);

            return fieldOption;
        }

        public override FieldOption ExecuteMatch(
            JToken snapshotData,
            JToken expectedSnapshotData)
        {
            var actualFieldOption = new FieldOption(snapshotData);
            JToken[] actualValues = actualFieldOption.FindFieldTokens(_fieldsPath);

            var expectedFieldOption = new FieldOption(expectedSnapshotData);
            JToken[] expectedHashes = expectedFieldOption.FindFieldTokens(_fieldsPath);

            if (actualValues.Length != expectedHashes.Length)
            {
                throw new SnapshotCompareException(
                    $"The hashed field(s) '{_fieldsPath}' does not match with snapshot.");
            }
            
            for (var i = 0; i < expectedHashes.Length; i++)
            {
                var expectedHash = expectedHashes[i].Value<string>();
                var actualHash = actualValues[i].ToSHA256();
                if (!string.Equals(actualHash, expectedHash, StringComparison.Ordinal))
                {
                    throw new SnapshotCompareException(
                        $"The hashed field(s) '{_fieldsPath}' does not match with snapshot.");
                }
            }

            return GetFieldOption(snapshotData);
        }
    }
}
