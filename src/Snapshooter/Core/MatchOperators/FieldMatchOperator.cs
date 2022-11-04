using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

#nullable enable

namespace Snapshooter.Core
{
    public class FieldMatchOperator<T> : FieldMatchOperator
    {
        private readonly Func<FieldOption, T> _fieldOption;
        private readonly Action<T> _fieldCompareAction;
        private readonly Action<JToken>? _fieldFormatAction;

        public FieldMatchOperator(
            Func<FieldOption, T> fieldOption,
            Action<T> fieldCompareAction,
            Action<JToken>? fieldFormatAction = null)
        {
            _fieldOption = fieldOption;
            _fieldCompareAction = fieldCompareAction;
            _fieldFormatAction = fieldFormatAction;
        }
        public override bool HasFormatAction()
        {
            return _fieldFormatAction is { };
        }

        public override JToken FormatField(JToken field)
        {
            if (_fieldFormatAction is { })
            {
                _fieldFormatAction(field);
            }

            return field;
        }

        public override FieldOption GetFieldOption(JToken snapshotData)
        {
            FieldOption fieldOption = new FieldOption(snapshotData);
            _fieldOption(fieldOption);

            return fieldOption;
        }

        public override FieldOption ExecuteMatch(JToken snapshotData, JToken expectedSnapshotData)
        {
            FieldOption fieldOption = new FieldOption(snapshotData);
            T fieldValue = _fieldOption(fieldOption);

            _fieldCompareAction(fieldValue);

            return fieldOption;
        }

        public override IEnumerable<JToken> GetFieldTokens(JToken snapshotData)
        {
            return new List<JToken>();
        }
    }

    public abstract class FieldMatchOperator
    {
        public abstract bool HasFormatAction();
        public abstract IEnumerable<JToken> GetFieldTokens(JToken snapshotData);
        public abstract FieldOption GetFieldOption(JToken snapshotData);
        public abstract FieldOption ExecuteMatch(JToken snapshotData, JToken expectedSnapshotData);
        public abstract JToken FormatField(JToken snapshotData);
    }
}
