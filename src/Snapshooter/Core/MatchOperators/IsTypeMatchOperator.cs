using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json.Linq;
using Snapshooter.Exceptions;
using Snapshooter.Extensions;

#nullable enable

namespace Snapshooter.Core
{
    public class IsTypeMatchOperator<T> : FieldMatchOperator
    {
        private readonly string _fieldsPath;
        private readonly Func<FieldOption, T>? _fieldOption;

        public IsTypeMatchOperator(string fieldsPath)
        {
            _fieldsPath = fieldsPath;
        }

        [Obsolete("Use the other constructor with the field path string.")]
        public IsTypeMatchOperator(Func<FieldOption, T> fieldOption)
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
            FieldOption fieldOption = new FieldOption(snapshotData);

            object? fields = null;
            if (_fieldOption is { })
            {
                fields = _fieldOption(fieldOption);
            }
            else
            {
                fields = fieldOption.FindAllFields<T>(_fieldsPath);
            }

            bool isType = fields is T
                or T[]
                or IEnumerable<T>
                or IList<T>
                or ICollection<T>;

            if (!isType)
            {
                throw new SnapshotFieldException($"" +
                    $"IsType match option failed, " +
                    $"because the field with value " +
                    $"'{fields}' is not of type {typeof(T)}.");
            }

            return fieldOption;
        }
    }
}
