using SqlKata.Execution;

namespace Alessa.ALex.SqlKata
{
    /// <summary>
    /// Query builder class.
    /// </summary>
    public sealed class ParserFull : ParserBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ParserFull"/> class.
        /// </summary>
        /// <param name="queryFactory">The query factory.</param>
        public ParserFull(QueryFactory queryFactory) : base(new ALexGrammarFull(), queryFactory)
        {
        }
    }
}
