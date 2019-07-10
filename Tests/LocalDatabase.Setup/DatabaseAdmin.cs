using LocalDatabase.Setup.Excel;
using System.IO;

namespace LocalDatabase.Setup
{
    public class DatabaseAdmin
    {
        private System.Data.IDbConnection _connection;

        public void CheckConnection()
        {
            this.StartConnection(Constants.AlessaConnectionString.Replace("AlessaData", "master"));
            var isOk = this.IsDBAdded();
            if (!isOk)
            {
                isOk = this.SetSampleDatabase();
            }
        }

        public void AddTables()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo()
            {
                WorkingDirectory = Path.GetFullPath(@"..\..\..\")
            };
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C dotnet ef database update ";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            this.AddViews();
        }

        private void AddViews()
        {
            try
            {
                var scriptPath = Path.GetFullPath(@"..\..\..\..\..\Common\DataSchema\ViewsCreation.sql");
                var fullScript = File.ReadAllText(scriptPath);

                this.StartConnection(Constants.AlessaConnectionString);
                this.OpenConnection();

                foreach (var varScript in fullScript.Split("GO"))
                {
                    using (var command = this._connection.CreateCommand())
                    {
                        command.CommandText = varScript;
                        command.CommandType = System.Data.CommandType.Text;

                        command.ExecuteNonQuery();
                    }
                }

            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void DeleteData()
        {
            this.StartConnection(Constants.AlessaConnectionString);
            var seed = new DataSeed(this._connection);
            seed.DeleteData();
        }

        public void DeleteTables()
        {
            this.StartConnection(Constants.AlessaConnectionString);
            var seed = new DataSeed(this._connection);
            seed.DeleteTables();
        }

        public void SeedTestData()
        {
            this.StartConnection(Constants.AlessaConnectionString);
            var seed = new DataSeed(this._connection);
            seed.SeedAllTables();
        }

        private bool IsDBAdded()
        {
            bool result;
            this.OpenConnection();
            using (var command = this._connection.CreateCommand())
            {
                command.CommandText = "SELECT name from sys.databases WHERE name  = 'AlessaData'";
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();

                result = reader.Read();
            }
            return result;
        }

        private bool SetSampleDatabase()
        {
            this.CloseConnection();
            var actualSampleLocation = ";AttachDBFilename=" + System.IO.Path.GetFullPath(@"..\..\..\..\..\Common\Database\AlessaData.mdf");
            this.StartConnection(Constants.AlessaConnectionString + actualSampleLocation);
            var result = IsDBAdded();
            return result;
        }

        private void StartConnection(string connectionString)
        {
            this._connection = new System.Data.SqlClient.SqlConnection(connectionString);
        }

        private void OpenConnection()
        {
            if (this._connection.State == System.Data.ConnectionState.Closed)
            {
                this._connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (this._connection.State == System.Data.ConnectionState.Open)
            {
                this._connection.Close();
            }

            this._connection.Dispose();
        }
    }
}
