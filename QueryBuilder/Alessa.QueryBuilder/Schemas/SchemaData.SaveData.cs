using Alessa.ALex;
using Alessa.ALex.SqlKata;
using Alessa.QueryBuilder.Entities;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using Alessa.QueryBuilder.Entities.Data;
using Alessa.QueryBuilder.Entities.Results;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alessa.QueryBuilder
{
    public partial class SchemaData
    {
        #region Before/After events
        /// <summary>
        /// Processes the before create record event.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        private async Task<SavingResult> ProcessBeforeCreateRecordAsync(SaveParameters parameters)
        {
            SavingResult result;

            try
            {
                await this.ProcessBeforeCreateRecordAsync(parameters);
                result = new SavingResult()
                {
                    Result = parameters.AdditionalParameters
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<SavingResult>(ex, 4001);
            }

            return result;
        }
        /// <summary>
        /// Processes the before update record event.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        private async Task<SavingResult> ProcessBeforeUpdateRecordAsync(SaveParameters parameters)
        {
            SavingResult result;

            try
            {
                await this.ExecuteActionsAsync(parameters, ETableDbEventType.BeforeUpdate);

                var record = this.GetDataAsync(parameters, null);
                result = new SavingResult()
                {
                    Result = record
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<SavingResult>(ex, 4002);
            }
            return result;
        }
        /// <summary>
        /// Processes the after create record event.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        private async Task<SavingResult> ProcessAfterCreateRecordAsync(SaveParameters parameters)
        {
            SavingResult result;

            try
            {
                await this.ExecuteActionsAsync(parameters, ETableDbEventType.AfterCreate);
                var record = this.GetDataAsync(parameters, null);
                result = new SavingResult()
                {
                    Result = record
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<SavingResult>(ex, 4003);
            }
            return result;
        }
        /// <summary>
        /// Processes the after create record event.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        private async Task<SavingResult> ProcessAfterUpdateRecordAsync(SaveParameters parameters)
        {
            SavingResult result;

            try
            {
                await this.ExecuteActionsAsync(parameters, ETableDbEventType.AfterUpdate);
                result = new SavingResult()
                {
                    Result = parameters.AdditionalParameters
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<SavingResult>(ex, 4004);
            }
            return result;
        }

        /// <summary>
        /// Processes the before delete record event.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        private async Task<SavingResult> ProcessBeforeDeleteRecordAsync(SaveParameters parameters)
        {
            SavingResult result;

            try
            {
                await this.ExecuteActionsAsync(parameters, ETableDbEventType.BeforeCreate);
                var record = this.GetDataAsync(parameters, null);
                result = new SavingResult()
                {
                    Result = record
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<SavingResult>(ex, 4005);
            }
            return result;
        }
        /// <summary>
        /// Processes the after delete record event.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        private async Task<SavingResult> ProcessAfterDeleteRecordAsync(SaveParameters parameters)
        {
            SavingResult result;

            try
            {
                await this.ExecuteActionsAsync(parameters, ETableDbEventType.BeforeCreate);
                result = new SavingResult()
                {
                    Result = parameters.AdditionalParameters
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<SavingResult>(ex, 4006);
            }
            return result;
        }

        #endregion

        /// <summary>
        /// Saves the specified parameters.
        /// </summary>
        /// <param name="parameters">Parameters with the info to save.</param>
        /// <param name="basicValidations">Whether the basic validations (Is Required, Regex, Range, Max/Min length) must be executed.</param>
        /// <param name="advancedValidations">Whether the advanced validations (such as ALex/Javascript)</param>
        /// <param name="processAfterEvent">Processes the after events (if any).</param>
        /// <param name="processBeforeEvent">Processes the before events (if any).</param>
        /// <returns></returns>
        public async Task SaveAsync(SaveParameters parameters, bool basicValidations = true, bool advancedValidations = true, bool processBeforeEvent = true, bool processAfterEvent = true)
        {
            SavingResult result = new SavingResult();
            IEnumerable<string> actualFields = parameters.AdditionalParameters.Select(e => e.Key);
            List<FieldDefinition> fieldDefinitions = await (from fd in base.GetFieldDefinitions(parameters)
                                                            where actualFields.Contains(fd.ItemName)
                                                            select new FieldDefinition()
                                                            {
                                                                ItemName = fd.ItemName,
                                                                FieldType = fd.FieldType,
                                                                FieldDefinitionUi = new FieldDefinitionUi()
                                                                {
                                                                    IsRequired = fd.FieldDefinitionUi.IsRequired,
                                                                    Regex = fd.FieldDefinitionUi.Regex,
                                                                    MaxLength = fd.FieldDefinitionUi.MaxLength,
                                                                    MinLength = fd.FieldDefinitionUi.MinLength,
                                                                    RangeMax = fd.FieldDefinitionUi.RangeMax,
                                                                    RangeMin = fd.FieldDefinitionUi.RangeMin,
                                                                    RequiredErrorMsg = fd.FieldDefinitionUi.RequiredErrorMsg,
                                                                    RegexErrorMsg = fd.FieldDefinitionUi.RegexErrorMsg,
                                                                    MinLengthErrorMsg = fd.FieldDefinitionUi.MinLengthErrorMsg,
                                                                    MaxLengthErrorMsg = fd.FieldDefinitionUi.MaxLengthErrorMsg,
                                                                    FormatErrorMsg = fd.FieldDefinitionUi.FormatErrorMsg,
                                                                    RangeErrorMsg = fd.FieldDefinitionUi.RangeErrorMsg,
                                                                    DisplayName = fd.FieldDefinitionUi.DisplayName,
                                                                    DisplayFormat = fd.FieldDefinitionUi.DisplayFormat,
                                                                },
                                                                TableFieldValidations = (from tfv in fd.TableFieldValidations
                                                                                         select new TableFieldValidation()
                                                                                         {
                                                                                             ExecutionResultType = tfv.ExecutionResultType,
                                                                                             ExecutionSource = new ExecutionSource()
                                                                                             {
                                                                                                 ExecutionText = tfv.ExecutionSource.ExecutionText,
                                                                                                 ExecutionType = tfv.ExecutionSource.ExecutionType,
                                                                                                 TableConfiguration = new TableConfiguration()
                                                                                                 {
                                                                                                     ConnectionString = tfv.ExecutionSource.TableConfiguration.ConnectionString
                                                                                                 }
                                                                                             }
                                                                                         }).ToList()
                                                            }).ToListAsync();

            // Exdecutes the basic validations.
            if (basicValidations)
            {
                var validations = BasicValidations.GetValidations(parameters, fieldDefinitions);
                result.Messages.AddRange(validations);
            }

            if (advancedValidations)
            {

            }

            // There are errors in the validations.
            result.HasError = result.Messages.Any(e => e.MessageType == Core.Entities.Results.EMessageType.Error);
        }

        /// <summary>
        /// Process the Create request before inserting any record in the database.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public async Task<SavingResult> ProcessOnEnterCreate(SaveParameters parameters)
        {
            SavingResult result;

            try
            {
                await this.ExecuteActionsAsync(parameters, ETableDbEventType.OnEnterCreate);
                result = new SavingResult()
                {
                    Result = parameters.AdditionalParameters
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<SavingResult>(ex, 4007);
            }
            return result;
        }

        /// <summary>
        /// Process the Update request before updating any record in the database.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public async Task<SavingResult> ProcessOnEnterUpdate(SaveParameters parameters)
        {
            SavingResult result;

            try
            {
                await this.ExecuteActionsAsync(parameters, ETableDbEventType.OnEnterUpdate);
                result = new SavingResult()
                {
                    Result = parameters.AdditionalParameters
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<SavingResult>(ex, 4008);
            }
            return result;
        }

        /// <summary>
        /// Process the Delete request before deleting any record in the database.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public async Task<SavingResult> ProcessOnEnterDelete(SaveParameters parameters)
        {
            SavingResult result;

            try
            {
                await this.ExecuteActionsAsync(parameters, ETableDbEventType.OnEnterUpdate);
                result = new SavingResult()
                {
                    Result = parameters.AdditionalParameters
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<SavingResult>(ex, 4009);
            }
            return result;
        }

        #region Validations

        #endregion

        private IQueryable<ExecutionSource> GetTableExecutionSources(SaveParameters parameters, ETableDbEventType tableDbEventType)
        {
            var tables = base.GetTableDefinitions(parameters);

            switch (tableDbEventType)
            {
                case ETableDbEventType.BeforeCreate:
                case ETableDbEventType.AfterCreate:
                case ETableDbEventType.OnEnterCreate:
                    tables = tables.Where(e => e.TableDefinitionUi.AllowCreate);
                    break;
                case ETableDbEventType.BeforeUpdate:
                case ETableDbEventType.AfterUpdate:
                case ETableDbEventType.OnEnterUpdate:
                    tables = tables.Where(e => e.TableDefinitionUi.AllowEdit);
                    break;
                case ETableDbEventType.BeforeDelete:
                case ETableDbEventType.AfterDelete:
                case ETableDbEventType.OnEnterDelete:
                    tables = tables.Where(e => e.TableDefinitionUi.AllowDelete);
                    break;
                case ETableDbEventType.AfterCreateOrUpdate:
                case ETableDbEventType.BeforeCreateOrUpdate:
                case ETableDbEventType.OnEnterCreateOrUpdate:
                    tables = tables.Where(e => e.TableDefinitionUi.AllowCreate || e.TableDefinitionUi.AllowEdit);
                    break;
            }


            return tables
                .SelectMany(e => e.TableActions)
                .Where(e => (tableDbEventType == ETableDbEventType.BeforeCreate ? e.TableDbEventType == ETableDbEventType.BeforeCreateOrUpdate || e.TableDbEventType == ETableDbEventType.BeforeCreate :
                             tableDbEventType == ETableDbEventType.AfterCreate ? e.TableDbEventType == ETableDbEventType.AfterCreateOrUpdate || e.TableDbEventType == ETableDbEventType.AfterCreate :
                             tableDbEventType == ETableDbEventType.BeforeUpdate ? e.TableDbEventType == ETableDbEventType.BeforeCreateOrUpdate || e.TableDbEventType == ETableDbEventType.BeforeUpdate :
                             tableDbEventType == ETableDbEventType.AfterUpdate ? e.TableDbEventType == ETableDbEventType.AfterCreateOrUpdate || e.TableDbEventType == ETableDbEventType.AfterUpdate :
                             tableDbEventType == ETableDbEventType.OnEnterCreate ? e.TableDbEventType == ETableDbEventType.OnEnterCreateOrUpdate || e.TableDbEventType == ETableDbEventType.OnEnterCreate :
                             tableDbEventType == ETableDbEventType.OnEnterUpdate ? e.TableDbEventType == ETableDbEventType.OnEnterCreateOrUpdate || e.TableDbEventType == ETableDbEventType.OnEnterUpdate
                        : false) && e.ExecutionSource.ExecutionType != EExecutionType.JavaScript)
                .OrderBy(e => e.ExecutionOrder)
                .Select(e => e.ExecutionSource)
                ;
        }

        /// <summary>
        /// Executes the specified actions and returns the results of the executions.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="eventType">Event type.</param>
        /// <returns></returns>
        private async Task ExecuteActionsAsync(SaveParameters parameters, ETableDbEventType eventType)
        {
            // Gets the execution sources.
            var execution = await this.GetTableExecutionSources(parameters, eventType)
            .Select(e => new ExecutionSource()
            {
                ExecutionType = e.ExecutionType,
                ExecutionText = e.ExecutionText,
                TableConfiguration = new TableConfiguration()
                {
                    ConnectionString = e.TableConfiguration.ConnectionString
                }
            })
            .ToListAsync();

            System.Collections.Concurrent.ConcurrentDictionary<string, object> resultDictionary = new System.Collections.Concurrent.ConcurrentDictionary<string, object>();

            // Iterates the execution sources.
            int index;
            for (index = 0; index < execution.Count; index++)
            {
                List<IDictionary<string, object>> result = null;

                // Executes the ALex query.
                if (execution[index].ExecutionType == EExecutionType.ALex)
                {
                    // Gets the factory context.
                    var factory = base.Context.QueryBuilderOptions.ConnectionsPool[execution[index].TableConfiguration.ConnectionString];

                    // Parses the query.
                    var q = new ParserFull(factory)
                        .ParseToQuery(execution[index].ExecutionText, resultDictionary);

                    // Gets the execution result.
                    result = (await q.GetAsync()).Cast<IDictionary<string, object>>().ToList();
                }

                // Adds the results 8if any) into the dictionary.
                Parallel.For(0, result.Count, (i) =>
                {
                    Parallel.ForEach(result[i], (e) =>
                    {
                        resultDictionary.AddOrUpdate(e.Key, e.Value, (f, g) => g);
                    });
                });
            }

            // If the result is null.
            if (parameters.AdditionalParameters == null)
                parameters.AdditionalParameters = new Dictionary<string, object>();

            // Adds the concurrent dictionary results into the parameters.
            foreach (string key in resultDictionary.Keys)
            {
                if (!parameters.AdditionalParameters.ContainsKey(key))
                {
                    parameters.AdditionalParameters.Add(key, resultDictionary[key]);
                }
                else
                {
                    parameters.AdditionalParameters[key] = resultDictionary[key];
                }
            }
        }
    }
}
