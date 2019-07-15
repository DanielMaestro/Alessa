using Alessa.ALex;
using Alessa.Core.Entities.Results;
using Alessa.QueryBuilder.Entities;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using Alessa.QueryBuilder.Entities.Data;
using Alessa.QueryBuilder.Entities.Results;
using System.Linq;

namespace Alessa.QueryBuilder
{
    /// <summary>
    /// The base class for schema retrieving.
    /// </summary>
    public abstract class SchemaBase
    {
        /// <summary>
        /// Gets the schema context assotiated to this object.
        /// </summary>
        protected virtual SchemaContext Context { get; private set; }
        /// <summary>
        /// Initializes a new instance of <see cref="SchemaBase"/> class.
        /// </summary>
        /// <param name="schemaContext">The schema context for this object.</param>
        public SchemaBase(SchemaContext schemaContext)
        {
            this.Context = schemaContext;
        }

        /// <summary>
        /// Gets the fields assigned to the request.
        /// </summary>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>A queryable collection.</returns>
        /// <exception cref="ALexException">Used for displaying a message to the user.</exception>
        protected IQueryable<FieldDefinition> GetFieldDefinitions(IBuilderParameters parameters)
        {
            if (parameters.QueryType == EQueryType.Undefined)
                throw new ALexException("The query type is not set. You must specify the query type.", 100) { };

            var tableDef = this.GetTableDefinitions(parameters);

            var fields = from td in tableDef
                         from fd in td.FieldDefinitions
                         where fd.IsEnabled
                         select fd;

            switch (parameters.QueryType)
            {
                case EQueryType.DetailListView:
                    fields = fields.Where(e => e.FieldDefinitionUi.ShowInDetails);
                    break;
                case EQueryType.EditView:
                    fields = fields.Where(e => e.FieldDefinitionUi.ShowInEdit);
                    break;
                case EQueryType.GridView:
                    fields = fields.Where(e => e.FieldDefinitionUi.ShowInGrid);
                    break;
                default:
                    throw new ALexException("The query type is not set. You must specify the query type.", 101);
            }

            return fields;
        }

        /// <summary>
        /// Gets the table definition.
        /// </summary>
        /// <param name="parameters">The query parameters.</param>
        /// <returns></returns>
        /// <exception cref="ALexException">Used for displaying a message to the user.</exception>
        protected IQueryable<TableDefinition> GetTableDefinitions(IBuilderParameters parameters)
        {
            var result = from td in this.Context.QueryBuilderDbContext.TableDefinitions
                         where td.ItemName == parameters.ItemName && td.IsEnabled
                         select td;


            switch (parameters.QueryType)
            {
                case EQueryType.DetailListView:
                    result = result.Where(e => e.TableDefinitionUi.ShowInDetails);
                    break;
                case EQueryType.EditView:
                    result = result.Where(e => e.TableDefinitionUi.ShowInEdit);
                    break;
                case EQueryType.GridView:
                    result = result.Where(e => e.TableDefinitionUi.ShowInGrid);
                    break;
                default:
                    throw new ALexException("The query type is not set. You must specify the query type.", 102);
            }

            return result;
        }

        /// <summary>
        /// Gets a <see cref="DataResult"/> objec from the expection.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="methodCode">The method code where the exception was thrown.</param>
        /// <returns></returns>
        protected T GetResultFromException<T>(ALexException ex, int methodCode)
            where T : GeneralResult, new()
        {
            var result = new T();

            result.Messages.Add(new GeneralMessage()
            {
                Code = ex.HResult,
                MessageType = EMessageType.Error,
                Message = ex.Message,
            });
            result.HasError = true;
            result.Code = methodCode;
            return result;
        }
    }
}
