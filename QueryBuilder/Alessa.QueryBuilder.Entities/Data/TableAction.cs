namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// A table action class.
    /// </summary>
    public class TableAction
    {
        /// <summary>
        /// The table definition identifier.
        /// </summary>
        public virtual int TableDefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="ExecutionSource"/> identifier.
        /// </summary>
        public virtual int ExecutionSourceId { get; set; }
        /// <summary>
        /// Gets or sets the Database event type.
        /// </summary>
        public virtual ETableDbEventType TableDbEventType { get; set; }
        /// <summary>
        /// Gets or sets the execution order.
        /// </summary>
        public virtual int ExecutionOrder { get; set; }

        /// <summary>
        /// Navigation property for <see cref="ExecutionSource"/>.
        /// </summary>
        public virtual ExecutionSource ExecutionSource { get; set; }
        /// <summary>
        /// Navigation property for <see cref="TableDefinition"/>.
        /// </summary>
        public virtual TableDefinition TableDefinition { get; set; }
    }
}
