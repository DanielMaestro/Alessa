using Alessa.Core.Entities.QueryModels;
using System.Collections.Generic;
using System.Text;

namespace Alessa.QueryBuilder.Entities.BuilderParameters
{
    /// <summary>
    /// The data parameters class.
    /// </summary>
    public class DataParameters : QueryParameters, IBuilderParameters
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
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder(base.ToString());
            builder.AppendFormat(",QuerySource = {0}", this.QuerySource).Append(",");
            builder.AppendFormat("QueryType = {0}", this.QueryType).Append(",");
            builder.AppendFormat("ItemName = {0}", this.ItemName).Append(",");
            return builder.ToString();
        }
    }
}
