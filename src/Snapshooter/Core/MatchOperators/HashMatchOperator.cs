using System;
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
                var actualHash = actualValues[i].Value<string>();
                if (!string.Equals(actualHash, expectedHash, StringComparison.Ordinal))
                {
                    throw new SnapshotCompareException(
                        $"The hashed field(s) '{_fieldsPath}' does not match with snapshot.");
                }
            }

            return actualFieldOption;
        }

        private JToken FormatField(JToken field)
        {
            var hash = field.ToSHA256();

            field.Replace(new JValue(hash));

            return field;
        }
    }
}
