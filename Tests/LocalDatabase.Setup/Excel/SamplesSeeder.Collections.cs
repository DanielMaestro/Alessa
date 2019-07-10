using Bogus;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesterBase.Entities;

namespace LocalDatabase.Setup.Excel
{

    internal partial class SamplesSeeder
    {
        private static readonly Faker _faker = new Faker();
        private static readonly int _MaxLimit = 100;
        private static readonly int _MinLimit = 10;

        private IEnumerable<CatalogType> GetCatalogs()
        {
            for (int catalogCounter = 1; catalogCounter <= _faker.Random.Int(_MinLimit, (int)(_MaxLimit * 0.1)); catalogCounter++)
            {
                var type = GetCatalogType();

                for (int valueCounter = 1; valueCounter <= _faker.Random.Int(_MinLimit, (int)(_MaxLimit * 0.1)); valueCounter++)
                {
                    var value = GetCatalogValue(type);
                    type.CatalogValues.Add(value);
                }

                yield return type;
            }
        }

        private IEnumerable<MultiSelectSample> GetMultiSelectSamples()
        {
            IEnumerable<BasicColumnType> basicColumnTypes = GetBasicColumnTypes().ToList();

            for (int multiCounter = 1; multiCounter <= basicColumnTypes.Count(); multiCounter++)
            {
                var multi = GetMultiSelectSample();

                yield return multi;
            }
        }

        private IEnumerable<BasicColumnType> GetBasicColumnTypes()
        {
            for (int basicCounter = 1; basicCounter <= _faker.Random.Int(_MinLimit, _MaxLimit); basicCounter++)
            {
                var type = GetBasicColumnType();

                yield return type;
            }
        }

        private IEnumerable<HideEnableSample> GetHideEnableSamples()
        {
            for (int basicCounter = 1; basicCounter <= _faker.Random.Int(_MinLimit, _MaxLimit); basicCounter++)
            {
                var type = GetHideEnableSample();

                yield return type;
            }
        }

        private IEnumerable<HideEnableSample> GetHideEnableSamples(List<CatalogValue> catalogValues)
        {
            var samples = GetHideEnableSamples().ToList();
            int to;
            Parallel.ForEach(samples, item =>
            {
                var multiSelections = new ConcurrentBag<HideEnableMultiselection>();
                to = _faker.Random.Int(_MinLimit, catalogValues.Count - 1);
                Parallel.For(1, to, counter =>
                {
                    var catIndex = _faker.Random.Int(0, catalogValues.Count - 1);

                    var entity = GetHideEnableMultiselection(item, catalogValues[catIndex]);
                    multiSelections.Add(entity);
                });

                SetList(multiSelections, (c, l) => !l.Any(e => e.CatalogValueId == c.CatalogValueId), (List<HideEnableMultiselection>)item.HideEnableMultiselections);
            });

            return samples;
        }

        private IEnumerable<MultiSelectSample> GetMultiSelectSamples(List<CatalogValue> catalogValues, List<BasicColumnType> basicColumnTypes)
        {
            var samples = GetMultiSelectSamples().ToList();
            int to;

            Parallel.ForEach(samples, item =>
            {
                var checkboxes = new ConcurrentBag<MultiSelectCheckbox>();
                to = _faker.Random.Int(_MinLimit, catalogValues.Count - 1);
                Parallel.For(1, to, counter =>
                {
                    int catIndex;
                    catIndex = _faker.Random.Int(0, catalogValues.Count - 1);

                    var entity = GetMultiSelectCheckbox(item, catalogValues[catIndex]);
                    checkboxes.Add(entity);
                });

                var multiList = new ConcurrentBag<MultiSelectList>();
                to = _faker.Random.Int(_MinLimit, catalogValues.Count - 1);
                Parallel.For(1, to, counter =>
                {
                    int catIndex;
                    catIndex = _faker.Random.Int(0, catalogValues.Count - 1);

                    var entity = GetMultiSelectList(item, catalogValues[catIndex]);
                    multiList.Add(entity);
                });

                var selectTable = new ConcurrentBag<MultiSelectTable>();
                to = _faker.Random.Int(_MinLimit, catalogValues.Count - 1);
                Parallel.For(1, to, counter =>
                {
                    int catIndex;
                    catIndex = _faker.Random.Int(0, basicColumnTypes.Count - 1);

                    var entity = GetMultiSelectTable(item, basicColumnTypes[catIndex]);
                    selectTable.Add(entity);
                });

                SetList(checkboxes, (c, l) => !l.Any(e => e.CatalogValueId == c.CatalogValueId), (List<MultiSelectCheckbox>)item.MultiSelectCheckboxes);
                SetList(multiList, (c, l) => !l.Any(e => e.CatalogValueId == c.CatalogValueId), (List<MultiSelectList>)item.MultiSelectLists);
                SetList(selectTable, (c, l) => !l.Any(e => e.BasicColumnTypeId == c.BasicColumnTypeId), (List<MultiSelectTable>)item.MultiSelectTables);
            });

            return samples;
        }

        private IEnumerable<CatalogsJoinSample> GetCatalogsJoinSamples(List<CatalogValue> catalogValues)
        {
            int a, b;

            for (int basicCounter = 1; basicCounter <= _faker.Random.Int(_MinLimit, _MaxLimit); basicCounter++)
            {
                a = _faker.Random.Int(0, catalogValues.Count - 1);
                b = _faker.Random.Int(0, catalogValues.Count - 1);
                var type = GetCatalogsJoinSample(catalogValues[a], catalogValues[b]);

                yield return type;
            }
        }

        private IEnumerable<ValidationSample> GetValidationSamples()
        {
            for (int basicCounter = 1; basicCounter <= _faker.Random.Int(_MinLimit, _MaxLimit); basicCounter++)
            {
                var type = GetValidationSample();

                yield return type;
            }
        }

        private void SetList<T>(ConcurrentBag<T> concurrent, Func<T, List<T>, bool> comparer, List<T> list)
        {
            var en = concurrent.GetEnumerator();
            while (en.MoveNext())
            {
                if (comparer(en.Current, list))
                {
                    list.Add(en.Current);
                }
            }
        }
    }
}
