using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// The field definition class.
    /// </summary>
    public class FieldDefinition
    {
        /// <summary>
        /// Gets or sets the <see cref="FieldDefinition"/> identifier.
        /// </summary>
        public virtual int FieldDefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        public virtual string ItemName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this field is a key field or not.
        /// </summary>
        public virtual bool IsKey { get; set; }
        /// <summary>
        /// Gets or sets the field lenght.
        /// </summary>
        public virtual int? FieldLength { get; set; }
        /// <summary>
        /// Gets or sets the field type.
        /// </summary>
        public virtual EFieldType FieldType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this entity is enabled or not.
        /// </summary>
        public virtual bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this field is identity or not.
        /// </summary>
        public virtual bool IsIdentity { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="TableDefinition"/> identifier.
        /// </summary>
        public virtual int TableDefinitionId { get; set; }

        /// <summary>
        /// Navigation property for <see cref="TableDefinition"/> table.
        /// </summary>
        public virtual TableDefinition TableDefinition { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldDefinitionUi"/> table.
        /// </summary>
        public virtual FieldDefinitionUi FieldDefinitionUi { get; set; }

        /// <summary>
        /// Navigation property for <see cref="FieldListSource"/> table.
        /// </summary>
        public virtual ICollection<FieldListSource> FieldListSources { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldListSource"/> table.
        /// </summary>
        public virtual ICollection<FieldListSource> RequiredFieldListSources { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldIncludeManySource"/> table.
        /// </summary>
        public virtual ICollection<FieldIncludeManySource> FieldIncludeManySources { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldKeysRelationship"/> table.
        /// </summary>
        public virtual ICollection<FieldKeysRelationship> FieldKeysRelationships { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldKeysRelationship"/> table.
        /// </summary>
        public virtual ICollection<FieldKeysRelationship> FieldSaveTargetIds { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldKeysRelationship"/> table.
        /// </summary>
        public virtual ICollection<FieldKeysRelationship> KeyFieldIds { get; set; }
        /// <summary>
        /// Navigation property for <see cref="TableFieldValidation"/> table.
        /// </summary>
        public virtual ICollection<TableFieldValidation> TableFieldValidations { get; set; }
    }
}
