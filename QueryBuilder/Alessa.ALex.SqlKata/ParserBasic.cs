using SqlKata.Execution;

namespace Alessa.ALex.SqlKata
{
    /// <summary>
    /// The query builder basic class to parse the basic languaje.
    /// </summary>
    public sealed class ParserBasic : ParserBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ParserBasic"/> class.
        /// </summary>
        /// <param name="queryFactory">The query factory.</param>
        public ParserBasic(QueryFactory queryFactory) : base(new ALexGrammarBasic(), queryFactory)
        {
        }
    }
}
