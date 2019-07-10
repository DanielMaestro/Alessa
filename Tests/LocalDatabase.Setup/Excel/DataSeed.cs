using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LocalDatabase.Setup.Excel
{
    public class DataSeed
    {
        private readonly IDbConnection dbConnection;

        public DataSeed(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public void SeedSampleTables(SqlTransaction transaction)
        {
            var sampleSeeder = new SamplesSeeder();

            var tableList = sampleSeeder.GetTableList();

            BulkInsert(tableList, transaction);
        }

        public void SeedAlessaTables(SqlTransaction transaction)
        {
            var excelReader = new ExcelReader();
            var tableList = excelReader.ReadExcel();

            BulkInsert(tableList, transaction);
        }

        private void BulkInsert(List<DataTable> tableList, SqlTransaction transaction)
        {
            for (int index = 0; index < tableList.Count; index++)
            {
                SqlBulkCopy bulk = new SqlBulkCopy(dbConnection as SqlConnection, SqlBulkCopyOptions.KeepIdentity, transaction);
                bulk.DestinationTableName = tableList[index].TableName;

                this.SetTableStructured(tableList[index], transaction);
                System.Diagnostics.Debug.Write(string.Format("Bulk on table: {0}\n", bulk.DestinationTableName));
                bulk.WriteToServer(tableList[index]);
            }
        }

        public void SeedAllTables()
        {
            SqlTransaction transaction = null;
            try
            {
                this.OpenConnection();
                using (transaction = dbConnection.BeginTransaction() as SqlTransaction)
                {
                    this.SeedSampleTables(transaction);
                    this.SeedAlessaTables(transaction);
                    transaction.Commit();
                }
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void DeleteTables()
        {
            try
            {
                this.OpenConnection();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    using (var command = dbConnection.CreateCommand())
                    {
                        command.CommandText = "IF OBJECT_ID('dbo.__EFMigrationsHistory') IS NOT NULL DROP TABLE dbo.__EFMigrationsHistory";
                        command.CommandType = CommandType.Text;
                        command.Transaction = transaction;

                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                var fullList = GetTables(new string[] { "Alessa", "Samples" });
                var tables = fullList.Where(e => e.TypeName == "BASE TABLE").Select(e => e.TableName).ToList();
                this.ExecuteCommand("DROP TABLE {0}", tables, false);
                tables = fullList.Where(e => e.TypeName == "VIEW").Select(e => e.TableName).ToList();
                this.ExecuteCommand("DROP VIEW {0}", tables, false);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void DeleteData()
        {
            try
            {
                var fullList = GetTables(new string[] { "Alessa", "Samples" });
                var tables = fullList.Where(e => e.TypeName == "BASE TABLE").Select(e => e.TableName).ToList();
                this.ExecuteCommand("TRUNCATE TABLE {0}", tables, true);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        private void ExecuteCommand(string commandFormat, bool restoreConstraints)
        {
            var schemas = new string[] { "Alessa", "Samples" };
            var tables = this.GetTables(schemas).Select(e => e.TableName).ToList();

            this.ExecuteCommand(commandFormat, tables, restoreConstraints);
        }

        private void ExecuteCommand(string commandFormat, List<string> tableObjects, bool restoreConstraints)
        {
            var schemas = new string[] { "Alessa", "Samples" };
            var constraints = this.GetConstraintsQueries(schemas);
            try
            {
                this.OpenConnection();

                DeleteConstraints(constraints.DeleteConstraint);
                using (var transaction = dbConnection.BeginTransaction())
                {
                    for (var index = 0; index < tableObjects.Count; index++)
                    {
                        using (var command = dbConnection.CreateCommand())
                        {
                            command.CommandText = string.Format(commandFormat, tableObjects[index]);
                            command.CommandType = CommandType.Text;
                            command.Transaction = transaction;

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
            }
            finally
            {
                if (restoreConstraints)
                    CreateConstraints(constraints.CreateConstraint);
                this.CloseConnection();
            }
        }
        private void DeleteConstraints(string statements)
        {
            if (!string.IsNullOrWhiteSpace(statements))
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = statements;
                    command.ExecuteNonQuery();
                }
        }

        private void CreateConstraints(string statements)
        {
            if (!string.IsNullOrWhiteSpace(statements))
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = statements;
                    command.ExecuteNonQuery();
                }
        }

        /// <summary>
        /// It retrieves a structured <see cref="DataTable"/> in case the <paramref name="table"/> has a different column positions.
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="table"></param>
        private void SetTableStructured(DataTable table, SqlTransaction sqlTransaction)
        {
            using (var command = dbConnection.CreateCommand())
            {
                // It will retrive only the columns in the table. We don't need data.
                command.CommandText = "SELECT * FROM " + table.TableName + " WHERE 1 <> 1";
                command.CommandType = CommandType.Text;
                command.Transaction = sqlTransaction;

                using (var reader = command.ExecuteReader())
                {
                    for (int index = 0; index < reader.FieldCount; index++)
                    {
                        var columnName = reader.GetName(index);

                        if (!table.Columns[index].ColumnName.Equals(columnName, System.StringComparison.OrdinalIgnoreCase))
                        {
                            System.Diagnostics.Debug.Write(string.Format("Column name: {0}\tExpected: {1} \n", columnName, table.Columns[index].ColumnName));
                            table.Columns[columnName].SetOrdinal(index);
                        }
                    }
                }
            }
        }

        private void OpenConnection()
        {
            if (this.dbConnection.State == ConnectionState.Closed)
            {
                this.dbConnection.Open();
            }
        }

        private void CloseConnection()
        {
            if (this.dbConnection.State == ConnectionState.Open)
            {
                this.dbConnection.Close();
            }
        }

        private (string DeleteConstraint, string CreateConstraint) GetConstraintsQueries(IEnumerable<string> schemas)
        {
            try
            {
                this.OpenConnection();

                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = @"SELECT
	PKTABLE_OWNER + '.' + PKTABLE_NAME AS 'PKTABLE_FULLNAME'
	, PKTABLE_OWNER, PKTABLE_NAME, PKCOLUMN_NAME
	, FKTABLE_OWNER + '.' + FKTABLE_NAME AS 'FKTABLE_FULLNAME'
	, FKTABLE_OWNER, FKTABLE_NAME, FKCOLUMN_NAME, FK_NAME
	, 'ALTER TABLE [' + FKTABLE_OWNER + '].[' + FKTABLE_NAME + ']  DROP CONSTRAINT [' + FK_NAME + ']' AS 'DeleteForeignKey'
	, 'ALTER TABLE [' + FKTABLE_OWNER + '].[' + FKTABLE_NAME + ']  WITH NOCHECK ADD CONSTRAINT [' + 
        FK_NAME + '] FOREIGN KEY([' + FKCOLUMN_NAME + ']) REFERENCES ' + 
        '[' + PKTABLE_OWNER + '].[' + PKTABLE_NAME + '] ([' +
        PKCOLUMN_NAME + '])' AS 'CreateForeignKey'
FROM
(
SELECT PKTABLE_QUALIFIER = CONVERT(SYSNAME,DB_NAME()),
       PKTABLE_OWNER = CONVERT(SYSNAME,SCHEMA_NAME(O1.SCHEMA_ID)),
       PKTABLE_NAME = CONVERT(SYSNAME,O1.NAME),
       PKCOLUMN_NAME = CONVERT(SYSNAME,C1.NAME),
       FKTABLE_QUALIFIER = CONVERT(SYSNAME,DB_NAME()),
       FKTABLE_OWNER = CONVERT(SYSNAME,SCHEMA_NAME(O2.SCHEMA_ID)),
       FKTABLE_NAME = CONVERT(SYSNAME,O2.NAME),
       FKCOLUMN_NAME = CONVERT(SYSNAME,C2.NAME),
       -- Force the column to be non-nullable (see SQL BU 325751)
       --KEY_SEQ             = isnull(convert(smallint,k.constraint_column_id), sysconv(smallint,0)),
       UPDATE_RULE = CONVERT(SMALLINT,CASE OBJECTPROPERTY(F.OBJECT_ID,'CnstIsUpdateCascade') 
                                        WHEN 1 THEN 0
                                        ELSE 1
                                      END),
       DELETE_RULE = CONVERT(SMALLINT,CASE OBJECTPROPERTY(F.OBJECT_ID,'CnstIsDeleteCascade') 
                                        WHEN 1 THEN 0
                                        ELSE 1
                                      END),
       FK_NAME = CONVERT(SYSNAME,OBJECT_NAME(F.OBJECT_ID)),
       PK_NAME = CONVERT(SYSNAME,I.NAME),
       DEFERRABILITY = CONVERT(SMALLINT,7)   -- SQL_NOT_DEFERRABLE
FROM   SYS.ALL_OBJECTS O1,
       SYS.ALL_OBJECTS O2,
       SYS.ALL_COLUMNS C1,
       SYS.ALL_COLUMNS C2,
       SYS.FOREIGN_KEYS F
       INNER JOIN SYS.FOREIGN_KEY_COLUMNS K
         ON (K.CONSTRAINT_OBJECT_ID = F.OBJECT_ID)
       INNER JOIN SYS.INDEXES I
         ON (F.REFERENCED_OBJECT_ID = I.OBJECT_ID
             AND F.KEY_INDEX_ID = I.INDEX_ID)
WHERE  O1.OBJECT_ID = F.REFERENCED_OBJECT_ID
       AND O2.OBJECT_ID = F.PARENT_OBJECT_ID
       AND C1.OBJECT_ID = F.REFERENCED_OBJECT_ID
       AND C2.OBJECT_ID = F.PARENT_OBJECT_ID
       AND C1.COLUMN_ID = K.REFERENCED_COLUMN_ID
       AND C2.COLUMN_ID = K.PARENT_COLUMN_ID
) AS T
WHERE PKTABLE_OWNER IN (" + "'" + string.Join("','", schemas) + "'" + ")";

                    var deleteConstraintsBuilder = new StringBuilder();
                    var createConstraintsBuilder = new StringBuilder();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            deleteConstraintsBuilder.Append(reader.GetString(9)).Append(';').AppendLine();
                            createConstraintsBuilder.Append(reader.GetString(10)).Append(';').AppendLine();
                        }
                    }

                    return (DeleteConstraint: deleteConstraintsBuilder.ToString(), CreateConstraint: createConstraintsBuilder.ToString());
                }
            }
            finally
            {
                this.CloseConnection();
            }
        }

        private List<(string TableName, string TypeName)> GetTables(IEnumerable<string> schemas)
        {
            this.OpenConnection();
            var tableList = new List<(string TableName, string TypeName)>();
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "SELECT TABLE_SCHEMA + '.' + TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA IN (" + "'" + string.Join("','", schemas) + "'" + ")";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tableName = (TableName: reader.GetString(0), TypeName: reader.GetString(1));
                        if (!tableList.Contains(tableName))
                            tableList.Add(tableName);

                    }
                }
            }

            return tableList;
        }
    }
}
