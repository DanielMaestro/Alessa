using Alessa.ALex.SqlKata;

namespace ALex.Test
{
    public class ALexTesterFull : ALexTesterBase
    {
        public ALexTesterFull()
        {
        }
        //private static readonly QueryParserFull queryParserFull = new QueryParserFull();
        protected override ParserBase GetParser()
        {
            return new ParserFull(QueryFactory);
            //return queryParserFull;
        }
    }
}
