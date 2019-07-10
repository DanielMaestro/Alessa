using System.Text;

namespace Alessa.Core.Entities.QueryModels
{
    /// <summary>
    /// Class which represents filter in a request.
    /// </summary>
    public class QueryFilter
    {
        #region Properties
        /// <summary>
        /// Gets or sets the group operator.
        /// </summary>
        public virtual EGroupOperator GroupOperator { get; set; }
        /// <summary>
        /// Gets the searching column name.
        /// </summary>
        public virtual string FieldName { get; set; }

        /// <summary>
        /// Gets the searching value.
        /// </summary>
        public virtual object SearchingValue { get; set; }

        /// <summary>
        /// Gets the searching operator.
        /// </summary>
        public virtual EFilterOperator SearchingOperator { get; set; }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("FieldName = {0}", this.FieldName).Append(",");
            builder.AppendFormat("SearchingValue = {0}", this.SearchingValue).Append(",");
            builder.AppendFormat("SearchingOperator = {0}", this.SearchingOperator);

            return builder.ToString();
        }
    }
}
