using System.Collections.Generic;
using System.Text;

namespace Alessa.QueryBuilder.Entities.BuilderParameters
{
    /// <summary>
    /// Query parameters base class.
    /// </summary>
    public class ConfigurationParameters : IBuilderParameters
    {
        /// <summary>
        /// Gets or sets the query source.
        /// </summary>
        public virtual EQuerySource QuerySource { get; set; }
        /// <summary>
        /// Gets or sets the query type.
        /// </summary>
        public virtual EQueryType QueryType { get; set; }
        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        public virtual string ItemName { get; set; }
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public virtual string UserId { get; set; }
        /// <summary>
        /// Gets or sets the required fields.
        /// </summary>
        public virtual IDictionary<string, object> AdditionalParameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("QuerySource = {0}", this.QuerySource).Append(",");
            builder.AppendFormat("QueryType = {0}", this.QueryType).Append(",");
            builder.AppendFormat("UserId = {0}", this.UserId).Append(",");
            builder.AppendFormat("ItemName = {0}", this.ItemName).Append(",");
            return builder.ToString();
        }
    }
}
