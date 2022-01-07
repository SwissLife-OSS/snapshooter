using System;
using Newtonsoft.Json.Linq;
using Snapshooter.Exceptions;
using Snapshooter.Extensions;

namespace Snapshooter.Core
{
    public class AcceptMatchOperator<T> : FieldMatchOperator
    {
        private readonly string _fieldsPath;
        private readonly bool _keepOriginalValue;

        public AcceptMatchOperator(
            string fieldsPath,
            bool keepOriginalValue)
        {
            _fieldsPath = fieldsPath;
            _keepOriginalValue = keepOriginalValue;
        }

        public override bool IsFormatActionSet() => true;

        public override JToken FormatField(JToken field)
        {
            string originalValue = string.Empty;
            if (_keepOriginalValue)
            {
                var fieldValue = field.ToString();

                if(string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = "Null";
                }

                originalValue = $"(original: '{fieldValue}')";
            }

            string typeAlias = typeof(T).GetAliasName();

            field.Replace(new JValue($"AcceptAny<{typeAlias}>{originalValue}"));

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
            FieldOption fieldOption = new FieldOption(snapshotData);

            object fieldValue = fieldOption.Field<object>(_fieldsPath);

            VerifyFieldType(fieldValue);

            return fieldOption;
        }

        private void VerifyFieldType(object field)
        {
            if (typeof(T) == typeof(double) || typeof(T) == typeof(double?) ||
                typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?) ||
                typeof(T) == typeof(float) || typeof(T) == typeof(float?))
            {
                if (field is double || field is decimal || field is float)
                {
                    return;
                }
            }

            if(field == null)
            {
                if(!typeof(T).IsNullable())
                {
                    throw new SnapshotFieldException(
                    CreateAcceptExceptionMessage(field,
                        $"but defined accept type " +
                        $"'{typeof(T).GetAliasName()}' is not nullable."));
                }

                return;
            }

            if (!(field is T))
            {
                throw new SnapshotFieldException(
                    CreateAcceptExceptionMessage(field,
                        $"and therefore not of type " +
                        $"'{typeof(T).GetAliasName()}'."));
            }
        }

        private string CreateAcceptExceptionMessage(object field, string message)
        {
            return
                $"Accept match option failed, " +
                $"because the field value of '{_fieldsPath}' is " +
                $"'{GetAcceptFieldValueString(field)}', " +
                $"{message}";
        }

        private string GetAcceptFieldValueString(object field)
        {
            return field is { } ? field.ToString() : "Null";
        }
    }
}
