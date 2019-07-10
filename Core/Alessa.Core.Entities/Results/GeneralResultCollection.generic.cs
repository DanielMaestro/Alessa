using System.Collections.Generic;

namespace Alessa.Core.Entities.Results
{
    /// <summary>
    /// Defines a <see cref="GeneralResult"/> object with a collection of object of type <typeparamref name="T"/>.
    /// </summary>
    public class GeneralResultCollection<T>
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public ICollection<T> Result { get; set; }
    }
}
