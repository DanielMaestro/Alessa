namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// Table and fields validations table.
    /// </summary>
    public class TableFieldValidation
    {
        /// <summary>
        /// Gets or sets the changed event <see cref="FieldDefinition"/> identifier.
        /// </summary>
        public virtual int ChangeFieldDefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="ExecutionSource"/> identifier.
        /// </summary>
        public virtual int ExecutionSourceId { get; set; }
        /// <summary>
        /// Whether this validation is executed on client side.
        /// </summary>
        public virtual bool ValidateOnClient { get; set; }
        /// <summary>
        /// Gets or sets the execution result type.
        /// </summary>
        public virtual EExecutionResultType ExecutionResultType { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldDefinition"/> table.
        /// </summary>
        public virtual FieldDefinition ChangeFieldDefinition { get; set; }
        /// <summary>
        /// Navigation property for <see cref="ExecutionSource"/> table.
        /// </summary>
        public virtual ExecutionSource ExecutionSource { get; set; }
    }
}
