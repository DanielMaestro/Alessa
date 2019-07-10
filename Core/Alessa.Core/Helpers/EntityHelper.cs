using Alessa.Core.Entities.QueryModels;
using System;
using System.Linq;

namespace Alessa.Core.Helpers
{
    /// <summary>
    /// Entity helper class.
    /// </summary>
    public partial class EntityHelper
    {

        /// <summary>
        /// Gets a list of primitive types.
        /// </summary>
        public static System.Collections.Generic.IEnumerable<Type> PrimitiveTypes
        {
            get
            {
                Type[] allowedTypes =
                {
                    typeof(string),
                    typeof(bool),
                    typeof(bool?),
                    typeof(int),
                    typeof(int?),
                    typeof(long),
                    typeof(long?),
                    typeof(double),
                    typeof(double?),
                    typeof(decimal),
                    typeof(decimal?),
                    typeof(float),
                    typeof(float?),
                    typeof(Guid),
                    typeof(Guid?),
                    typeof(byte[]),
                    typeof(byte),
                    typeof(DateTime),
                    typeof(DateTime?),
                    typeof(short),
                    typeof(short?),
                    typeof(TimeSpan),
                    typeof(TimeSpan?),
                };

                return allowedTypes;
            }
        }
        /// <summary>
        /// Gets a list of not allowed types.
        /// </summary>
        public static System.Collections.Generic.IEnumerable<Type> NotAllowedTypes
        {
            get
            {
                Type[] notAllowedTypes =
                {
                    typeof(Enumerable),
                };

                return notAllowedTypes;
            }
        }

        /// <summary>
        /// Gets the property list.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <returns></returns>
        public static System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> GetProperties<T>()
        //where T : class, new()
        {
            var properties = GetProperties(typeof(T));

            return properties;
        }

        /// <summary>
        /// Gets the property list.
        /// </summary>
        /// <param name="type">Entity</param>
        /// <returns></returns>
        public static System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> GetProperties(Type type)
        {
            if (type == null)
                throw new ArgumentException("The parameter 'type' must not be null.", "type");

            var properties = from property in type.GetProperties()
                             join propertyType in PrimitiveTypes on property.PropertyType equals propertyType
                             from notAllowed in NotAllowedTypes
                             where property.PropertyType != notAllowed && property.CanRead
                             select property;

            return properties;
        }

        /// <summary>
        /// Gets the converted value.
        /// </summary>
        /// <param name="valueToConvert">Value to convert.</param>
        /// <param name="typeToConvert">Type to convert.</param>
        /// <returns></returns>
        public static object GetConvertedValue(object valueToConvert, Type typeToConvert)
        {
            object result = null;

            if ((typeToConvert == typeof(Guid) || typeToConvert == typeof(Guid?)))
            {

                if (!string.IsNullOrWhiteSpace(string.Format("{0}", valueToConvert)) && "_empty" != valueToConvert.ToString())
                    result = new Guid(valueToConvert.ToString());
            }
            else if (typeToConvert == typeof(bool) || typeToConvert == typeof(bool?))
            {
                string text = valueToConvert != null ? valueToConvert.ToString().ToLower() : null;
                bool? flag = null;
                if (!string.IsNullOrWhiteSpace(text))
                {
                    if (text.Equals("yes") || text.Equals("on") || text.Equals("true") || text.Equals("1"))
                        flag = true;
                    else if (text.Equals("no") || text.Equals("off") || text.Equals("false") || text.Equals("0"))
                        flag = false;
                }

                result = flag == null ? null : new bool?(flag.Value);
            }
            else if (typeToConvert == typeof(DateTime) || typeToConvert == typeof(DateTime?))
            {
                if (valueToConvert != null && !string.IsNullOrWhiteSpace(valueToConvert.ToString()))
                {
                    result = System.Convert.ToDateTime(valueToConvert.ToString());
                }
                else
                {
                    result = default(DateTime);
                }
            }
            else if (typeToConvert == typeof(decimal) || typeToConvert == typeof(decimal?))
            {
                if (valueToConvert != null && !string.IsNullOrWhiteSpace(valueToConvert.ToString()))
                {
                    result = System.Convert.ToDecimal(System.Convert.ToDouble(valueToConvert.ToString()));
                }
                else
                {
                    if (typeToConvert != typeof(decimal?))
                    {
                        result = default(decimal);
                    }
                }
            }
            else if (typeToConvert == typeof(int) || typeToConvert == typeof(int?))
            {
                if (valueToConvert != null && !string.IsNullOrWhiteSpace(valueToConvert.ToString()))
                {
                    result = System.Convert.ToInt32(valueToConvert.ToString());
                }
                else
                {
                    if (typeToConvert != typeof(int?))
                    {
                        result = default(int);
                    }
                }
            }
            else if (typeToConvert == typeof(double) || typeToConvert == typeof(double?))
            {
                if (valueToConvert != null && !string.IsNullOrWhiteSpace(valueToConvert.ToString()))
                {
                    result = System.Convert.ToDouble(valueToConvert.ToString());
                }
                else
                {
                    if (typeToConvert != typeof(double?))
                    {
                        result = default(double);
                    }
                }
            }
            else if (typeToConvert == typeof(long) || typeToConvert == typeof(long?))
            {
                if (valueToConvert != null && !string.IsNullOrWhiteSpace(valueToConvert.ToString()))
                {
                    result = System.Convert.ToInt64(valueToConvert.ToString());
                }
                else
                {
                    if (typeToConvert != typeof(long?))
                    {
                        result = default(long);
                    }
                }
            }
            else if (typeToConvert == typeof(float) || typeToConvert == typeof(float?))
            {
                if (valueToConvert != null && !string.IsNullOrWhiteSpace(valueToConvert.ToString()))
                {
                    result = System.Convert.ToInt64(valueToConvert.ToString());
                }
                else
                {
                    if (typeToConvert != typeof(float?))
                    {
                        result = default(float);
                    }
                }
            }
            else if (typeToConvert == typeof(short) || typeToConvert == typeof(short?))
            {
                if (valueToConvert != null && !string.IsNullOrWhiteSpace(valueToConvert.ToString()))
                {
                    result = System.Convert.ToInt16(valueToConvert.ToString());
                }
                else
                {
                    if (typeToConvert != typeof(short?))
                    {
                        result = default(short);
                    }
                }
            }
            else if (typeToConvert == typeof(string))
            {
                if (valueToConvert != null && !string.IsNullOrWhiteSpace(valueToConvert.ToString()))
                    result = System.Convert.ChangeType(valueToConvert, typeToConvert);
            }

            return result;
        }
        /// <summary>
        /// Copies the entity values into the target. This is useful when updating entities.
        /// </summary>
        /// <param name="source">Source.</param>
        /// <param name="target">Target.</param>
        /// <param name="propertySet">If this value is specified, the it excludes the properties with the names contained in this collection.</param>
        /// <param name="includeOrExclude">Indicates if the <paramref name="propertySet"/> collection must be included (true) or excluded (false) when the properties are set.</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task CopyEntityValuesAsync(object source, object target, System.Collections.Generic.IEnumerable<string> propertySet = null, bool includeOrExclude = false)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                var properties = from p1 in GetProperties(source.GetType())
                                 join p2 in GetProperties(target.GetType()) on new { p1.Name, p1.PropertyType } equals new { p2.Name, p2.PropertyType }
                                 select new
                                 {
                                     Source = p1,
                                     Target = p2
                                 };

                if (propertySet != null)
                {
                    properties = from property in properties
                                 from name in propertySet
                                 where includeOrExclude ? property.Source.Name.Equals(name) : !property.Source.Name.Equals(name)
                                 select property;

                }

                System.Threading.Tasks.Parallel.ForEach(properties, (e) =>
                {
                    var sValue = e.Source.GetValue(source);
                    e.Target.SetValue(target, sValue);
                });
            });
        }

        /// <summary>
        /// Gets the filter statement adding an index as parameter
        /// </summary>
        /// <param name="searchingName">Searching name or field name.</param>
        /// <param name="searchingOperator">Operator.</param>
        /// <param name="parameterIndex">Parameter index.</param>
        /// <param name="searchingValue">Value to find.</param>
        /// <returns></returns>
        public static string GetFilterStatement(string searchingName, EFilterOperator searchingOperator, object searchingValue, int parameterIndex)
        {
            string searchingOperatorString = string.Empty;
            string result = null;
            switch (searchingOperator)
            {
                case EFilterOperator.Equals:
                    result = string.Format("{0} = @{1}", searchingName, parameterIndex);
                    break;
                case EFilterOperator.NotEquals:
                    result = string.Format("{0} != @{1}", searchingName, parameterIndex);
                    break;
                case EFilterOperator.LessThan:
                    result = string.Format("{0} < @{1}", searchingName, parameterIndex);
                    break;
                case EFilterOperator.LessOrEquals:
                    result = string.Format("{0} <= @{1}", searchingName, parameterIndex);
                    break;
                case EFilterOperator.GreaterThan:
                    result = string.Format("{0} > @{1}", searchingName, parameterIndex);
                    break;
                case EFilterOperator.GreaterOrEquals:
                    result = string.Format("{0} >= @{1}", searchingName, parameterIndex);
                    break;
                case EFilterOperator.BeginsWith:
                    result = string.Format("{0}.StartsWith(\"{1}\")", searchingName, searchingValue);
                    break;
                case EFilterOperator.NotBeginsWith:
                    result = string.Format("!{0}.StartsWith(\"{1}\")", searchingName, searchingValue);
                    break;
                case EFilterOperator.EndsWith:
                    result = string.Format("{0}.EndsWith(\"{1}\")", searchingName, searchingValue);
                    break;
                case EFilterOperator.NotEndsWith:
                    result = string.Format("!{0}.EndsWith(\"{1}\")", searchingName, searchingValue);
                    break;
                case EFilterOperator.Contains:
                    result = string.Format("{0}.Contains(\"{1}\")", searchingName, searchingValue);
                    //result = System.String.Format("{0} = \"@{1}\"", searchingName, searchingValue);
                    break;
                case EFilterOperator.NotContains:
                    result = string.Format("!{0}.Contains(\"{1}\")", searchingName, parameterIndex);
                    break;
                default:
                    result = string.Format("{0} = @{1}", searchingName, parameterIndex);
                    break;
            }


            return result;
        }
        /// <summary>
        /// Gets an SQL server operator.
        /// </summary>
        /// <param name="searchingOperator">Search operator.</param>
        /// <returns></returns>
        private static string GetSqlOperatorString(EFilterOperator searchingOperator)
        {
            string result;
            switch (searchingOperator)
            {
                case EFilterOperator.Equals:
                default:
                    result = "=";
                    break;
                case EFilterOperator.NotEquals:
                    result = "<>";
                    break;
                case EFilterOperator.LessThan:
                    result = "<";
                    break;
                case EFilterOperator.LessOrEquals:
                    result = "<=";
                    break;
                case EFilterOperator.GreaterThan:
                    result = ">";
                    break;
                case EFilterOperator.GreaterOrEquals:
                    result = ">=";
                    break;
                case EFilterOperator.BeginsWith:
                case EFilterOperator.EndsWith:
                case EFilterOperator.Contains:
                    result = "LIKE";
                    break;
                case EFilterOperator.NotBeginsWith:
                case EFilterOperator.NotEndsWith:
                case EFilterOperator.NotContains:
                    result = "NOT LIKE";
                    break;
                case EFilterOperator.In:
                    result = "IN";
                    break;
                case EFilterOperator.NotIn:
                    result = "NOT IN";
                    break;
                case EFilterOperator.IsNull:
                    result = "IS";
                    break;
                case EFilterOperator.IsNotNull:
                    result = "IS NOT";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Gets the sql operation string.
        /// </summary>
        /// <param name="searchingOperator">The operation.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value to filter. It can be a <see cref="System.Collections.Generic.IEnumerable{T}"/>.</param>
        /// <returns></returns>
        public static string GetSqlOperatorString(EFilterOperator searchingOperator, string field, object value)
        {
            string result = field + " " + GetSqlOperatorString(searchingOperator);

            switch (searchingOperator)
            {
                case EFilterOperator.In:
                case EFilterOperator.NotIn:
                    result += " (" + GetCollection(value) + ")";
                    break;
                case EFilterOperator.BeginsWith:
                case EFilterOperator.NotBeginsWith:
                    result += " '" + value.ToString() + "%'";
                    break;
                case EFilterOperator.EndsWith:
                case EFilterOperator.NotEndsWith:
                    result += " '%" + value.ToString() + "'";
                    break;
                case EFilterOperator.Contains:
                case EFilterOperator.NotContains:
                    result += " '%" + value.ToString() + "%'";
                    break;
                case EFilterOperator.IsNull:
                case EFilterOperator.IsNotNull:
                    result = " NULL";
                    break;
                default:
                    result += " " + value.ToString();
                    break;
            }

            return result;
        }

        private static string GetCollection(object value)
        {
            string result = string.Empty;

            if (value != null)
            {
                var method = value.GetType().GetMethod("GetEnumerator");
                System.Collections.IEnumerator enumerator = method?.Invoke(value, null) as System.Collections.IEnumerator;

                if (enumerator != null)
                {
                    var next = enumerator.MoveNext();
                    do
                    {
                        if (next)
                            result += enumerator.Current?.ToString() ?? "NULL";

                        next = enumerator.MoveNext();
                        if (next)
                            result += ",";
                    }
                    while (next);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the key properties from the specified <see cref="System.Type"/>.
        /// </summary>
        /// <param name="type">Type of entity.</param>
        /// <returns></returns>
        public static System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> GetKeyProperties(System.Type type)
        {
            var properties = GetProperties(type);
            var keyProperties = from property in properties
                                from att in property.CustomAttributes
                                where att.AttributeType.Name == "System.ComponentModel.DataAnnotations.KeyAttribute"
                                select property;

            // No properties with Key attribute are set, now will find the first one with ID.
            if (keyProperties.Count() == 0)
            {
                // Select the one with the entity name and ends with ID.
                keyProperties = from property in properties
                                where property.Name.ToUpper() == type.Name.ToUpper() + "ID"
                                select property;

            }

            //// Well, no key property, then it  will search for the Mo
            //if (keyProperties.Count() == 0)
            //{
            //}

            return keyProperties;
        }
        /// <summary>
        /// Gets the key properties from the specified <typeparamref name="E"/> type.
        /// </summary>
        /// <typeparam name="E">Type of entity.</typeparam>
        /// <returns></returns>
        public static System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> GetKeyProperties<E>() //where E : class, new()
        {
            var result = GetKeyProperties(typeof(E));
            return result;
        }
    }
}
