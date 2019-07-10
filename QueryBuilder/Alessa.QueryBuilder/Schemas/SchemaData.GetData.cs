using Alessa.ALex;
using Alessa.ALex.SqlKata;
using Alessa.Core.Entities.QueryModels;
using Alessa.Core.Entities.Results;
using Alessa.Core.Helpers;
using Alessa.QueryBuilder.Entities;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using Alessa.QueryBuilder.Entities.Data;
using Alessa.QueryBuilder.Entities.Results;
using Alessa.QueryBuilder.Extensions;
using Microsoft.EntityFrameworkCore;
using SqlKata;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alessa.QueryBuilder
{
    /// <summary>
    /// The data retriever.
    /// </summary>
    public partial class SchemaData : SchemaBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SchemaData"/> with the specified options.
        /// </summary>
        /// <param name="schemaContext">The schema context for this object.</param>
        public SchemaData(SchemaContext schemaContext) : base(schemaContext)
        {
        }

        /// <summary>
        /// Gets the table data.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <returns></returns>
        /// <exception cref="ALexException">Provides a user exception containing a message to the end user.</exception>
        public async Task<GridResult> GetDataAsync(DataParameters parameters)
        {
            GridResult result;
            try
            {
                result = await this.GetDataAsync(parameters, null);

                return result;
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<GridResult>(ex, 3001);
            }

            return result;
        }

        /// <summary>
        /// Gets the source assigned to the specified field.
        /// </summary>
        /// <param name="parameters">Source parameters.</param>
        /// <returns></returns>
        public async Task<DataResult> GetDataSourceAsync(SourceParameters parameters)
        {
            DataResult result = new DataResult();
            try
            {
                var fieldDefinitions = await (from fd in base.GetFieldDefinitions(parameters)
                                              from s in fd.FieldListSources
                                              where fd.ItemName == parameters.FieldItemName && fd.FieldType != EFieldType.TableReference && s.ExecutionSource.ExecutionType == EExecutionType.ALex
                                              select new
                                              {
                                                  s.ExecutionSource.ExecutionText,
                                                  fd.FieldType,
                                                  TableItemName = s.TableDefinition.ItemName,
                                                  ConnectionName = s.ExecutionSource.TableConfiguration.ConnectionString
                                              }).FirstAsync();


                var queryFactory = this.Context.QueryBuilderOptions.ConnectionsPool[fieldDefinitions.ConnectionName];

                // Creates the query.
                var parser = new ParserFull(queryFactory);

                // Parses the select query.
                var selectQuery = parser.ParseToQuery(fieldDefinitions.ExecutionText, parameters.AdditionalParameters);

                result.Result = await selectQuery.GetAsync();

            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<DataResult>(ex, 3002);
            }

            return result;
        }

        /// <summary>
        /// Gets the table source in case the  table have a table definiton as source.
        /// </summary>
        /// <param name="sourceParameters">The source parameters.</param>
        /// <param name="dataParameters">The data parameters.</param>
        /// <returns></returns>
        public async Task<GridResult> GetTableSourceAsync(SourceParameters sourceParameters, DataParameters dataParameters)
        {
            GridResult result;

            try
            {
                // Gets the aditional statements.
                var additionalFilters = await (from fd in base.GetFieldDefinitions(sourceParameters)
                                               from s in fd.FieldListSources
                                               where fd.ItemName == sourceParameters.FieldItemName && fd.FieldType == EFieldType.TableReference && s.ExecutionSource.ExecutionType == EExecutionType.ALex
                                               select s.ExecutionSource.ExecutionText).ToListAsync();

                // If not configured it will return an error result.
                if (additionalFilters.Count == 0)
                    throw new ALexException("A relationship between '" + sourceParameters.ItemName + "." + sourceParameters.FieldItemName + "' has not been set in the configurations.", 304);

                if (sourceParameters.AdditionalParameters != null)
                {
                    if (dataParameters.AdditionalParameters == null)
                        dataParameters.AdditionalParameters = sourceParameters.AdditionalParameters;
                    else
                    {
                        // Copies the data from source parameters into the data parameters if it's not present.
                        foreach (KeyValuePair<string, object> key in sourceParameters.AdditionalParameters)
                        {
                            if (!dataParameters.AdditionalParameters.ContainsKey(key.Key))
                            {
                                dataParameters.AdditionalParameters.Add(key.Key, key.Value);
                            }
                            else
                            {
                                // If there is a different value it will be overriden.
                                dataParameters.AdditionalParameters[key.Key] = key.Value;
                            }
                        }
                    }

                }

                result = await this.GetDataAsync(dataParameters, additionalFilters);
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<GridResult>(ex, 3003);
            }

            return result;
        }

        /// <summary>
        /// Gets the table data.
        /// </summary>
        /// <param name="parameters">The table parameters.</param>
        /// <param name="aditionalFilters">AditionalFilters</param>
        /// <returns></returns>
        private async Task<GridResult> GetDataAsync(DataParameters parameters, IEnumerable<string> aditionalFilters)
        {
            GridResult result = new GridResult();

            var fields = await this.BuildMainQueryAsync(parameters, aditionalFilters);
            if (fields.FieldsValidations?.Count > 0)
            {
                result.Messages = fields.FieldsValidations;
                result.HasError = true;
            }
            else
            {

                var connectionName = fields.FieldDefinitions.FirstOrDefault()?.TableDefinition?.TableConfiguration?.ConnectionString;

                var queryFactory = this.Context.QueryBuilderOptions.ConnectionsPool[connectionName];

                // Why this is not in Dependency injection? Because the parser is not thread safe.
                // So it needs to be created everytime.
                var parser = new ParserFull(queryFactory);

                // Parses the select query.
                var selectQuery = parser.ParseToQuery(fields.QueryString.ToString(), parameters.AdditionalParameters);

                // Sets the IncludeMany statement and limits to 3 to try to gain some milliseconds of performance.
                this.SetIncludeMany(selectQuery, queryFactory, fields.FieldDefinitions, 3);

                // Parses the counter query
                var counterQuery = parser.ParseToQuery(fields.CounterString.ToString(), parameters.AdditionalParameters);

                var data = await selectQuery.GetAsync();

                var totalCount = await counterQuery.FirstAsync<int>();

                result.TotalRecords = totalCount;
                result.Result = data;

            }
            return result;
        }

        /// <summary>
        /// Builds the main query.
        /// </summary>
        /// <param name="parameters">Parameters to be used in the query building.</param>
        /// <param name="aditionalStatemnts">Additional statements for adding to the queries.</param>
        /// <returns></returns>
        private async Task<(List<GeneralMessage> FieldsValidations, StringBuilder QueryString, StringBuilder CounterString, IEnumerable<FieldDefinition> FieldDefinitions)> BuildMainQueryAsync(DataParameters parameters, IEnumerable<string> aditionalStatemnts)
        {
            // Only gets the requeired fields and loads them in memory. Dong this we can manipulate an use the info ealisy without
            // making multple request to the dataabase.
            var fieldDefinitions = await (from fd in base.GetFieldDefinitions(parameters)
                                          select new FieldDefinition()
                                          {
                                              ItemName = fd.ItemName,
                                              FieldType = fd.FieldType,
                                              FieldDefinitionId = fd.FieldDefinitionId,
                                              FieldDefinitionUi = new FieldDefinitionUi()
                                              {
                                                  AllowFilter = fd.FieldDefinitionUi.AllowFilter,
                                                  AllowSort = fd.FieldDefinitionUi.AllowSort,
                                              },
                                              TableDefinition = new TableDefinition()
                                              {
                                                  TableName = fd.TableDefinition.TableName,
                                                  TableConfiguration = new TableConfiguration()
                                                  {
                                                      ConnectionString = fd.TableDefinition.TableConfiguration.ConnectionString,
                                                  }
                                              },
                                              FieldIncludeManySources = (from es in fd.FieldIncludeManySources
                                                                             //where es.ExecutionSource.ExecutionType != EExecutionType.JavaScript
                                                                         select new FieldIncludeManySource()
                                                                         {
                                                                             ExecutionSource = new ExecutionSource()
                                                                             {
                                                                                 ExecutionType = es.ExecutionSource.ExecutionType,
                                                                                 ExecutionText = es.ExecutionSource.ExecutionText,
                                                                             },
                                                                             ForeignKey = es.ForeignKey,
                                                                             LocalKey = es.LocalKey,
                                                                             FieldDefinitionId = es.FieldDefinitionId,
                                                                             FieldDefinition = new FieldDefinition()
                                                                             {
                                                                                 TableDefinition = new TableDefinition()
                                                                                 {
                                                                                     TableConfiguration = new TableConfiguration()
                                                                                     {
                                                                                         ConnectionString = es.FieldDefinition.TableDefinition.TableConfiguration.ConnectionString
                                                                                     }
                                                                                 }
                                                                             }
                                                                         }).ToList()
                                          }
                                         ).ToListAsync();

            var mainQuery = await BuildMainQueryAsync(fieldDefinitions, parameters, aditionalStatemnts);

            // Validates the input query.
            var validations = this.GetFieldsValidations(fieldDefinitions, parameters);

            if (validations.Count > 0)
            {
                return (FieldsValidations: validations, QueryString: null, CounterString: null, FieldDefinitions: null);
            }
            else
            {
                // Gets the filter statement.
                var filterStmt = GetFilter(parameters.FilterCollection);

                // Sets the same filter for both queries.
                mainQuery.QueryString.Append(filterStmt).AppendLine();
                mainQuery.CounterString.Append(filterStmt).AppendLine();

                // Only sets the Limit statement when the data to retrive is for displaying in a grid or the detail list.
                if ((parameters.QueryType == EQueryType.GridView || parameters.QueryType == EQueryType.DetailListView) && parameters.RecordsCount > 0)
                {
                    mainQuery.QueryString.Append("Page(").Append(parameters.PageIndex >= 0 ? parameters.PageIndex + 1 : 1).AppendLine(")");
                    mainQuery.QueryString.Append("Limit(").Append(parameters.RecordsCount).AppendLine(")");
                }

                return (FieldsValidations: null, mainQuery.QueryString, mainQuery.CounterString, FieldDefinitions: fieldDefinitions);
            }

        }

        /// <summary>
        /// Builds the main query.
        /// </summary>
        /// <param name="parameters">Parameters to be used in the query building.</param>
        /// <param name="aditionalStatemnts">Additional statements for adding to the queries.</param>
        /// <param name="fieldDefinitions">A collection of <see cref="FieldDefinition"/>.</param>
        /// <returns></returns>
        private async Task<(StringBuilder QueryString, StringBuilder CounterString)> BuildMainQueryAsync(IEnumerable<FieldDefinition> fieldDefinitions, IBuilderParameters parameters, IEnumerable<string> aditionalStatemnts)
        {
            (StringBuilder QueryString, StringBuilder CounterString) result;

            var queryBuilder = new StringBuilder();
            var counterBuilder = new StringBuilder();

            await Task.Run(() =>
            {

                var selectFields = fieldDefinitions.Where(e => e.FieldType != EFieldType.MultiselectCheckbox && e.FieldType != EFieldType.MultiselectList && e.FieldType != EFieldType.TableReference);

                // The table query is the same for both queries.
                string alias = "T1";
                queryBuilder.Append("From(").Append(fieldDefinitions.First().TableDefinition.TableName).Append(" AS ").Append(alias).AppendLine(")");
                counterBuilder.Append(queryBuilder);

                // Select statement for query builder.
                queryBuilder.Append("Select(").Append(string.Join(",", selectFields.Select(e => alias + "." + e.ItemName))).Append(")");

                // Counter statement for counter.
                counterBuilder.Append("Select(COUNT(1) AS TotalCount)").AppendLine();

                // If there are aditional filters (or statements) they are added to the queries.
                if (aditionalStatemnts != null)
                {
                    foreach (string filter in aditionalStatemnts)
                    {
                        queryBuilder.AppendLine(filter);
                        counterBuilder.AppendLine(filter);
                    }
                }


                // Adds the table alias to the dictionary in order to use with the query.
                if (parameters.AdditionalParameters == null)
                    parameters.AdditionalParameters = new Dictionary<string, object>();

                if (!parameters.AdditionalParameters.ContainsKey("FirstAlias"))
                {
                    parameters.AdditionalParameters.Add("FirstAlias", alias);
                }

                if (parameters.QueryType == EQueryType.EditView)
                {
                    // Only one record since it is the detail view.
                    queryBuilder.Append("Limit(1)").AppendLine();
                }

            });

            result = (QueryString: queryBuilder, CounterString: counterBuilder);
            return result;
        }

        private void SetIncludeMany(Query query, QueryFactory queryFactory, IEnumerable<FieldDefinition> fieldDefinitions, int setLimit)
        {
            var includeMany = from source in fieldDefinitions.SelectMany(e => e.FieldIncludeManySources)
                              join fd in fieldDefinitions on source.FieldDefinitionId equals fd.FieldDefinitionId
                              where
                               (fd.FieldType == EFieldType.MultiselectCheckbox || fd.FieldType == EFieldType.MultiselectList || fd.FieldType == EFieldType.SingleSelect || fd.FieldType == EFieldType.Radio)
                              select new
                              {
                                  source.ExecutionSource.ExecutionText,
                                  source.ExecutionSource.ExecutionType,
                                  fd.ItemName,
                                  source.LocalKey,
                                  source.ForeignKey,
                                  fd.FieldType,
                                  ExecuteConnectionString = source.FieldDefinition.TableDefinition.TableConfiguration.ConnectionString
                              };

            foreach (var item in includeMany)
            {
                if (item.ExecutionType == EExecutionType.ALex)
                {
                    var factory = this.Context.QueryBuilderOptions.ConnectionsPool[item.ExecuteConnectionString];
                    var q = new ParserFull(factory)
                        .ParseToQuery(item.ExecutionText);

                    // If the value is set then it will make a TOP (limit) in the specified IncludeMany statement.
                    if (setLimit > 0)
                    {
                        q.Limit(setLimit);
                    }

                    if (item.FieldType == EFieldType.SingleSelect || item.FieldType == EFieldType.MultiselectCheckbox || item.FieldType == EFieldType.MultiselectList || item.FieldType == EFieldType.Radio)
                    {
                        query.IncludeMany(item.ItemName, q, item.ForeignKey, item.LocalKey);
                    }
                }
            }
        }

        private string GetFilter(QueryFilterCollection queryFilterCollection)
        {
            StringBuilder builder = new StringBuilder();

            if (queryFilterCollection.QueryFilters.Count > 0)
            {
                builder.AppendLine()
                        .Append("Filter(");

                int index;
                for (index = 0; index < queryFilterCollection.QueryFilters.Count; index++)
                {
                    SetQueryFilter(builder, queryFilterCollection.QueryFilters[index], index, queryFilterCollection.QueryFilters.Count - 1);
                }


                for (var f = 0; f < queryFilterCollection.Groups.Count; f++)
                {
                    builder.Append(" ").Append(queryFilterCollection.Groups[f].GroupingOperator != EGroupOperator.None ? queryFilterCollection.Groups[f].GroupingOperator : EGroupOperator.And).Append(" (");
                    for (index = 0; index < queryFilterCollection.Groups[f].QueryFilters.Count; index++)
                    {
                        SetQueryFilter(builder, queryFilterCollection.Groups[f].QueryFilters[index], index, queryFilterCollection.Groups[f].QueryFilters.Count - 1);
                    }
                    builder.Append(")");
                }

                builder.Append(")");
            }

            return builder.ToString();
        }

        private void SetQueryFilter(StringBuilder builder, QueryFilter queryFilter, int index, int max)
        {
            builder.Append(EntityHelper.GetSqlOperatorString(queryFilter.SearchingOperator, queryFilter.FieldName, (queryFilter.SearchingValue)));

            if (max > 0 && index < max)
            {
                builder.Append(" ").Append(queryFilter.GroupOperator != EGroupOperator.None ? queryFilter.GroupOperator : EGroupOperator.And).Append(" ");
            }
        }

        /// <summary>
        /// Validates the specified parameters.
        /// </summary>
        /// <param name="fieldDefinitions">The fieldefinitions.</param>
        /// <param name="parameters">The parameters-</param>
        /// <returns></returns>
        private List<GeneralMessage> GetFieldsValidations(IEnumerable<FieldDefinition> fieldDefinitions, DataParameters parameters)
        {
            List<GeneralMessage> result = new List<GeneralMessage>();

            if (fieldDefinitions.Count() == 0)
            {
                result.Add(new GeneralMessage()
                {
                    Message = string.Format("There is no data assotiated with the name '{0}'.", parameters.ItemName),
                    Code = 300,
                    MessageType = EMessageType.Error,
                });
            }
            else
            {
                if (parameters.FilterCollection.QueryFilters.Count() > 0)
                {
                    var invalidFilters = fieldDefinitions.FullOuterJoin(parameters.FilterCollection.QueryFilters, e => e.ItemName, e => e.FieldName, (fd, f, k) =>
                          {
                              return new
                              {
                                  ItemName = fd?.ItemName ?? f?.FieldName,
                                  IsValid = (string.IsNullOrWhiteSpace(f?.FieldName) ? !string.IsNullOrWhiteSpace(fd?.ItemName) : true) && (fd?.FieldDefinitionUi?.AllowFilter ?? false),
                              };
                          }).Where(e => e.IsValid == false).ToArray();


                    if (invalidFilters.Count() > 0)
                    {
                        result.Add(new GeneralMessage()
                        {
                            Message = "The following fields are not valid for filtering due to the field does not exists or is not allowed to filter in the settings:\r\n" + string.Join("\r\n", invalidFilters.Select(e => e.ItemName)),
                            Code = 301,
                            MessageType = EMessageType.Error,
                        });
                    }
                }

                if (parameters.SortingNames.Count() > 0)
                {
                    var invalidSortings = fieldDefinitions.FullOuterJoin(parameters.SortingNames, e => e.ItemName, e => e.ItemName, (fd, f, k) =>
                    {
                        return new
                        {
                            ItemName = fd?.ItemName ?? f?.ItemName,
                            IsValid = (string.IsNullOrWhiteSpace(f?.ItemName) ? !string.IsNullOrWhiteSpace(fd?.ItemName) : true) && (fd?.FieldDefinitionUi?.AllowSort ?? false)
                        };
                    }).Where(e => e.IsValid == false).ToArray();

                    if (invalidSortings.Count() > 0)
                    {
                        result.Add(new GeneralMessage()
                        {
                            Message = "The following fields are not valid for sorting due to the field does not exists or is not allowed to sort in the settings:" + string.Join("\r\n", invalidSortings.Select(e => e.ItemName)),
                            Code = 302,
                            MessageType = EMessageType.Error,
                        });
                    }
                }

                if (parameters.RecordsCount == 0 || parameters.RecordsCount >= 10000)
                    result.Add(new GeneralMessage()
                    {
                        Message = "The rows counter must be set between 1 and 10000. Set as -1 to retrieve all the recods (not recomended due to performance issues).",
                        Code = 303,
                        MessageType = EMessageType.Error,
                    });
            }

            return result;
        }
    }
}
