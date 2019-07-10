namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// Field list source table.
    /// </summary>
    public class FieldListSource
    {
        /// <summary>
        /// Gets or sets the <see cref="FieldDefinition"/> identifier.
        /// </summary>
        public virtual int FieldDefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="ExecutionSource"/> identifier.
        /// </summary>
        public virtual int ExecutionSourceId { get; set; }
        /// <summary>
        /// Gets or sets the required <see cref="FieldDefinition"/> identifier.
        /// </summary>
        public virtual int RequiredFieldDefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="TableDefinition"/> identifier.
        /// </summary>
        public virtual int? TableDefinitionId { get; set; }

        /// <summary>
        /// Navigation property for <see cref="FieldDefinition"/> table.
        /// </summary>
        public virtual FieldDefinition FieldDefinition { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldDefinition"/> table.
        /// </summary>
        public virtual FieldDefinition RequiredFieldDefinition { get; set; }
        /// <summary>
        /// Navigation property for <see cref="ExecutionSource"/> table.
        /// </summary>
        public virtual ExecutionSource ExecutionSource { get; set; }
        /// <summary>
        /// Navigation property for <see cref="TableDefinition"/> table.
        /// </summary>
        public virtual TableDefinition TableDefinition { get; set; }

    }
}
