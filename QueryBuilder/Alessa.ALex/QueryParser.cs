using Irony.Parsing;
using System;
using System.Collections.Generic;

namespace Alessa.ALex
{
    /// <summary>
    /// An implemented class of the interface <see cref="IQueryParser{QType}"/>.
    /// </summary>
    public abstract class QueryParser<QType> : IQueryParser<QType>, IDisposable
    {
        /// <summary>
        /// The parser class.
        /// </summary>
        protected Parser Parser;

        /// <summary>
        /// Initializes a new instance of the class <see cref="QueryParser{QType}"/>.
        /// </summary>
        /// <param name="aLexGrammar">The ALex grammar.</param>
        public QueryParser(ALexGrammarBase aLexGrammar)
        {
            this.Parser = new Parser(aLexGrammar);
        }

        /// <summary>
        /// Gets a converted <typeparamref name="QType"/> object from the specified ALex string.
        /// </summary>
        /// <param name="aLexQuery">The ALex query.</param>
        /// <returns></returns>
        public virtual QType ParseToQuery(string aLexQuery)
        {
            var parsed = this.Parser.Parse(aLexQuery);

            if (parsed.HasErrors())
            {
                var builder = new System.Text.StringBuilder("There are errors in the parser:\n");
                foreach (var item in parsed.ParserMessages)
                {
                    builder.AppendLine(item.Message);
                }
                var grammarError = new GrammarError(GrammarErrorLevel.Error, parsed.ParserMessages[0].ParserState, parsed.ParserMessages[0].Message);
                var ex = new GrammarErrorException(builder.ToString(), grammarError);
                throw new ALexException(builder.ToString(), 10, ex);
            }

            var result = this.GetQuery(parsed);

            return result;
        }
        /// <summary>
        /// Gets a converted <typeparamref name="QType"/> object from the specified ALex string and replaces any values contained in the <paramref name="values"/> data.
        /// </summary>
        /// <param name="aLexQuery">The ALex query. The labes are identified by double curly bracets {{}}</param>
        /// <param name="values">Data contained to replace in the <paramref name="aLexQuery"/> object.</param>
        public virtual QType ParseToQuery(string aLexQuery, IDictionary<string, object> values)
        {
            var parsedString = this.ParseValues(aLexQuery, values);
            var result = this.ParseToQuery(parsedString);
            return result;
        }

        /// <summary>
        /// Gets a converted <typeparamref name="QType"/> object from the specified ALex string and replaces any values contained in the properties of <paramref name="data"/> data.
        /// </summary>
        /// <param name="aLexQuery">The ALex query. The labes are identified by double curly bracets {{}}</param>
        /// <param name="data">Data contained to replace in the <paramref name="aLexQuery"/> object.</param>
        public virtual QType ParseToQuery(string aLexQuery, object data)
        {
            var parsedString = this.ParseValues(aLexQuery, data);
            var result = this.ParseToQuery(parsedString);
            return result;
        }
        /// <summary>
        /// Gets a parsed string from the specified ALex string and replaces any values contained in the <paramref name="values"/> data.
        /// </summary>
        /// <param name="aLexQuery">The ALex query. The labes are identified by double curly bracets {{}}</param>
        /// <param name="values">Data contained to replace in the <paramref name="aLexQuery"/> object.</param>
        public string ParseValues(string aLexQuery, IDictionary<string, object> values)
        {
            return aLexQuery.FormatQuery(values);
        }

        /// <summary>
        /// Gets a parsed string from the specified ALex string and replaces any values contained in the properties of <paramref name="data"/> data.
        /// </summary>
        /// <param name="aLexQuery">The ALex query. The labes are identified by double curly bracets {{}}</param>
        /// <param name="data">Data contained to replace in the <paramref name="aLexQuery"/> object.</param>
        public string ParseValues(string aLexQuery, object data)
        {
            return aLexQuery.FormatQuery(data);
        }

        /// <summary>
        /// Gets the query of type <typeparamref name="QType"/>.
        /// </summary>
        /// <param name="parseTree">The parser tree.</param>
        /// <returns></returns>
        protected abstract QType GetQuery(ParseTree parseTree);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Parser = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~QueryParser() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        /// <summary>
        /// Dispose method.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
