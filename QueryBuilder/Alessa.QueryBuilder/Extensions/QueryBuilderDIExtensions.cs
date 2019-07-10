using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Data;

namespace Alessa.QueryBuilder
{
    /// <summary>
    /// A set of extensions to configure with dependency injection.
    /// </summary>
    public static class QueryBuilderDIExtensions
    {
        /// <summary>
        /// Sets the connections needed by Alessa.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="options">Options.</param>
        /// <returns></returns>
        public static IServiceCollection SetAlessaConnections(this IServiceCollection services, Action<QueryBuilderOptions> options)
        {
            QueryBuilderOptions builderOptions = new QueryBuilderOptions();
            options?.Invoke(builderOptions);
            services.AddSingleton(builderOptions);
            services.Configure(options);
            return services;
        }

        /// <summary>
        /// Adds a new connection.
        /// </summary>
        /// <typeparam name="TConnection"></typeparam>
        /// <param name="options"></param>
        /// <param name="cnnName"></param>
        /// <param name="cnnString"></param>
        public static void AddConnection<TConnection>(this QueryBuilderOptions options, string cnnName, string cnnString)
            where TConnection : IDbConnection, new()
        {
            TConnection connection = new TConnection()
            {
                ConnectionString = cnnString
            };

            Compiler compiler = GetCompiler<TConnection>();
            QueryFactory queryFactory = new QueryFactory(connection, compiler);
#if DEBUG
            queryFactory.Logger = e => System.Diagnostics.Debug.WriteLine(e);
#endif

            options?.ConnectionsPool.Add(cnnName, queryFactory);
        }

        private static Compiler GetCompiler<T>()
            where T : IDbConnection, new()
        {
            Compiler result;
            var name = typeof(T).Name;
            switch (name)
            {
                case "SqliteConnection ":
                    result = new SqliteCompiler();
                    break;
                case "FbConnection":
                    result = new FirebirdCompiler();
                    break;
                case "MySqlConnection":
                    result = new MySqlCompiler();
                    break;
                case "OracleConnection":
                    result = new OracleCompiler();
                    break;
                case "NpgsqlConnection":
                    result = new PostgresCompiler();
                    break;
                default:
                    result = new SqlServerCompiler();
                    break;
            }

            return result;
        }

        /// <summary>
        /// Sets a logger.
        /// </summary>
        /// <param name="options">The builder options.</param>
        /// <param name="logger">The logger to set.</param>
        public static void SetLogger(this QueryBuilderOptions options, ILogger logger)
        {
            options.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
