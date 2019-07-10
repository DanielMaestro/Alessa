using Alessa.ALex.SqlKata;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Diagnostics;
using TesterBase;

namespace ALex.Test
{
    public abstract class ALexTesterBase : TestLogBase
    {
        protected static readonly SqlKata.Compilers.Compiler _Compiler = new SqlKata.Compilers.SqlServerCompiler();
        protected static readonly QueryFactory QueryFactory = new QueryFactory();

        static ALexTesterBase()
        {
        }

        public ALexTesterBase() : base(@"C:\Temp\ALexTest.log")
        {
            // Loads this in order to improve the test performance.
            // If I don't do this the first load last up to 300ms the first time.
            this.GetParser().ParseToQuery("From(something)Select(F1)");
        }

        protected Query GetQuery(string query)
        {
            Func<Query, string> logQuery = (q) => this.GetSentence(q).Replace("\r\n", " ").Replace("\n", " ");
            var result = base.Execute(() => this.GetParser().ParseToQuery(query), logQuery, this.GetType());

            return result;
        }


        protected abstract ParserBase GetParser();

        protected string GetSentence(Query query)
        {
            var comp = _Compiler.Compile(query);
            return comp.ToString().Replace("\n", "\r\n");
        }
    }
}
