using System.Collections.Generic;
using System.Linq;

namespace Alessa.ALex
{
    /// <summary>
    /// Contains a set of utilities to handle the strings.
    /// </summary>
    public static class AlexQueryExtensions
    {
        private const string openBracets = "{{";
        private const string closeBracets = "}}";

        /// <summary>
        /// Formats the <paramref name="queryTemplate"/> string with the specified data. When the template contains a {{propertyName}} it is replaced with
        /// the <paramref name="data"/> property values.
        /// </summary>
        /// <param name="queryTemplate">The query template string.</param>
        /// <param name="data">The object with the property values to replace.</param>
        /// <returns>A string with the formatted query.</returns>
        public static string FormatQuery(this string queryTemplate, object data)
        {
            IDictionary<string, object> properties = GetDictionary(data);

            var result = FormatQuery(queryTemplate, properties);

            return result;
        }

        /// <summary>
        /// Formats the <paramref name="queryTemplate"/> string with the specified data. When the template contains a {{propertyName}} it is replaced with
        /// the <paramref name="properties"/> property values.
        /// </summary>
        /// <param name="queryTemplate">The query template string.</param>
        /// <param name="properties">The collection with the property values to replace.</param>
        /// <returns>A string with the formatted query.</returns>
        public static string FormatQuery(this string queryTemplate, IDictionary<string, object> properties)
        {
            // Searches for property references embeded in the query template.
            var collection = System.Text.RegularExpressions.Regex.Matches(queryTemplate, openBracets + "\\s{0,5}\\w{1,50}\\s{0,5}" + closeBracets);

            // Builds a new string builder.
            var builder = new System.Text.StringBuilder(queryTemplate);

            // Replaces the token names.
            if (properties != null)
            {
                for (int e = 0; e < collection.Count; e++)
                {
                    builder.Replace(collection[e].Value, properties[collection[e].Value.Replace(openBracets, string.Empty).Replace(closeBracets, string.Empty)]?.ToString());
                };
            }
            return builder.ToString();

        }

        /// <summary>
        /// Gets a <see cref="IDictionary{TKey, TValue}"/> from the specified object. It is useful whn calling the <see cref="FormatQuery(string, object)"/> method multiple times in one method.
        /// </summary>
        /// <typeparam name="T">The object type where the properties are get.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static IDictionary<string, object> GetDictionary<T>(this T obj)
            where T : class, new()
        {
            IDictionary<string, object> properties = null;
            // Converts the data properties in a dictionary.
            if (obj != null)
            {
                if (obj is IDictionary<string, object>)
                {
                    properties = new Dictionary<string, object>((obj as IDictionary<string, object>)/*.ToDictionary(e => openBracets + e.Key + closeBracets, e => e.Value)*/);
                }
                else
                {
                    properties = obj.GetType().GetProperties().ToDictionary(e => /*openBracets +*/ e.Name/* + closeBracets*/, e => e.GetValue(obj));
                }
            }

            return properties;
        }
    }
}
