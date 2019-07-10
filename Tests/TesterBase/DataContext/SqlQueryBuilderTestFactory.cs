﻿using Alessa.QueryBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace TesterBase.DataContext
{
    public class SqlQueryBuilderTestFactory : IDesignTimeDbContextFactory<SqlQueryBuilderTestDataContext>
    {
        public SqlQueryBuilderTestDataContext CreateDbContext(string[] args)
        {
            var loggerFactory = new LoggerFactory();
            //var logger = new LoggerConfiguration()
            //    .WriteTo.Debug()
            //    .CreateLogger();
            //loggerFactory.AddSerilog(logger);

            loggerFactory.AddDebug();

            var options = new DbContextOptionsBuilder<SqlQueryBuilderTestDataContext>()
            .UseSqlServer(Constants.AlessaConnectionString)
            .UseLoggerFactory(loggerFactory) //Optional, this logs SQL generated by EF Core to the Console
            .Options;

            return new SqlQueryBuilderTestDataContext(options);
        }

        public QueryBuilderOptions GetQueryBuilderOptions()
        {
            var result = new QueryBuilderOptions();
            result.AddConnection<SqlConnection>("DefaultConnection", Constants.AlessaConnectionString);
            result.AddConnection<SqlConnection>("TestConnection1", Constants.AlessaConnectionString);
            result.AddConnection<SqlConnection>("TestConnection2", Constants.AlessaConnectionString);

            return result;
        }

    }
}
