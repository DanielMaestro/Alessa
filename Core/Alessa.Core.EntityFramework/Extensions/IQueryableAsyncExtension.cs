using Alessa.Core.Entities.QueryModels;

namespace Alessa.Core.EntityFramework.Extensions
{
    /// <summary>
    /// Provides asyncroonous extensions for <see cref="System.Linq.IQueryable{T}"/> object.
    /// </summary>
    public static class IQueryableAsyncExtension
    {
        /// <summary>
        /// Gets a querable collection of type "E" with the specified parameters.
        /// </summary>
        /// <typeparam name="E">Entity type contained in <see cref="System.Linq.IQueryable{E}"/> input.</typeparam>
        /// <param name="request">Request with the specified parameters.</param>
        /// <param name="input">Collection to query.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<System.Linq.IQueryable<E>> GetDataAsync<E>(this System.Linq.IQueryable<E> input, QueryParameters request)
        //where E : class, new()
        {
            return await System.Threading.Tasks.Task.FromResult(input.GetData<E>(request));
        }
        /// <summary>
        /// Gets the collection of entities for the specified filter expression.
        /// </summary>
        /// <typeparam name="E">Entity type in the context.</typeparam>
        /// <param name="filterExpression">Filter Expression.</param>
        /// <param name="orderExpression">Order by Expression</param>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Page Size.</param>
        /// <param name="input">Collection to query.</param>
        /// <param name="values">Object array to user when the filter expression has parameters.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<System.Linq.IQueryable<E>> GetDataAsync<E>(this System.Linq.IQueryable<E> input, string filterExpression,
               string orderExpression, int pageNumber = 0, int pageSize = 0, params object[] values)
        //where E : class, new()
        {
            return await System.Threading.Tasks.Task.FromResult(input.GetData<E>(filterExpression, orderExpression, pageNumber, pageSize, values));
        }

        /// <summary>
        /// Gets the total of records for the query.
        /// </summary>
        /// <param name="filterExpression">Filter expression string.</param>
        /// <param name="values">Object array containing the parameter values in the filter expression.</param>
        /// <param name="input">Querable object.</param>
        /// <typeparam name="E">Entity target.</typeparam>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<int> GetCounterAsync<E>(this System.Linq.IQueryable<E> input, string filterExpression, params object[] values) //where E : class, new()
        {
            return await System.Threading.Tasks.Task.FromResult(input.GetCounter<E>(filterExpression, values));
        }

        /// <summary>
        /// Gets the response for a query.
        /// </summary>
        /// <typeparam name="E">Entity type.</typeparam>
        /// <param name="input">Query input.</param>
        /// <param name="request">Request.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<RecordResult> GetResponseAsync<E>(this System.Linq.IQueryable<E> input, QueryParameters request)
        {
            var result = await System.Threading.Tasks.Task.FromResult(input.GetResponse(request));
            return result;
        }

    }
}
