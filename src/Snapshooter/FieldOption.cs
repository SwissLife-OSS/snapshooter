using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Snapshooter.Exceptions;
using Snapshooter.Extensions;

namespace Snapshooter
{
    /// <summary>
    /// The <see cref="FieldOption"/> is responsible to retrieve
    /// a specific field from a json snapshot by the path of the field.
    /// </summary>
    public class FieldOption
    {
        private JToken _snapshotData;

        /// <summary>
        /// Constructor of the class <see cref="FieldOption"/>
        /// initializes a new instance.
        /// </summary>
        /// <param name="snapshotData">The snapshot json data.</param>
        public FieldOption(JToken snapshotData)
        {
            _snapshotData = snapshotData;
        }

        /// <summary>
        /// The path of the field, which was requested.
        /// </summary>
        public string[] FieldPaths { get; private set; }

        /// <summary>
        /// Finds all jtokens by the given field path. if the field path
        /// starts with '**.' then all the jtokens of the given name are searched.
        /// </summary>
        /// <param name="fieldPath">The path of the jtokens within the snapshot.</param>
        /// <returns>The found jtokens.</returns>
        internal JToken[] FindFieldTokens(string fieldPath)
        {
            if (fieldPath.TryFindFieldsByName(out string fieldName))
            {
                return GetTokensByName(fieldName);
            }

            return GetTokensByPath(fieldPath);
        }

        /// <summary>
        /// Finds all field values by the given field path. if the field path
        /// starts with '**.' then all the fields of the given name are searched.
        /// </summary>
        /// <param name="fieldPath">The path of the fields within the snapshot.</param>
        /// <returns>The values of the requested fields.</returns>
        public object[] FindAllFields(string fieldPath)
        {
            return FindAllFields<object>(fieldPath);
        }

        /// <summary>
        /// Finds all field values by the given field path. if the field path
        /// starts with '**.' then all the fields of the given name are searched.
        /// </summary>
        /// <typeparam name="T">The type of the fields to return.</typeparam>
        /// <param name="fieldPath">The path of the fields within the snapshot.</param>
        /// <returns>The values of the requested fields.</returns>
        public T[] FindAllFields<T>(string fieldPath)
        {
            if (fieldPath.TryFindFieldsByName(out string fieldName))
            {
                return GetAllFieldsByName<T>(fieldName);
            }

            return Fields<T>(fieldPath);            
        }

        /// <summary>
        /// Retrieves field value by the field path.
        /// </summary>
        /// <typeparam name="T">The type of the field to convert and return.</typeparam>
        /// <param name="fieldPath">The path of the field within the snapshot.</param>
        /// <returns>The value of the requested field.</returns>
        public T Field<T>(string fieldPath)
        {
            try
            {
                IEnumerable<JToken> fields = GetTokensByPath(fieldPath);

                if (fields.Count() > 1)
                {
                    throw new SnapshotFieldException(
                        $"The field of the path '{fieldPath}' has an array as return value, " +
                        $"Please use the FieldOption for fields array (Fields).");
                }

                T fieldValue = ConvertToType<T>(fields.Single());

                return fieldValue;
            }
            catch (Exception err)
            {
                throw new SnapshotFieldException($"The field '{fieldPath}' of " +
                    $"the compare context caused an error. {err.Message}", err);
            }
        }

        /// <summary>
        /// Retrieves field values by the field path.
        /// </summary>
        /// <typeparam name="T">The type of the fields to convert and return.</typeparam>
        /// <param name="fieldPath">The path of the fields within the snapshot.</param>
        /// <returns>The value of the requested fields.</returns>
        public T[] Fields<T>(string fieldPath)
        {
            try
            {
                IEnumerable<JToken> fields = GetTokensByPath(fieldPath);

                T[] fieldValues = fields.Select(f => ConvertToType<T>(f)).ToArray();

                return fieldValues;
            }
            catch (Exception err)
            {
                throw new SnapshotFieldException($"The fields of '{fieldPath}' of " +
                    $"the compare context caused an error. {err.Message}", err);
            }
        }        

        /// <summary>
        /// Retrieves all field values from all fields with the given name.
        /// </summary>
        /// <typeparam name="T">The type of the field value.</typeparam>
        /// <param name="name">The name of the field.</param>
        /// <returns></returns>
        public T[] GetAllFieldsByName<T>(string name)
        {
            try
            {
                IEnumerable<JProperty> properties =
                    GetPropertiesByName(name);

                T[] fieldValues = properties
                    .Select(jprop => ConvertToType<T>(jprop.Value))
                    .ToArray();
                
                return fieldValues;
            }
            catch (Exception err)
            {
                throw new SnapshotFieldException($"The field with name '{name}' of " +
                    $"the compare context caused an error. {err.Message}", err);
            }
        }

        private JToken[] GetTokensByName(string name)
        {            
            return GetPropertiesByName(name)
                    .Select(jprop => jprop.Value)
                    .ToArray();
        }

        private JProperty[] GetPropertiesByName(string name)
        {
            if (_snapshotData is JValue jValue)
            {
                throw new SnapshotFieldException($"No snapshot match options are " +
                    $"supported for snapshots with scalar values. Therefore the " +
                    $"match option with field name '{name}' is not allowed.");
            }

            JProperty[] properties = ((JContainer)_snapshotData)
                .Descendants()
                .OfType<JProperty>()
                .Where(jprop => jprop.Name == name)
                .ToArray();

            FieldPaths = properties
                .Select(jprop => jprop.Path)
                .ToArray();

            return properties;
        }

        private JToken[] GetTokensByPath(string fieldPath)
        {
            FieldPaths = new[] { fieldPath };

            if (_snapshotData is JValue)
            {
                throw new SnapshotFieldException($"No snapshot match options are " +
                    $"supported for snapshots with scalar values. Therefore the " +
                    $"match option with field '{fieldPath}' is not allowed.");
            }

            IEnumerable<JToken> jTokens = _snapshotData
                .SelectTokens(fieldPath, false);                

            if (jTokens == null)
            {
                throw new SnapshotFieldException(
                    $"No fields of the path '{fieldPath}' could not be found.");
            }

            return jTokens.ToArray();
        }

        private static T ConvertToType<T>(JToken field)
        {
            if (typeof(T) == typeof(int))
            {
                // This is a workaround, because the json method ToObject<> rounds
                // decimal values to integer values, which is wrong.
                return JsonConvert.DeserializeObject<T>(field.Value<string>());
            }
            //if(typeof(T) == typeof(string))
            //{
            //    if(field.Type != JTokenType.String ||
            //       field.Type != JTokenType.Object ||
            //       field.Type != JTokenType.Null)
            //    {
            //        throw new SnapshotFieldException(
            //            $"The snapshot field with value '{field}' " +
            //            $"is of Type '{field.Type}' and can not be " +
            //            $"converted to requested type 'typeof(T)'.");
            //    }
            //}

            return field.ToObject<T>();
        }
    }
}
