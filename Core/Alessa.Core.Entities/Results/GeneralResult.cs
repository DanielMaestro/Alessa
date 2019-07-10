using System.Collections.Generic;

namespace Alessa.Core.Entities.Results
{
    /// <summary>
    /// Provides a general result.
    /// </summary>
    public class GeneralResult
    {
        /// <summary>
        /// Gets or sets the message list.
        /// </summary>
        public virtual IList<GeneralMessage> Messages { get; set; } = new List<GeneralMessage>();

        /// <summary>
        /// Gets or sets the message origin.
        /// </summary>
        public virtual string Origin { get; set; }
        /// <summary>
        /// Gets or sets the result code.
        /// </summary>
        public virtual int Code { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the result has an error or not.
        /// </summary>
        public bool HasError { get; set; }
    }
}
