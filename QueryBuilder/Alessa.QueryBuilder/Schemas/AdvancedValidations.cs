using Alessa.Core.Entities.Results;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace Alessa.QueryBuilder.Schemas
{
    /// <summary>
    /// The advanced validations class
    /// </summary>
    internal class AdvancedValidations
    {
        /// <summary>
        /// Gets the advanced validations.
        /// </summary>
        /// <param name="parameters">Prameters.</param>
        /// <param name="fieldDefinitions">The field definition collection.</param>
        /// <param name="schemaContext">The schema context.</param>
        /// <returns></returns>
        internal static List<GeneralMessage> GetValidations(SaveParameters parameters, IEnumerable<FieldDefinition> fieldDefinitions, SchemaContext schemaContext)
        {
            var result = new ConcurrentBag<GeneralMessage>();

            return result.ToList();
        }
    }
}
