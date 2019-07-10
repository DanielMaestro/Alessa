using System;
using System.Collections;
using System.Text;

namespace Alessa.QueryBuilder
{
    /// <summary>
    /// Exception for query builder.
    /// </summary>
    public class QueryBuilderException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="QueryBuilderException"/> class with the specified <see cref="SchemaContext"/> object, the inner exception and message are set as well.
        /// </summary>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="errorCode">The error code for this exception.</param>
        public QueryBuilderException(int errorCode, string message, Exception innerException) : base(message, innerException)
        {
            this.ErrorCode = errorCode;
        }
        /// <summary>
        /// Gets the error code.
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// Creates and returns a string representation of the current exception.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder(base.ToString());

            builder.AppendFormat("Exception ErrorCode:\t\t{0}", this.ErrorCode);

            if (base.Data.Count > 0)
            {
                builder
                    .AppendLine()
                    .AppendLine("Additional data:")
                    .AppendLine();

                foreach (DictionaryEntry item in base.Data)
                {
                    builder.AppendFormat("{0}\t\t\t{1}", item.Key, item.Value).AppendLine();
                }
            }
            return builder.ToString();
        }
    }
}
