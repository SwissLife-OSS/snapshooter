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
                .ToString(Formatting.None);

            var hash = SHA256Hash.Hash(fieldValue);

            field.Replace(new JValue(hash));

            return field;
        }

        public override FieldOption GetFieldOption(JToken snapshotData)
        {
            FieldOption fieldOption = new FieldOption(snapshotData);

            fieldOption.Field<object>(_fieldsPath);

            return fieldOption;
        }

        public override FieldOption ExecuteMatch(JToken snapshotData)
        {
            return GetFieldOption(snapshotData);
        }
    }
}
