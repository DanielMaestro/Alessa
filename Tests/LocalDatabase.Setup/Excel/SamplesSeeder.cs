using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LocalDatabase.Setup.Excel
{
    internal partial class SamplesSeeder
    {
        internal List<DataTable> GetTableList()
        {
            var catalogs = GetCatalogs().ToList();
            var values = catalogs.SelectMany(e => e.CatalogValues).ToList();

            var basicColumnTypes = GetBasicColumnTypes().ToList();
            var multiSelectSamples = GetMultiSelectSamples(values, basicColumnTypes);

            var hideEnableSamples = GetHideEnableSamples(values);

            var catalogJoinsSamples = GetCatalogsJoinSamples(values);

            var validationSamples = GetValidationSamples();

            var result = new List<DataTable>();

            const string prefix = "Samples.";

            // Catalog types.
            var table = catalogs.CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            // Catalog values.
            table = values.CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            // HideEnableSamples.
            table = hideEnableSamples.CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            // HideEnableMultiselection.
            table = hideEnableSamples.SelectMany(e => e.HideEnableMultiselections).CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            // CatalogsJoinSamples.
            table = catalogJoinsSamples.CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            // BasicColumnsTypes.
            table = basicColumnTypes.CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            // MultiSelectSamples.
            table = multiSelectSamples.CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            // MultiSelectLists.
            table = multiSelectSamples.SelectMany(e => e.MultiSelectLists).CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            // MultiSelectTables.
            table = multiSelectSamples.SelectMany(e => e.MultiSelectTables).CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            // MultiSelectCheckboxes.
            table = multiSelectSamples.SelectMany(e => e.MultiSelectCheckboxes).CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            // ValidationSamples.
            table = validationSamples.CopyToDataTable();
            table.TableName = prefix + table.TableName;
            result.Add(table);

            return result;
        }
    }
}
