namespace Alessa.Core.Entities.QueryModels
{
    /// <summary>
    /// Defines available operators for search fields for request.
    /// </summary>
    public enum EFilterOperator
    {
        /// <summary>
        /// Equals.
        /// </summary>
        Equals,
        /// <summary>
        /// Not equals.
        /// </summary>
        NotEquals,
        /// <summary>
        /// Less than.
        /// </summary>
        LessThan,
        /// <summary>
        /// Less or equals.
        /// </summary>
        LessOrEquals,
        /// <summary>
        /// Greater than.
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Greater or equals.
        /// </summary>
        GreaterOrEquals,
        /// <summary>
        /// Is in.
        /// </summary>
        In,
        /// <summary>
        /// Is not in.
        /// </summary>
        NotIn,
        /// <summary>
        /// Contains
        /// </summary>
        Contains,
        /// <summary>
        /// Does not contain
        /// </summary>
        NotContains,
        /// <summary>
        /// Is null
        /// </summary>
        IsNull,
        /// <summary>
        /// Is not null
        /// </summary>
        IsNotNull,
        /// <summary>
        /// Begins With.
        /// </summary>
        BeginsWith,
        /// <summary>
        /// Not Begins With.
        /// </summary>
        NotBeginsWith,
        /// <summary>
        /// Ends With.
        /// </summary>
        EndsWith,
        /// <summary>
        /// Not ends With.
        /// </summary>
        NotEndsWith
    }
}
