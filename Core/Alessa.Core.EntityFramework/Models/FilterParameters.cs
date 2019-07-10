using Alessa.Core.Entities.QueryModels;
using Alessa.Core.Helpers;
using System.Linq;

namespace Alessa.Core.EntityFramework.Models
{
    /// <summary>
    /// Entity class that contains a string and object parameters.
    /// </summary>
    internal class FilterParameters
    {
        /// <summary>
        /// Gets or sets the filter expression.
        /// </summary>
        internal string FilterExpression { get; set; }
        /// <summary>
        /// Gets or sets the object parameters to use in the filter expression.
        /// </summary>
        internal object[] Parameters { get; set; }

        /// <summary>
        /// Gets the object to user with a String and parameters collection.
        /// </summary>
        /// <param name="filters">Entity to get the parameters.</param>
        /// <typeparam name="E">Entity type.</typeparam>
        /// <returns></returns>
        internal static FilterParameters GetFilterExpression<E>(QueryFilterCollection filters)
        {
            // Object array for parameters.
            var objects = new object[0];
            // Filter statements.
            System.Text.StringBuilder filterExpressionBuilder = new System.Text.StringBuilder();

            if (filters != null && filters.QueryFilters != null)
            {
                // Gets the properties contained in the entity and match with the filter statement in requets.
                var properties = (from property in typeof(E).GetProperties()
                                  join filter in filters.QueryFilters on property.Name equals filter.FieldName
                                  select new
                                  {
                                      Name = property.Name,
                                      Type = property.PropertyType,
                                      DeclaredValue = filter.SearchingValue,
                                      Operator = filter.SearchingOperator
                                  }).ToArray();

                // Looks for the whole array.
                for (int i = 0; i < properties.Length; i++)
                {
                    // Gets the value from the property in the filter statement.
                    var value = EntityHelper.GetConvertedValue(properties[i].DeclaredValue, properties[i].Type);
                    // Appends the string statement.
                    filterExpressionBuilder.Append(EntityHelper.GetFilterStatement(properties[i].Name, properties[i].Operator, value, i));
                    if (i + 1 < properties.Length)
                        filterExpressionBuilder.AppendFormat(" {0} ", filters.GroupingOperator.ToString().ToUpper());
                    // Changes the array size and adds the new value.
                    System.Array.Resize(ref objects, objects.Length + 1);
                    objects[objects.Length - 1] = value;
                }
            }
            var result = new FilterParameters()
            {
                FilterExpression = filterExpressionBuilder.ToString(),
                Parameters = objects
            };
            return result;
        }
    }
}
