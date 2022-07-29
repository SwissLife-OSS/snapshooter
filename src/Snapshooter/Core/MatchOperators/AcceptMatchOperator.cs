using System;
using Newtonsoft.Json;
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
                var fieldValue = field
                    .ToString(Formatting.None)
                    .Replace("\"", string.Empty);

                if(fieldValue == "null") // TODO remove this and execute all tests
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
            // TODO here limit if T is byte and the number is bigger or smaller, than exception
            if (typeof(T) == typeof(double) || typeof(T) == typeof(double?) ||
                typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?) ||
                typeof(T) == typeof(float) || typeof(T) == typeof(float?))
            {
                if (field is double || field is decimal || field is float)
                {
                    return;
                }
            }

            // TODO here limit if T is byte and the number is bigger or smaller, than exception
            if (typeof(T) == typeof(int) || typeof(T) == typeof(int?) ||
                typeof(T) == typeof(long) || typeof(T) == typeof(long?) ||
                typeof(T) == typeof(short) || typeof(T) == typeof(short?) ||
                typeof(T) == typeof(byte) || typeof(T) == typeof(byte?))
            {
                if (field is int || field is long || field is short || field is byte)
                {
                    return;
                }
            }

            if (field == null)
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

            if(typeof(T) == typeof(Guid) || typeof(T) == typeof(Guid?))
            {
                if(Guid.TryParse(field.ToString(), out Guid value))
                {
                    return;
                }
            }

            if (typeof(T) == typeof(byte[]) && field is string stringField)
            {
                if(stringField.IsBase64String())
                {
                    return;
                }
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
