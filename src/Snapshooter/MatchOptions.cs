using System;
using System.Collections.Generic;
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
        public IEnumerable<FieldMatchOperator> MatchOperators
        {
            get { return _matchOperators.AsReadOnly(); }
        }

        /// <summary>
        /// With the <see cref="IgnoreField"/> option, you can ignore the given field 
        /// during snapshot comparison. Therefore the field will just be skipped during compare.
        /// </summary>
        /// <param name="fieldPath">The json path to the field to ignore.</param>
        public MatchOptions IgnoreField(string fieldPath)
        {
            Func<FieldOption, object> fieldOption = option => option.Field<object>(fieldPath);
            Ignore(fieldOption);

            return this;
        }

        /// <summary>
        /// With the <see cref="IgnoreFields"/> option, you can ignore the given fields 
        /// during snapshot comparison. Therefore the fields will just be skipped during compare.
        /// </summary>
        /// <param name="fieldsPath">The json path to the fields to ignore.</param>
        public MatchOptions IgnoreFields(string fieldsPath)
        {
            Func<FieldOption, object> fieldOption = option => option.Fields<object>(fieldsPath);
            Ignore(fieldOption);

            return this;
        }

        /// <summary>
        /// With the <see cref="IgnoreField"/> option, you can ignore the given field 
        /// during snapshot comparison. Therefore the field will just be skipped during compare.
        /// </summary>
        /// <param name="fieldPath">The json path to the field to ignore.</param>
        public MatchOptions IgnoreField<T>(string fieldPath)
        {
            Func<FieldOption, object> fieldOption = option => option.Field<T>(fieldPath);
            Ignore(fieldOption);

            return this;
        }

        /// <summary>
        /// With the <see cref="IgnoreFields"/> option, you can ignore the given fields 
        /// during snapshot comparison. Therefore the fields will just be skipped during compare.
        /// </summary>
        /// <param name="fieldsPath">The json path to the fields to ignore.</param>
        public MatchOptions IgnoreFields<T>(string fieldsPath)
        {
            Func<FieldOption, object> fieldsOption = option => option.Fields<T>(fieldsPath);
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

            IsType<T>(fieldOption);

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
        /// to assert/validate a specific field seperately. Therefore the given field
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
    }
}
