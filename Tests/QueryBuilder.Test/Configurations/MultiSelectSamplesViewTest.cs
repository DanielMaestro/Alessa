using Alessa.QueryBuilder.Entities.BuilderParameters;
using Alessa.QueryBuilder.Entities.Configuration;
using Xunit;

namespace QueryBuilder.Test.Configurations
{
    public class MultiSelectSamplesViewTest : ConfigurationsTestBase
    {
        public MultiSelectSamplesViewTest() : base("MultiSelectSamplesView")
        {
        }

        protected override EditConfigViewModel GetEditConfiguration(ConfigurationParameters queryParameters)
        {
            var result = SchemaConfigurations.GetEditConfigAsync(queryParameters).Result;
            Assert.Equal(@"{""DetailFormat"":""{{MultiselectSampleId}}"",""AllowExport"":true,""Groups"":[{""ItemName"":""MultiSelectSampleViewGroup"",""DisplayName"":""Multiselection group"",""GroupType"":1,""GroupWidth"":12,""GroupDetails"":[{""GroupType"":3,""GroupWidth"":12,""Fields"":[{""AllowEditInDetail"":true,""EditWidth"":12,""IsRequired"":true,""Regex"":null,""MinLength"":null,""MaxLength"":null,""SourceList"":[],""FieldType"":5,""DisplayType"":2,""IsReadOnly"":false,""ItemName"":""CreatedDate"",""DisplayName"":""Created"",""DisplayOrder"":2,""IsKey"":false,""IsHidden"":false,""DisplayFormat"":null},{""AllowEditInDetail"":true,""EditWidth"":12,""IsRequired"":true,""Regex"":null,""MinLength"":null,""MaxLength"":null,""SourceList"":[],""FieldType"":1,""DisplayType"":2,""IsReadOnly"":false,""ItemName"":""MultiSelectSampleId"",""DisplayName"":""Key field"",""DisplayOrder"":4,""IsKey"":true,""IsHidden"":false,""DisplayFormat"":null}],""ItemName"":""CommonData"",""DisplayName"":""Common Data"",""DisplayOrder"":1},{""GroupType"":2,""GroupWidth"":12,""Fields"":[{""AllowEditInDetail"":true,""EditWidth"":12,""IsRequired"":true,""Regex"":null,""MinLength"":null,""MaxLength"":null,""SourceList"":[""MultiSelectSampleId""],""FieldType"":12,""DisplayType"":2,""IsReadOnly"":false,""ItemName"":""CheckboxSelection"",""DisplayName"":""Checkbox selection"",""DisplayOrder"":1,""IsKey"":false,""IsHidden"":false,""DisplayFormat"":null}],""ItemName"":""MultiCheckbox"",""DisplayName"":""Checkbox"",""DisplayOrder"":2},{""GroupType"":2,""GroupWidth"":12,""Fields"":[{""AllowEditInDetail"":true,""EditWidth"":12,""IsRequired"":true,""Regex"":null,""MinLength"":null,""MaxLength"":null,""SourceList"":[""MultiSelectSampleId""],""FieldType"":11,""DisplayType"":2,""IsReadOnly"":false,""ItemName"":""Multiselection"",""DisplayName"":""Multiselection"",""DisplayOrder"":3,""IsKey"":false,""IsHidden"":false,""DisplayFormat"":null}],""ItemName"":""MultiSelect"",""DisplayName"":""Select List"",""DisplayOrder"":3},{""GroupType"":4,""GroupWidth"":12,""Fields"":[{""AllowEditInDetail"":true,""EditWidth"":12,""IsRequired"":true,""Regex"":null,""MinLength"":null,""MaxLength"":null,""SourceList"":[""MultiSelectSampleId""],""FieldType"":13,""DisplayType"":2,""IsReadOnly"":false,""ItemName"":""RecordsGrid"",""DisplayName"":""Records"",""DisplayOrder"":5,""IsKey"":false,""IsHidden"":false,""DisplayFormat"":null}],""ItemName"":""Grid"",""DisplayName"":""Grid view"",""DisplayOrder"":4}]}],""Validations"":[],""ItemName"":""MultiSelectSamplesView"",""DisplayName"":""The multiselection sample view""}"
             , GetResultString(result.Result));
            return result.Result;
        }

        protected override DetailListConfigViewModel GetDetailListConfiguration(ConfigurationParameters queryParameters)
        {
            var result = SchemaConfigurations.GetDetailListConfigAsync(queryParameters).Result;
            Assert.Equal(@"{""DetailFormat"":""{{MultiselectSampleId}}"",""Groups"":[{""ItemName"":""MultiSelectSampleId"",""DisplayName"":null,""DisplayOrder"":0,""IsKey"":false,""IsHidden"":false,""DisplayFormat"":null}],""ItemName"":""MultiSelectSamplesView"",""DisplayName"":""The multiselection sample view""}"
                , GetResultString(result.Result));
            return result.Result;
        }

        protected override GridConfigViewModel GetGridConfiguration(ConfigurationParameters queryParameters)
        {
            var result = SchemaConfigurations.GetGridConfigAsync(queryParameters).Result;
            Assert.Equal(@"{""AllowExport"":true,""Groups"":[{""Fields"":[{""AllowSort"":true,""AllowFilter"":true,""GridWidth"":1,""DisplayType"":2,""IsReadOnly"":false,""ItemName"":""CreatedDate"",""DisplayName"":""Created"",""DisplayOrder"":2,""IsKey"":false,""IsHidden"":false,""DisplayFormat"":null},{""AllowSort"":true,""AllowFilter"":true,""GridWidth"":1,""DisplayType"":2,""IsReadOnly"":false,""ItemName"":""MultiSelectSampleId"",""DisplayName"":""Key field"",""DisplayOrder"":4,""IsKey"":true,""IsHidden"":false,""DisplayFormat"":null}],""ItemName"":""MultiSelectSampleViewGroup"",""DisplayName"":""Multiselection group"",""DisplayOrder"":0},{""Fields"":[{""AllowSort"":true,""AllowFilter"":true,""GridWidth"":1,""DisplayType"":2,""IsReadOnly"":false,""ItemName"":""CheckboxSelection"",""DisplayName"":""Checkbox selection"",""DisplayOrder"":1,""IsKey"":false,""IsHidden"":false,""DisplayFormat"":null}],""ItemName"":""MultiSelectSampleViewGroup"",""DisplayName"":""Multiselection group"",""DisplayOrder"":0},{""Fields"":[{""AllowSort"":true,""AllowFilter"":true,""GridWidth"":1,""DisplayType"":2,""IsReadOnly"":false,""ItemName"":""Multiselection"",""DisplayName"":""Multiselection"",""DisplayOrder"":3,""IsKey"":false,""IsHidden"":false,""DisplayFormat"":null}],""ItemName"":""MultiSelectSampleViewGroup"",""DisplayName"":""Multiselection group"",""DisplayOrder"":0},{""Fields"":[{""AllowSort"":true,""AllowFilter"":true,""GridWidth"":1,""DisplayType"":2,""IsReadOnly"":false,""ItemName"":""RecordsGrid"",""DisplayName"":""Records"",""DisplayOrder"":5,""IsKey"":false,""IsHidden"":false,""DisplayFormat"":null}],""ItemName"":""MultiSelectSampleViewGroup"",""DisplayName"":""Multiselection group"",""DisplayOrder"":0}],""ItemName"":""MultiSelectSamplesView"",""DisplayName"":""The multiselection sample view""}"
             , GetResultString(result.Result));
            return result.Result;
        }
    }
}
