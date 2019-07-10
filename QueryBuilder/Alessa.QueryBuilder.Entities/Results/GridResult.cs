namespace Alessa.QueryBuilder.Entities.Results
{
    /// <summary>
    /// Defines the grid result.
    /// </summary>
    public class GridResult : DataResult
    {
        /// <summary>
        /// Gets or sets the total records.
        /// </summary>
        public virtual int TotalRecords { get; set; }
    }
}
