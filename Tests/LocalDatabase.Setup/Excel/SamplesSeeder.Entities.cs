using Bogus;
using System;
using System.Collections.Generic;
using TesterBase.Entities;

namespace LocalDatabase.Setup.Excel
{
    internal class CounterSingleton
    {
        private static readonly CounterSingleton counterSingleton = new CounterSingleton();
        static CounterSingleton() { }
        private CounterSingleton() { }

        internal int[] CounterArray = new int[] { 1, 1, 1, 1, 1, 1, 1 };
        internal static CounterSingleton Instance
        {
            get
            {
                return counterSingleton;
            }
        }

        internal void ResetCounter()
        {
            for (int i = 0; i < this.CounterArray.Length; i++)
            {
                CounterArray[i] = 1;
            }
        }
    }

    internal partial class SamplesSeeder
    {
        internal SamplesSeeder()
        {
            CounterSingleton.Instance.ResetCounter();
        }

        private CatalogType GetCatalogType()
        {
            var faker = new Faker<CatalogType>()
                .Rules((f, e) =>
                {
                    e.CatalogTypeId = CounterSingleton.Instance.CounterArray[3]++;
                    e.CatalogTypeName = f.Commerce.ProductAdjective();
                    e.CatalogTypeText = f.Commerce.Product();
                    e.IsEnabled = true;
                    e.CatalogValues = new List<CatalogValue>();
                });

            return faker.Generate();
        }

        private CatalogValue GetCatalogValue(CatalogType catalogType)
        {
            var faker = new Faker<CatalogValue>()
            .Rules((f, e) =>
            {
                e.CatalogValueId = CounterSingleton.Instance.CounterArray[4]++;
                e.CatalogType = catalogType;
                e.CatalogTypeId = catalogType.CatalogTypeId;
                e.CatalogValueDisplayEnabled = true;
                e.CatalogValueName = f.Commerce.Department();
                e.CatalogValueText = f.Commerce.ProductName();
                e.IsEnabled = true;
            });

            return faker.Generate();
        }

        private BasicColumnType GetBasicColumnType()
        {
            var faker = new Faker<BasicColumnType>()
            .Rules((f, e) =>
            {
                e.BasicColumnTypeId = string.Format("BAS-{0:D5}", CounterSingleton.Instance.CounterArray[0]++);
                e.ColCheckbox = f.Random.Bool(0.90f);
                e.ColDate = f.Date.Recent(7);
                e.ColDateTime = f.Date.Soon(7);
                e.ColDouble = f.Random.Decimal(10, 144000);
                e.ColInteger = f.Random.Int();
                e.ColMoney = f.Random.Decimal(10, 144000);
                e.ColRichTextArea = f.Random.Words(f.Random.Int(1, 10));
                e.ColText = f.Random.Words(f.Random.Int(1, 3));
                e.ColTextArea = f.Random.Words(f.Random.Int(1, 5));
                e.ColTime = f.Date.Timespan(TimeSpan.FromDays(1));
                e.MultiSelectTables = new List<MultiSelectTable>();
            });

            return faker.Generate();
        }

        private CatalogsJoinSample GetCatalogsJoinSample(CatalogValue category, CatalogValue recordType)
        {
            var faker = new Faker<CatalogsJoinSample>()
            .Rules((f, e) =>
            {
                e.Category = category;
                e.RecordType = recordType;
                e.CategoryId = category.CatalogValueId;
                e.RecordTypeId = recordType.CatalogValueId;
                e.Comments = f.Random.Words(f.Random.Int(2, 25));
                e.JoinSampleId = CounterSingleton.Instance.CounterArray[6]++;
                e.CreatedDate = DateTime.UtcNow;
                e.IsCommited = true;
                e.IsEnabled = true;
            });

            return faker.Generate();
        }

        private HideEnableSample GetHideEnableSample()
        {
            var faker = new Faker<HideEnableSample>()
            .Rules((f, e) =>
            {
                e.HideEnableSampleId = string.Format("{0:D7}", CounterSingleton.Instance.CounterArray[5]++);
                e.Checkbox = f.Random.Bool(0.70f);
                e.EnableWhen5 = f.Random.String2(f.Random.Int(1, 25));
                e.GridList = f.Random.Decimal();
                e.ShowChkbox = f.Random.String2(f.Random.Int(1, 32));
                e.ShowWhenBasic = f.Random.String2(f.Random.Int(1, 13));
                e.IsCommited = true;
                e.IsEnabled = true;
                e.HideEnableMultiselections = new List<HideEnableMultiselection>();
            });

            return faker.Generate();
        }

        private HideEnableMultiselection GetHideEnableMultiselection(HideEnableSample hideEnableSample, CatalogValue catalogValue)
        {
            var faker = new Faker<HideEnableMultiselection>()
            .Rules((f, e) =>
            {
                e.CatalogValue = catalogValue;
                e.CatalogValueId = catalogValue.CatalogValueId;
                e.HideEnableSample = hideEnableSample;
                e.HideEnableSampleId = hideEnableSample.HideEnableSampleId;
                e.IsEnabled = f.Random.Bool(0.9f);
            });

            return faker.Generate();
        }

        private MultiSelectSample GetMultiSelectSample()
        {
            var faker = new Faker<MultiSelectSample>()
            .Rules((f, e) =>
            {
                e.MultiSelectSampleId = CounterSingleton.Instance.CounterArray[1]++;
                e.CreatedDate = f.Date.Recent(5);
                e.MultiSelectCheckboxes = new List<MultiSelectCheckbox>();
                e.MultiSelectLists = new List<MultiSelectList>();
                e.MultiSelectTables = new List<MultiSelectTable>();
            });

            return faker.Generate();
        }

        private MultiSelectCheckbox GetMultiSelectCheckbox(MultiSelectSample multiSelectSample, CatalogValue catalogValue)
        {
            var faker = new Faker<MultiSelectCheckbox>()
            .Rules((f, e) =>
            {
                e.CatalogValue = catalogValue;
                e.CatalogValueId = catalogValue.CatalogValueId;
                e.MultiSelectSample = multiSelectSample;
                e.MultiSelectSampleId = multiSelectSample.MultiSelectSampleId;
                e.IsEnabled = f.Random.Bool(0.9f);
            });

            return faker.Generate();
        }

        private MultiSelectList GetMultiSelectList(MultiSelectSample multiSelectSample, CatalogValue catalogValue)
        {
            var faker = new Faker<MultiSelectList>()
            .Rules((f, e) =>
            {
                e.CatalogValue = catalogValue;
                e.CatalogValueId = catalogValue.CatalogValueId;
                e.MultiSelectSample = multiSelectSample;
                e.MultiSelectSampleId = multiSelectSample.MultiSelectSampleId;
                e.IsEnabled = f.Random.Bool(0.9f);
            });

            return faker.Generate();
        }

        private MultiSelectTable GetMultiSelectTable(MultiSelectSample multiSelectSample, BasicColumnType basicColumnType)
        {
            var faker = new Faker<MultiSelectTable>()
            .Rules((f, e) =>
            {
                e.BasicColumnType = basicColumnType;
                e.BasicColumnTypeId = basicColumnType.BasicColumnTypeId;
                e.MultiSelectSample = multiSelectSample;
                e.MultiSelectSampleId = multiSelectSample.MultiSelectSampleId;
                e.IsEnabled = f.Random.Bool(0.9f);
            });

            return faker.Generate();
        }

        private ValidationSample GetValidationSample()
        {
            var faker = new Faker<ValidationSample>()
            .Rules((f, e) =>
            {
                e.AnythinButValue = f.Random.Int();
                e.NotBeforeTwoDays = f.Date.Past(2);
                e.OnlyOneSupportedValue = 5;
                e.RangeNumber = f.Random.Int(1, 5);
                e.Regex = f.Address.ZipCode();
                e.Required = f.Address.StreetName();
                e.RequiredIfBasic = f.Date.Soon();
                e.ValidationSampleId = CounterSingleton.Instance.CounterArray[2]++.ToString("D5");
                e.VariableLength = f.Random.String2(5);
            });

            return faker.Generate();
        }

    }
}
