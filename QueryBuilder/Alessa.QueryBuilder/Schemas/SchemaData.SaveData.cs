using Alessa.ALex;
using Alessa.ALex.SqlKata;
using Alessa.Core.Entities.Results;
using Alessa.QueryBuilder.Entities;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using Alessa.QueryBuilder.Entities.Data;
using Alessa.QueryBuilder.Entities.Results;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;
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
        public async Task<SavingResult> ProcessBeforeCreateRecordAsync(SaveParameters parameters)
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
        public async Task<SavingResult> ProcessBeforeUpdateRecordAsync(SaveParameters parameters)
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
        public async Task<SavingResult> ProcessAfterCreateRecordAsync(SaveParameters parameters)
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
        public async Task<SavingResult> ProcessAfterUpdateRecordAsync(SaveParameters parameters)
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
        public async Task<SavingResult> ProcessBeforeDeleteRecordAsync(SaveParameters parameters)
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
        public async Task<SavingResult> ProcessAfterDeleteRecordAsync(SaveParameters parameters)
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

        /// <summary>
        /// Executes the specified actions and returns the results of the executions.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <param name="eventType">Event type.</param>
        /// <returns></returns>
        private async Task ExecuteActionsAsync(SaveParameters parameters, ETableDbEventType eventType)
        {
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

            int index;
            for (index = 0; index < execution.Count; index++)
            {
                var factory = base.Context.QueryBuilderOptions.ConnectionsPool[execution[index].TableConfiguration.ConnectionString];

                var q = new ParserFull(factory)
                    .ParseToQuery(execution[index].ExecutionText, resultDictionary);

                var result = (await q.GetAsync()).Cast<IDictionary<string, object>>().ToList();

                Parallel.For(0, result.Count, (i) =>
                {
                    Parallel.ForEach(result[i], (e) =>
                    {
                        resultDictionary.AddOrUpdate(e.Key, e.Value, (f, g) => g);
                    });
                });
            }

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

        #endregion

        /// <summary>
        /// Saves the specified parameters.
        /// </summary>
        /// <param name="parameters">Parameters with the info to save.</param>
        /// <returns></returns>
        public async Task SaveAsync(SaveParameters parameters)
        {
            var actualFields = parameters.AdditionalParameters.Select(e => e.Key);
            var fieldDefinitions = await (from fd in base.GetFieldDefinitions(parameters)
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
                                              }
                                          }).ToListAsync();

            var cross = (from name in fieldDefinitions
                         join item in parameters.AdditionalParameters on name.ItemName equals item.Key into jr
                         from item in jr.DefaultIfEmpty()
                         select new
                         {
                             Item = item,
                             FieldDefinition = name.FieldDefinitionUi
                         }).ToList();

            var messages = new System.Collections.Concurrent.ConcurrentBag<GeneralMessage>();

            Parallel.For(0, cross.Count, (i) =>
            {
                if (cross[i].FieldDefinition.IsRequired && cross[i].Item.Value == null)
                {
                }
            });

            SavingResult result = new SavingResult();

            // Makes the basic validations


        }

        private IQueryable<ExecutionSource> GetTableExecutionSources(SaveParameters parameters, ETableDbEventType tableDbEventType)
        {
            var tables = base.GetTableDefinitions(parameters);

            switch (tableDbEventType)
            {
                case ETableDbEventType.BeforeCreate:
                case ETableDbEventType.AfterCreate:
                    tables = tables.Where(e => e.TableDefinitionUi.AllowCreate);
                    break;
                case ETableDbEventType.BeforeUpdate:
                case ETableDbEventType.AfterUpdate:
                    tables = tables.Where(e => e.TableDefinitionUi.AllowEdit);
                    break;
                case ETableDbEventType.BeforeDelete:
                case ETableDbEventType.AfterDelete:
                    tables = tables.Where(e => e.TableDefinitionUi.AllowDelete);
                    break;
                case ETableDbEventType.AfterCreateOrUpdate:
                case ETableDbEventType.BeforeCreateOrUpdate:
                    tables = tables.Where(e => e.TableDefinitionUi.AllowCreate || e.TableDefinitionUi.AllowEdit);
                    break;
            }


            return tables
                .SelectMany(e => e.TableActions)
                .Where(e => (tableDbEventType == ETableDbEventType.BeforeCreate ? e.TableDbEventType == ETableDbEventType.BeforeCreateOrUpdate || e.TableDbEventType == ETableDbEventType.BeforeCreate :
                             tableDbEventType == ETableDbEventType.AfterCreate ? e.TableDbEventType == ETableDbEventType.AfterCreateOrUpdate || e.TableDbEventType == ETableDbEventType.AfterCreate :
                             tableDbEventType == ETableDbEventType.BeforeUpdate ? e.TableDbEventType == ETableDbEventType.BeforeCreateOrUpdate || e.TableDbEventType == ETableDbEventType.BeforeUpdate :
                             tableDbEventType == ETableDbEventType.AfterUpdate ? e.TableDbEventType == ETableDbEventType.AfterCreateOrUpdate || e.TableDbEventType == ETableDbEventType.AfterUpdate
                        : false) && e.ExecutionSource.ExecutionType != EExecutionType.JavaScript)
                .OrderBy(e => e.ExecutionOrder)
                .Select(e => e.ExecutionSource)
                ;
        }
    }
}
