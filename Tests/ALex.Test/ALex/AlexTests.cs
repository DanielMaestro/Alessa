using Alessa.ALex;
using Alessa.ALex.SqlKata;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ALex.Test.ALex
{
    public class AlexTests : ALexTesterBase
    {
        #region Fields for testing
        private static readonly Dictionary<string, object> _Dictionary = new Dictionary<string, object>()
        {
            { "AggregatesComplex", "kfoucar26@reference.com" },
            { "AggregatesSimple", "mblethynt@macromedia.com" },
            { "AnythinButValue", "kgladtbache@utexas.edu" },
            { "BasicColumnTypeId", 9 },
            { "CatalogTypeId", 1 },
            { "CatalogTypeName", 2 },
            { "CatalogTypeText", 3 },
            { "CatalogValueDisplayEnabled", 5 },
            { "CatalogValueId", 6 },
            { "CatalogValueName", 7 },
            { "CatalogValueText", 8 },
            { "Category", "dbannerman0@va.gov" },
            { "CategoryId", 20 },
            { "Checkbox", "cyeabsley3@rambler.ru" },
            { "CheckboxSelection", "cbunstoneb@technorati.com" },
            { "Col1", "fgurdon2i@cbsnews.com" },
            { "Col2", "rplacide2j@fotki.com" },
            { "Col3", "kwimpeney2k@house.gov" },
            { "Col4", "mcomelli2l@discuz.net" },
            { "Col5", "kmiettinen2m@intel.com" },
            { "Col6", "rhuc2n@sun.com" },
            { "ColCheckbox", 10 },
            { "ColDate", 11 },
            { "ColDateTime", 12 },
            { "ColDouble", 13 },
            { "ColInteger", 14 },
            { "ColMoney", 15 },
            { "ColRichTextArea", 16 },
            { "ColText", 17 },
            { "ColTextArea", 18 },
            { "ColTime", 19 },
            { "Comments", 21 },
            { "CreatedDate", 22 },
            { "DeleteWithFilter", "mcurreeno@sina.com.cn" },
            { "EnableWhen5", "fkealy4@economist.com" },
            { "FilterAtEnd", 44 },
            { "FilterRaw", 48 },
            { "GridList", "ophlippi6@eepurl.com" },
            { "GroupByComplex", 59 },
            { "GroupBySimple", "sbloomer27@indiegogo.com" },
            { "HideEnableSampleId", "cpurtell2@unesco.org" },
            { "HideWhen2OrMore", "aswinney5@uiuc.edu" },
            { "InsertWithManyValues", 53 },
            { "InsertWithQuery", 46 },
            { "InStatement", 50 },
            { "IsCommited", 25 },
            { "IsEnabled", 4 },
            { "IsNotNullStatement", 60 },
            { "IsNullStatement", 51 },
            { "JoinAfterStatement", "hbyerx@earthlink.net" },
            { "JoinManyConstraints", 57 },
            { "JoinMultipleTables", "pgoldney2c@senate.gov" },
            { "JoinSampleId", 23 },
            { "JoinSimple", "wmumby2b@mail.ru" },
            { "JoinWithFilters", "cmaylotts@nsw.gov.au" },
            { "LikeStatement", 61 },
            { "Multiselect", "taldred7@creativecommons.org" },
            { "Multiselection", "ditzkovitchc@i2i.jp" },
            { "MultiSelectSampleId", "tcolgravea@networkadvertising.org" },
            { "NotBeforeTwoDays", "ltruslerf@taobao.com" },
            { "NotInStatement", 63 },
            { "NotLikeStatement", "snewarte24@princeton.edu" },
            { "OnlyOneSupportedValue", "ccollacombeg@com.com" },
            { "OrderBy", "dhydechambers2d@blog.com" },
            { "Paging", 54 },
            { "RangeNumber", "jmowbrayh@simplemachines.org" },
            { "RawInFilter", 47 },
            { "RawInSelect", "etooley2h@prweb.com" },
            { "RecordsGrid", "bduddend@dyndns.org" },
            { "RecordType", "efashion1@mail.ru" },
            { "RecordTypeId", 24 },
            { "Regex", "acordelettei@topsy.com" },
            { "Required", "keslerj@youku.com" },
            { "RequiredIfBasic", "jbousfieldk@seesaa.net" },
            { "SelectWithHarcodedValues", 43 },
            { "SelectWithManyOperationInFilter", "fpurle28@cmu.edu" },
            { "SelectWithManyOperationInFilterAliases", 49 },
            { "SelectWithManyOperationInSelect", "hbarribalr@nba.com" },
            { "SelectWithOneOperationInFilter", "dmcelwee2f@cloudflare.com" },
            { "SelectWithOneOperationInSelect", 62 },
            { "ShowChkbox", "shanway8@is.gd" },
            { "ShowWhenBasic", "zbarlass9@usda.gov" },
            { "SimpleDelete", "ralbasiniv@irs.gov" },
            { "SimpleInsert", "dfeldhammeru@netlog.com" },
            { "SimpleSelect", "ftarte2e@miibeian.gov.cn" },
            { "SimpleSelectDistinct", 52 },
            { "SimpleSelectWithColumnAlias", "jeakly2g@nytimes.com" },
            { "SimpleSelectWithNull", "mmacarthur2a@friendfeed.com" },
            { "SimpleSelectWithOneFilter", 58 },
            { "SimpleSelectWithTableAlias", "cmillp@virginia.edu" },
            { "SimpleSelectWithTwoFilters", 45 },
            { "SimpleSelectWithTwoFiltersAndAlias", "rtucknutt29@ucoz.com" },
            { "SimpleUpdate", 56 },
            { "TestAllTogether", "drosenauw@imgur.com" },
            { "TopStatement", "dmcniven25@exblog.jp" },
            { "UpdateMultipleSets", "tdellcasaq@imageshack.us" },
            { "UpdateWithFilter", 55 },
            { "ValidationSampleId", "ymendenhalll@xinhuanet.com" },
            { "Values", "iwinkelln@yahoo.com" },
            { "VariableLength", "rrimmerm@reverbnation.com" },
       };

        private static readonly object _Data = new
        {
            AggregatesComplex = "kfoucar26@reference.com",
            AggregatesSimple = "mblethynt@macromedia.com",
            AnythinButValue = "kgladtbache@utexas.edu",
            BasicColumnTypeId = 9,
            CatalogTypeId = 1,
            CatalogTypeName = 2,
            CatalogTypeText = 3,
            CatalogValueDisplayEnabled = 5,
            CatalogValueId = 6,
            CatalogValueName = 7,
            CatalogValueText = 8,
            Category = "dbannerman0@va.gov",
            CategoryId = 20,
            Checkbox = "cyeabsley3@rambler.ru",
            CheckboxSelection = "cbunstoneb@technorati.com",
            Col1 = "fgurdon2i@cbsnews.com",
            Col2 = "rplacide2j@fotki.com",
            Col3 = "kwimpeney2k@house.gov",
            Col4 = "mcomelli2l@discuz.net",
            Col5 = "kmiettinen2m@intel.com",
            Col6 = "rhuc2n@sun.com",
            ColCheckbox = 10,
            ColDate = 11,
            ColDateTime = 12,
            ColDouble = 13,
            ColInteger = 14,
            ColMoney = 15,
            ColRichTextArea = 16,
            ColText = 17,
            ColTextArea = 18,
            ColTime = 19,
            Comments = 21,
            CreatedDate = 22,
            DeleteWithFilter = "mcurreeno@sina.com.cn",
            EnableWhen5 = "fkealy4@economist.com",
            FilterAtEnd = 44,
            FilterRaw = 48,
            GridList = "ophlippi6@eepurl.com",
            GroupByComplex = 59,
            GroupBySimple = "sbloomer27@indiegogo.com",
            HideEnableSampleId = "cpurtell2@unesco.org",
            HideWhen2OrMore = "aswinney5@uiuc.edu",
            InsertWithManyValues = 53,
            InsertWithQuery = 46,
            InStatement = 50,
            IsCommited = 25,
            IsEnabled = 4,
            IsNotNullStatement = 60,
            IsNullStatement = 51,
            JoinAfterStatement = "hbyerx@earthlink.net",
            JoinManyConstraints = 57,
            JoinMultipleTables = "pgoldney2c@senate.gov",
            JoinSampleId = 23,
            JoinSimple = "wmumby2b@mail.ru",
            JoinWithFilters = "cmaylotts@nsw.gov.au",
            LikeStatement = 61,
            Multiselect = "taldred7@creativecommons.org",
            Multiselection = "ditzkovitchc@i2i.jp",
            MultiSelectSampleId = "tcolgravea@networkadvertising.org",
            NotBeforeTwoDays = "ltruslerf@taobao.com",
            NotInStatement = 63,
            NotLikeStatement = "snewarte24@princeton.edu",
            OnlyOneSupportedValue = "ccollacombeg@com.com",
            OrderBy = "dhydechambers2d@blog.com",
            Paging = 54,
            RangeNumber = "jmowbrayh@simplemachines.org",
            RawInFilter = 47,
            RawInSelect = "etooley2h@prweb.com",
            RecordsGrid = "bduddend@dyndns.org",
            RecordType = "efashion1@mail.ru",
            RecordTypeId = 24,
            Regex = "acordelettei@topsy.com",
            Required = "keslerj@youku.com",
            RequiredIfBasic = "jbousfieldk@seesaa.net",
            SelectWithHarcodedValues = 43,
            SelectWithManyOperationInFilter = "fpurle28@cmu.edu",
            SelectWithManyOperationInFilterAliases = 49,
            SelectWithManyOperationInSelect = "hbarribalr@nba.com",
            SelectWithOneOperationInFilter = "dmcelwee2f@cloudflare.com",
            SelectWithOneOperationInSelect = 62,
            ShowChkbox = "shanway8@is.gd",
            ShowWhenBasic = "zbarlass9@usda.gov",
            SimpleDelete = "ralbasiniv@irs.gov",
            SimpleInsert = "dfeldhammeru@netlog.com",
            SimpleSelect = "ftarte2e@miibeian.gov.cn",
            SimpleSelectDistinct = 52,
            SimpleSelectWithColumnAlias = "jeakly2g@nytimes.com",
            SimpleSelectWithNull = "mmacarthur2a@friendfeed.com",
            SimpleSelectWithOneFilter = 58,
            SimpleSelectWithTableAlias = "cmillp@virginia.edu",
            SimpleSelectWithTwoFilters = 45,
            SimpleSelectWithTwoFiltersAndAlias = "rtucknutt29@ucoz.com",
            SimpleUpdate = 56,
            TestAllTogether = "drosenauw@imgur.com",
            TopStatement = "dmcniven25@exblog.jp",
            UpdateMultipleSets = "tdellcasaq@imageshack.us",
            UpdateWithFilter = 55,
            ValidationSampleId = "ymendenhalll@xinhuanet.com",
            Values = "iwinkelln@yahoo.com",
            VariableLength = "rrimmerm@reverbnation.com",
        };
        #endregion


        [Fact]
        public void ParseValuesWithDataSmall()
        {
            var aLex = @"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement)
Filter(My.JoinAfterStatement = '{{AggregatesComplex}}' AND  My.JoinManyConstraints = '{{AggregatesSimple}}' AND  My.JoinMultipleTables = '{{AnythinButValue}}' AND  My.JoinSampleId = {{BasicColumnTypeId}} AND  My.JoinSimple = {{CatalogTypeId}} AND  My.JoinWithFilters = {{CatalogTypeName}} AND  My.LikeStatement = {{CatalogTypeText}} AND  My.Multiselect = {{CatalogValueDisplayEnabled}})
";
            var q = this.GetALexString(aLex, _Data);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\t{0}\t{1}\t{2}\t{3}", string.Join('\t', MethodBase.GetCurrentMethod().ReflectedType.ToString().Split('.')), MethodBase.GetCurrentMethod().Name, q.Time, q.Converted.Replace("\r\n", " ").Replace("\n", " "));
            base.Log(builder.ToString());
            Assert.Equal(@"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement)
Filter(My.JoinAfterStatement = 'kfoucar26@reference.com' AND  My.JoinManyConstraints = 'mblethynt@macromedia.com' AND  My.JoinMultipleTables = 'kgladtbache@utexas.edu' AND  My.JoinSampleId = 9 AND  My.JoinSimple = 1 AND  My.JoinWithFilters = 2 AND  My.LikeStatement = 3 AND  My.Multiselect = 5)
", q.Converted);
        }

        [Fact]
        public void ParseValuesWithDataLarge()
        {
            var aLex = @"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement,My.NotLikeStatement,T1.OnlyOneSupportedValue,T1.OrderBy,T1.Paging,T1.RangeNumber,T1.RawInFilter,T1.RawInSelect,T1.RecordsGrid,T1.RecordType,T1.RecordTypeId,T1.Regex,T1.Required,T1.RequiredIfBasic,T1.SelectWithHarcodedValues,T1.SelectWithManyOperationInFilter,T1.SelectWithManyOperationInFilterAliases,T1.SelectWithManyOperationInSelect,T1.SelectWithOneOperationInFilter,T1.SelectWithOneOperationInSelect,T1.ShowChkbox,T1.ShowWhenBasic,T1.SimpleDelete,T1.SimpleInsert,T1.SimpleSelect,T1.SimpleSelectDistinct,T1.SimpleSelectWithColumnAlias,T1.SimpleSelectWithNull,T1.SimpleSelectWithOneFilter,T1.SimpleSelectWithTableAlias,T1.SimpleSelectWithTwoFilters,T1.SimpleSelectWithTwoFiltersAndAlias,T1.SimpleUpdate,T1.TestAllTogether,T1.TopStatement,T1.UpdateMultipleSets,T2.UpdateWithFilter,T2.ValidationSampleId,T2.Values, T2.VariableLength,T2.AggregatesComplex,T2.AggregatesSimple,T2.AnythinButValue,T2.BasicColumnTypeId,T2.CatalogTypeId,T2.CatalogTypeName,T2.CatalogTypeText,T2.CatalogValueDisplayEnabled,T2.CatalogValueId,T2.CatalogValueName,T2.CatalogValueText,T2.Category,T2.CategoryId,T2.Checkbox,T2.CheckboxSelection,T2.Col1,T2.Col2,T2.Col3,T2.Col4,T2.Col5,T2.Col6,T2.ColCheckbox,T2.ColDate,T2.ColDateTime,T2.ColDouble,T2.ColInteger,T2.ColMoney,T2.ColRichTextArea,T2.ColText,T2.ColTextArea,T2.ColTime,T2.Comments,T2.CreatedDate,T2.DeleteWithFilter,T2.EnableWhen5,T2.FilterAtEnd,T2.FilterRaw,T2.GridList,T2.GroupByComplex,T2.GroupBySimple,T3.HideEnableSampleId,T3.HideWhen2OrMore,T3.InsertWithManyValues,T3.InsertWithQuery,T3.InStatement,T3.IsCommited,T3.IsEnabled,T3.IsNotNullStatement,T3.IsNullStatement)
Filter(My.JoinAfterStatement = '{{AggregatesComplex}}' AND  My.JoinManyConstraints = '{{AggregatesSimple}}' AND  My.JoinMultipleTables = '{{AnythinButValue}}' AND  My.JoinSampleId = {{BasicColumnTypeId}} AND  My.JoinSimple = {{CatalogTypeId}} AND  My.JoinWithFilters = {{CatalogTypeName}} AND  My.LikeStatement = {{CatalogTypeText}} AND  My.Multiselect = {{CatalogValueDisplayEnabled}} AND  My.Multiselection = {{CatalogValueId}} AND  My.MultiSelectSampleId = {{CatalogValueName}} AND  My.NotBeforeTwoDays = {{CatalogValueText}} AND  My.NotInStatement = '{{Category}}' AND  My.NotLikeStatement = {{CategoryId}} AND  T1.OnlyOneSupportedValue = '{{Checkbox}}' AND  T1.OrderBy = '{{CheckboxSelection}}' AND  T1.Paging = '{{Col1}}' AND  T1.RangeNumber = '{{Col2}}' AND  T1.RawInFilter = '{{Col3}}' AND  T1.RawInSelect = '{{Col4}}' AND  T1.RecordsGrid = '{{Col5}}' AND  T1.RecordType = '{{Col6}}' AND  T1.RecordTypeId = {{ColCheckbox}} AND  T1.Regex = {{ColDate}} AND  T1.Required = {{ColDateTime}} AND  T1.RequiredIfBasic = {{ColDouble}} AND  T1.SelectWithHarcodedValues = {{ColInteger}} AND  T1.SelectWithManyOperationInFilter = {{ColMoney}} AND  T1.SelectWithManyOperationInFilterAliases = {{ColRichTextArea}} AND  T1.SelectWithManyOperationInSelect = {{ColText}} AND  T1.SelectWithOneOperationInFilter = {{ColTextArea}} AND  T1.SelectWithOneOperationInSelect = {{ColTime}} AND  T1.ShowChkbox = {{Comments}} AND  T1.ShowWhenBasic = {{CreatedDate}} AND  T1.SimpleDelete = '{{DeleteWithFilter}}' AND  T1.SimpleInsert = '{{EnableWhen5}}' AND  T1.SimpleSelect = {{FilterAtEnd}} AND  T1.SimpleSelectDistinct = {{FilterRaw}} AND  T1.SimpleSelectWithColumnAlias = '{{GridList}}' AND  T1.SimpleSelectWithNull = {{GroupByComplex}} AND  T1.SimpleSelectWithOneFilter = '{{GroupBySimple}}' AND  T1.SimpleSelectWithTableAlias = '{{HideEnableSampleId}}' AND  T1.SimpleSelectWithTwoFilters = '{{HideWhen2OrMore}}' AND  T1.SimpleSelectWithTwoFiltersAndAlias = {{InsertWithManyValues}} AND  T1.SimpleUpdate = {{InsertWithQuery}} AND  T1.TestAllTogether = {{InStatement}} AND  T1.TopStatement = {{IsCommited}} AND  T1.UpdateMultipleSets = {{IsEnabled}} AND  T2.UpdateWithFilter = {{IsNotNullStatement}} AND  T2.ValidationSampleId = {{IsNullStatement}} AND  T2.Values = '{{JoinAfterStatement}}' AND T2.VariableLength = {{JoinManyConstraints}} AND  T2.AggregatesComplex = '{{JoinMultipleTables}}' AND  T2.AggregatesSimple = {{JoinSampleId}} AND  T2.AnythinButValue = '{{JoinSimple}}' AND  T2.BasicColumnTypeId = '{{JoinWithFilters}}' AND  T2.CatalogTypeId = {{LikeStatement}} AND  T2.CatalogTypeName = '{{Multiselect}}' AND  T2.CatalogTypeText = '{{Multiselection}}' AND  T2.CatalogValueDisplayEnabled = '{{MultiSelectSampleId}}' AND  T2.CatalogValueId = '{{NotBeforeTwoDays}}' AND  T2.CatalogValueName = {{NotInStatement}} AND  T2.CatalogValueText = '{{NotLikeStatement}}' AND  T2.Category = '{{OnlyOneSupportedValue}}' AND  T2.CategoryId = '{{OrderBy}}' AND  T2.Checkbox = {{Paging}} AND  T2.CheckboxSelection = '{{RangeNumber}}' AND  T2.Col1 = {{RawInFilter}} AND  T2.Col2 = '{{RawInSelect}}' AND  T2.Col3 = '{{RecordsGrid}}' AND  T2.Col4 = '{{RecordType}}' AND  T2.Col5 = {{RecordTypeId}} AND  T2.Col6 = '{{Regex}}' AND  T2.ColCheckbox = '{{Required}}' AND  T2.ColDate = '{{RequiredIfBasic}}' AND  T2.ColDateTime = {{SelectWithHarcodedValues}} AND  T2.ColDouble = '{{SelectWithManyOperationInFilter}}' AND  T2.ColInteger = {{SelectWithManyOperationInFilterAliases}} AND  T2.ColMoney = '{{SelectWithManyOperationInSelect}}' AND  T2.ColRichTextArea = '{{SelectWithOneOperationInFilter}}' AND  T2.ColText = {{SelectWithOneOperationInSelect}} AND  T2.ColTextArea = '{{ShowChkbox}}' AND  T2.ColTime = '{{ShowWhenBasic}}' AND  T2.Comments = '{{SimpleDelete}}' AND  T2.CreatedDate = '{{SimpleInsert}}' AND  T2.DeleteWithFilter = '{{SimpleSelect}}' AND  T2.EnableWhen5 = {{SimpleSelectDistinct}} AND  T2.FilterAtEnd = '{{SimpleSelectWithColumnAlias}}' AND  T2.FilterRaw = '{{SimpleSelectWithNull}}' AND  T2.GridList = {{SimpleSelectWithOneFilter}} AND  T2.GroupByComplex = '{{SimpleSelectWithTableAlias}}' AND  T2.GroupBySimple = {{SimpleSelectWithTwoFilters}} AND  T3.HideEnableSampleId = '{{SimpleSelectWithTwoFiltersAndAlias}}' AND  T3.HideWhen2OrMore = {{SimpleUpdate}} AND  T3.InsertWithManyValues = '{{TestAllTogether}}' AND  T3.InsertWithQuery = '{{TopStatement}}' AND  T3.InStatement = '{{UpdateMultipleSets}}' AND  T3.IsCommited = {{UpdateWithFilter}} AND  T3.IsEnabled = '{{ValidationSampleId}}' AND  T3.IsNotNullStatement = '{{Values}}' AND  T3.IsNullStatement = '{{VariableLength}}')
Join(INNER, Table1 AS T1, T1.VariableLength = {{JoinManyConstraints}})
Join(INNER, Table2 AS T2, T2.JoinManyConstraints = '{{AnythinButValue}}')
Join(INNER, Table3 AS T3, T3.JoinMultipleTables = {{BasicColumnTypeId}})
";
            var q = this.GetALexString(aLex, _Data);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\t{0}\t{1}\t{2}\t{3}", string.Join('\t', MethodBase.GetCurrentMethod().ReflectedType.ToString().Split('.')), MethodBase.GetCurrentMethod().Name, q.Time, q.Converted.Replace("\r\n", " ").Replace("\n", " "));
            base.Log(builder.ToString());
            Assert.Equal(@"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement,My.NotLikeStatement,T1.OnlyOneSupportedValue,T1.OrderBy,T1.Paging,T1.RangeNumber,T1.RawInFilter,T1.RawInSelect,T1.RecordsGrid,T1.RecordType,T1.RecordTypeId,T1.Regex,T1.Required,T1.RequiredIfBasic,T1.SelectWithHarcodedValues,T1.SelectWithManyOperationInFilter,T1.SelectWithManyOperationInFilterAliases,T1.SelectWithManyOperationInSelect,T1.SelectWithOneOperationInFilter,T1.SelectWithOneOperationInSelect,T1.ShowChkbox,T1.ShowWhenBasic,T1.SimpleDelete,T1.SimpleInsert,T1.SimpleSelect,T1.SimpleSelectDistinct,T1.SimpleSelectWithColumnAlias,T1.SimpleSelectWithNull,T1.SimpleSelectWithOneFilter,T1.SimpleSelectWithTableAlias,T1.SimpleSelectWithTwoFilters,T1.SimpleSelectWithTwoFiltersAndAlias,T1.SimpleUpdate,T1.TestAllTogether,T1.TopStatement,T1.UpdateMultipleSets,T2.UpdateWithFilter,T2.ValidationSampleId,T2.Values, T2.VariableLength,T2.AggregatesComplex,T2.AggregatesSimple,T2.AnythinButValue,T2.BasicColumnTypeId,T2.CatalogTypeId,T2.CatalogTypeName,T2.CatalogTypeText,T2.CatalogValueDisplayEnabled,T2.CatalogValueId,T2.CatalogValueName,T2.CatalogValueText,T2.Category,T2.CategoryId,T2.Checkbox,T2.CheckboxSelection,T2.Col1,T2.Col2,T2.Col3,T2.Col4,T2.Col5,T2.Col6,T2.ColCheckbox,T2.ColDate,T2.ColDateTime,T2.ColDouble,T2.ColInteger,T2.ColMoney,T2.ColRichTextArea,T2.ColText,T2.ColTextArea,T2.ColTime,T2.Comments,T2.CreatedDate,T2.DeleteWithFilter,T2.EnableWhen5,T2.FilterAtEnd,T2.FilterRaw,T2.GridList,T2.GroupByComplex,T2.GroupBySimple,T3.HideEnableSampleId,T3.HideWhen2OrMore,T3.InsertWithManyValues,T3.InsertWithQuery,T3.InStatement,T3.IsCommited,T3.IsEnabled,T3.IsNotNullStatement,T3.IsNullStatement)
Filter(My.JoinAfterStatement = 'kfoucar26@reference.com' AND  My.JoinManyConstraints = 'mblethynt@macromedia.com' AND  My.JoinMultipleTables = 'kgladtbache@utexas.edu' AND  My.JoinSampleId = 9 AND  My.JoinSimple = 1 AND  My.JoinWithFilters = 2 AND  My.LikeStatement = 3 AND  My.Multiselect = 5 AND  My.Multiselection = 6 AND  My.MultiSelectSampleId = 7 AND  My.NotBeforeTwoDays = 8 AND  My.NotInStatement = 'dbannerman0@va.gov' AND  My.NotLikeStatement = 20 AND  T1.OnlyOneSupportedValue = 'cyeabsley3@rambler.ru' AND  T1.OrderBy = 'cbunstoneb@technorati.com' AND  T1.Paging = 'fgurdon2i@cbsnews.com' AND  T1.RangeNumber = 'rplacide2j@fotki.com' AND  T1.RawInFilter = 'kwimpeney2k@house.gov' AND  T1.RawInSelect = 'mcomelli2l@discuz.net' AND  T1.RecordsGrid = 'kmiettinen2m@intel.com' AND  T1.RecordType = 'rhuc2n@sun.com' AND  T1.RecordTypeId = 10 AND  T1.Regex = 11 AND  T1.Required = 12 AND  T1.RequiredIfBasic = 13 AND  T1.SelectWithHarcodedValues = 14 AND  T1.SelectWithManyOperationInFilter = 15 AND  T1.SelectWithManyOperationInFilterAliases = 16 AND  T1.SelectWithManyOperationInSelect = 17 AND  T1.SelectWithOneOperationInFilter = 18 AND  T1.SelectWithOneOperationInSelect = 19 AND  T1.ShowChkbox = 21 AND  T1.ShowWhenBasic = 22 AND  T1.SimpleDelete = 'mcurreeno@sina.com.cn' AND  T1.SimpleInsert = 'fkealy4@economist.com' AND  T1.SimpleSelect = 44 AND  T1.SimpleSelectDistinct = 48 AND  T1.SimpleSelectWithColumnAlias = 'ophlippi6@eepurl.com' AND  T1.SimpleSelectWithNull = 59 AND  T1.SimpleSelectWithOneFilter = 'sbloomer27@indiegogo.com' AND  T1.SimpleSelectWithTableAlias = 'cpurtell2@unesco.org' AND  T1.SimpleSelectWithTwoFilters = 'aswinney5@uiuc.edu' AND  T1.SimpleSelectWithTwoFiltersAndAlias = 53 AND  T1.SimpleUpdate = 46 AND  T1.TestAllTogether = 50 AND  T1.TopStatement = 25 AND  T1.UpdateMultipleSets = 4 AND  T2.UpdateWithFilter = 60 AND  T2.ValidationSampleId = 51 AND  T2.Values = 'hbyerx@earthlink.net' AND T2.VariableLength = 57 AND  T2.AggregatesComplex = 'pgoldney2c@senate.gov' AND  T2.AggregatesSimple = 23 AND  T2.AnythinButValue = 'wmumby2b@mail.ru' AND  T2.BasicColumnTypeId = 'cmaylotts@nsw.gov.au' AND  T2.CatalogTypeId = 61 AND  T2.CatalogTypeName = 'taldred7@creativecommons.org' AND  T2.CatalogTypeText = 'ditzkovitchc@i2i.jp' AND  T2.CatalogValueDisplayEnabled = 'tcolgravea@networkadvertising.org' AND  T2.CatalogValueId = 'ltruslerf@taobao.com' AND  T2.CatalogValueName = 63 AND  T2.CatalogValueText = 'snewarte24@princeton.edu' AND  T2.Category = 'ccollacombeg@com.com' AND  T2.CategoryId = 'dhydechambers2d@blog.com' AND  T2.Checkbox = 54 AND  T2.CheckboxSelection = 'jmowbrayh@simplemachines.org' AND  T2.Col1 = 47 AND  T2.Col2 = 'etooley2h@prweb.com' AND  T2.Col3 = 'bduddend@dyndns.org' AND  T2.Col4 = 'efashion1@mail.ru' AND  T2.Col5 = 24 AND  T2.Col6 = 'acordelettei@topsy.com' AND  T2.ColCheckbox = 'keslerj@youku.com' AND  T2.ColDate = 'jbousfieldk@seesaa.net' AND  T2.ColDateTime = 43 AND  T2.ColDouble = 'fpurle28@cmu.edu' AND  T2.ColInteger = 49 AND  T2.ColMoney = 'hbarribalr@nba.com' AND  T2.ColRichTextArea = 'dmcelwee2f@cloudflare.com' AND  T2.ColText = 62 AND  T2.ColTextArea = 'shanway8@is.gd' AND  T2.ColTime = 'zbarlass9@usda.gov' AND  T2.Comments = 'ralbasiniv@irs.gov' AND  T2.CreatedDate = 'dfeldhammeru@netlog.com' AND  T2.DeleteWithFilter = 'ftarte2e@miibeian.gov.cn' AND  T2.EnableWhen5 = 52 AND  T2.FilterAtEnd = 'jeakly2g@nytimes.com' AND  T2.FilterRaw = 'mmacarthur2a@friendfeed.com' AND  T2.GridList = 58 AND  T2.GroupByComplex = 'cmillp@virginia.edu' AND  T2.GroupBySimple = 45 AND  T3.HideEnableSampleId = 'rtucknutt29@ucoz.com' AND  T3.HideWhen2OrMore = 56 AND  T3.InsertWithManyValues = 'drosenauw@imgur.com' AND  T3.InsertWithQuery = 'dmcniven25@exblog.jp' AND  T3.InStatement = 'tdellcasaq@imageshack.us' AND  T3.IsCommited = 55 AND  T3.IsEnabled = 'ymendenhalll@xinhuanet.com' AND  T3.IsNotNullStatement = 'iwinkelln@yahoo.com' AND  T3.IsNullStatement = 'rrimmerm@reverbnation.com')
Join(INNER, Table1 AS T1, T1.VariableLength = 57)
Join(INNER, Table2 AS T2, T2.JoinManyConstraints = 'kgladtbache@utexas.edu')
Join(INNER, Table3 AS T3, T3.JoinMultipleTables = 9)
", q.Converted);
        }

        [Fact]
        public void ParseValuesWithDictionarySmall()
        {
            var aLex = @"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement)
Filter(My.JoinAfterStatement = '{{AggregatesComplex}}' AND  My.JoinManyConstraints = '{{AggregatesSimple}}' AND  My.JoinMultipleTables = '{{AnythinButValue}}' AND  My.JoinSampleId = {{BasicColumnTypeId}} AND  My.JoinSimple = {{CatalogTypeId}} AND  My.JoinWithFilters = {{CatalogTypeName}} AND  My.LikeStatement = {{CatalogTypeText}} AND  My.Multiselect = {{CatalogValueDisplayEnabled}})
";
            var q = this.GetALexString(aLex, _Dictionary);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\t{0}\t{1}\t{2}\t{3}", string.Join('\t', MethodBase.GetCurrentMethod().ReflectedType.ToString().Split('.')), MethodBase.GetCurrentMethod().Name, q.Time, q.Converted.Replace("\r\n", " ").Replace("\n", " "));
            base.Log(builder.ToString());
            Assert.Equal(@"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement)
Filter(My.JoinAfterStatement = 'kfoucar26@reference.com' AND  My.JoinManyConstraints = 'mblethynt@macromedia.com' AND  My.JoinMultipleTables = 'kgladtbache@utexas.edu' AND  My.JoinSampleId = 9 AND  My.JoinSimple = 1 AND  My.JoinWithFilters = 2 AND  My.LikeStatement = 3 AND  My.Multiselect = 5)
", q.Converted);
        }

        [Fact]
        public void ParseValuesWithDictionaryLarge()
        {
            var aLex = @"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement,My.NotLikeStatement,T1.OnlyOneSupportedValue,T1.OrderBy,T1.Paging,T1.RangeNumber,T1.RawInFilter,T1.RawInSelect,T1.RecordsGrid,T1.RecordType,T1.RecordTypeId,T1.Regex,T1.Required,T1.RequiredIfBasic,T1.SelectWithHarcodedValues,T1.SelectWithManyOperationInFilter,T1.SelectWithManyOperationInFilterAliases,T1.SelectWithManyOperationInSelect,T1.SelectWithOneOperationInFilter,T1.SelectWithOneOperationInSelect,T1.ShowChkbox,T1.ShowWhenBasic,T1.SimpleDelete,T1.SimpleInsert,T1.SimpleSelect,T1.SimpleSelectDistinct,T1.SimpleSelectWithColumnAlias,T1.SimpleSelectWithNull,T1.SimpleSelectWithOneFilter,T1.SimpleSelectWithTableAlias,T1.SimpleSelectWithTwoFilters,T1.SimpleSelectWithTwoFiltersAndAlias,T1.SimpleUpdate,T1.TestAllTogether,T1.TopStatement,T1.UpdateMultipleSets,T2.UpdateWithFilter,T2.ValidationSampleId,T2.Values, T2.VariableLength,T2.AggregatesComplex,T2.AggregatesSimple,T2.AnythinButValue,T2.BasicColumnTypeId,T2.CatalogTypeId,T2.CatalogTypeName,T2.CatalogTypeText,T2.CatalogValueDisplayEnabled,T2.CatalogValueId,T2.CatalogValueName,T2.CatalogValueText,T2.Category,T2.CategoryId,T2.Checkbox,T2.CheckboxSelection,T2.Col1,T2.Col2,T2.Col3,T2.Col4,T2.Col5,T2.Col6,T2.ColCheckbox,T2.ColDate,T2.ColDateTime,T2.ColDouble,T2.ColInteger,T2.ColMoney,T2.ColRichTextArea,T2.ColText,T2.ColTextArea,T2.ColTime,T2.Comments,T2.CreatedDate,T2.DeleteWithFilter,T2.EnableWhen5,T2.FilterAtEnd,T2.FilterRaw,T2.GridList,T2.GroupByComplex,T2.GroupBySimple,T3.HideEnableSampleId,T3.HideWhen2OrMore,T3.InsertWithManyValues,T3.InsertWithQuery,T3.InStatement,T3.IsCommited,T3.IsEnabled,T3.IsNotNullStatement,T3.IsNullStatement)
Filter(My.JoinAfterStatement = '{{AggregatesComplex}}' AND  My.JoinManyConstraints = '{{AggregatesSimple}}' AND  My.JoinMultipleTables = '{{AnythinButValue}}' AND  My.JoinSampleId = {{BasicColumnTypeId}} AND  My.JoinSimple = {{CatalogTypeId}} AND  My.JoinWithFilters = {{CatalogTypeName}} AND  My.LikeStatement = {{CatalogTypeText}} AND  My.Multiselect = {{CatalogValueDisplayEnabled}} AND  My.Multiselection = {{CatalogValueId}} AND  My.MultiSelectSampleId = {{CatalogValueName}} AND  My.NotBeforeTwoDays = {{CatalogValueText}} AND  My.NotInStatement = '{{Category}}' AND  My.NotLikeStatement = {{CategoryId}} AND  T1.OnlyOneSupportedValue = '{{Checkbox}}' AND  T1.OrderBy = '{{CheckboxSelection}}' AND  T1.Paging = '{{Col1}}' AND  T1.RangeNumber = '{{Col2}}' AND  T1.RawInFilter = '{{Col3}}' AND  T1.RawInSelect = '{{Col4}}' AND  T1.RecordsGrid = '{{Col5}}' AND  T1.RecordType = '{{Col6}}' AND  T1.RecordTypeId = {{ColCheckbox}} AND  T1.Regex = {{ColDate}} AND  T1.Required = {{ColDateTime}} AND  T1.RequiredIfBasic = {{ColDouble}} AND  T1.SelectWithHarcodedValues = {{ColInteger}} AND  T1.SelectWithManyOperationInFilter = {{ColMoney}} AND  T1.SelectWithManyOperationInFilterAliases = {{ColRichTextArea}} AND  T1.SelectWithManyOperationInSelect = {{ColText}} AND  T1.SelectWithOneOperationInFilter = {{ColTextArea}} AND  T1.SelectWithOneOperationInSelect = {{ColTime}} AND  T1.ShowChkbox = {{Comments}} AND  T1.ShowWhenBasic = {{CreatedDate}} AND  T1.SimpleDelete = '{{DeleteWithFilter}}' AND  T1.SimpleInsert = '{{EnableWhen5}}' AND  T1.SimpleSelect = {{FilterAtEnd}} AND  T1.SimpleSelectDistinct = {{FilterRaw}} AND  T1.SimpleSelectWithColumnAlias = '{{GridList}}' AND  T1.SimpleSelectWithNull = {{GroupByComplex}} AND  T1.SimpleSelectWithOneFilter = '{{GroupBySimple}}' AND  T1.SimpleSelectWithTableAlias = '{{HideEnableSampleId}}' AND  T1.SimpleSelectWithTwoFilters = '{{HideWhen2OrMore}}' AND  T1.SimpleSelectWithTwoFiltersAndAlias = {{InsertWithManyValues}} AND  T1.SimpleUpdate = {{InsertWithQuery}} AND  T1.TestAllTogether = {{InStatement}} AND  T1.TopStatement = {{IsCommited}} AND  T1.UpdateMultipleSets = {{IsEnabled}} AND  T2.UpdateWithFilter = {{IsNotNullStatement}} AND  T2.ValidationSampleId = {{IsNullStatement}} AND  T2.Values = '{{JoinAfterStatement}}' AND T2.VariableLength = {{JoinManyConstraints}} AND  T2.AggregatesComplex = '{{JoinMultipleTables}}' AND  T2.AggregatesSimple = {{JoinSampleId}} AND  T2.AnythinButValue = '{{JoinSimple}}' AND  T2.BasicColumnTypeId = '{{JoinWithFilters}}' AND  T2.CatalogTypeId = {{LikeStatement}} AND  T2.CatalogTypeName = '{{Multiselect}}' AND  T2.CatalogTypeText = '{{Multiselection}}' AND  T2.CatalogValueDisplayEnabled = '{{MultiSelectSampleId}}' AND  T2.CatalogValueId = '{{NotBeforeTwoDays}}' AND  T2.CatalogValueName = {{NotInStatement}} AND  T2.CatalogValueText = '{{NotLikeStatement}}' AND  T2.Category = '{{OnlyOneSupportedValue}}' AND  T2.CategoryId = '{{OrderBy}}' AND  T2.Checkbox = {{Paging}} AND  T2.CheckboxSelection = '{{RangeNumber}}' AND  T2.Col1 = {{RawInFilter}} AND  T2.Col2 = '{{RawInSelect}}' AND  T2.Col3 = '{{RecordsGrid}}' AND  T2.Col4 = '{{RecordType}}' AND  T2.Col5 = {{RecordTypeId}} AND  T2.Col6 = '{{Regex}}' AND  T2.ColCheckbox = '{{Required}}' AND  T2.ColDate = '{{RequiredIfBasic}}' AND  T2.ColDateTime = {{SelectWithHarcodedValues}} AND  T2.ColDouble = '{{SelectWithManyOperationInFilter}}' AND  T2.ColInteger = {{SelectWithManyOperationInFilterAliases}} AND  T2.ColMoney = '{{SelectWithManyOperationInSelect}}' AND  T2.ColRichTextArea = '{{SelectWithOneOperationInFilter}}' AND  T2.ColText = {{SelectWithOneOperationInSelect}} AND  T2.ColTextArea = '{{ShowChkbox}}' AND  T2.ColTime = '{{ShowWhenBasic}}' AND  T2.Comments = '{{SimpleDelete}}' AND  T2.CreatedDate = '{{SimpleInsert}}' AND  T2.DeleteWithFilter = '{{SimpleSelect}}' AND  T2.EnableWhen5 = {{SimpleSelectDistinct}} AND  T2.FilterAtEnd = '{{SimpleSelectWithColumnAlias}}' AND  T2.FilterRaw = '{{SimpleSelectWithNull}}' AND  T2.GridList = {{SimpleSelectWithOneFilter}} AND  T2.GroupByComplex = '{{SimpleSelectWithTableAlias}}' AND  T2.GroupBySimple = {{SimpleSelectWithTwoFilters}} AND  T3.HideEnableSampleId = '{{SimpleSelectWithTwoFiltersAndAlias}}' AND  T3.HideWhen2OrMore = {{SimpleUpdate}} AND  T3.InsertWithManyValues = '{{TestAllTogether}}' AND  T3.InsertWithQuery = '{{TopStatement}}' AND  T3.InStatement = '{{UpdateMultipleSets}}' AND  T3.IsCommited = {{UpdateWithFilter}} AND  T3.IsEnabled = '{{ValidationSampleId}}' AND  T3.IsNotNullStatement = '{{Values}}' AND  T3.IsNullStatement = '{{VariableLength}}')
Join(INNER, Table1 AS T1, T1.VariableLength = {{JoinManyConstraints}})
Join(INNER, Table2 AS T2, T2.JoinManyConstraints = '{{AnythinButValue}}')
Join(INNER, Table3 AS T3, T3.JoinMultipleTables = {{BasicColumnTypeId}})
";
            var q = this.GetALexString(aLex, _Dictionary);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\t{0}\t{1}\t{2}\t{3}", string.Join('\t', MethodBase.GetCurrentMethod().ReflectedType.ToString().Split('.')), MethodBase.GetCurrentMethod().Name, q.Time, q.Converted.Replace("\r\n", " ").Replace("\n", " "));
            base.Log(builder.ToString());
            Assert.Equal(@"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement,My.NotLikeStatement,T1.OnlyOneSupportedValue,T1.OrderBy,T1.Paging,T1.RangeNumber,T1.RawInFilter,T1.RawInSelect,T1.RecordsGrid,T1.RecordType,T1.RecordTypeId,T1.Regex,T1.Required,T1.RequiredIfBasic,T1.SelectWithHarcodedValues,T1.SelectWithManyOperationInFilter,T1.SelectWithManyOperationInFilterAliases,T1.SelectWithManyOperationInSelect,T1.SelectWithOneOperationInFilter,T1.SelectWithOneOperationInSelect,T1.ShowChkbox,T1.ShowWhenBasic,T1.SimpleDelete,T1.SimpleInsert,T1.SimpleSelect,T1.SimpleSelectDistinct,T1.SimpleSelectWithColumnAlias,T1.SimpleSelectWithNull,T1.SimpleSelectWithOneFilter,T1.SimpleSelectWithTableAlias,T1.SimpleSelectWithTwoFilters,T1.SimpleSelectWithTwoFiltersAndAlias,T1.SimpleUpdate,T1.TestAllTogether,T1.TopStatement,T1.UpdateMultipleSets,T2.UpdateWithFilter,T2.ValidationSampleId,T2.Values, T2.VariableLength,T2.AggregatesComplex,T2.AggregatesSimple,T2.AnythinButValue,T2.BasicColumnTypeId,T2.CatalogTypeId,T2.CatalogTypeName,T2.CatalogTypeText,T2.CatalogValueDisplayEnabled,T2.CatalogValueId,T2.CatalogValueName,T2.CatalogValueText,T2.Category,T2.CategoryId,T2.Checkbox,T2.CheckboxSelection,T2.Col1,T2.Col2,T2.Col3,T2.Col4,T2.Col5,T2.Col6,T2.ColCheckbox,T2.ColDate,T2.ColDateTime,T2.ColDouble,T2.ColInteger,T2.ColMoney,T2.ColRichTextArea,T2.ColText,T2.ColTextArea,T2.ColTime,T2.Comments,T2.CreatedDate,T2.DeleteWithFilter,T2.EnableWhen5,T2.FilterAtEnd,T2.FilterRaw,T2.GridList,T2.GroupByComplex,T2.GroupBySimple,T3.HideEnableSampleId,T3.HideWhen2OrMore,T3.InsertWithManyValues,T3.InsertWithQuery,T3.InStatement,T3.IsCommited,T3.IsEnabled,T3.IsNotNullStatement,T3.IsNullStatement)
Filter(My.JoinAfterStatement = 'kfoucar26@reference.com' AND  My.JoinManyConstraints = 'mblethynt@macromedia.com' AND  My.JoinMultipleTables = 'kgladtbache@utexas.edu' AND  My.JoinSampleId = 9 AND  My.JoinSimple = 1 AND  My.JoinWithFilters = 2 AND  My.LikeStatement = 3 AND  My.Multiselect = 5 AND  My.Multiselection = 6 AND  My.MultiSelectSampleId = 7 AND  My.NotBeforeTwoDays = 8 AND  My.NotInStatement = 'dbannerman0@va.gov' AND  My.NotLikeStatement = 20 AND  T1.OnlyOneSupportedValue = 'cyeabsley3@rambler.ru' AND  T1.OrderBy = 'cbunstoneb@technorati.com' AND  T1.Paging = 'fgurdon2i@cbsnews.com' AND  T1.RangeNumber = 'rplacide2j@fotki.com' AND  T1.RawInFilter = 'kwimpeney2k@house.gov' AND  T1.RawInSelect = 'mcomelli2l@discuz.net' AND  T1.RecordsGrid = 'kmiettinen2m@intel.com' AND  T1.RecordType = 'rhuc2n@sun.com' AND  T1.RecordTypeId = 10 AND  T1.Regex = 11 AND  T1.Required = 12 AND  T1.RequiredIfBasic = 13 AND  T1.SelectWithHarcodedValues = 14 AND  T1.SelectWithManyOperationInFilter = 15 AND  T1.SelectWithManyOperationInFilterAliases = 16 AND  T1.SelectWithManyOperationInSelect = 17 AND  T1.SelectWithOneOperationInFilter = 18 AND  T1.SelectWithOneOperationInSelect = 19 AND  T1.ShowChkbox = 21 AND  T1.ShowWhenBasic = 22 AND  T1.SimpleDelete = 'mcurreeno@sina.com.cn' AND  T1.SimpleInsert = 'fkealy4@economist.com' AND  T1.SimpleSelect = 44 AND  T1.SimpleSelectDistinct = 48 AND  T1.SimpleSelectWithColumnAlias = 'ophlippi6@eepurl.com' AND  T1.SimpleSelectWithNull = 59 AND  T1.SimpleSelectWithOneFilter = 'sbloomer27@indiegogo.com' AND  T1.SimpleSelectWithTableAlias = 'cpurtell2@unesco.org' AND  T1.SimpleSelectWithTwoFilters = 'aswinney5@uiuc.edu' AND  T1.SimpleSelectWithTwoFiltersAndAlias = 53 AND  T1.SimpleUpdate = 46 AND  T1.TestAllTogether = 50 AND  T1.TopStatement = 25 AND  T1.UpdateMultipleSets = 4 AND  T2.UpdateWithFilter = 60 AND  T2.ValidationSampleId = 51 AND  T2.Values = 'hbyerx@earthlink.net' AND T2.VariableLength = 57 AND  T2.AggregatesComplex = 'pgoldney2c@senate.gov' AND  T2.AggregatesSimple = 23 AND  T2.AnythinButValue = 'wmumby2b@mail.ru' AND  T2.BasicColumnTypeId = 'cmaylotts@nsw.gov.au' AND  T2.CatalogTypeId = 61 AND  T2.CatalogTypeName = 'taldred7@creativecommons.org' AND  T2.CatalogTypeText = 'ditzkovitchc@i2i.jp' AND  T2.CatalogValueDisplayEnabled = 'tcolgravea@networkadvertising.org' AND  T2.CatalogValueId = 'ltruslerf@taobao.com' AND  T2.CatalogValueName = 63 AND  T2.CatalogValueText = 'snewarte24@princeton.edu' AND  T2.Category = 'ccollacombeg@com.com' AND  T2.CategoryId = 'dhydechambers2d@blog.com' AND  T2.Checkbox = 54 AND  T2.CheckboxSelection = 'jmowbrayh@simplemachines.org' AND  T2.Col1 = 47 AND  T2.Col2 = 'etooley2h@prweb.com' AND  T2.Col3 = 'bduddend@dyndns.org' AND  T2.Col4 = 'efashion1@mail.ru' AND  T2.Col5 = 24 AND  T2.Col6 = 'acordelettei@topsy.com' AND  T2.ColCheckbox = 'keslerj@youku.com' AND  T2.ColDate = 'jbousfieldk@seesaa.net' AND  T2.ColDateTime = 43 AND  T2.ColDouble = 'fpurle28@cmu.edu' AND  T2.ColInteger = 49 AND  T2.ColMoney = 'hbarribalr@nba.com' AND  T2.ColRichTextArea = 'dmcelwee2f@cloudflare.com' AND  T2.ColText = 62 AND  T2.ColTextArea = 'shanway8@is.gd' AND  T2.ColTime = 'zbarlass9@usda.gov' AND  T2.Comments = 'ralbasiniv@irs.gov' AND  T2.CreatedDate = 'dfeldhammeru@netlog.com' AND  T2.DeleteWithFilter = 'ftarte2e@miibeian.gov.cn' AND  T2.EnableWhen5 = 52 AND  T2.FilterAtEnd = 'jeakly2g@nytimes.com' AND  T2.FilterRaw = 'mmacarthur2a@friendfeed.com' AND  T2.GridList = 58 AND  T2.GroupByComplex = 'cmillp@virginia.edu' AND  T2.GroupBySimple = 45 AND  T3.HideEnableSampleId = 'rtucknutt29@ucoz.com' AND  T3.HideWhen2OrMore = 56 AND  T3.InsertWithManyValues = 'drosenauw@imgur.com' AND  T3.InsertWithQuery = 'dmcniven25@exblog.jp' AND  T3.InStatement = 'tdellcasaq@imageshack.us' AND  T3.IsCommited = 55 AND  T3.IsEnabled = 'ymendenhalll@xinhuanet.com' AND  T3.IsNotNullStatement = 'iwinkelln@yahoo.com' AND  T3.IsNullStatement = 'rrimmerm@reverbnation.com')
Join(INNER, Table1 AS T1, T1.VariableLength = 57)
Join(INNER, Table2 AS T2, T2.JoinManyConstraints = 'kgladtbache@utexas.edu')
Join(INNER, Table3 AS T3, T3.JoinMultipleTables = 9)
", q.Converted);
        }

        [Fact]
        public void ParseQueryWithDictionaryLarge()
        {
            var query = @"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement,My.NotLikeStatement,T1.OnlyOneSupportedValue,T1.OrderBy,T1.Paging,T1.RangeNumber,T1.RawInFilter,T1.RawInSelect,T1.RecordsGrid,T1.RecordType,T1.RecordTypeId,T1.Regex,T1.Required,T1.RequiredIfBasic,T1.SelectWithHarcodedValues,T1.SelectWithManyOperationInFilter,T1.SelectWithManyOperationInFilterAliases,T1.SelectWithManyOperationInSelect,T1.SelectWithOneOperationInFilter,T1.SelectWithOneOperationInSelect,T1.ShowChkbox,T1.ShowWhenBasic,T1.SimpleDelete,T1.SimpleInsert,T1.SimpleSelect,T1.SimpleSelectDistinct,T1.SimpleSelectWithColumnAlias,T1.SimpleSelectWithNull,T1.SimpleSelectWithOneFilter,T1.SimpleSelectWithTableAlias,T1.SimpleSelectWithTwoFilters,T1.SimpleSelectWithTwoFiltersAndAlias,T1.SimpleUpdate,T1.TestAllTogether,T1.TopStatement,T1.UpdateMultipleSets,T2.UpdateWithFilter,T2.ValidationSampleId,T2.Values, T2.VariableLength,T2.AggregatesComplex,T2.AggregatesSimple,T2.AnythinButValue,T2.BasicColumnTypeId,T2.CatalogTypeId,T2.CatalogTypeName,T2.CatalogTypeText,T2.CatalogValueDisplayEnabled,T2.CatalogValueId,T2.CatalogValueName,T2.CatalogValueText,T2.Category,T2.CategoryId,T2.Checkbox,T2.CheckboxSelection,T2.Col1,T2.Col2,T2.Col3,T2.Col4,T2.Col5,T2.Col6,T2.ColCheckbox,T2.ColDate,T2.ColDateTime,T2.ColDouble,T2.ColInteger,T2.ColMoney,T2.ColRichTextArea,T2.ColText,T2.ColTextArea,T2.ColTime,T2.Comments,T2.CreatedDate,T2.DeleteWithFilter,T2.EnableWhen5,T2.FilterAtEnd,T2.FilterRaw,T2.GridList,T2.GroupByComplex,T2.GroupBySimple,T3.HideEnableSampleId,T3.HideWhen2OrMore,T3.InsertWithManyValues,T3.InsertWithQuery,T3.InStatement,T3.IsCommited,T3.IsEnabled,T3.IsNotNullStatement,T3.IsNullStatement)
Filter(My.JoinAfterStatement = '{{AggregatesComplex}}' AND  My.JoinManyConstraints = '{{AggregatesSimple}}' AND  My.JoinMultipleTables = '{{AnythinButValue}}' AND  My.JoinSampleId = {{BasicColumnTypeId}} AND  My.JoinSimple = {{CatalogTypeId}} AND  My.JoinWithFilters = {{CatalogTypeName}} AND  My.LikeStatement = {{CatalogTypeText}} AND  My.Multiselect = {{CatalogValueDisplayEnabled}} AND  My.Multiselection = {{CatalogValueId}} AND  My.MultiSelectSampleId = {{CatalogValueName}} AND  My.NotBeforeTwoDays = {{CatalogValueText}} AND  My.NotInStatement = '{{Category}}' AND  My.NotLikeStatement = {{CategoryId}} AND  T1.OnlyOneSupportedValue = '{{Checkbox}}' AND  T1.OrderBy = '{{CheckboxSelection}}' AND  T1.Paging = '{{Col1}}' AND  T1.RangeNumber = '{{Col2}}' AND  T1.RawInFilter = '{{Col3}}' AND  T1.RawInSelect = '{{Col4}}' AND  T1.RecordsGrid = '{{Col5}}' AND  T1.RecordType = '{{Col6}}' AND  T1.RecordTypeId = {{ColCheckbox}} AND  T1.Regex = {{ColDate}} AND  T1.Required = {{ColDateTime}} AND  T1.RequiredIfBasic = {{ColDouble}} AND  T1.SelectWithHarcodedValues = {{ColInteger}} AND  T1.SelectWithManyOperationInFilter = {{ColMoney}} AND  T1.SelectWithManyOperationInFilterAliases = {{ColRichTextArea}} AND  T1.SelectWithManyOperationInSelect = {{ColText}} AND  T1.SelectWithOneOperationInFilter = {{ColTextArea}} AND  T1.SelectWithOneOperationInSelect = {{ColTime}} AND  T1.ShowChkbox = {{Comments}} AND  T1.ShowWhenBasic = {{CreatedDate}} AND  T1.SimpleDelete = '{{DeleteWithFilter}}' AND  T1.SimpleInsert = '{{EnableWhen5}}' AND  T1.SimpleSelect = {{FilterAtEnd}} AND  T1.SimpleSelectDistinct = {{FilterRaw}} AND  T1.SimpleSelectWithColumnAlias = '{{GridList}}' AND  T1.SimpleSelectWithNull = {{GroupByComplex}} AND  T1.SimpleSelectWithOneFilter = '{{GroupBySimple}}' AND  T1.SimpleSelectWithTableAlias = '{{HideEnableSampleId}}' AND  T1.SimpleSelectWithTwoFilters = '{{HideWhen2OrMore}}' AND  T1.SimpleSelectWithTwoFiltersAndAlias = {{InsertWithManyValues}} AND  T1.SimpleUpdate = {{InsertWithQuery}} AND  T1.TestAllTogether = {{InStatement}} AND  T1.TopStatement = {{IsCommited}} AND  T1.UpdateMultipleSets = {{IsEnabled}} AND  T2.UpdateWithFilter = {{IsNotNullStatement}} AND  T2.ValidationSampleId = {{IsNullStatement}} AND  T2.Values = '{{JoinAfterStatement}}' AND T2.VariableLength = {{JoinManyConstraints}} AND  T2.AggregatesComplex = '{{JoinMultipleTables}}' AND  T2.AggregatesSimple = {{JoinSampleId}} AND  T2.AnythinButValue = '{{JoinSimple}}' AND  T2.BasicColumnTypeId = '{{JoinWithFilters}}' AND  T2.CatalogTypeId = {{LikeStatement}} AND  T2.CatalogTypeName = '{{Multiselect}}' AND  T2.CatalogTypeText = '{{Multiselection}}' AND  T2.CatalogValueDisplayEnabled = '{{MultiSelectSampleId}}' AND  T2.CatalogValueId = '{{NotBeforeTwoDays}}' AND  T2.CatalogValueName = {{NotInStatement}} AND  T2.CatalogValueText = '{{NotLikeStatement}}' AND  T2.Category = '{{OnlyOneSupportedValue}}' AND  T2.CategoryId = '{{OrderBy}}' AND  T2.Checkbox = {{Paging}} AND  T2.CheckboxSelection = '{{RangeNumber}}' AND  T2.Col1 = {{RawInFilter}} AND  T2.Col2 = '{{RawInSelect}}' AND  T2.Col3 = '{{RecordsGrid}}' AND  T2.Col4 = '{{RecordType}}' AND  T2.Col5 = {{RecordTypeId}} AND  T2.Col6 = '{{Regex}}' AND  T2.ColCheckbox = '{{Required}}' AND  T2.ColDate = '{{RequiredIfBasic}}' AND  T2.ColDateTime = {{SelectWithHarcodedValues}} AND  T2.ColDouble = '{{SelectWithManyOperationInFilter}}' AND  T2.ColInteger = {{SelectWithManyOperationInFilterAliases}} AND  T2.ColMoney = '{{SelectWithManyOperationInSelect}}' AND  T2.ColRichTextArea = '{{SelectWithOneOperationInFilter}}' AND  T2.ColText = {{SelectWithOneOperationInSelect}} AND  T2.ColTextArea = '{{ShowChkbox}}' AND  T2.ColTime = '{{ShowWhenBasic}}' AND  T2.Comments = '{{SimpleDelete}}' AND  T2.CreatedDate = '{{SimpleInsert}}' AND  T2.DeleteWithFilter = '{{SimpleSelect}}' AND  T2.EnableWhen5 = {{SimpleSelectDistinct}} AND  T2.FilterAtEnd = '{{SimpleSelectWithColumnAlias}}' AND  T2.FilterRaw = '{{SimpleSelectWithNull}}' AND  T2.GridList = {{SimpleSelectWithOneFilter}} AND  T2.GroupByComplex = '{{SimpleSelectWithTableAlias}}' AND  T2.GroupBySimple = {{SimpleSelectWithTwoFilters}} AND  T3.HideEnableSampleId = '{{SimpleSelectWithTwoFiltersAndAlias}}' AND  T3.HideWhen2OrMore = {{SimpleUpdate}} AND  T3.InsertWithManyValues = '{{TestAllTogether}}' AND  T3.InsertWithQuery = '{{TopStatement}}' AND  T3.InStatement = '{{UpdateMultipleSets}}' AND  T3.IsCommited = {{UpdateWithFilter}} AND  T3.IsEnabled = '{{ValidationSampleId}}' AND  T3.IsNotNullStatement = '{{Values}}' AND  T3.IsNullStatement = '{{VariableLength}}')
Join(INNER, Table1 AS T1, T1.VariableLength = {{JoinManyConstraints}})
Join(INNER, Table2 AS T2, T2.JoinManyConstraints = '{{AnythinButValue}}')
Join(INNER, Table3 AS T3, T3.JoinMultipleTables = {{BasicColumnTypeId}})
";
            var aLex = this.GetALexString(query, _Dictionary);
            var q = base.GetQuery(aLex.Converted);
            Assert.Equal(@"SELECT [My].[JoinAfterStatement], [My].[JoinManyConstraints], [My].[JoinMultipleTables], [My].[JoinSampleId], [My].[JoinSimple], [My].[JoinWithFilters], [My].[LikeStatement], [My].[Multiselect], [My].[Multiselection], [My].[MultiSelectSampleId], [My].[NotBeforeTwoDays], [My].[NotInStatement], [My].[NotLikeStatement], [T1].[OnlyOneSupportedValue], [T1].[OrderBy], [T1].[Paging], [T1].[RangeNumber], [T1].[RawInFilter], [T1].[RawInSelect], [T1].[RecordsGrid], [T1].[RecordType], [T1].[RecordTypeId], [T1].[Regex], [T1].[Required], [T1].[RequiredIfBasic], [T1].[SelectWithHarcodedValues], [T1].[SelectWithManyOperationInFilter], [T1].[SelectWithManyOperationInFilterAliases], [T1].[SelectWithManyOperationInSelect], [T1].[SelectWithOneOperationInFilter], [T1].[SelectWithOneOperationInSelect], [T1].[ShowChkbox], [T1].[ShowWhenBasic], [T1].[SimpleDelete], [T1].[SimpleInsert], [T1].[SimpleSelect], [T1].[SimpleSelectDistinct], [T1].[SimpleSelectWithColumnAlias], [T1].[SimpleSelectWithNull], [T1].[SimpleSelectWithOneFilter], [T1].[SimpleSelectWithTableAlias], [T1].[SimpleSelectWithTwoFilters], [T1].[SimpleSelectWithTwoFiltersAndAlias], [T1].[SimpleUpdate], [T1].[TestAllTogether], [T1].[TopStatement], [T1].[UpdateMultipleSets], [T2].[UpdateWithFilter], [T2].[ValidationSampleId], [T2].[Values], [T2].[VariableLength], [T2].[AggregatesComplex], [T2].[AggregatesSimple], [T2].[AnythinButValue], [T2].[BasicColumnTypeId], [T2].[CatalogTypeId], [T2].[CatalogTypeName], [T2].[CatalogTypeText], [T2].[CatalogValueDisplayEnabled], [T2].[CatalogValueId], [T2].[CatalogValueName], [T2].[CatalogValueText], [T2].[Category], [T2].[CategoryId], [T2].[Checkbox], [T2].[CheckboxSelection], [T2].[Col1], [T2].[Col2], [T2].[Col3], [T2].[Col4], [T2].[Col5], [T2].[Col6], [T2].[ColCheckbox], [T2].[ColDate], [T2].[ColDateTime], [T2].[ColDouble], [T2].[ColInteger], [T2].[ColMoney], [T2].[ColRichTextArea], [T2].[ColText], [T2].[ColTextArea], [T2].[ColTime], [T2].[Comments], [T2].[CreatedDate], [T2].[DeleteWithFilter], [T2].[EnableWhen5], [T2].[FilterAtEnd], [T2].[FilterRaw], [T2].[GridList], [T2].[GroupByComplex], [T2].[GroupBySimple], [T3].[HideEnableSampleId], [T3].[HideWhen2OrMore], [T3].[InsertWithManyValues], [T3].[InsertWithQuery], [T3].[InStatement], [T3].[IsCommited], [T3].[IsEnabled], [T3].[IsNotNullStatement], [T3].[IsNullStatement] FROM [My].[Table] AS [My] 
INNER JOIN [Table1] AS [T1] ON ([T1].[VariableLength] =  57)
INNER JOIN [Table2] AS [T2] ON ([T2].[JoinManyConstraints] =  'kgladtbache@utexas.edu')
INNER JOIN [Table3] AS [T3] ON ([T3].[JoinMultipleTables] =  9) WHERE ([My].[JoinAfterStatement] =  'kfoucar26@reference.com' AND [My].[JoinManyConstraints] =  'mblethynt@macromedia.com' AND [My].[JoinMultipleTables] =  'kgladtbache@utexas.edu' AND [My].[JoinSampleId] =  9 AND [My].[JoinSimple] =  1 AND [My].[JoinWithFilters] =  2 AND [My].[LikeStatement] =  3 AND [My].[Multiselect] =  5 AND [My].[Multiselection] =  6 AND [My].[MultiSelectSampleId] =  7 AND [My].[NotBeforeTwoDays] =  8 AND [My].[NotInStatement] =  'dbannerman0@va.gov' AND [My].[NotLikeStatement] =  20 AND [T1].[OnlyOneSupportedValue] =  'cyeabsley3@rambler.ru' AND [T1].[OrderBy] =  'cbunstoneb@technorati.com' AND [T1].[Paging] =  'fgurdon2i@cbsnews.com' AND [T1].[RangeNumber] =  'rplacide2j@fotki.com' AND [T1].[RawInFilter] =  'kwimpeney2k@house.gov' AND [T1].[RawInSelect] =  'mcomelli2l@discuz.net' AND [T1].[RecordsGrid] =  'kmiettinen2m@intel.com' AND [T1].[RecordType] =  'rhuc2n@sun.com' AND [T1].[RecordTypeId] =  10 AND [T1].[Regex] =  11 AND [T1].[Required] =  12 AND [T1].[RequiredIfBasic] =  13 AND [T1].[SelectWithHarcodedValues] =  14 AND [T1].[SelectWithManyOperationInFilter] =  15 AND [T1].[SelectWithManyOperationInFilterAliases] =  16 AND [T1].[SelectWithManyOperationInSelect] =  17 AND [T1].[SelectWithOneOperationInFilter] =  18 AND [T1].[SelectWithOneOperationInSelect] =  19 AND [T1].[ShowChkbox] =  21 AND [T1].[ShowWhenBasic] =  22 AND [T1].[SimpleDelete] =  'mcurreeno@sina.com.cn' AND [T1].[SimpleInsert] =  'fkealy4@economist.com' AND [T1].[SimpleSelect] =  44 AND [T1].[SimpleSelectDistinct] =  48 AND [T1].[SimpleSelectWithColumnAlias] =  'ophlippi6@eepurl.com' AND [T1].[SimpleSelectWithNull] =  59 AND [T1].[SimpleSelectWithOneFilter] =  'sbloomer27@indiegogo.com' AND [T1].[SimpleSelectWithTableAlias] =  'cpurtell2@unesco.org' AND [T1].[SimpleSelectWithTwoFilters] =  'aswinney5@uiuc.edu' AND [T1].[SimpleSelectWithTwoFiltersAndAlias] =  53 AND [T1].[SimpleUpdate] =  46 AND [T1].[TestAllTogether] =  50 AND [T1].[TopStatement] =  25 AND [T1].[UpdateMultipleSets] =  4 AND [T2].[UpdateWithFilter] =  60 AND [T2].[ValidationSampleId] =  51 AND [T2].[Values] =  'hbyerx@earthlink.net' AND [T2].[VariableLength] =  57 AND [T2].[AggregatesComplex] =  'pgoldney2c@senate.gov' AND [T2].[AggregatesSimple] =  23 AND [T2].[AnythinButValue] =  'wmumby2b@mail.ru' AND [T2].[BasicColumnTypeId] =  'cmaylotts@nsw.gov.au' AND [T2].[CatalogTypeId] =  61 AND [T2].[CatalogTypeName] =  'taldred7@creativecommons.org' AND [T2].[CatalogTypeText] =  'ditzkovitchc@i2i.jp' AND [T2].[CatalogValueDisplayEnabled] =  'tcolgravea@networkadvertising.org' AND [T2].[CatalogValueId] =  'ltruslerf@taobao.com' AND [T2].[CatalogValueName] =  63 AND [T2].[CatalogValueText] =  'snewarte24@princeton.edu' AND [T2].[Category] =  'ccollacombeg@com.com' AND [T2].[CategoryId] =  'dhydechambers2d@blog.com' AND [T2].[Checkbox] =  54 AND [T2].[CheckboxSelection] =  'jmowbrayh@simplemachines.org' AND [T2].[Col1] =  47 AND [T2].[Col2] =  'etooley2h@prweb.com' AND [T2].[Col3] =  'bduddend@dyndns.org' AND [T2].[Col4] =  'efashion1@mail.ru' AND [T2].[Col5] =  24 AND [T2].[Col6] =  'acordelettei@topsy.com' AND [T2].[ColCheckbox] =  'keslerj@youku.com' AND [T2].[ColDate] =  'jbousfieldk@seesaa.net' AND [T2].[ColDateTime] =  43 AND [T2].[ColDouble] =  'fpurle28@cmu.edu' AND [T2].[ColInteger] =  49 AND [T2].[ColMoney] =  'hbarribalr@nba.com' AND [T2].[ColRichTextArea] =  'dmcelwee2f@cloudflare.com' AND [T2].[ColText] =  62 AND [T2].[ColTextArea] =  'shanway8@is.gd' AND [T2].[ColTime] =  'zbarlass9@usda.gov' AND [T2].[Comments] =  'ralbasiniv@irs.gov' AND [T2].[CreatedDate] =  'dfeldhammeru@netlog.com' AND [T2].[DeleteWithFilter] =  'ftarte2e@miibeian.gov.cn' AND [T2].[EnableWhen5] =  52 AND [T2].[FilterAtEnd] =  'jeakly2g@nytimes.com' AND [T2].[FilterRaw] =  'mmacarthur2a@friendfeed.com' AND [T2].[GridList] =  58 AND [T2].[GroupByComplex] =  'cmillp@virginia.edu' AND [T2].[GroupBySimple] =  45 AND [T3].[HideEnableSampleId] =  'rtucknutt29@ucoz.com' AND [T3].[HideWhen2OrMore] =  56 AND [T3].[InsertWithManyValues] =  'drosenauw@imgur.com' AND [T3].[InsertWithQuery] =  'dmcniven25@exblog.jp' AND [T3].[InStatement] =  'tdellcasaq@imageshack.us' AND [T3].[IsCommited] =  55 AND [T3].[IsEnabled] =  'ymendenhalll@xinhuanet.com' AND [T3].[IsNotNullStatement] =  'iwinkelln@yahoo.com' AND [T3].[IsNullStatement] =  'rrimmerm@reverbnation.com')"
, base.GetSentence(q));
        }

        [Fact]
        public void ParseQueryWithDataLarge()
        {
            var query = @"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement,My.NotLikeStatement,T1.OnlyOneSupportedValue,T1.OrderBy,T1.Paging,T1.RangeNumber,T1.RawInFilter,T1.RawInSelect,T1.RecordsGrid,T1.RecordType,T1.RecordTypeId,T1.Regex,T1.Required,T1.RequiredIfBasic,T1.SelectWithHarcodedValues,T1.SelectWithManyOperationInFilter,T1.SelectWithManyOperationInFilterAliases,T1.SelectWithManyOperationInSelect,T1.SelectWithOneOperationInFilter,T1.SelectWithOneOperationInSelect,T1.ShowChkbox,T1.ShowWhenBasic,T1.SimpleDelete,T1.SimpleInsert,T1.SimpleSelect,T1.SimpleSelectDistinct,T1.SimpleSelectWithColumnAlias,T1.SimpleSelectWithNull,T1.SimpleSelectWithOneFilter,T1.SimpleSelectWithTableAlias,T1.SimpleSelectWithTwoFilters,T1.SimpleSelectWithTwoFiltersAndAlias,T1.SimpleUpdate,T1.TestAllTogether,T1.TopStatement,T1.UpdateMultipleSets,T2.UpdateWithFilter,T2.ValidationSampleId,T2.Values, T2.VariableLength,T2.AggregatesComplex,T2.AggregatesSimple,T2.AnythinButValue,T2.BasicColumnTypeId,T2.CatalogTypeId,T2.CatalogTypeName,T2.CatalogTypeText,T2.CatalogValueDisplayEnabled,T2.CatalogValueId,T2.CatalogValueName,T2.CatalogValueText,T2.Category,T2.CategoryId,T2.Checkbox,T2.CheckboxSelection,T2.Col1,T2.Col2,T2.Col3,T2.Col4,T2.Col5,T2.Col6,T2.ColCheckbox,T2.ColDate,T2.ColDateTime,T2.ColDouble,T2.ColInteger,T2.ColMoney,T2.ColRichTextArea,T2.ColText,T2.ColTextArea,T2.ColTime,T2.Comments,T2.CreatedDate,T2.DeleteWithFilter,T2.EnableWhen5,T2.FilterAtEnd,T2.FilterRaw,T2.GridList,T2.GroupByComplex,T2.GroupBySimple,T3.HideEnableSampleId,T3.HideWhen2OrMore,T3.InsertWithManyValues,T3.InsertWithQuery,T3.InStatement,T3.IsCommited,T3.IsEnabled,T3.IsNotNullStatement,T3.IsNullStatement)
Filter(My.JoinAfterStatement = '{{AggregatesComplex}}' AND  My.JoinManyConstraints = '{{AggregatesSimple}}' AND  My.JoinMultipleTables = '{{AnythinButValue}}' AND  My.JoinSampleId = {{BasicColumnTypeId}} AND  My.JoinSimple = {{CatalogTypeId}} AND  My.JoinWithFilters = {{CatalogTypeName}} AND  My.LikeStatement = {{CatalogTypeText}} AND  My.Multiselect = {{CatalogValueDisplayEnabled}} AND  My.Multiselection = {{CatalogValueId}} AND  My.MultiSelectSampleId = {{CatalogValueName}} AND  My.NotBeforeTwoDays = {{CatalogValueText}} AND  My.NotInStatement = '{{Category}}' AND  My.NotLikeStatement = {{CategoryId}} AND  T1.OnlyOneSupportedValue = '{{Checkbox}}' AND  T1.OrderBy = '{{CheckboxSelection}}' AND  T1.Paging = '{{Col1}}' AND  T1.RangeNumber = '{{Col2}}' AND  T1.RawInFilter = '{{Col3}}' AND  T1.RawInSelect = '{{Col4}}' AND  T1.RecordsGrid = '{{Col5}}' AND  T1.RecordType = '{{Col6}}' AND  T1.RecordTypeId = {{ColCheckbox}} AND  T1.Regex = {{ColDate}} AND  T1.Required = {{ColDateTime}} AND  T1.RequiredIfBasic = {{ColDouble}} AND  T1.SelectWithHarcodedValues = {{ColInteger}} AND  T1.SelectWithManyOperationInFilter = {{ColMoney}} AND  T1.SelectWithManyOperationInFilterAliases = {{ColRichTextArea}} AND  T1.SelectWithManyOperationInSelect = {{ColText}} AND  T1.SelectWithOneOperationInFilter = {{ColTextArea}} AND  T1.SelectWithOneOperationInSelect = {{ColTime}} AND  T1.ShowChkbox = {{Comments}} AND  T1.ShowWhenBasic = {{CreatedDate}} AND  T1.SimpleDelete = '{{DeleteWithFilter}}' AND  T1.SimpleInsert = '{{EnableWhen5}}' AND  T1.SimpleSelect = {{FilterAtEnd}} AND  T1.SimpleSelectDistinct = {{FilterRaw}} AND  T1.SimpleSelectWithColumnAlias = '{{GridList}}' AND  T1.SimpleSelectWithNull = {{GroupByComplex}} AND  T1.SimpleSelectWithOneFilter = '{{GroupBySimple}}' AND  T1.SimpleSelectWithTableAlias = '{{HideEnableSampleId}}' AND  T1.SimpleSelectWithTwoFilters = '{{HideWhen2OrMore}}' AND  T1.SimpleSelectWithTwoFiltersAndAlias = {{InsertWithManyValues}} AND  T1.SimpleUpdate = {{InsertWithQuery}} AND  T1.TestAllTogether = {{InStatement}} AND  T1.TopStatement = {{IsCommited}} AND  T1.UpdateMultipleSets = {{IsEnabled}} AND  T2.UpdateWithFilter = {{IsNotNullStatement}} AND  T2.ValidationSampleId = {{IsNullStatement}} AND  T2.Values = '{{JoinAfterStatement}}' AND T2.VariableLength = {{JoinManyConstraints}} AND  T2.AggregatesComplex = '{{JoinMultipleTables}}' AND  T2.AggregatesSimple = {{JoinSampleId}} AND  T2.AnythinButValue = '{{JoinSimple}}' AND  T2.BasicColumnTypeId = '{{JoinWithFilters}}' AND  T2.CatalogTypeId = {{LikeStatement}} AND  T2.CatalogTypeName = '{{Multiselect}}' AND  T2.CatalogTypeText = '{{Multiselection}}' AND  T2.CatalogValueDisplayEnabled = '{{MultiSelectSampleId}}' AND  T2.CatalogValueId = '{{NotBeforeTwoDays}}' AND  T2.CatalogValueName = {{NotInStatement}} AND  T2.CatalogValueText = '{{NotLikeStatement}}' AND  T2.Category = '{{OnlyOneSupportedValue}}' AND  T2.CategoryId = '{{OrderBy}}' AND  T2.Checkbox = {{Paging}} AND  T2.CheckboxSelection = '{{RangeNumber}}' AND  T2.Col1 = {{RawInFilter}} AND  T2.Col2 = '{{RawInSelect}}' AND  T2.Col3 = '{{RecordsGrid}}' AND  T2.Col4 = '{{RecordType}}' AND  T2.Col5 = {{RecordTypeId}} AND  T2.Col6 = '{{Regex}}' AND  T2.ColCheckbox = '{{Required}}' AND  T2.ColDate = '{{RequiredIfBasic}}' AND  T2.ColDateTime = {{SelectWithHarcodedValues}} AND  T2.ColDouble = '{{SelectWithManyOperationInFilter}}' AND  T2.ColInteger = {{SelectWithManyOperationInFilterAliases}} AND  T2.ColMoney = '{{SelectWithManyOperationInSelect}}' AND  T2.ColRichTextArea = '{{SelectWithOneOperationInFilter}}' AND  T2.ColText = {{SelectWithOneOperationInSelect}} AND  T2.ColTextArea = '{{ShowChkbox}}' AND  T2.ColTime = '{{ShowWhenBasic}}' AND  T2.Comments = '{{SimpleDelete}}' AND  T2.CreatedDate = '{{SimpleInsert}}' AND  T2.DeleteWithFilter = '{{SimpleSelect}}' AND  T2.EnableWhen5 = {{SimpleSelectDistinct}} AND  T2.FilterAtEnd = '{{SimpleSelectWithColumnAlias}}' AND  T2.FilterRaw = '{{SimpleSelectWithNull}}' AND  T2.GridList = {{SimpleSelectWithOneFilter}} AND  T2.GroupByComplex = '{{SimpleSelectWithTableAlias}}' AND  T2.GroupBySimple = {{SimpleSelectWithTwoFilters}} AND  T3.HideEnableSampleId = '{{SimpleSelectWithTwoFiltersAndAlias}}' AND  T3.HideWhen2OrMore = {{SimpleUpdate}} AND  T3.InsertWithManyValues = '{{TestAllTogether}}' AND  T3.InsertWithQuery = '{{TopStatement}}' AND  T3.InStatement = '{{UpdateMultipleSets}}' AND  T3.IsCommited = {{UpdateWithFilter}} AND  T3.IsEnabled = '{{ValidationSampleId}}' AND  T3.IsNotNullStatement = '{{Values}}' AND  T3.IsNullStatement = '{{VariableLength}}')
Join(INNER, Table1 AS T1, T1.VariableLength = {{JoinManyConstraints}})
Join(INNER, Table2 AS T2, T2.JoinManyConstraints = '{{AnythinButValue}}')
Join(INNER, Table3 AS T3, T3.JoinMultipleTables = {{BasicColumnTypeId}})
";
            var aLex = this.GetALexString(query, _Data);
            var q = base.GetQuery(aLex.Converted);
            Assert.Equal(@"SELECT [My].[JoinAfterStatement], [My].[JoinManyConstraints], [My].[JoinMultipleTables], [My].[JoinSampleId], [My].[JoinSimple], [My].[JoinWithFilters], [My].[LikeStatement], [My].[Multiselect], [My].[Multiselection], [My].[MultiSelectSampleId], [My].[NotBeforeTwoDays], [My].[NotInStatement], [My].[NotLikeStatement], [T1].[OnlyOneSupportedValue], [T1].[OrderBy], [T1].[Paging], [T1].[RangeNumber], [T1].[RawInFilter], [T1].[RawInSelect], [T1].[RecordsGrid], [T1].[RecordType], [T1].[RecordTypeId], [T1].[Regex], [T1].[Required], [T1].[RequiredIfBasic], [T1].[SelectWithHarcodedValues], [T1].[SelectWithManyOperationInFilter], [T1].[SelectWithManyOperationInFilterAliases], [T1].[SelectWithManyOperationInSelect], [T1].[SelectWithOneOperationInFilter], [T1].[SelectWithOneOperationInSelect], [T1].[ShowChkbox], [T1].[ShowWhenBasic], [T1].[SimpleDelete], [T1].[SimpleInsert], [T1].[SimpleSelect], [T1].[SimpleSelectDistinct], [T1].[SimpleSelectWithColumnAlias], [T1].[SimpleSelectWithNull], [T1].[SimpleSelectWithOneFilter], [T1].[SimpleSelectWithTableAlias], [T1].[SimpleSelectWithTwoFilters], [T1].[SimpleSelectWithTwoFiltersAndAlias], [T1].[SimpleUpdate], [T1].[TestAllTogether], [T1].[TopStatement], [T1].[UpdateMultipleSets], [T2].[UpdateWithFilter], [T2].[ValidationSampleId], [T2].[Values], [T2].[VariableLength], [T2].[AggregatesComplex], [T2].[AggregatesSimple], [T2].[AnythinButValue], [T2].[BasicColumnTypeId], [T2].[CatalogTypeId], [T2].[CatalogTypeName], [T2].[CatalogTypeText], [T2].[CatalogValueDisplayEnabled], [T2].[CatalogValueId], [T2].[CatalogValueName], [T2].[CatalogValueText], [T2].[Category], [T2].[CategoryId], [T2].[Checkbox], [T2].[CheckboxSelection], [T2].[Col1], [T2].[Col2], [T2].[Col3], [T2].[Col4], [T2].[Col5], [T2].[Col6], [T2].[ColCheckbox], [T2].[ColDate], [T2].[ColDateTime], [T2].[ColDouble], [T2].[ColInteger], [T2].[ColMoney], [T2].[ColRichTextArea], [T2].[ColText], [T2].[ColTextArea], [T2].[ColTime], [T2].[Comments], [T2].[CreatedDate], [T2].[DeleteWithFilter], [T2].[EnableWhen5], [T2].[FilterAtEnd], [T2].[FilterRaw], [T2].[GridList], [T2].[GroupByComplex], [T2].[GroupBySimple], [T3].[HideEnableSampleId], [T3].[HideWhen2OrMore], [T3].[InsertWithManyValues], [T3].[InsertWithQuery], [T3].[InStatement], [T3].[IsCommited], [T3].[IsEnabled], [T3].[IsNotNullStatement], [T3].[IsNullStatement] FROM [My].[Table] AS [My] 
INNER JOIN [Table1] AS [T1] ON ([T1].[VariableLength] =  57)
INNER JOIN [Table2] AS [T2] ON ([T2].[JoinManyConstraints] =  'kgladtbache@utexas.edu')
INNER JOIN [Table3] AS [T3] ON ([T3].[JoinMultipleTables] =  9) WHERE ([My].[JoinAfterStatement] =  'kfoucar26@reference.com' AND [My].[JoinManyConstraints] =  'mblethynt@macromedia.com' AND [My].[JoinMultipleTables] =  'kgladtbache@utexas.edu' AND [My].[JoinSampleId] =  9 AND [My].[JoinSimple] =  1 AND [My].[JoinWithFilters] =  2 AND [My].[LikeStatement] =  3 AND [My].[Multiselect] =  5 AND [My].[Multiselection] =  6 AND [My].[MultiSelectSampleId] =  7 AND [My].[NotBeforeTwoDays] =  8 AND [My].[NotInStatement] =  'dbannerman0@va.gov' AND [My].[NotLikeStatement] =  20 AND [T1].[OnlyOneSupportedValue] =  'cyeabsley3@rambler.ru' AND [T1].[OrderBy] =  'cbunstoneb@technorati.com' AND [T1].[Paging] =  'fgurdon2i@cbsnews.com' AND [T1].[RangeNumber] =  'rplacide2j@fotki.com' AND [T1].[RawInFilter] =  'kwimpeney2k@house.gov' AND [T1].[RawInSelect] =  'mcomelli2l@discuz.net' AND [T1].[RecordsGrid] =  'kmiettinen2m@intel.com' AND [T1].[RecordType] =  'rhuc2n@sun.com' AND [T1].[RecordTypeId] =  10 AND [T1].[Regex] =  11 AND [T1].[Required] =  12 AND [T1].[RequiredIfBasic] =  13 AND [T1].[SelectWithHarcodedValues] =  14 AND [T1].[SelectWithManyOperationInFilter] =  15 AND [T1].[SelectWithManyOperationInFilterAliases] =  16 AND [T1].[SelectWithManyOperationInSelect] =  17 AND [T1].[SelectWithOneOperationInFilter] =  18 AND [T1].[SelectWithOneOperationInSelect] =  19 AND [T1].[ShowChkbox] =  21 AND [T1].[ShowWhenBasic] =  22 AND [T1].[SimpleDelete] =  'mcurreeno@sina.com.cn' AND [T1].[SimpleInsert] =  'fkealy4@economist.com' AND [T1].[SimpleSelect] =  44 AND [T1].[SimpleSelectDistinct] =  48 AND [T1].[SimpleSelectWithColumnAlias] =  'ophlippi6@eepurl.com' AND [T1].[SimpleSelectWithNull] =  59 AND [T1].[SimpleSelectWithOneFilter] =  'sbloomer27@indiegogo.com' AND [T1].[SimpleSelectWithTableAlias] =  'cpurtell2@unesco.org' AND [T1].[SimpleSelectWithTwoFilters] =  'aswinney5@uiuc.edu' AND [T1].[SimpleSelectWithTwoFiltersAndAlias] =  53 AND [T1].[SimpleUpdate] =  46 AND [T1].[TestAllTogether] =  50 AND [T1].[TopStatement] =  25 AND [T1].[UpdateMultipleSets] =  4 AND [T2].[UpdateWithFilter] =  60 AND [T2].[ValidationSampleId] =  51 AND [T2].[Values] =  'hbyerx@earthlink.net' AND [T2].[VariableLength] =  57 AND [T2].[AggregatesComplex] =  'pgoldney2c@senate.gov' AND [T2].[AggregatesSimple] =  23 AND [T2].[AnythinButValue] =  'wmumby2b@mail.ru' AND [T2].[BasicColumnTypeId] =  'cmaylotts@nsw.gov.au' AND [T2].[CatalogTypeId] =  61 AND [T2].[CatalogTypeName] =  'taldred7@creativecommons.org' AND [T2].[CatalogTypeText] =  'ditzkovitchc@i2i.jp' AND [T2].[CatalogValueDisplayEnabled] =  'tcolgravea@networkadvertising.org' AND [T2].[CatalogValueId] =  'ltruslerf@taobao.com' AND [T2].[CatalogValueName] =  63 AND [T2].[CatalogValueText] =  'snewarte24@princeton.edu' AND [T2].[Category] =  'ccollacombeg@com.com' AND [T2].[CategoryId] =  'dhydechambers2d@blog.com' AND [T2].[Checkbox] =  54 AND [T2].[CheckboxSelection] =  'jmowbrayh@simplemachines.org' AND [T2].[Col1] =  47 AND [T2].[Col2] =  'etooley2h@prweb.com' AND [T2].[Col3] =  'bduddend@dyndns.org' AND [T2].[Col4] =  'efashion1@mail.ru' AND [T2].[Col5] =  24 AND [T2].[Col6] =  'acordelettei@topsy.com' AND [T2].[ColCheckbox] =  'keslerj@youku.com' AND [T2].[ColDate] =  'jbousfieldk@seesaa.net' AND [T2].[ColDateTime] =  43 AND [T2].[ColDouble] =  'fpurle28@cmu.edu' AND [T2].[ColInteger] =  49 AND [T2].[ColMoney] =  'hbarribalr@nba.com' AND [T2].[ColRichTextArea] =  'dmcelwee2f@cloudflare.com' AND [T2].[ColText] =  62 AND [T2].[ColTextArea] =  'shanway8@is.gd' AND [T2].[ColTime] =  'zbarlass9@usda.gov' AND [T2].[Comments] =  'ralbasiniv@irs.gov' AND [T2].[CreatedDate] =  'dfeldhammeru@netlog.com' AND [T2].[DeleteWithFilter] =  'ftarte2e@miibeian.gov.cn' AND [T2].[EnableWhen5] =  52 AND [T2].[FilterAtEnd] =  'jeakly2g@nytimes.com' AND [T2].[FilterRaw] =  'mmacarthur2a@friendfeed.com' AND [T2].[GridList] =  58 AND [T2].[GroupByComplex] =  'cmillp@virginia.edu' AND [T2].[GroupBySimple] =  45 AND [T3].[HideEnableSampleId] =  'rtucknutt29@ucoz.com' AND [T3].[HideWhen2OrMore] =  56 AND [T3].[InsertWithManyValues] =  'drosenauw@imgur.com' AND [T3].[InsertWithQuery] =  'dmcniven25@exblog.jp' AND [T3].[InStatement] =  'tdellcasaq@imageshack.us' AND [T3].[IsCommited] =  55 AND [T3].[IsEnabled] =  'ymendenhalll@xinhuanet.com' AND [T3].[IsNotNullStatement] =  'iwinkelln@yahoo.com' AND [T3].[IsNullStatement] =  'rrimmerm@reverbnation.com')"
, base.GetSentence(q));
        }

        [Fact]
        public void ParseQueryWithDictionarySmall()
        {
            var query = @"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement)
Filter(My.JoinAfterStatement = '{{AggregatesComplex}}' AND  My.JoinManyConstraints = '{{AggregatesSimple}}' AND  My.JoinMultipleTables = '{{AnythinButValue}}' AND  My.JoinSampleId = {{BasicColumnTypeId}} AND  My.JoinSimple = {{CatalogTypeId}} AND  My.JoinWithFilters = {{CatalogTypeName}} AND  My.LikeStatement = {{CatalogTypeText}} AND  My.Multiselect = {{CatalogValueDisplayEnabled}})
";

            var aLex = this.GetALexString(query, _Dictionary);
            var q = base.GetQuery(aLex.Converted);
            Assert.Equal(@"SELECT [My].[JoinAfterStatement], [My].[JoinManyConstraints], [My].[JoinMultipleTables], [My].[JoinSampleId], [My].[JoinSimple], [My].[JoinWithFilters], [My].[LikeStatement], [My].[Multiselect], [My].[Multiselection], [My].[MultiSelectSampleId], [My].[NotBeforeTwoDays], [My].[NotInStatement] FROM [My].[Table] AS [My] WHERE ([My].[JoinAfterStatement] =  'kfoucar26@reference.com' AND [My].[JoinManyConstraints] =  'mblethynt@macromedia.com' AND [My].[JoinMultipleTables] =  'kgladtbache@utexas.edu' AND [My].[JoinSampleId] =  9 AND [My].[JoinSimple] =  1 AND [My].[JoinWithFilters] =  2 AND [My].[LikeStatement] =  3 AND [My].[Multiselect] =  5)"
, base.GetSentence(q));
        }

        [Fact]
        public void ParseQueryWithDataSmall()
        {
            var query = @"From(My.Table AS My)
Select(My.JoinAfterStatement,My.JoinManyConstraints,My.JoinMultipleTables,My.JoinSampleId,My.JoinSimple,My.JoinWithFilters,My.LikeStatement,My.Multiselect,My.Multiselection,My.MultiSelectSampleId,My.NotBeforeTwoDays,My.NotInStatement)
Filter(My.JoinAfterStatement = '{{AggregatesComplex}}' AND  My.JoinManyConstraints = '{{AggregatesSimple}}' AND  My.JoinMultipleTables = '{{AnythinButValue}}' AND  My.JoinSampleId = {{BasicColumnTypeId}} AND  My.JoinSimple = {{CatalogTypeId}} AND  My.JoinWithFilters = {{CatalogTypeName}} AND  My.LikeStatement = {{CatalogTypeText}} AND  My.Multiselect = {{CatalogValueDisplayEnabled}})
";

            var aLex = this.GetALexString(query, _Data);
            var q = base.GetQuery(aLex.Converted);
            Assert.Equal(@"SELECT [My].[JoinAfterStatement], [My].[JoinManyConstraints], [My].[JoinMultipleTables], [My].[JoinSampleId], [My].[JoinSimple], [My].[JoinWithFilters], [My].[LikeStatement], [My].[Multiselect], [My].[Multiselection], [My].[MultiSelectSampleId], [My].[NotBeforeTwoDays], [My].[NotInStatement] FROM [My].[Table] AS [My] WHERE ([My].[JoinAfterStatement] =  'kfoucar26@reference.com' AND [My].[JoinManyConstraints] =  'mblethynt@macromedia.com' AND [My].[JoinMultipleTables] =  'kgladtbache@utexas.edu' AND [My].[JoinSampleId] =  9 AND [My].[JoinSimple] =  1 AND [My].[JoinWithFilters] =  2 AND [My].[LikeStatement] =  3 AND [My].[Multiselect] =  5)"
, base.GetSentence(q));
        }

        [Fact]
        public async Task GrammarError()
        {
            await Assert.ThrowsAnyAsync<ALexException>(async ()=>
                {
                    await Task.Run(() =>
                    {
                        var query = @"From(My.Table AS My)";

                        var aLex = this.GetALexString(query, _Data);
                        var q = base.GetQuery(aLex.Converted);
                    });
            });
        }


        private (string Converted, string Time) GetALexString(string aLex, object data)
        {
            var watch = new Stopwatch();
            watch.Start();
            var converted = this.GetParser().ParseValues(aLex, data);
            watch.Stop();
            return (Converted: converted, Time: watch.Elapsed.ToString());
        }

        protected override ParserBase GetParser()
        {
            return new ParserFull(QueryFactory);
        }
    }
}
