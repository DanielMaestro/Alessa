using Alessa.ALex;
using Alessa.Core.Entities.QueryModels;
using Alessa.Core.Helpers;
using Alessa.QueryBuilder.Entities;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using System;
using System.Linq;
using Xunit;

namespace QueryBuilder.Test.Data
{
    public abstract class DataTestBase : SchemaTestBase
    {
        protected readonly string ItemName;

        public DataTestBase(string itemName) : base(@"C:\Temp\ConfigurationTest.log")
        {
            ItemName = itemName;
        }

        [Fact]
        public void GetDetailListData()
        {
            var parameters = base.GetDataParameters(ItemName, EQueryType.DetailListView, 10, 0);
            var result = base.Execute(() => this.GetDetailListResult(parameters), (t) => this.GetResultString(t), this.GetType());

            Assert.True(result.Result.Count == 10);
        }

        [Fact]
        public void GetOnlyOneRecord()
        {
            var parameters = base.GetDataParameters(ItemName, EQueryType.GridView, 1, 0);
            var result = base.Execute(() => this.GetOnlyOneRecodResult(parameters), (t) => this.GetResultString(t), this.GetType());

            Assert.True(result.Result.Count == 1);
        }

        [Fact]
        public void GetThreeRecords()
        {
            var parameters = base.GetDataParameters(ItemName, EQueryType.GridView, 3, 0);
            var result = base.Execute(() => this.GetThreeRecodsResult(parameters), (t) => this.GetResultString(t), this.GetType());

            Assert.True(result.Result.Count == 3);
        }

        [Fact]
        public void UserExceptionTest()
        {
            try
            {
                var filters = new QueryFilterCollection();
                filters.QueryFilters.Add(new QueryFilter()
                {
                    FieldName = "RandomName",
                    GroupOperator = EGroupOperator.And,
                    SearchingOperator = EFilterOperator.Contains,
                    SearchingValue = 2
                });

                var sort = new SortingName[]
                    {
                    new SortingName()
                    {
                        ItemName = "OtherRandomName",
                        Order = "DESC"
                    }
                    };

                var parameters = base.GetDataParameters(ItemName, EQueryType.GridView, 0, 0, filters, sort);

                var result = base.Execute(() => SchemaData.GetDataAsync(parameters).Result, (t) => this.GetResultString(t), this.GetType());
            }
            catch (AggregateException ex)
            {
                if (!(ex.InnerException is ALexException))
                {
                    throw ex;
                }
            }
        }

        [Fact]
        public void GetSecondPageRecord()
        {
            var parameters = base.GetDataParameters(ItemName, EQueryType.GridView, 1, 0);

            var result1 = SchemaData.GetDataAsync(parameters).Result;

            parameters.PageIndex = 1;

            var result = base.Execute(() => SchemaData.GetDataAsync(parameters).Result, (t) => this.GetResultString(t), this.GetType());

            int index = 0;
            bool cont = true;
            string key = string.Empty;

            foreach (string k in result.Result[0].Keys)
            {
                Type type = result.Result[0][k].GetType();

                if (EntityHelper.PrimitiveTypes.Contains(type))
                {
                    cont = false;
                    key = k;
                }
                else
                {
                    index++;
                }

                if (index >= result.Result[0].Keys.Count)
                {
                    cont = false;
                }

                if (!cont)
                    break;
            }

            Assert.NotEqual(result1.Result[0][key], result.Result[0][key]);
            Assert.True(result.Result.Count == 1);
        }

        public abstract void GetSource();

        public abstract void GetTableReference();

        protected abstract dynamic GetDetailListResult(DataParameters queryParameters);
        protected abstract dynamic GetOnlyOneRecodResult(DataParameters queryParameters);
        protected abstract dynamic GetThreeRecodsResult(DataParameters queryParameters);
    }
}
