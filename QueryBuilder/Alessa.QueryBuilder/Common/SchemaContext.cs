namespace Alessa.QueryBuilder
{
    /// <summary>
    /// An schema context class.
    /// </summary>
    public sealed class SchemaContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SchemaContext"/> class.
        /// </summary>
        /// <param name="queryBuilderOptions">Query builder options.</param>
        /// <param name="queryBuilderDbContext">Query builder database context.</param>
        public SchemaContext(QueryBuilderOptions queryBuilderOptions, QueryBuilderDbContext queryBuilderDbContext)
        {
            this.QueryBuilderDbContext = queryBuilderDbContext;
            this.QueryBuilderOptions = queryBuilderOptions;
        }
        /// <summary>
        /// Gets or sets a query builder options.
        /// </summary>
        public QueryBuilderOptions QueryBuilderOptions { get; }
        /// <summary>
        /// Gets or sets the query builder database context.
        /// </summary>
        public QueryBuilderDbContext QueryBuilderDbContext { get; }
    }
}
