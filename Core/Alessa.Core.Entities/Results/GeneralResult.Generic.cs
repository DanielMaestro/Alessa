namespace Alessa.Core.Entities.Results
{
    /// <summary>
    /// Defines the generic <see cref="GeneralResult"/> class.
    /// </summary>
    public class GeneralResult<T> : GeneralResult
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public T Result { get; set; }
    }
}
