using Alessa.QueryBuilder.Entities;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using Alessa.QueryBuilder.Entities.Configuration;
using Xunit;

namespace QueryBuilder.Test.Configurations
{
    public abstract class ConfigurationsTestBase : SchemaTestBase
    {
        protected readonly string ItemName;

        public ConfigurationsTestBase(string itemName) : base(@"C:\Temp\ConfigurationTest.log")
        {
            ItemName = itemName;
        }

        [Fact]
        public void GetGridViewConfigurations()
        {
            var parameters = base.GetConfigurationParameters(ItemName, EQueryType.GridView);

            var result = base.Execute(() => this.GetGridConfiguration(parameters), (t) => this.GetResultString(t), this.GetType());
        }

        [Fact]
        public void GetDetailListConfigurations()
        {
            var parameters = base.GetConfigurationParameters(ItemName, EQueryType.DetailListView);

            var result = this.Execute(() => this.GetDetailListConfiguration(parameters), (t) => this.GetResultString(t), this.GetType());
        }

        [Fact]
        public void GetEditConfigurations()
        {
            var parameters = base.GetConfigurationParameters(ItemName, EQueryType.EditView);

            var result = base.Execute(() => this.GetEditConfiguration(parameters), (t) => this.GetResultString(t), this.GetType());
        }

        protected abstract GridConfigViewModel GetGridConfiguration(ConfigurationParameters queryParameters);
        protected abstract DetailListConfigViewModel GetDetailListConfiguration(ConfigurationParameters queryParameters);
        protected abstract EditConfigViewModel GetEditConfiguration(ConfigurationParameters queryParameters);
    }
}
