using Microsoft.Extensions.Logging;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Text;

namespace Alessa.QueryBuilder
{
    /// <summary>
    /// Builder options class.
    /// </summary>
    public sealed class QueryBuilderOptions
    {
        private ILogger _Logger;
        /// <summary>
        /// Gets the logger object.
        /// </summary>
        public ILogger Logger
        {
            get
            {
                if (this._Logger == null)
                {
                    this._Logger = new LoggerFactory().CreateLogger<QueryBuilderOptions>();
                }

                return this._Logger;
            }
            internal set
            {
                this._Logger = value;
            }
        }

        /// <summary>
        /// Gets the connections pool.
        /// </summary>
        public IDictionary<string, QueryFactory> ConnectionsPool { get; } = new Dictionary<string, QueryFactory>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("ConnectionsPool:\tCount = {0}", this.ConnectionsPool.Count);
            return builder.ToString();
        }
    }
}
