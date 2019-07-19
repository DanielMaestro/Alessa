using Funq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;

namespace Alessa.ServiceStack.Extensions
{
    /// <summary>
    /// The IoC extensions class.
    /// </summary>
    public static class QueryBuilderIoCExtensions
    {
        /// <summary>
        /// Sets the connections needed by Alessa.
        /// </summary>
        /// <param name="container">The ServiceStack container.</param>
        /// <param name="options">Options.</param>
        /// <returns></returns>
        public static void SetAlessaConnections(this Container container, Action<OrmLiteConnectionFactory> options)
        {
            OrmLiteConnectionFactory factory = new OrmLiteConnectionFactory();

            options?.Invoke(factory);
            container.AddSingleton<IDbConnectionFactory>(() => factory);
        }
    }
}
