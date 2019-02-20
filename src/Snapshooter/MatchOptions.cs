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
        protected List<FieldMatchOperator> _matchOperators;

        public MatchOptions()
        {
            _matchOperators = new List<FieldMatchOperator>();
        }

        public IEnumerable<FieldMatchOperator> MatchOperators
        {
            get { return _matchOperators.AsReadOnly(); }
        }

        public MatchOptions IgnoreField(string fieldPath)
        {
            Func<FieldOption, object> fieldOption = option => option.Field<object>(fieldPath);
            Ignore(fieldOption);

            return this;
        }

        public MatchOptions IgnoreFields(string fieldsPath)
        {
            Func<FieldOption, object> fieldOption = option => option.Fields<object>(fieldsPath);
            Ignore(fieldOption);

            return this;
        }

        public MatchOptions IgnoreField<T>(string fieldPath)
        {
            Func<FieldOption, object> fieldOption = option => option.Field<T>(fieldPath);
            Ignore(fieldOption);

            return this;
        }

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
        /// The <see cref="IsAny"/> type option allows, only to check for the type of
        /// a field within the snapshot comparison. Therefore the field will not be compared
        /// with the snapshot, it will only checked if the field has the given type.
        /// </summary>
        /// <typeparam name="T">The type which the field should have.</typeparam>
        /// <param name="ofTypeField">The field to check.</param>
        public MatchOptions IsType<T>(Func<FieldOption, T> isTypeField)
        {
            _matchOperators.Add(new FieldMatchOperator<T>(isTypeField, field => {
                if (!(field is T))
                {
                    throw new SnapshotFieldException($"{nameof(IsType)} failed, because the field " +
                    $"with value '{field}' is not of type {typeof(T)}.");
                }
            }));

            return this;
        }

        public MatchOptions IsType<T>(Func<FieldOption, T[]> isTypeField)
        {
            IsType<T[]>(isTypeField);

            return this;
        }

        public MatchOptions IsTypeField<T>(string fieldPath)
        {
            Func<FieldOption, T> fieldOption = option => option.Field<T>(fieldPath);

            IsType<T>(fieldOption);

            return this;
        }

        public MatchOptions IsTypeFields<T>(string fieldsPath)
        {
            Func<FieldOption, T[]> fieldsOption = option => option.Fields<T>(fieldsPath);

            IsType<T[]>(fieldsOption);

            return this;
        }

        public MatchOptions Assert(Action<FieldOption> assertAction)
        {
            Func<FieldOption, FieldOption> fieldOption = option => option;

            _matchOperators.Add(new FieldMatchOperator<FieldOption>(fieldOption, assertAction));

            return this;
        }
    }
}
