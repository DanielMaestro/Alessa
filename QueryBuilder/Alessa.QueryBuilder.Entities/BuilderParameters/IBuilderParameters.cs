using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.BuilderParameters
{
    /// <summary>
    /// Defines the query source and query type.
    /// </summary>
    public interface IBuilderParameters
    {
        /// <summary>
        /// Gets or sets the query source.
        /// </summary>
        EQuerySource QuerySource { get; set; }
        /// <summary>
        /// Gets or sets the query type.
        /// </summary>
        EQueryType QueryType { get; set; }
        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        string ItemName { get; set; }
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        string UserId { get; set; }
        /// <summary>
        /// Gets or sets the additional parameters for this builder.
        /// </summary>
        IDictionary<string, object> AdditionalParameters { get; set; }
    }
}
