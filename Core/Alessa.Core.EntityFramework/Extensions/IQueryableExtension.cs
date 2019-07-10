using Alessa.Core.Entities.QueryModels;
using Alessa.Core.EntityFramework.Models;
using System.Linq;
using static System.Linq.Dynamic.Core.DynamicQueryableExtensions;

namespace Alessa.Core.EntityFramework.Extensions
{
    /// <summary>
    /// Provides extensions for <see cref="IQueryable{T}"/> object.
    /// </summary>
    public static class IQueryableExtension
    {
        /// <summary>
        /// Gets a querable collection of type "E" with the specified parameters.
        /// </summary>
        /// <typeparam name="E">Entity type contained in class.</typeparam>
        /// <param name="request">Request with the specified parameters.</param>
        /// <param name="input">This is the input to apply the query.</param>
        /// <returns></returns>
        public static IQueryable<E> GetData<E>(this IQueryable<E> input, QueryParameters request)
        //where E : class, new()
        {
            var filterParameters = FilterParameters.GetFilterExpression<E>(request.FilterCollection);
            var result = input.GetData<E>(filterParameters.FilterExpression, string.Join(' ', request.SortingNames), request.PageIndex, request.RecordsCount, filterParameters.Parameters);
            return result;
        }

        /// <summary>
        /// Gets the collection of entities for the specified filter expression.
        /// </summary>
        /// <typeparam name="E">Entity type in the context.</typeparam>
        /// <param name="filterExpression">Filter Expression.</param>
        /// <param name="orderExpression">Order by Expression</param>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Page Size.</param>
        /// <param name="values">Object array to user when the filter expression has parameters.</param>
        /// <param name="input">This is the input to apply the query.</param>
        /// <returns></returns>
        public static IQueryable<E> GetData<E>(this IQueryable<E> input, string filterExpression, string orderExpression, int pageNumber = 0, int pageSize = 0, params object[] values)
        //where E : class, new()
        {
            IQueryable<E> result = null;

            try
            {
                if (string.IsNullOrWhiteSpace(orderExpression))
                {
                    if (pageSize > 0)
                    {
                        throw new System.ArgumentException("If the 'pageSize' parameter has a valid value then the parameter 'orderExpression' must be specified.");
                    }
                }
                int thisSkip = pageNumber * pageSize;

                // 8 different cases for where clause.
                if (!string.IsNullOrWhiteSpace(filterExpression) && pageNumber <= 0 && pageSize <= 0)
                {
                    result = input.Where(filterExpression, values);
                }
                else if (!string.IsNullOrWhiteSpace(filterExpression) && pageNumber <= 0 && pageSize > 0)
                {
                    result = input.Where(filterExpression, values).Take(pageSize);
                }
                else if (!string.IsNullOrWhiteSpace(filterExpression) && pageNumber > 0 && pageSize <= 0)
                {
                    result = input.Where(filterExpression, values).OrderBy(orderExpression).Skip(thisSkip);
                }
                else if (!string.IsNullOrWhiteSpace(filterExpression) && pageNumber > 0 && pageSize > 0)
                {
                    result = input.Where(filterExpression, values).OrderBy(orderExpression).Skip(thisSkip).Take(pageSize);
                }
                else if (string.IsNullOrWhiteSpace(filterExpression) && pageNumber <= 0 && pageSize <= 0)
                {
                    result = input;
                }
                else if (string.IsNullOrWhiteSpace(filterExpression) && pageNumber <= 0 && pageSize > 0)
                {
                    result = input.OrderBy(orderExpression).Take(pageSize);
                }
                else if (string.IsNullOrWhiteSpace(filterExpression) && pageNumber > 0 && pageSize <= 0)
                {
                    result = input.OrderBy(orderExpression).Skip(thisSkip);
                }
                else if (string.IsNullOrWhiteSpace(filterExpression) && pageNumber > 0 && pageSize > 0)
                {
                    result = input.OrderBy(orderExpression).Skip(thisSkip).Take(pageSize);
                }

                // Order the recors.
                if (!string.IsNullOrWhiteSpace(orderExpression))
                {
                    result = result.OrderBy(orderExpression);
                }
            }
            catch (System.Exception)
            {
                throw;
            }


            return result;
        }

        /// <summary>
        /// Gets the total of records for the query.
        /// </summary>
        /// <param name="filterExpression">Filter expression string.</param>
        /// <param name="values">Object array containing the parameter values in the filter expression.</param>
        /// <param name="input">Querable object.</param>
        /// <typeparam name="E">Entity target.</typeparam>
        /// <returns></returns>
        public static int GetCounter<E>(this IQueryable<E> input, string filterExpression, params object[] values)
        //where E : class, new()
        {
            if (!string.IsNullOrWhiteSpace(filterExpression))
            {
                return input.Where(filterExpression, values).Count();
            }
            else
            {
                return input.Count();
            }
        }
        /// <summary>
        /// Gets the response for a query.
        /// </summary>
        /// <typeparam name="E">Entity type.</typeparam>
        /// <param name="input">Query input.</param>
        /// <param name="request">Request.</param>
        /// <returns></returns>
        public static RecordResult GetResponse<E>(this IQueryable<E> input, QueryParameters request)
        {
            var filterParameters = FilterParameters.GetFilterExpression<E>(request.FilterCollection);
            var queryResult = input.GetData<E>(filterParameters.FilterExpression, string.Join(' ', request.SortingNames), request.PageIndex, request.RecordsCount, filterParameters.Parameters);

            var totalRecordsCount = input.GetCounter<E>(filterParameters.FilterExpression, filterParameters.Parameters);
            var result = new RecordResult()
            {
                PageIndex = request.PageIndex,
                TotalPagesCount = (int)System.Math.Ceiling((float)totalRecordsCount / (float)request.RecordsCount),
                TotalRecordsCount = totalRecordsCount,
                Records = queryResult.Cast<object>().ToList()
            };

            return result;
        }
        #region Private classes
        #endregion
    }
}
