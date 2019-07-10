namespace Alessa.Core.Entities.QueryModels
{
    /// <summary>
    /// Defines available group operators for filtering.
    /// </summary>
    public enum EGroupOperator
    {
        /// <summary>
        /// There is no operator.
        /// </summary>
        None,
        /// <summary>
        /// All
        /// </summary>
        And,
        /// <summary>
        /// Any
        /// </summary>
        Or
    }
}