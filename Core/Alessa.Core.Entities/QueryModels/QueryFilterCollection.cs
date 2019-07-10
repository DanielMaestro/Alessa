using System.Text;

namespace Alessa.Core.Entities.QueryModels
{
    /// <summary>
    /// Class which represents filters.
    /// </summary>
    public class QueryFilterCollection
    {
        #region Properties
        /// <summary>
        /// Gets the operator grouping the filters.
        /// </summary>
        public virtual EGroupOperator GroupingOperator { get; set; }
        /// <summary>
        /// Gets the list of filters.
        /// </summary>
        public virtual System.Collections.Generic.List<QueryFilter> QueryFilters { get; }
        /// <summary>
        /// Gets the list of filters sub groups.
        /// </summary>
        public virtual System.Collections.Generic.List<QueryFilterCollection> Groups { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFilterCollection"/> class.
        /// </summary>
        public QueryFilterCollection()
        {
            QueryFilters = new System.Collections.Generic.List<QueryFilter>();
            Groups = new System.Collections.Generic.List<QueryFilterCollection>();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("GroupingOperator = {0}", this.GroupingOperator).Append(",");
            builder.AppendFormat("QueryFilters = {0}", string.Join(", ", this.QueryFilters)).Append(",");
            builder.AppendFormat("Groups = {0}", string.Join(", ", this.Groups));


            return builder.ToString();
        }
    }
}
