using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// The client side validation class.
    /// </summary>
    public class DataValidation
    {
        /// <summary>
        /// The statement to validate.
        /// </summary>
        public virtual string Statement { get; set; }
        /// <summary>
        /// Gets or sets the execution result type.
        /// </summary>
        public virtual EExecutionResultType ExecutionResultType { get; set; }
        /// <summary>
        /// Gets or sets the fields for executing the statement.
        /// </summary>
        public virtual ICollection<string> Fields { get; set; }
        /// <summary>
        /// Gets or sets whether the statement must be executed on server side or client side.
        /// </summary>
        public virtual bool ExecuteOnClientSide { get; set; }
    }
}
