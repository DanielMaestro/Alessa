using System;

namespace Alessa.ALex
{
    /// <summary>
    /// The ALex exception.
    /// </summary>
    public class ALexException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ALexException"/> class.
        /// </summary>
        /// <param name="message">User message.</param>
        /// <param name="hResult">The error code assigned to <see cref="System.Exception.HResult"/> property.</param>
        /// <param name="innerException">The inner exception.</param>
        public ALexException(string message, int hResult, Exception innerException = null) : base(message, innerException)
        {
            base.HResult = HResult;
        }

    }
}
