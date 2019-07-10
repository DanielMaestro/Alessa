using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace LocalDatabase.Setup.Excel
{
    public class ExcelReader
    {
        private const string Int = "Int";
        private const string Text = "Text";
        private const string Bit = "Bit";

        private static readonly string[] _validColumnTypes = { Int, Text, Bit };

        public List<DataTable> ReadExcel()
        {
            var list = new List<DataTable>();
            int index;

            var existingFile = new FileInfo(Path.GetFullPath(@"..\..\..\..\..\Common\DataSchema\Schema.xlsx"));
            using (var p = new ExcelPackage(existingFile))
            {
                for (index = 0; index < p.Workbook.Worksheets.Count; index++)
                {
                    list.Add(this.BuildDataTable(p.Workbook.Worksheets[index]));
                }
            }

            return list;
        }

        private DataTable BuildDataTable(ExcelWorksheet worksheet)
        {
            var columns = new Dictionary<(string ColumnName, string TypeName), int>();

            // Calculates the start and end of the columns with data.
            for (int index = 1; index <= worksheet.Dimension.Columns; index++)
            {
                string isColumn = worksheet.Cells[2, index].GetValue<string>();
                if (_validColumnTypes.Contains(isColumn))
                {
                    string colName = worksheet.Cells[1, index].GetValue<string>();
                    columns.Add((ColumnName: colName, TypeName: isColumn), index);
                }
            }

            var dataTable = this.BuildDataTable(worksheet, columns);
            return dataTable;
        }

        private DataTable BuildDataTable(ExcelWorksheet worksheet, Dictionary<(string ColumnName, string TypeName), int> dictionary)
        {
            DataTable dataTable = new DataTable();
            int index, y;

            // Adds the columns to the data table.
            var columns = dictionary.Select(e => new DataColumn(e.Key.ColumnName, this.GetFieldType(e.Key.TypeName))).ToArray();
            dataTable.Columns.AddRange(columns);
            dataTable.TableName = worksheet.Name;

            // column indexes.
            var indexes = dictionary.Select(e => e.Value).ToArray();

            for (index = 3; index <= worksheet.Dimension.Rows; index++)
            {
                var row = dataTable.NewRow();

                for (y = 0; y < indexes.Length; y++)
                {
                    var value = worksheet.Cells[index, indexes[y]].Value;

                    if (value == null || (value != DBNull.Value && value.ToString().Equals("NULL", StringComparison.OrdinalIgnoreCase)))
                    {
                        value = DBNull.Value;
                    }

                    row[y] = value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private Type GetFieldType(string typeName)
        {
            Type result = null;
            switch (typeName)
            {
                case Int:
                    result = typeof(int);
                    break;
                case Bit:
                    result = typeof(bool);
                    break;
                case Text:
                    result = typeof(string);
                    break;
            }

            return result;
        }
    }
}
