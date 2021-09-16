using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Snapshooter.Exceptions;

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
        /// Retrieves the field value by the field path.
        /// </summary>
        /// <typeparam name="T">The type of the field to convert and return.</typeparam>
        /// <param name="fieldPath">The path of the field within the snapshot.</param>
        /// <returns>The value of the requested field.</returns>
        public T Field<T>(string fieldPath)
        {
            try
            {
                FieldPaths = new[] { fieldPath };

                if (_snapshotData is JValue)
                {
                    throw new SnapshotFieldException($"No snapshot match options are " +
                        $"supported for snapshots with scalar values. Therefore the " +
                        $"match option with field '{fieldPath}' is not allowed.");
                }
                                
                IEnumerable<JToken> fields = _snapshotData.SelectTokens(fieldPath, true);

                if (fields == null || fields.Count() == 0)
                {
                    throw new SnapshotFieldException(
                        $"The field of the path '{fieldPath}' could not be found.");
                }

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
        /// Retrieves the field values by the field path.
        /// </summary>
        /// <typeparam name="T">The type of the fields to convert and return.</typeparam>
        /// <param name="fieldPath">The path of the fields within the snapshot.</param>
        /// <returns>The value of the requested fields.</returns>
        public T[] Fields<T>(string fieldPath)
        {
            try
            {
                FieldPaths = new[] { fieldPath };

                if (_snapshotData is JValue)
                {
                    throw new SnapshotFieldException($"No snapshot match options are " +
                        $"supported for snapshots with scalar values. Therefore the " +
                        $"match option with field '{fieldPath}' is not allowed.");
                }

                IEnumerable<JToken> fields = _snapshotData.SelectTokens(fieldPath, true);

                if (fields == null || fields.Count() == 0)
                {
                    throw new SnapshotFieldException(
                        $"No fields of the path '{fieldPath}' could not be found.");
                }
                
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
                if (_snapshotData is JValue)
                {
                    throw new SnapshotFieldException($"No snapshot match options are " +
                        $"supported for snapshots with scalar values. Therefore the " +
                        $"match option with field name '{name}' is not allowed.");
                }

                List<JProperty> properties = ((JContainer)_snapshotData)
                    .Descendants()
                    .OfType<JProperty>()
                    .Where(jprop => jprop.Name == name)
                    .ToList();

                T[] fieldValues = properties
                    .Select(jprop => ConvertToType<T>(jprop.Value))
                    .ToArray();

                string[] fieldPaths = properties
                    .Select(jprop => jprop.Path)
                    .ToArray();

                FieldPaths = fieldPaths;

                return fieldValues;
            }
            catch (Exception err)
            {
                throw new SnapshotFieldException($"The field with name '{name}' of " +
                    $"the compare context caused an error. {err.Message}", err);
            }
        }

        private static T ConvertToType<T>(JToken field)
        {
            if (typeof(T) == typeof(int))
            {
                // This is a workaround, because the json method ToObject<> rounds
                // decimal values to integer values, which is wrong. 
                return JsonConvert.DeserializeObject<T>(field.Value<string>());
            }
            else
            {
                return field.ToObject<T>();
            }
        }        
    }
}
