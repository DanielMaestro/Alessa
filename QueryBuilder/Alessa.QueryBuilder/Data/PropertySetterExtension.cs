using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alessa.QueryBuilder
{
    /// <summary>
    /// A property setter extension just to make easier the fluent API mapping.
    /// </summary>
    internal static class PropertySetterExtension
    {
        /// <summary>
        /// Sets the table name.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        /// <param name="tableName">Table name.</param>
        /// <param name="schema">Schema name.</param>
        /// <returns></returns>
        internal static EntityTypeBuilder<TEntity> SetTable<TEntity>(this EntityTypeBuilder<TEntity> builder, string tableName, string schema = null)
            where TEntity : class, new()
        {
            if (!string.IsNullOrWhiteSpace(schema))
                builder.ToTable(tableName, schema);
            else
                builder.ToTable(tableName);

            return builder;
        }

        /// <summary>
        /// Sets the display name property.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        /// <param name="isRequired">Whether this field is required or not.</param>
        /// <param name="maxLength">The max length for this string ficolumn.</param>
        /// <returns></returns>
        internal static PropertyBuilder<string> SetVarcharProperty(this PropertyBuilder<string> builder, int maxLength = 150, bool isRequired = true)
        {
            builder.SetVarcharProperty<string>(maxLength, isRequired);

            return builder;
        }
        /// <summary>
        /// Sets the display name property.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        /// <param name="isRequired">Whether this field is required or not.</param>
        /// <param name="maxLength">The max length for this string ficolumn.</param>
        /// <returns></returns>
        internal static PropertyBuilder<TProperty> SetVarcharProperty<TProperty>(this PropertyBuilder<TProperty> builder, int maxLength = 150, bool isRequired = true)
        {
            // Only sets the unicode.
            if (maxLength >= 8000)
            {
                builder.IsUnicode(false);
            }
            else
            {
                builder
                    .HasMaxLength(maxLength)
                    .IsUnicode(false);
            }

            if (isRequired)
                builder.IsRequired();

            return builder;
        }
        /// <summary>
        /// Sets the property as bit.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        /// <param name="isRequired">Whether this field is required or not.</param>
        /// <returns></returns>
        internal static PropertyBuilder<bool> SetBitProperty(this PropertyBuilder<bool> builder, bool isRequired = true)
        {
            if (isRequired)
                builder.IsRequired();

            return builder;
        }
        /// <summary>
        /// Sets the property as nullable integer.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        /// <param name="isRequired">Whether this field is required or not.</param>
        /// <returns></returns>
        internal static PropertyBuilder<int?> SetIntProperty(this PropertyBuilder<int?> builder, bool isRequired = true)
        {
            if (isRequired)
                builder.IsRequired();

            return builder;
        }
        /// <summary>
        /// Sets the property as integer.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        /// <param name="isRequired">Whether this field is required or not.</param>
        /// <returns></returns>
        internal static PropertyBuilder<int> SetIntProperty(this PropertyBuilder<int> builder, bool isRequired = true)
        {
            if (isRequired)
                builder.IsRequired();

            return builder;
        }

        /// <summary>
        /// Sets the property as integer.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        /// <param name="isRequired">Whether this field is required or not.</param>
        /// <returns></returns>
        internal static PropertyBuilder<TProperty> SetEnumProperty<TProperty>(this PropertyBuilder<TProperty> builder, bool isRequired = true)
            where TProperty : System.Enum
        {

            builder
                .SetVarcharProperty(30)
                .HasConversion(
                    e => e.ToString(),
                    e => (TProperty)System.Enum.Parse(typeof(TProperty), e));

            if (isRequired)
                builder.IsRequired();

            return builder;
        }
        /// <summary>
        /// Sets the property as integer.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        /// <param name="isRequired">Whether this field is required or not.</param>
        /// <returns></returns>
        internal static PropertyBuilder<TProperty> SetDoubleProperty<TProperty>(this PropertyBuilder<TProperty> builder, bool isRequired = true)
        {
            if (isRequired)
                builder.IsRequired();

            return builder;
        }

        /// <summary>
        /// Sets the property as integer.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        /// <param name="isRequired">Whether this field is required or not.</param>
        /// <returns></returns>
        internal static PropertyBuilder<System.Collections.Generic.ICollection<string>> SetStringArray(this PropertyBuilder<System.Collections.Generic.ICollection<string>> builder, bool isRequired = true)
        {
            const string separator = ", ";
            builder.SetVarcharProperty(512, isRequired)
                 .HasConversion(
                     v => v != null ? string.Join(separator, v) : null,
                     v => v != null ? v.Split(separator, System.StringSplitOptions.RemoveEmptyEntries) : null);

            return builder;
        }
    }
}
