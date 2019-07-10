using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// The execution source table. Contains the execution instructions.
    /// </summary>
    public class ExecutionSource
    {
        /// <summary>
        /// Gets or sets the <see cref="ExecutionSource"/> identifier.
        /// </summary>
        public virtual int ExecutionSourceId { get; set; }
        /// <summary>
        /// Gets or sets the execution type.
        /// </summary>
        public virtual EExecutionType ExecutionType { get; set; }
        /// <summary>
        /// Gets or sets the execution description.
        /// </summary>
        public virtual string ExecutionDescription { get; set; }
        /// <summary>
        /// Gets or sets the execution text.
        /// </summary>
        public virtual string ExecutionText { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="TableConfiguration"/> identifier.
        /// </summary>
        public virtual int TableConfigurationId { get; set; }


        /// <summary>
        /// Navigation property for <see cref="TableConfiguration"/> table.
        /// </summary>
        public virtual TableConfiguration TableConfiguration { get; set; }
        /// <summary>
        /// Navigation property for <see cref="TableAction"/> table.
        /// </summary>
        public virtual ICollection<TableAction> TableActions { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldListSource"/> table.
        /// </summary>
        public virtual ICollection<FieldListSource> FieldListSources { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldIncludeManySource"/> table.
        /// </summary>
        public virtual ICollection<FieldIncludeManySource> FieldIncludeManySources { get; set; }
        /// <summary>
        /// Navigation property for <see cref="TableFieldValidation"/> table.
        /// </summary>
        public virtual ICollection<TableFieldValidation> TableFieldValidations { get; set; }
    }
}
