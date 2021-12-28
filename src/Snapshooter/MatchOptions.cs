using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Snapshooter.Core;
using Snapshooter.Exceptions;

namespace Snapshooter
{
    /// <summary>
    /// The class <see cref="MatchOptions"/> has function options for snapshot comparison.
    /// During snapshot comparison the <see cref="MatchOptions"/> options can be used to
    /// ignore some fields or check the fields only for its type.
    /// </summary>
    public class MatchOptions
    {
        /// <summary>
        /// The configured match operators.
        /// </summary>
        protected List<FieldMatchOperator> _matchOperators;

        /// <summary>
        /// Constructor of the <see cref="MatchOptions"/> class to create
        /// a new instance.
        /// </summary>
        public MatchOptions()
        {
            _matchOperators = new List<FieldMatchOperator>();
        }

        /// <summary>
        /// Returns all configured field match operators.
        /// </summary>
        public IReadOnlyCollection<FieldMatchOperator> MatchOperators
        {
            get { return _matchOperators.AsReadOnly(); }
        }

        /// <summary>
        /// The <see cref="AcceptField{T}(string)"/> match option accepts a snapshot field, if the
        /// type of the field is equal to the given Type. The value of the field will NOT be 
        /// compared with the original snapshot.
        /// In addition, the field will be replaced by the text 'AcceptAny{T}()'"
        /// </summary>
        /// <typeparam name="T">The type to check the field for.</typeparam>
        /// <param name="fieldPath">The json path to the field(s) to ignore.</param>
        public MatchOptions AcceptField<T>(string fieldPath)
        {
            return Accept<T>(fieldPath);
        }

        /// <summary>
        /// The <see cref="AcceptField{T}(string)"/> match option accepts a snapshot field, if the
        /// type of the field is equal to the given Type. The value of the field will NOT be 
        /// compared with the original snapshot.
        /// In addition, the field will be replaced by the following text 'AcceptAny{T}()'.
        /// If we enable the keep original value flag, then the value from the first original 
        /// snapshot will always be included into the output 'AcceptAny{T}(1235.218)'.
        /// </summary>
        /// <typeparam name="T">The type to check the field for.</typeparam>
        /// <param name="fieldPath">
        /// The json path to the field(s) to ignore.
        /// </param>
        /// <param name="keepOriginalValue">
        /// The flag, which defines if the original value 
        /// of the field shall be kept in the snapshot.
        /// </param>
        public MatchOptions AcceptField<T>(string fieldPath, bool keepOriginalValue = false)
        {
            return Accept<T>(fieldPath, keepOriginalValue);
        }

        /// <summary>
        /// The <see cref="IgnoreField(string)"/> option ignores the existing field(s) by the given 
        /// json path. The field(s) will be ignored during snapshot comparison.
        /// 
        /// Example:
        /// {
        ///     "UserId": "0A332E69-FDDB-46B9-8E42-C411C3F633AC",
        ///     "Firstname": "David",
        ///     "Lastname": "Walton",
        ///     "Relatives": [
        ///         {
        ///             "UserId": "E20EEEE6-39D1-4878-B8A9-621CECDDDA82",
        ///             "Firstname": "Mark",
        ///             "Lastname": "Walton",
        ///         },
        ///         {
        ///             "UserId": "355910B4-6CD9-4FC3-962B-75D079C50415",
        ///             "Firstname": "Jenny",
        ///             "Lastname": "Walton",
        ///         },
        ///     ]
        /// }
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreField("Firstname"))
        /// >> This will ignore the 'Firstname' field in the root element. ('David')
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreField("Relatives[0].Firstname"))
        /// >> This will ignore the first 'Firstname' field of the Relatives element. ('Mark')
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreField("Relatives[*].Firstname"))
        /// >> This will ignore all 'Firstname' fields of the Relatives element. ('Mark' and 'Jenny')
        /// 
        /// To ignore all fields of a specific name, use the syntax '**.fieldName'
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreField("**.Firstname"))
        /// >> This will ignore all 'Firstname' fields of the entire json. ('David', 'Mark' and 'Jenny')
        /// 
        /// </summary>
        /// <param name="fieldPath">The json path to the field(s) to ignore.</param>
        public MatchOptions IgnoreField(string fieldPath)
        {
            return IgnoreFields<object>(fieldPath);
        }

        /// <summary>
        /// The <see cref="IgnoreFields(string)"/> option ignores the existing field(s) by the given 
        /// json path. The field(s) will be ignored during snapshot comparison.
        ///
        /// Example:
        /// {
        ///     "UserId": "0A332E69-FDDB-46B9-8E42-C411C3F633AC",
        ///     "Firstname": "David",
        ///     "Lastname": "Walton",
        ///     "Relatives": [
        ///         {
        ///             "UserId": "E20EEEE6-39D1-4878-B8A9-621CECDDDA82",
        ///             "Firstname": "Mark",
        ///             "Lastname": "Walton",
        ///         },
        ///         {
        ///             "UserId": "355910B4-6CD9-4FC3-962B-75D079C50415",
        ///             "Firstname": "Jenny",
        ///             "Lastname": "Walton",
        ///         },
        ///     ]
        /// }
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreFields("Firstname"))
        /// >> This will ignore the 'Firstname' field in the root element. ('David')
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreFields("Relatives[0].Firstname"))
        /// >> This will ignore the first 'Firstname' field of the Relatives element. ('Mark')
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreFields("Relatives[*].Firstname"))
        /// >> This will ignore all 'Firstname' fields of the Relatives element. ('Mark' and 'Jenny')
        /// 
        /// To ignore all fields of a specific name, use the syntax '**.fieldName'
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreFields("**.Firstname"))
        /// >> This will ignore all 'Firstname' fields of the entire json. ('David', 'Mark' and 'Jenny')
        /// 
        /// </summary>
        /// <param name="fieldsPath">The json path to the field(s) to ignore.</param>
        public MatchOptions IgnoreFields(string fieldsPath)
        {
            return IgnoreFields<object>(fieldsPath);
        }

        /// <summary>
        /// The <see cref="IgnoreField{T}(string)"/> option ignores the existing field(s) by the given 
        /// json path. The field(s) will be ignored during snapshot comparison.
        /// In addition, it will be checked if the value of the ignored field is of the given type T.
        /// 
        /// Example:
        /// {
        ///     "UserId": "0A332E69-FDDB-46B9-8E42-C411C3F633AC",
        ///     "Firstname": "David",
        ///     "Lastname": "Walton",
        ///     "Relatives": [
        ///         {
        ///             "UserId": "E20EEEE6-39D1-4878-B8A9-621CECDDDA82",
        ///             "Firstname": "Mark",
        ///             "Lastname": "Walton",
        ///         },
        ///         {
        ///             "UserId": "355910B4-6CD9-4FC3-962B-75D079C50415",
        ///             "Firstname": "Jenny",
        ///             "Lastname": "Walton",
        ///         },
        ///     ]
        /// }
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreField{string}("Firstname"))
        /// >> This will ignore the 'Firstname' field in the root element. ('David')
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreField{string}("Relatives[0].Firstname"))
        /// >> This will ignore the first 'Firstname' field of the Relatives element. ('Mark')
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreField{string}("Relatives[*].Firstname"))
        /// >> This will ignore all 'Firstname' fields of the Relatives element. ('Mark' and 'Jenny')
        /// 
        /// To ignore all fields of a specific name, use the syntax '**.fieldName'
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreField{string}("**.Firstname"))
        /// >> This will ignore all 'Firstname' fields of the entire json. ('David', 'Mark' and 'Jenny')
        /// 
        /// </summary>
        /// <typeparam name="T">The type of which the field value has to be.</typeparam>
        /// <param name="fieldPath">The json path to the field(s) to ignore.</param>
        public MatchOptions IgnoreField<T>(string fieldPath)
        {
            return IgnoreFields<T>(fieldPath);
        }

        /// <summary>
        /// The <see cref="IgnoreFields{T}(string)"/> option ignores the existing field(s) by the given 
        /// json path. The field(s) will be ignored during snapshot comparison.
        /// In addition, it will be checked if the value of the ignored field is of the given type T.
        /// 
        /// Example:
        /// {
        ///     "UserId": "0A332E69-FDDB-46B9-8E42-C411C3F633AC",
        ///     "Firstname": "David",
        ///     "Lastname": "Walton",
        ///     "Relatives": [
        ///         {
        ///             "UserId": "E20EEEE6-39D1-4878-B8A9-621CECDDDA82",
        ///             "Firstname": "Mark",
        ///             "Lastname": "Walton",
        ///         },
        ///         {
        ///             "UserId": "355910B4-6CD9-4FC3-962B-75D079C50415",
        ///             "Firstname": "Jenny",
        ///             "Lastname": "Walton",
        ///         },
        ///     ]
        /// }
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreFields{string}("Firstname"))
        /// >> This will ignore the 'Firstname' field in the root element. ('David')
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreFields{string}("Relatives[0].Firstname"))
        /// >> This will ignore the first 'Firstname' field of the Relatives element. ('Mark')
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreFields{string}("Relatives[*].Firstname"))
        /// >> This will ignore all 'Firstname' fields of the Relatives element. ('Mark' and 'Jenny')
        /// 
        /// To ignore all fields of a specific name, use the syntax '**.fieldName'
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreFields{string}("**.Firstname"))
        /// >> This will ignore all 'Firstname' fields of the entire json. ('David', 'Mark' and 'Jenny')
        /// 
        /// </summary>
        /// <typeparam name="T">The type of which the field value has to be.</typeparam>
        /// <param name="fieldsPath">The json path to the field(s) to ignore.</param>
        public MatchOptions IgnoreFields<T>(string fieldsPath)
        {
            if(fieldsPath.StartsWith("**.", true, CultureInfo.InvariantCulture))
            {
                return IgnoreAllFields<T>(fieldsPath.Remove(0, 3));
            }

            return IgnoreFieldsByPath<T>(fieldsPath);
        }
        
        /// <summary>
        /// The <see cref="IgnoreAllFields(string)"/> option ignores all available 
        /// fields by the given name. All fields with the given name will be ignored
        /// during snapshot comparison.
        /// 
        /// Example:
        /// {
        ///     "UserId": "0A332E69-FDDB-46B9-8E42-C411C3F633AC",
        ///     "Firstname": "David",
        ///     "Lastname": "Walton",
        ///     "Relatives": [
        ///         {
        ///             "UserId": "E20EEEE6-39D1-4878-B8A9-621CECDDDA82",
        ///             "Firstname": "Mark",
        ///             "Lastname": "Walton",
        ///         },
        ///         {
        ///             "UserId": "355910B4-6CD9-4FC3-962B-75D079C50415",
        ///             "Firstname": "Jenny",
        ///             "Lastname": "Walton",
        ///         },
        ///     ]
        /// }
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreAllFields("UserId")
        /// 
        /// This configured match option has the effect, that all 'UserId' fields 
        /// will be ignored within the json. (Therefore all 3 UserIds)
        /// 
        /// Only one 'name' per <see cref="IgnoreAllFields"/> option is allowed. 
        /// (No concatenated name strings)
        /// 
        /// </summary>
        /// <param name="name">The name of the field(s) to be ignored.</param>
        public MatchOptions IgnoreAllFields(string name)
        {
            return IgnoreAllFields<object>(name);
        }

        /// <summary>
        /// The <see cref="IgnoreAllFields{T}(string)"/> option ignores all available 
        /// fields by the given name. All fields with the given name will be ignored
        /// during snapshot comparison.
        /// In addition, it will be checked if the value of the ignored fields is of the given type T.
        /// 
        /// Example:
        /// {
        ///     "UserId": "0A332E69-FDDB-46B9-8E42-C411C3F633AC",
        ///     "Firstname": "David",
        ///     "Lastname": "Walton",
        ///     "Relatives": [
        ///         {
        ///             "UserId": "E20EEEE6-39D1-4878-B8A9-621CECDDDA82",
        ///             "Firstname": "Mark",
        ///             "Lastname": "Walton",
        ///         },
        ///         {
        ///             "UserId": "355910B4-6CD9-4FC3-962B-75D079C50415",
        ///             "Firstname": "Mark",
        ///             "Lastname": "Walton",
        ///         },
        ///     ]
        /// }
        /// 
        /// Snapshot.Match(userDavidWalton, matchOptions => matchOptions.IgnoreAllFields{Guid}("UserId")
        /// 
        /// This configured match option has the effect, that all 'UserId' fields 
        /// will be ignored within the json, but the value type of the 'UserId' 
        /// fields will be verified. (Therefore all 3 UserIds)
        /// 
        /// Only one name per <see cref="IgnoreAllFields{T}(string)"/> option is allowed.
        /// 
        /// </summary>
        /// <typeparam name="T">The type which the field value has to be.</typeparam>
        /// <param name="name">The name of the field(s) to be ignored.</param>
        public MatchOptions IgnoreAllFields<T>(string name)
        {
            Func<FieldOption, object> fieldsOption = 
                option => option.GetAllFieldsByName<T>(name);

            Ignore(fieldsOption);
            
            return this;
        }

        /// <summary>
        /// With the <see cref="Ignore"/> option, you can ignore the given field during snapshot
        /// comparison. Therefore the field will just be skipped during compare.
        /// </summary>
        /// <param name="ignoreFieldOption">The snapshot field to ignore.</param>
        public MatchOptions Ignore(Func<FieldOption, object> ignoreFieldOption)
        {
            _matchOperators.Add(
                new FieldMatchOperator<object>(ignoreFieldOption, field => { }));

            return this;
        }

        /// <summary>
        /// The method IsType allows you during the snapshot comparison,
        /// to check if a field is from a specific type. Therefore the given field will NOT 
        /// be compared with the field of the snapshot, it will only be checked if the 
        /// field has the given type.
        /// </summary>
        /// <typeparam name="T">The type which the field should have.</typeparam>
        /// <param name="isTypeField">The field to check.</param>
        public MatchOptions IsType<T>(Func<FieldOption, T> isTypeField)
        {
            _matchOperators.Add(new FieldMatchOperator<T>(isTypeField, field => {
                if (!(field is T))
                {
                    throw new SnapshotFieldException($"{nameof(IsType)} failed, " +
                        $"because the field " +
                        $"with value '{field}' is not of type {typeof(T)}.");
                }
            }));

            return this;
        }

        /// <summary>
        /// The method IsType allows you during the snapshot comparison,
        /// to check if a field is from a specific type. Therefore the given field will NOT 
        /// be compared with the field of the snapshot, it will only be checked if the 
        /// field has the given type.
        /// </summary>
        /// <typeparam name="T">The type which the field should have.</typeparam>
        /// <param name="isTypeField">The field to check.</param>
        public MatchOptions IsType<T>(Func<FieldOption, T[]> isTypeField)
        {
            IsType<T[]>(isTypeField);

            return this;
        }

        /// <summary>
        /// The method <see cref="IsTypeField"/> allows you during the snapshot comparison,
        /// to check if a field is from a specific type. Therefore the given field will NOT 
        /// be compared with the field of the snapshot, it will only be checked if the 
        /// field is from the given type.
        /// </summary>
        /// <typeparam name="T">The type which the field should have.</typeparam>
        /// <param name="fieldPath">The json path of the field to check for the type.</param>
        public MatchOptions IsTypeField<T>(string fieldPath)
        {
            Func<FieldOption, T> fieldOption = option => option.Field<T>(fieldPath);

            IsType(fieldOption);

            return this;
        }

        /// <summary>
        /// The method <see cref="IsTypeFields"/> allows you during the snapshot comparison,
        /// to check if the given fields are from a specific type. Therefore the given fields
        /// will NOT be compared with the fields of the snapshot, it will only be checked if the 
        /// fields are from the given type.
        /// </summary>
        /// <typeparam name="T">The type which the field should have.</typeparam>
        /// <param name="fieldsPath">The json path of the fields to check for the type.</param>
        public MatchOptions IsTypeFields<T>(string fieldsPath)
        {
            Func<FieldOption, T[]> fieldsOption = option => option.Fields<T>(fieldsPath);

            IsType<T[]>(fieldsOption);

            return this;
        }

        /// <summary>
        /// The method <see cref="Assert"/> allows you during the snapshot comparison,
        /// to assert/validate a specific field separately. Therefore the given field
        /// will NOT be compared with the fields of the snapshot, it will only be validated
        /// with the assert function defined.
        /// </summary>
        /// <param name="assertAction">
        /// The validation action, which shall be executed
        /// on the value of the field.
        /// </param>
        public MatchOptions Assert(Action<FieldOption> assertAction)
        {
            Func<FieldOption, FieldOption> fieldOption = option => option;

            _matchOperators.Add(new FieldMatchOperator<FieldOption>(fieldOption, assertAction));

            return this;
        }

        private MatchOptions IgnoreFieldsByPath<T>(string fieldsPath)
        {
            Func<FieldOption, object> fieldOption =
                option => option.Fields<T>(fieldsPath);

            Ignore(fieldOption);

            return this;
        }

        private MatchOptions Accept<T>(
            string fieldsPath,
            bool keepOriginalValue = false)
        {
            Func<FieldOption, T> fieldOption =
                option => option.Field<T>(fieldsPath);

            _matchOperators.Add(new FieldMatchOperator<T>(fieldOption,
                field =>
                {
                    if (!(field is T))
                    {
                        throw new SnapshotFieldException($"{nameof(IsType)} failed, " +
                            $"because the field " +
                            $"with value '{field}' is not of type {typeof(T)}.");
                    }
                },
                field =>
                {
                    string originalValue = string.Empty;
                    if (keepOriginalValue)
                    {
                        originalValue = $"(original: '{field}')";
                    }

                    field.Replace(new JValue($"AcceptAny<{typeof(T).Name}>{originalValue}"));
                }));

            return this;
        }
    }
}
