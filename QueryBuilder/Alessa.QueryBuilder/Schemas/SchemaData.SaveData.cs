using Alessa.ALex;
using Alessa.ALex.SqlKata;
using Alessa.Core.Entities.Results;
using Alessa.QueryBuilder.Entities;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using Alessa.QueryBuilder.Entities.Data;
using Alessa.QueryBuilder.Entities.Results;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Alessa.ALex.AlexQueryExtensions;

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
                                                  RequiredErrorMsg = fd.FieldDefinitionUi.RequiredErrorMsg,
                                                  RegexErrorMsg = fd.FieldDefinitionUi.RegexErrorMsg,
                                                  MinLengthErrorMsg = fd.FieldDefinitionUi.MinLengthErrorMsg,
                                                  MaxLengthErrorMsg = fd.FieldDefinitionUi.MaxLengthErrorMsg,
                                                  DisplayName = fd.FieldDefinitionUi.DisplayName,
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


            var basicValidations = this.GetBasicValidations(parameters, fieldDefinitions);


            SavingResult result = new SavingResult();


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

        private List<GeneralMessage> GetBasicValidations(SaveParameters parameters, IEnumerable<FieldDefinition> fieldDefinitions)
        {
            List<Tuple<KeyValuePair<string, object>, FieldDefinitionUi, FieldDefinition>> cross;

            // When the save type is Create it means the action to execute is an insert, it means all the field definitions
            // must be validated, e.g. When you have to enroll an User you need the First and Last name, thenin case you provide
            // only the First Name then it will throw a message indicating the Last name is required.
            if (parameters.SaveType == ESaveType.Create)
            {
                cross = (from name in fieldDefinitions
                         join item in parameters.AdditionalParameters on name.ItemName equals item.Key into jr
                         from item in jr.DefaultIfEmpty()
                         select Tuple.Create(item, name.FieldDefinitionUi, name)).ToList();
            }
            else
            {
                // Otherwise if the record already exist and only you send the First name, then it will only validate the first name.
                cross = (from name in fieldDefinitions
                         join item in parameters.AdditionalParameters on name.ItemName equals item.Key
                         select Tuple.Create(item, name.FieldDefinitionUi, name)).ToList();
            }

            var messages = new System.Collections.Concurrent.ConcurrentBag<GeneralMessage>();

            // Tohose are constant codes used for the messaging.
            const int requiredCode = 1;
            const int regexCode = 2;
            const int maxCode = 3;
            const int minCode = 4;
            const int betweenCode = 5;

            const string defaultRequiredErrorMsg = "The field {{DisplayName}} is required.";
            const string defaultRegexErrorMsg = "The field {{DisplayName}} must match the regular expression '{{Regex}}'.";
            const string defaultMinCodeErrorMsg = "The length of {{DisplayName}} must be {{MinLength}} characters or more.";
            const string defaultMaxErrorMsg = "The length of {{DisplayName}} must be {{MaxLength}} cahracters or fewer.";
            const string defaultBetweenErrorMsg = "The length of {{DisplayName}} must be between {{MinLength}} and {{MaxLength}} characters.";

            // Validations parallel processing.
            Parallel.For(0, cross.Count, (i) =>
            {
                // Gets the dicitonary from the object properties.
                var dictionary = cross[i].Item2.GetDictionary();
                bool isNullValue = cross[i].Item1.Value == null;

                // Is required?
                if (cross[i].Item2.IsRequired && (isNullValue || string.IsNullOrWhiteSpace(cross[i].Item1.Value.ToString())))
                {
                    isNullValue = true;
                    messages.Add(new GeneralMessage()
                    {
                        Code = requiredCode,
                        MessageType = EMessageType.Error,
                        Message = cross[i].Item2.RequiredErrorMsg ?? defaultRequiredErrorMsg.FormatQuery(dictionary),
                        Source = cross[i].Item3.ItemName,
                    });
                }

                // Regular expression.
                if (!isNullValue)
                {
                    var strValue = cross[i].Item1.Value.ToString();

                    if (!string.IsNullOrWhiteSpace(cross[i].Item2.Regex) && !Regex.IsMatch(strValue, cross[i].Item2.Regex))
                    {
                        messages.Add(new GeneralMessage()
                        {
                            Code = regexCode,
                            MessageType = EMessageType.Error,
                            Message = cross[i].Item2.RegexErrorMsg ?? defaultRegexErrorMsg.FormatQuery(dictionary),
                            Source = cross[i].Item3.ItemName,
                        });
                    }

                    // When the MinLength and MaxLength are set for this field.
                    if (cross[i].Item2.MinLength != null && cross[i].Item2.MinLength > 0 && cross[i].Item2.MaxLength != null && cross[i].Item2.MinLength > 0)
                    {
                        if (strValue.Length < cross[i].Item2.MinLength.Value || strValue.Length > cross[i].Item2.MaxLength.Value)
                        {
                            messages.Add(new GeneralMessage()
                            {
                                Code = betweenCode,
                                MessageType = EMessageType.Error,
                                Message = defaultBetweenErrorMsg.FormatQuery(dictionary),
                                Source = cross[i].Item3.ItemName,
                            });
                        }
                    }
                    // Only MinLength is set.
                    else if ((cross[i].Item2.MinLength != null && cross[i].Item2.MinLength > 0) && (cross[i].Item2.MaxLength == null || cross[i].Item2.MinLength <= 0))
                    {
                        if (strValue.Length < cross[i].Item2.MinLength.Value)
                        {
                            messages.Add(new GeneralMessage()
                            {
                                Code = minCode,
                                MessageType = EMessageType.Error,
                                Message = cross[i].Item2.MinLengthErrorMsg ?? defaultMinCodeErrorMsg.FormatQuery(dictionary),
                                Source = cross[i].Item3.ItemName,
                            });
                        }
                    }
                    // Only MaxLength is set.
                    else if ((cross[i].Item2.MinLength == null || cross[i].Item2.MinLength <= 0) && (cross[i].Item2.MaxLength != null && cross[i].Item2.MinLength > 0))
                    {
                        if (strValue.Length > cross[i].Item2.MaxLength.Value)
                        {
                            messages.Add(new GeneralMessage()
                            {
                                Code = maxCode,
                                MessageType = EMessageType.Error,
                                Message = cross[i].Item2.MaxLengthErrorMsg ?? defaultMaxErrorMsg.FormatQuery(dictionary),
                                Source = cross[i].Item3.ItemName,
                            });
                        }
                    }
                }
            });

            return messages.ToList();
        }

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
