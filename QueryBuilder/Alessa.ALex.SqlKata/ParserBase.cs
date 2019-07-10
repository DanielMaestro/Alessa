using Irony.Parsing;
using SqlKata;
using SqlKata.Execution;

namespace Alessa.ALex.SqlKata
{
    /// <summary>
    /// The query parser base class.
    /// </summary>
    public abstract class ParserBase : QueryParser<Query>
    {
        private readonly QueryFactory _queryFactory;
        /// <summary>
        /// Initializes a new instance of <see cref="ParserBase"/> class.
        /// </summary>
        /// <param name="aLexGrammar">The grammar to use.</param>
        /// <param name="queryFactory">The query factory.</param>
        public ParserBase(ALexGrammarBase aLexGrammar, QueryFactory queryFactory) : base(aLexGrammar)
        {
            _queryFactory = queryFactory;
        }

        /// <summary>
        /// Gets a query to handle with dapper.
        /// </summary>
        /// <param name="parseTree">The parser tree.</param>
        /// <returns></returns>
        protected override Query GetQuery(ParseTree parseTree)
        {
            var query = _queryFactory.Query();

            for (var index = 0; index < parseTree.Root.ChildNodes.Count; index++)
            {
                QueryParser.SetNode(parseTree.Root.ChildNodes[index], query);
            }

            return query;
        }
    }
}
