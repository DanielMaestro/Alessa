using Alessa.QueryBuilder.Entities;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using Xunit;

namespace QueryBuilder.Test.Data
{
    public class MultiSelectSamplesViewTest : DataTestBase
    {
        public MultiSelectSamplesViewTest() : base("MultiSelectSamplesView")
        {
        }

        protected override dynamic GetDetailListResult(DataParameters queryParameters)
        {
            var result = SchemaData.GetDataAsync(queryParameters).Result;

            return result;
        }

        protected override dynamic GetOnlyOneRecodResult(DataParameters queryParameters)
        {
            var result = SchemaData.GetDataAsync(queryParameters).Result;

            Assert.True(result.Result[0].Count == 4);
            Assert.True(result.Result[0]["MultiSelectSampleId"] != 0);

            return result;
        }

        protected override dynamic GetThreeRecodsResult(DataParameters queryParameters)
        {
            var result = SchemaData.GetDataAsync(queryParameters).Result;

            Assert.True(result.Result[1].Count == 4);
            Assert.True(result.Result[2]["MultiSelectSampleId"] != 0);

            return result;
        }

        [Fact]
        public override void GetSource()
        {
            var parameters = new SourceParameters()
            {
                FieldItemName = "CheckboxSelection",
                ItemName = base.ItemName,
                QueryType = EQueryType.EditView,
                AdditionalParameters = new System.Collections.Generic.Dictionary<string, object>()
                {
                    {"MultiSelectSampleId", 1 }
                },
            };
            var result = base.Execute(() => SchemaData.GetDataSourceAsync(parameters).Result, (t) => this.GetResultString(t), this.GetType());

            Assert.True(result.Result.Count > 0);
            Assert.True(result.Result[0].Value != 0);
        }

        [Fact]
        public override void GetTableReference()
        {
            var dataParameters = base.GetDataParameters("BasicColumnType", EQueryType.GridView, 10, 1);
            var sourceParameters = new SourceParameters()
            {
                FieldItemName = "RecordsGrid",
                ItemName = "MultiSelectSamplesView",
                QueryType = EQueryType.EditView,
                AdditionalParameters = new System.Collections.Generic.Dictionary<string, object>()
                {
                    { "MultiSelectSampleId", 1 }
                },
            };
            var result = base.Execute(() => SchemaData.GetTableSourceAsync(sourceParameters, dataParameters).Result, (t) => this.GetResultString(t), this.GetType());
        }
    }
}
