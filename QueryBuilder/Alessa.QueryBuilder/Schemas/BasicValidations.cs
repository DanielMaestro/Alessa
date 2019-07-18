using Alessa.Core.Entities.Results;
using Alessa.Core.Helpers;
using Alessa.QueryBuilder.Entities;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using Alessa.QueryBuilder.Entities.Data;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static Alessa.ALex.AlexQueryExtensions;

namespace Alessa.QueryBuilder
{
    internal static class BasicValidations
    {
        #region Fields and constants
        // Those are constant codes used for the messaging.
        private const int requiredCode = 1;
        private const int regexCode = 2;
        private const int maxCode = 3;
        private const int minCode = 4;
        private const int betweenCode = 5;
        private const int notValidFormatCode = 6;
        private const int rangeCode = 7;

        private const string valueStr = "Value";
        private const string defaultRequiredErrorMsg = "The field {{DisplayName}} is required.";
        private const string defaultRegexErrorMsg = "The field {{DisplayName}} must match the regular expression '{{Regex}}'.";
        private const string defaultMinCodeErrorMsg = "The length of {{DisplayName}} must be {{MinLength}} or more.";
        private const string defaultMaxErrorMsg = "The length of {{DisplayName}} must be {{MaxLength}} or fewer.";
        private const string defaultBetweenErrorMsg = "The length of {{DisplayName}} must be between {{MinLength}} and {{MaxLength}}.";
        private const string defaultFormatErrorMsg = "The value {{Value}} is not valid for {{DisplayName}}.";
        private const string defaultRangeErrorMsg = "The value for {{DisplayName}} must be between {{MinRange}} and {{MaxRange}}";
        private const string defaultMinRangeErrorMsg = "The value for {{DisplayName}} must be greater than {{MinRange}}";
        private const string defaultMaxRangeErrorMsg = "The value for {{DisplayName}} must be less than {{MaxRange}}";

        private const string defaultCollectionMinCodeErrorMsg = "The selected item for {{DisplayName}} must be {{MinLength}} or more.";
        private const string defaultCollectionMaxErrorMsg = "The selected item for {{DisplayName}} must be {{MaxLength}} or fewer.";
        private const string defaultCollectionBetweenErrorMsg = "The selected item for {{DisplayName}} must be between {{MinLength}} and {{MaxLength}}.";
        #endregion

        /// <summary>
        /// Gets a set of validations.
        /// </summary>
        /// <param name="parameters">The parameters object.</param>
        /// <param name="fieldDefinitions">The field definitions.</param>
        /// <returns></returns>
        internal static List<GeneralMessage> GetValidations(SaveParameters parameters, IEnumerable<FieldDefinition> fieldDefinitions)
        {
            var messages = new ConcurrentBag<GeneralMessage>();

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

            // Validations parallel processing.
            //Parallel.For(0, cross.Count, (i) =>
            for (int i = 0; i < cross.Count; i++)
            {
                // Gets the dicitonary from the object properties.
                var dictionary = new ConcurrentDictionary<string, object>(cross[i].Item2.GetDictionary());

                // The value is a collection? (But not an string)
                if (cross[i].Item1.Value is IEnumerable && !(cross[i].Item1.Value is string))
                {
                    IEnumerable<object> values;
                    values = ((IEnumerable)cross[i].Item1.Value)?.Cast<object>();
                    ValidateCollectionyValues(dictionary, values, cross[i].Item3, ref messages);
                }
                else if (EntityHelper.PrimitiveTypes.Contains(cross[i].Item1.Value.GetType()))
                {
                    // Validates the value.
                    ValidateSingleValue(dictionary, cross[i].Item1.Value, cross[i].Item3, ref messages);
                }
            }
            //);

            return messages.ToList();
        }

        #region Basic validations
        private static void ValidateCollectionyValues(IDictionary<string, object> dictionary, IEnumerable<object> values, FieldDefinition fieldDefinition, ref ConcurrentBag<GeneralMessage> messages)
        {
            var length = values?.Count();

            // When the MinLength and MaxLength are set for this field.
            if (fieldDefinition.FieldDefinitionUi.MinLength != null && fieldDefinition.FieldDefinitionUi.MinLength > 0 && fieldDefinition.FieldDefinitionUi.MaxLength != null && fieldDefinition.FieldDefinitionUi.MinLength > 0)
            {
                if (length < fieldDefinition.FieldDefinitionUi.MinLength.Value || length > fieldDefinition.FieldDefinitionUi.MaxLength.Value)
                {
                    messages.Add(new GeneralMessage()
                    {
                        Code = betweenCode,
                        MessageType = EMessageType.Error,
                        Message = defaultCollectionBetweenErrorMsg.FormatQuery(dictionary),
                        Source = fieldDefinition.ItemName,
                    });
                }
            }

            // Only MinLength is set.
            else if ((fieldDefinition.FieldDefinitionUi.MinLength != null && fieldDefinition.FieldDefinitionUi.MinLength > 0) && (fieldDefinition.FieldDefinitionUi.MaxLength == null || fieldDefinition.FieldDefinitionUi.MinLength <= 0))
            {
                if (length < fieldDefinition.FieldDefinitionUi.MinLength.Value)
                {
                    messages.Add(new GeneralMessage()
                    {
                        Code = minCode,
                        MessageType = EMessageType.Error,
                        Message = fieldDefinition.FieldDefinitionUi.MinLengthErrorMsg ?? defaultCollectionMinCodeErrorMsg.FormatQuery(dictionary),
                        Source = fieldDefinition.ItemName,
                    });
                }
            }

            // Only MaxLength is set.
            else if ((fieldDefinition.FieldDefinitionUi.MinLength == null || fieldDefinition.FieldDefinitionUi.MinLength <= 0) && (fieldDefinition.FieldDefinitionUi.MaxLength != null && fieldDefinition.FieldDefinitionUi.MinLength > 0))
            {
                if (length > fieldDefinition.FieldDefinitionUi.MaxLength.Value)
                {
                    messages.Add(new GeneralMessage()
                    {
                        Code = maxCode,
                        MessageType = EMessageType.Error,
                        Message = fieldDefinition.FieldDefinitionUi.MaxLengthErrorMsg ?? defaultCollectionMaxErrorMsg.FormatQuery(dictionary),
                        Source = fieldDefinition.ItemName,
                    });
                }
            }
        }

        private static void ValidateSingleValue(IDictionary<string, object> dictionary, object value, FieldDefinition fieldDefinition, ref ConcurrentBag<GeneralMessage> messages)
        {
            bool isNullValue = value == null;
            string strValue = value?.ToString();
            // Is required?
            if (fieldDefinition.FieldDefinitionUi.IsRequired && (isNullValue || string.IsNullOrWhiteSpace(strValue)))
            {
                messages.Add(new GeneralMessage()
                {
                    Code = requiredCode,
                    MessageType = EMessageType.Error,
                    Message = fieldDefinition.FieldDefinitionUi.RequiredErrorMsg ?? defaultRequiredErrorMsg.FormatQuery(dictionary),
                    Source = fieldDefinition.ItemName,
                });
            }

            if (!isNullValue)
            {
                // Regular expression.
                if (!string.IsNullOrWhiteSpace(fieldDefinition.FieldDefinitionUi.Regex) && !Regex.IsMatch(strValue, fieldDefinition.FieldDefinitionUi.Regex))
                {
                    messages.Add(new GeneralMessage()
                    {
                        Code = regexCode,
                        MessageType = EMessageType.Error,
                        Message = fieldDefinition.FieldDefinitionUi.RegexErrorMsg ?? defaultRegexErrorMsg.FormatQuery(dictionary),
                        Source = fieldDefinition.ItemName,
                    });
                }

                // When the MinLength and MaxLength are set for this field.
                if (fieldDefinition.FieldDefinitionUi.MinLength != null && fieldDefinition.FieldDefinitionUi.MinLength > 0 && fieldDefinition.FieldDefinitionUi.MaxLength != null && fieldDefinition.FieldDefinitionUi.MinLength > 0)
                {
                    if (strValue.Length < fieldDefinition.FieldDefinitionUi.MinLength.Value || strValue.Length > fieldDefinition.FieldDefinitionUi.MaxLength.Value)
                    {
                        messages.Add(new GeneralMessage()
                        {
                            Code = betweenCode,
                            MessageType = EMessageType.Error,
                            Message = defaultBetweenErrorMsg.FormatQuery(dictionary),
                            Source = fieldDefinition.ItemName,
                        });
                    }
                }
                // Only MinLength is set.
                else if ((fieldDefinition.FieldDefinitionUi.MinLength != null && fieldDefinition.FieldDefinitionUi.MinLength > 0) && (fieldDefinition.FieldDefinitionUi.MaxLength == null || fieldDefinition.FieldDefinitionUi.MinLength <= 0))
                {
                    if (strValue.Length < fieldDefinition.FieldDefinitionUi.MinLength.Value)
                    {
                        messages.Add(new GeneralMessage()
                        {
                            Code = minCode,
                            MessageType = EMessageType.Error,
                            Message = fieldDefinition.FieldDefinitionUi.MinLengthErrorMsg ?? defaultMinCodeErrorMsg.FormatQuery(dictionary),
                            Source = fieldDefinition.ItemName,
                        });
                    }
                }
                // Only MaxLength is set.
                else if ((fieldDefinition.FieldDefinitionUi.MinLength == null || fieldDefinition.FieldDefinitionUi.MinLength <= 0) && (fieldDefinition.FieldDefinitionUi.MaxLength != null && fieldDefinition.FieldDefinitionUi.MinLength > 0))
                {
                    if (strValue.Length > fieldDefinition.FieldDefinitionUi.MaxLength.Value)
                    {
                        messages.Add(new GeneralMessage()
                        {
                            Code = maxCode,
                            MessageType = EMessageType.Error,
                            Message = fieldDefinition.FieldDefinitionUi.MaxLengthErrorMsg ?? defaultMaxErrorMsg.FormatQuery(dictionary),
                            Source = fieldDefinition.ItemName,
                        });
                    }
                }

                ValidateRangeValue(dictionary, value, fieldDefinition, ref messages);
            }
        }

        #region Sub-Validations
        /// <summary>
        /// Validates the format and range from the specified value. specified value.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="value"></param>
        /// <param name="fieldDefinition"></param>
        /// <param name="messages"></param>
        private static void ValidateRangeValue(IDictionary<string, object> dictionary, object value, FieldDefinition fieldDefinition, ref ConcurrentBag<GeneralMessage> messages)
        {
            // Only validates the selected field types.
            if (fieldDefinition.FieldType == EFieldType.Date || fieldDefinition.FieldType == EFieldType.DateTime || fieldDefinition.FieldType == EFieldType.Decimal || fieldDefinition.FieldType == EFieldType.Time || fieldDefinition.FieldType == EFieldType.Integer)
            {
                // The minimum value.
                ValueComparer min;
                // The maximun value.
                ValueComparer max;
                // The value.
                ValueComparer val;

                // Checks if is a valid value based on the field type.
                switch (fieldDefinition.FieldType)
                {
                    case EFieldType.Date:
                    case EFieldType.DateTime:
                        max = new ValueComparer(EntityHelper.GetConvertedValue(fieldDefinition.FieldDefinitionUi.RangeMax, typeof(DateTime?)), fieldDefinition.FieldType);
                        min = new ValueComparer(EntityHelper.GetConvertedValue(fieldDefinition.FieldDefinitionUi.RangeMin, typeof(DateTime?)), fieldDefinition.FieldType);
                        val = new ValueComparer(EntityHelper.GetConvertedValue(value, typeof(DateTime?)), fieldDefinition.FieldType);
                        break;
                    case EFieldType.Decimal:
                        max = new ValueComparer(EntityHelper.GetConvertedValue(fieldDefinition.FieldDefinitionUi.RangeMax, typeof(decimal?)), fieldDefinition.FieldType);
                        min = new ValueComparer(EntityHelper.GetConvertedValue(fieldDefinition.FieldDefinitionUi.RangeMin, typeof(decimal?)), fieldDefinition.FieldType);
                        val = new ValueComparer(EntityHelper.GetConvertedValue(value, typeof(decimal?)), fieldDefinition.FieldType);
                        break;
                    case EFieldType.Time:
                        max = new ValueComparer(EntityHelper.GetConvertedValue(fieldDefinition.FieldDefinitionUi.RangeMax, typeof(TimeSpan?)), fieldDefinition.FieldType);
                        min = new ValueComparer(EntityHelper.GetConvertedValue(fieldDefinition.FieldDefinitionUi.RangeMin, typeof(TimeSpan?)), fieldDefinition.FieldType);
                        val = new ValueComparer(EntityHelper.GetConvertedValue(value, typeof(TimeSpan?)), fieldDefinition.FieldType);
                        break;
                    default:
                        max = new ValueComparer(EntityHelper.GetConvertedValue(fieldDefinition.FieldDefinitionUi.RangeMax, typeof(long?)), fieldDefinition.FieldType);
                        min = new ValueComparer(EntityHelper.GetConvertedValue(fieldDefinition.FieldDefinitionUi.RangeMin, typeof(long?)), fieldDefinition.FieldType);
                        val = new ValueComparer(EntityHelper.GetConvertedValue(value, typeof(long?)), fieldDefinition.FieldType);
                        break;
                }


                // Creates a new dictionary to avoid conflicts with the actual.
                var d = new Dictionary<string, object>(dictionary)
                    .AddOrUpdate("Value", value)
                    .AddOrUpdate("MinRange", min.GetFormattedValue(fieldDefinition.FieldDefinitionUi.DisplayFormat))
                    .AddOrUpdate("MaxRange", max.GetFormattedValue(fieldDefinition.FieldDefinitionUi.DisplayFormat));

                // If has no value then it means the format is incorrect.
                if (val.HasValue)
                {
                    // There is a value specified for min and max.
                    if (min.HasValue && max.HasValue && (val < min || val > max))
                    {
                        messages.Add(new GeneralMessage()
                        {
                            Code = rangeCode,
                            MessageType = EMessageType.Error,
                            Message = fieldDefinition.FieldDefinitionUi.RangeErrorMsg?.FormatQuery(d) ?? defaultRangeErrorMsg.FormatQuery(d),
                            Source = fieldDefinition.ItemName,
                        });
                    }
                    // Only max has value.
                    else if (max.HasValue && val > max)
                    {
                        messages.Add(new GeneralMessage()
                        {
                            Code = rangeCode,
                            MessageType = EMessageType.Error,
                            Message = fieldDefinition.FieldDefinitionUi.RangeErrorMsg?.FormatQuery(d) ?? defaultMaxRangeErrorMsg.FormatQuery(d),
                            Source = fieldDefinition.ItemName,
                        });
                    }
                    // Only min has a value.
                    else if (min.HasValue && val < min)
                    {
                        messages.Add(new GeneralMessage()
                        {
                            Code = rangeCode,
                            MessageType = EMessageType.Error,
                            Message = fieldDefinition.FieldDefinitionUi.RangeErrorMsg?.FormatQuery(d) ?? defaultMinRangeErrorMsg.FormatQuery(d),
                            Source = fieldDefinition.ItemName,
                        });
                    }
                }
                else
                {
                    // BAd format error.
                    messages.Add(new GeneralMessage()
                    {
                        Code = notValidFormatCode,
                        MessageType = EMessageType.Error,
                        Message = fieldDefinition.FieldDefinitionUi.FormatErrorMsg?.FormatQuery(d) ?? defaultMinRangeErrorMsg.FormatQuery(d),
                        Source = fieldDefinition.ItemName,
                    });
                }
            }

        }
        #endregion

        private static IDictionary<string, object> AddOrUpdate(this IDictionary<string, object> dictionary, string key, object value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
            else
                dictionary[key] = value;

            return dictionary;
        }
        #endregion
    }
}
