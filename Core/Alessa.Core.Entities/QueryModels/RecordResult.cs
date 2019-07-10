namespace Alessa.Core.Entities.QueryModels
{
    /// <summary>
    /// Class which represents a query response.
    /// </summary>
    public class RecordResult
    {
        /// <summary>
        /// Gets or sets the index (zero based) of page to return.
        /// </summary>
        public virtual int PageIndex { get; set; }
        /// <summary>
        /// Gets or sets total pages count.
        /// </summary>
        public virtual int TotalPagesCount { get; set; }

        /// <summary>
        /// Gets or sets total records count.
        /// </summary>
        public virtual int TotalRecordsCount { get; set; }
        /// <summary>
        /// Gets or sets the records filtered.
        /// </summary>
        public virtual int RecordsFiltered { get; set; }
        /// <summary>
        /// Gets the rows list.
        /// </summary>
        public virtual dynamic Records { get; set; }

        /// <summary>
        /// Gets or sets custom data that will be send with the response.
        /// </summary>
        public virtual object UserData { get; set; }
    }
}
