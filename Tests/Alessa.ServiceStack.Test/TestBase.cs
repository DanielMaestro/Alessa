using Funq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Script;
using Xunit;

namespace Alessa.ServiceStack.Test
{
    public class TestBase
    {
        class AppHost : AppSelfHostBase
        {
            public AppHost() : base(nameof(TestBase), typeof(TestBase).Assembly) { }

            public override void Configure(Container container)
            {
                OrmLiteConnectionFactory factory = new OrmLiteConnectionFactory();
                container.AddSingleton<IDbConnectionFactory>((e) =>
                {
                    factory.RegisterConnection("DefaultConnection", TesterBase.Constants.AlessaConnectionString, SqlServer2016Dialect.Provider);
                    factory.RegisterConnection("TestConnection1", TesterBase.Constants.AlessaConnectionString, SqlServer2016Dialect.Provider);
                    factory.RegisterConnection("TestConnection2", TesterBase.Constants.AlessaConnectionString, SqlServer2016Dialect.Provider);
                    return factory;
                });
            }

        }

        [Fact]
        public void MyTest()
        {
            var context = new ScriptContext
            {
                ScriptMethods = { new DbScriptsAsync() }
            }.Init();

            var r = context.EvaluateScript(@"{{ sql | dbSelect({ DefaultConnection }) }}
{{ ""select Id, CompanyName, ContactName, ContactTitle, City, Country from Customer"" | assignTo: sql }}");
        }
    }
}
