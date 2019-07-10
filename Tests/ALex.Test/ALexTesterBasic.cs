using Alessa.ALex.SqlKata;

namespace ALex.Test
{
    public class ALexTesterBasic : ALexTesterBase
    {
        public ALexTesterBasic()
        {
        }
        protected override ParserBase GetParser()
        {
            return new ParserBasic(QueryFactory);
        }
    }
}
