using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Script;
using Xunit;

namespace Alessa.ServiceStack.Test
{
    public class TestBase//: IClassFixture<TestBase>
    {
        private ScriptContext ScriptContext;
        public TestBase()
        {
            var context = new ScriptContext
            {
                ScriptMethods = { new DbScripts() },

            };

            context.Container.AddSingleton<IDbConnectionFactory>(() => new OrmLiteConnectionFactory(
                 TesterBase.Constants.AlessaConnectionString, SqlServer2012Dialect.Provider));
            this.ScriptContext = context.Init();

            //context.Container.AddSingleton<IDbConnectionFactory>(() =>
            //{
            //    OrmLiteConnectionFactory factory = new OrmLiteConnectionFactory()
            //    {
            //        DialectProvider = SqlServer2016Dialect.Provider
            //    };
            //    factory.RegisterConnection("DefaultConnection", TesterBase.Constants.AlessaConnectionString, SqlServer2016Dialect.Provider);
            //    factory.RegisterConnection("TestConnection1", TesterBase.Constants.AlessaConnectionString, SqlServer2016Dialect.Provider);
            //    factory.RegisterConnection("TestConnection2", TesterBase.Constants.AlessaConnectionString, SqlServer2016Dialect.Provider);
            //    return factory;
            //});

            this.ScriptContext = context.Init();
        }

        //class AppHost : AppSelfHostBase
        //{
        //    public AppHost() : base(nameof(TestBase), typeof(TestBase).Assembly) { }

        //    public override void Configure(Container container)
        //    {
        //        OrmLiteConnectionFactory factory = new OrmLiteConnectionFactory();
        //        container.AddSingleton<IDbConnectionFactory>((e) =>
        //        {
        //            factory.RegisterConnection("DefaultConnection", TesterBase.Constants.AlessaConnectionString, SqlServer2016Dialect.Provider);
        //            factory.RegisterConnection("TestConnection1", TesterBase.Constants.AlessaConnectionString, SqlServer2016Dialect.Provider);
        //            factory.RegisterConnection("TestConnection2", TesterBase.Constants.AlessaConnectionString, SqlServer2016Dialect.Provider);
        //            return factory;
        //        });
        //    }

        //}


        [Fact]
        public void MyTest()
        {
            //var r = this.ScriptContext.EvaluateScript(@"Time is now: {{ now | dateFormat('HH:mm:ss') }}");
            var r = this.ScriptContext.EvaluateScript(@"{{ sql | dbSelect({ DefaultConnection }) }}");
            //            var r = this.ScriptContext.EvaluateScript(@"{{ sql | dbSelect({ DefaultConnection }) }}
            //{{ ""select Id, CompanyName, ContactName, ContactTitle, City, Country from Customer"" | assignTo: sql }}");
        }
    }
}
