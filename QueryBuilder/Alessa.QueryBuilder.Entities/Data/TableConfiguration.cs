using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// The table configuration class.
    /// </summary>
    public class TableConfiguration
    {
        /// <summary>
        /// Gets or sets the <see cref="TableConfiguration"/> identifier.
        /// </summary>
        public virtual int TableConfigurationId { get; set; }
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; }
        /// <summary>
        /// Navigation property for <see cref="TableDefinition"/>
        /// </summary>
        public virtual ICollection<TableDefinition> TableDefinitions { get; set; }
        /// <summary>
        /// Navigation property for <see cref="ExecutionSource"/>
        /// </summary>
        public virtual ICollection<ExecutionSource> ExecutionSources { get; set; }
    }
}
