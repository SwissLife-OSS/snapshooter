using System;
using System.Collections.Generic;
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
        private readonly Dictionary<string, object> _fields;

        public AcceptMatchOperator(
            string fieldsPath,
            bool keepOriginalValue)
        {
            _fieldsPath = fieldsPath;
            _keepOriginalValue = keepOriginalValue;
            _fields = new();
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

        public override FieldOption ExecuteMatch(JToken snapshotData, JToken expectedSnapshotData)
        {
            foreach (KeyValuePair<string, object> field in _fields)
            {
                VerifyFieldType(field.Key, field.Value);
            }
            
            var fieldOption = new FieldOption(snapshotData);

            fieldOption.FindAllFields(_fieldsPath);

            return fieldOption;
        }

        private JToken FormatField(JToken field)
        {
            string originalValue = string.Empty;
            if (_keepOriginalValue)
            {
                var fieldValue = field
                    .ToString(Formatting.None)
                    .Replace("\"", string.Empty);

                if (fieldValue == "null")
                {
                    fieldValue = "Null";
                }

                originalValue = $"(original: '{fieldValue}')";
            }

            _fields.Add(field.Path, field.ConvertToType<object>());

            string typeAlias = typeof(T).GetAliasName();

            field.Replace(new JValue($"AcceptAny<{typeAlias}>{originalValue}"));

            return field;
        }

        private void VerifyFieldType(string path, object field)
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
                if (!typeof(T).IsNullable())
                {
                    throw new SnapshotFieldException(
                    CreateAcceptExceptionMessage(
                        path, field,
                        $"but defined accept type " +
                        $"'{typeof(T).GetAliasName()}' is not nullable."));
                }

                return;
            }

            if (typeof(T) == typeof(Guid) || typeof(T) == typeof(Guid?))
            {
                if (Guid.TryParse(field.ToString(), out Guid value))
                {
                    return;
                }
            }

            if (typeof(T) == typeof(byte[]) && field is string stringField)
            {
                if (stringField.IsBase64String())
                {
                    return;
                }
            }

            if (!(field is T))
            {
                throw new SnapshotFieldException(
                    CreateAcceptExceptionMessage(
                        path, field,
                        $"and therefore not of type " +
                        $"'{typeof(T).GetAliasName()}'."));
            }
        }

        private string CreateAcceptExceptionMessage(
            string path, object field, string message)
        {
            return
                $"Accept match option failed, " +
                $"because the field value of '{path}' is " +
                $"'{GetAcceptFieldValueString(field)}', " +
                $"{message}";
        }

        private string GetAcceptFieldValueString(object field)
        {
            return field is { } ? field.ToString() : "Null";
        }
    }
}
