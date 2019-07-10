using Alessa.Core.Entities.QueryModels;
using Alessa.QueryBuilder;
using Alessa.QueryBuilder.Entities;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using System.Collections.Generic;
using TesterBase;
using TesterBase.DataContext;

namespace QueryBuilder.Test
{
    public abstract class SchemaTestBase : TestLogBase
    {
        protected readonly SchemaConfigurations SchemaConfigurations;
        protected readonly SchemaData SchemaData;

        static SchemaTestBase()
        {
        }

        public SchemaTestBase(string logPath) : base(logPath)
        {
            var factory = new SqlQueryBuilderTestFactory();

            var db = factory.CreateDbContext(null);
            var options = factory.GetQueryBuilderOptions();

            var context = new SchemaContext(options, db);
            SchemaData = new SchemaData(context);
            SchemaConfigurations = new SchemaConfigurations(context);

            // Loads this in order to improve the test performance.
            // If I don't do this the first load last up to 300ms the first time.
            var parameters = this.GetDataParameters("BasicColumnType", EQueryType.DetailListView, 1, 0);

            SchemaConfigurations.GetEditConfigAsync(parameters).Wait();
            SchemaData.GetDataAsync(parameters).Wait();
        }

        protected ConfigurationParameters GetConfigurationParameters(string itemName, EQueryType queryType)
        {
            var result = new ConfigurationParameters()
            {
                ItemName = itemName,
                QueryType = queryType,
            };

            return result;
        }


        protected DataParameters GetDataParameters(string itemName, EQueryType queryType, int rowCount = 3, int page = 0, QueryFilterCollection queryFilterCollection = null, IEnumerable<SortingName> sortingNames = null)
        {
            var result = new DataParameters()
            {
                ItemName = itemName,
                PageIndex = page,
                QueryType = queryType,
                RecordsCount = rowCount
            };

            if (sortingNames != null)
            {
                result.SortingNames.AddRange(sortingNames);
            }

            if (queryFilterCollection != null)
            {
                result.FilterCollection.Groups.AddRange(queryFilterCollection.Groups);
                result.FilterCollection.QueryFilters.AddRange(queryFilterCollection.QueryFilters);
                result.FilterCollection.GroupingOperator = queryFilterCollection.GroupingOperator;
            }

            return result;
        }


        protected string GetResultString<T>(T obj)
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return result;
        }

    }
}
