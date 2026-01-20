using System;

namespace Snapshooter
{
    public static class SnapshotBuilderExtensions
    {
        /// <summary>
        /// With the <see cref="IgnoreField"/> option, you can ignore the given field
        /// during snapshot comparison. Therefore the field will just be skipped during compare.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="fieldPath">The json path to the field to ignore.</param>
        public static ISnapshotBuilder IgnoreField(this ISnapshotBuilder builder, string fieldPath)
        {
            return builder.ConfigureOptions(x => x.IgnoreField(fieldPath));
        }

        /// <summary>
        /// With the <see cref="IgnoreFields"/> option, you can ignore the given fields
        /// during snapshot comparison. Therefore the fields will just be skipped during compare.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="fieldsPath">The json path to the fields to ignore.</param>
        public static ISnapshotBuilder IgnoreFields(
            this ISnapshotBuilder builder,
            string fieldsPath)
        {
            return builder.ConfigureOptions(x => x.IgnoreFields(fieldsPath));
        }

        /// <summary>
        /// With the <see cref="IgnoreField"/> option, you can ignore the given field
        /// during snapshot comparison. Therefore the field will just be skipped during compare.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="fieldPath">The json path to the field to ignore.</param>
        public static ISnapshotBuilder IgnoreField<T>(
            this ISnapshotBuilder builder,
            string fieldPath)
        {
            return builder.ConfigureOptions(x => x.IgnoreField<T>(fieldPath));
        }

        /// <summary>
        /// With the <see cref="IgnoreFields"/> option, you can ignore the given fields
        /// during snapshot comparison. Therefore the fields will just be skipped during compare.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="fieldsPath">The json path to the fields to ignore.</param>
        public static ISnapshotBuilder IgnoreFields<T>(
            this ISnapshotBuilder builder,
            string fieldsPath)
        {
            return builder.ConfigureOptions(x => x.IgnoreFields<T>(fieldsPath));
        }

        /// <summary>
        /// With the <see cref="Ignore"/> option, you can ignore the given field during snapshot
        /// comparison. Therefore the field will just be skipped during compare.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="ignoreFieldOption">The snapshot field to ignore.</param>
        public static ISnapshotBuilder Ignore(
            this ISnapshotBuilder builder,
            Func<FieldOption, object> ignoreFieldOption)
        {
            return builder.ConfigureOptions(x => x.Ignore(ignoreFieldOption));
        }

        /// <summary>
        /// The method IsType allows you during the snapshot comparison,
        /// to check if a field is from a specific type. Therefore the given field will NOT
        /// be compared with the field of the snapshot, it will only be checked if the
        /// field has the given type.
        /// </summary>
        /// <typeparam name="T">The type which the field should have.</typeparam>
        /// <param name="builder">The builder</param>
        /// <param name="isTypeField">The field to check.</param>
        public static ISnapshotBuilder IsType<T>(
            this ISnapshotBuilder builder,
            Func<FieldOption, T> isTypeField)
        {
            return builder.ConfigureOptions(x => x.IsType<T>(isTypeField));
        }

        /// <summary>
        /// The method IsType allows you during the snapshot comparison,
        /// to check if a field is from a specific type. Therefore the given field will NOT
        /// be compared with the field of the snapshot, it will only be checked if the
        /// field has the given type.
        /// </summary>
        /// <typeparam name="T">The type which the field should have.</typeparam>
        /// <param name="isTypeField">The field to check.</param>
        /// <param name="builder">The builder</param>
        public static ISnapshotBuilder IsType<T>(
            this ISnapshotBuilder builder,
            Func<FieldOption, T[]> isTypeField)
        {
            return builder.ConfigureOptions(x => x.IsType<T>(isTypeField));
        }

        /// <summary>
        /// The method <see cref="IsTypeField{T}"/> allows you during the snapshot comparison,
        /// to check if a field is from a specific type. Therefore the given field will NOT
        /// be compared with the field of the snapshot, it will only be checked if the
        /// field is from the given type.
        /// </summary>
        /// <typeparam name="T">The type which the field should have.</typeparam>
        /// <param name="fieldPath">The json path of the field to check for the type.</param>
        /// <param name="builder">The builder</param>
        public static ISnapshotBuilder IsTypeField<T>(
            this ISnapshotBuilder builder,
            string fieldPath)
        {
            return builder.ConfigureOptions(x => x.IgnoreField<T>(fieldPath));
        }

        /// <summary>
        /// The method <see cref="IsTypeFields{T}"/> allows you during the snapshot comparison,
        /// to check if the given fields are from a specific type. Therefore the given fields
        /// will NOT be compared with the fields of the snapshot, it will only be checked if the
        /// fields are from the given type.
        /// </summary>
        /// <typeparam name="T">The type which the field should have.</typeparam>
        /// <param name="fieldsPath">The json path of the fields to check for the type.</param>
        /// <param name="builder">The builder</param>
        public static ISnapshotBuilder IsTypeFields<T>(
            this ISnapshotBuilder builder,
            string fieldsPath)
        {
            return builder.ConfigureOptions(x => x.IsTypeFields<T>(fieldsPath));
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
        /// <param name="builder">The builder</param>
        public static ISnapshotBuilder Assert(
            this ISnapshotBuilder builder,
            Action<FieldOption> assertAction)
        {
            return builder.ConfigureOptions(x => x.Assert(assertAction));
        }

        /// <summary>
        /// Configures the name extension. The value passed as <paramref name="extensions"/> will be
        /// added to the
        /// </summary>
        /// <param name="extensions">The name of the extension</param>
        /// <param name="builder">The builder</param>
        /// <returns></returns>
        public static ISnapshotBuilder NameExtension(
            this ISnapshotBuilder builder,
            string extensions)
        {
            return builder.NameExtension(new SnapshotNameExtension(extensions));
        }
    }
}
