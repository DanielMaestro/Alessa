using System.Collections.Generic;
using System.Text;

namespace Alessa.Core.Entities.QueryModels
{
    /// <summary>
    /// Custom search request based on jqGridRequest.
    /// </summary>
    public class QueryParameters
    {
        /// <summary>
        /// Initializes a new instance of <see cref="QueryParameters"/> class.
        /// </summary>
        public QueryParameters()
        {
            this.SortingNames = new List<SortingName>();
            this.FilterCollection = new QueryFilterCollection();
        }

        #region Properties
        /// <summary>
        /// Gets a collection of query filters. 
        /// </summary>
        public virtual QueryFilterCollection FilterCollection { get; }

        /// <summary>
        /// Gets the sorting column names.
        /// </summary>
        public virtual List<SortingName> SortingNames { get; }
        /// <summary>
        /// Gets or sets the index (zero based) of page to return
        /// </summary>
        public virtual int PageIndex { get; set; }
        /// <summary>
        /// Gets the number of rows to return
        /// </summary>
        public virtual int RecordsCount { get; set; }
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public virtual string UserId { get; set; }
        /// <summary>
        /// Gets or sets an aditional data.
        /// </summary>
        public virtual IDictionary<string, object> AdditionalParameters { get; set; }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("PageIndex = {0}", this.PageIndex).Append(",");
            builder.AppendFormat("RecordsCount = {0}", this.RecordsCount).Append(",");
            builder.AppendFormat("UserId = {0}", this.UserId).Append(",");
            builder.AppendFormat("AditionalData = {0}", this.AdditionalParameters).Append(",");
            builder.AppendFormat("FilterCollection = {0}", this.FilterCollection).Append(",");

            builder.Append("SortingNames = ");
            foreach (var item in this.SortingNames)
            {
                builder.Append(item).Append(",");
            }

            return builder.ToString();
        }

    }
}
