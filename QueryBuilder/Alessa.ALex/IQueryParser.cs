using System.Collections.Generic;

namespace Alessa.ALex
{
    /// <summary>
    /// The query parser interface.
    /// </summary>
    /// <typeparam name="QType">The query result after the parse process.</typeparam>
    public interface IQueryParser<QType>
    {
        /// <summary>
        /// Gets a converted <typeparamref name="QType"/> object from the specified ALex string.
        /// </summary>
        /// <param name="aLexQuery">The ALex query.</param>
        /// <returns></returns>
        QType ParseToQuery(string aLexQuery);
        /// <summary>
        /// Gets a converted <typeparamref name="QType"/> object from the specified ALex string and replaces any values contained in the <paramref name="values"/> data.
        /// </summary>
        /// <param name="aLexQuery">The ALex query. The labes are identified by double curly bracets {{}}</param>
        /// <param name="values">Data contained to replace in the <paramref name="aLexQuery"/> object.</param>
        QType ParseToQuery(string aLexQuery, IDictionary<string, object> values);
        /// <summary>
        /// Gets a converted <typeparamref name="QType"/> object from the specified ALex string and replaces any values contained in the properties of <paramref name="data"/> data.
        /// </summary>
        /// <param name="aLexQuery">The ALex query. The labes are identified by double curly bracets {{}}</param>
        /// <param name="data">Data contained to replace in the <paramref name="aLexQuery"/> object.</param>
        QType ParseToQuery(string aLexQuery, object data);

        /// <summary>
        /// Gets a parsed string from the specified ALex string and replaces any values contained in the <paramref name="values"/> data.
        /// </summary>
        /// <param name="aLexQuery">The ALex query. The labes are identified by double curly bracets {{}}</param>
        /// <param name="values">Data contained to replace in the <paramref name="aLexQuery"/> object.</param>
        string ParseValues(string aLexQuery, IDictionary<string, object> values);
        /// <summary>
        /// Gets a parsed string from the specified ALex string and replaces any values contained in the properties of <paramref name="data"/> data.
        /// </summary>
        /// <param name="aLexQuery">The ALex query. The labes are identified by double curly bracets {{}}</param>
        /// <param name="data">Data contained to replace in the <paramref name="aLexQuery"/> object.</param>
        string ParseValues(string aLexQuery, object data);
    }
}
