using Alessa.ALex;
using Alessa.Core.Entities.Results;
using Alessa.QueryBuilder.Entities.BuilderParameters;
using Alessa.QueryBuilder.Entities.Configuration;
using Alessa.QueryBuilder.Entities.Data;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;
using System.Linq;
using System.Threading.Tasks;

namespace Alessa.QueryBuilder
{
    /// <summary>
    /// The data shcema for retriving the configurations.
    /// </summary>
    public class SchemaConfigurations : SchemaBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SchemaConfigurations"/> class.
        /// </summary>
        /// <param name="schemaContext">The schema context for this object.</param>
        public SchemaConfigurations(SchemaContext schemaContext) : base(schemaContext)
        {
        }

        /// <summary>
        /// Gets the table edit detail definition configuration based in the parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="ALexException">Used for displaying a message to the user.</exception>
        /// <exception cref="QueryBuilderException">Thrown with a code number.</exception>
        public virtual async Task<GeneralResult<EditConfigViewModel>> GetEditConfigAsync(IBuilderParameters parameters)
        {
            GeneralResult<EditConfigViewModel> result;

            try
            {
                var fieldGroups = this.GetGroups(parameters);

                var groups = from gr in fieldGroups
                             select new EditGroupViewModel()
                             {
                                 DisplayName = gr.DisplayName,
                                 GroupType = gr.GroupType,
                                 GroupWidth = gr.GroupWidth,
                                 ItemName = gr.ItemName,
                                 GroupDetails = (from grd in gr.FieldGroupDetails
                                                 select new EditGroupDetailViewModel()
                                                 {
                                                     DisplayName = grd.DisplayName,
                                                     ItemName = grd.ItemName,
                                                     GroupType = grd.GroupType,
                                                     DisplayOrder = grd.DisplayOrder,
                                                     GroupWidth = grd.GroupWidth,
                                                     Fields = (from f in grd.FieldDefinitionUis
                                                               select new EditFieldConfigViewModel()
                                                               {
                                                                   ItemName = f.FieldDefinition.ItemName,
                                                                   AllowEditInDetail = f.AllowEditInDetail,
                                                                   DisplayFormat = f.DisplayFormat,
                                                                   DisplayOrder = f.DisplayOrder,
                                                                   DisplayType = f.DisplayType,
                                                                   IsHidden = f.IsHidden,
                                                                   IsKey = f.FieldDefinition.IsKey,
                                                                   IsReadOnly = f.IsReadOnly,
                                                                   DisplayName = f.DisplayName,
                                                                   EditWidth = f.EditWidth,
                                                                   IsRequired = f.IsRequired,
                                                                   MaxLength = f.MaxLength,
                                                                   MinLength = f.MinLength,
                                                                   Regex = f.Regex,
                                                                   FieldType = f.FieldDefinition.FieldType,
                                                                   SourceList = (from s in f.FieldDefinition.FieldListSources
                                                                                 select s.RequiredFieldDefinition.ItemName).ToList()
                                                               }).ToList()
                                                 }).ToList()
                             };

                var fieldDefinitions = base.GetFieldDefinitions(parameters);

                var validations = from val in base.Context.QueryBuilderDbContext.TableFieldValidations
                                  where val.ValidateOnClient && val.IsEnabled // && val.ExecutionSource.ExecutionType == Entities.EExecutionType.JavaScript
                                  && (from f in fieldDefinitions
                                      select f.FieldDefinitionId).Contains(val.ChangeFieldDefinitionId)
                                  select val;

                var dataValidations = from val in validations
                                      group val by val.ExecutionSourceId into val
                                      select new DataValidation()
                                      {
                                          ExecuteOnClientSide = val.Max(e => e.ValidateOnClient),
                                          ExecutionResultType = val.Max(e => e.ExecutionResultType),
                                          Statement = val.Max(e => e.ExecutionSource.ExecutionText),
                                          AdditionalParameters = val.Max(e => e.ExecutionSource.AdditionalParameters),
                                          Fields = (from v in validations
                                                    select v.ChangeFieldDefinition.ItemName).ToList()
                                      };

                var table = await (from t in base.GetTableDefinitions(parameters)
                                   select new EditConfigViewModel()
                                   {
                                       AllowExport = t.TableDefinitionUi.AllowExport,
                                       DetailFormat = t.TableDefinitionUi.DetailFormat,
                                       DisplayName = t.TableDefinitionUi.DisplayName,
                                       ItemName = t.ItemName,
                                   }
                    ).FirstOrDefaultAsync();


                table.Validations = await dataValidations.ToListAsync();
                table.Groups = await groups.ToListAsync();

                result = new GeneralResult<EditConfigViewModel>()
                {
                    Result = table
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<GeneralResult<EditConfigViewModel>>(ex, 2001);
            }

            return result;
        }

        /// <summary>
        /// Gets the table list detail definition configuration based in the parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="ALexException">Used for displaying a message to the user.</exception>
        /// <exception cref="QueryBuilderException">Thrown with a code number.</exception>
        public virtual async Task<GeneralResult<DetailListConfigViewModel>> GetDetailListConfigAsync(IBuilderParameters parameters)
        {
            GeneralResult<DetailListConfigViewModel> result;
            try
            {

                var r = await (from td in base.GetTableDefinitions(parameters)
                               select new DetailListConfigViewModel()
                               {
                                   DetailFormat = td.TableDefinitionUi.DetailFormat,
                                   DisplayName = td.TableDefinitionUi.DisplayName,
                                   ItemName = td.ItemName,
                               }).FirstOrDefaultAsync();


                r.Groups = await (from f in base.GetFieldDefinitions(parameters)
                                  select new FieldConfigViewModel()
                                  {
                                      DisplayFormat = f.FieldDefinitionUi.DisplayFormat,
                                      ItemName = f.ItemName
                                  }).ToListAsync();

                result = new GeneralResult<DetailListConfigViewModel>()
                {
                    Result = r
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<GeneralResult<DetailListConfigViewModel>>(ex, 2002);
            }

            return result;
        }

        /// <summary>
        /// Gets the table definition configuration based in the parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="ALexException">Used for displaying a message to the user.</exception>
        /// <exception cref="QueryBuilderException">Thrown with a code number.</exception>
        public async virtual Task<GeneralResult<GridConfigViewModel>> GetGridConfigAsync(IBuilderParameters parameters)
        {
            GeneralResult<GridConfigViewModel> result;
            try
            {
                var fieldGroups = this.GetGroups(parameters);

                var groups = from fg in fieldGroups
                             from det in fg.FieldGroupDetails
                             select new GridGroupViewModel()
                             {
                                 DisplayName = fg.DisplayName,
                                 ItemName = fg.ItemName,
                                 Fields = (from f in det.FieldDefinitionUis
                                           select new GridFieldConfigViewModel()
                                           {
                                               ItemName = f.FieldDefinition.ItemName,
                                               AllowFilter = f.AllowFilter,
                                               AllowSort = f.AllowSort,
                                               GridWidth = f.GridWidth,
                                               DisplayFormat = f.DisplayFormat,
                                               DisplayOrder = f.DisplayOrder,
                                               DisplayType = f.DisplayType,
                                               IsHidden = f.IsHidden,
                                               IsKey = f.FieldDefinition.IsKey,
                                               IsReadOnly = f.IsReadOnly,
                                               DisplayName = f.DisplayName,
                                           }).ToList()
                             };

                var table = await (from t in base.GetTableDefinitions(parameters)
                                   select new GridConfigViewModel()
                                   {
                                       AllowExport = t.TableDefinitionUi.AllowExport,
                                       DisplayName = t.TableDefinitionUi.DisplayName,
                                       ItemName = t.ItemName,
                                   }
                    ).FirstOrDefaultAsync();

                table.Groups = await groups.ToListAsync();

                result = new GeneralResult<GridConfigViewModel>()
                {
                    Result = table
                };
            }
            catch (ALexException ex)
            {
                result = base.GetResultFromException<GeneralResult<GridConfigViewModel>>(ex, 2003);
            }

            return result;

        }

        private IQueryable<FieldGroup> GetGroups(IBuilderParameters parameters)
        {
            var fields = base.GetFieldDefinitions(parameters);

            var groups = from gr in base.Context.QueryBuilderDbContext.FieldGroups
                         where (from f in fields
                                select f.FieldDefinitionUi.FieldGroupDetail.FieldGroupId)
                                    .Distinct()
                                    .Contains(gr.FieldGroupId)
                         select gr;
            return groups;
        }
    }
}
