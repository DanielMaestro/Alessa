namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// Field keys relationship table.
    /// </summary>
    public class FieldKeysRelationship
    {
        /// <summary>
        /// Gets or sets the <see cref="FieldDefinition"/> identifier.
        /// </summary>
        public virtual int FieldDefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the target <see cref="FieldDefinition"/> identifier.
        /// </summary>
        public virtual int FieldSaveTargetId { get; set; }
        /// <summary>
        /// Gets or sets the key for <see cref="FieldDefinition"/> identifier.
        /// </summary>
        public virtual int KeyFieldId { get; set; }

        /// <summary>
        /// Navigation property for <see cref="FieldDefinition"/> table.
        /// </summary>
        public virtual FieldDefinition FieldDefinition { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldDefinition"/> table.
        /// </summary>
        public virtual FieldDefinition FieldSaveTarget { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldDefinition"/> table.
        /// </summary>
        public virtual FieldDefinition KeyField { get; set; }
    }
}
