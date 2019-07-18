using System;
using System.Collections.Generic;
using Xunit;

namespace QueryBuilder.Test.Saving
{
    public class SavingTestBase : SchemaTestBase
    {
        public SavingTestBase() : base(@"C:\Temp\SavingTest.log")
        {

        }

        [Fact]
        public void OnEnterCreate()
        {
            var result = base.Execute(() =>
             {
                 var res = base.SchemaData.ProcessOnEnterCreate(new Alessa.QueryBuilder.Entities.BuilderParameters.SaveParameters()
                 {
                     ItemName = "HideEnableSamplesView",
                     SaveType = Alessa.QueryBuilder.Entities.ESaveType.Create
                 }).Result;

                 return res;
             }, (t) => this.GetResultString(t), this.GetType());

            Assert.False(result.HasError);
            Assert.Equal(3, result.Result.Count);
            Assert.Equal(25, result.Result["HideWhen2OrMore"]);
            Assert.True(result.Result["EnableWhen5"].StartsWith("Record 000"));
        }

        [Fact]
        public void CreateRecord()
        {
            base.SchemaData.SaveAsync(new Alessa.QueryBuilder.Entities.BuilderParameters.SaveParameters()
            {
                ItemName = "ValidationSamples",
                SaveType = Alessa.QueryBuilder.Entities.ESaveType.Create,
                AdditionalParameters = new Dictionary<string, object>()
                {
                    {"AnythinButValue", 5},
                    {"NotBeforeTwoDays", DateTime.Now.AddDays(-3)},
                    {"OnlyOneSupportedValue", 45},
                    {"RangeNumber", 42},
                    {"Regex", "mail@mail.com."},
                    {"Required", ""},
                    {"RequiredIfBasic", ""},
                    {"VariableLength", "Lorem ipsum"},
                }
            }).Wait();
        }

    }
}
