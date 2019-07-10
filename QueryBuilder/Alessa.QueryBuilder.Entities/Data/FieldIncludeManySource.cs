namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// The include many configuration Table.
    /// </summary>
    public class FieldIncludeManySource
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
        /// Gets or sets the Foreign key name.
        /// </summary>
        public virtual string ForeignKey { get; set; }
        /// <summary>
        /// Gets or sets the Local key name.
        /// </summary>
        public virtual string LocalKey { get; set; }

        /// <summary>
        /// Navigation property for <see cref="FieldDefinition"/> table.
        /// </summary>
        public virtual FieldDefinition FieldDefinition { get; set; }
        /// <summary>
        /// Navigation property for <see cref="ExecutionSource"/> table.
        /// </summary>
        public virtual ExecutionSource ExecutionSource { get; set; }
    }
}
