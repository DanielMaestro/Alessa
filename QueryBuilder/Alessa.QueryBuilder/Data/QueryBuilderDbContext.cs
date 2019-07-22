using Alessa.QueryBuilder.Entities.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Alessa.QueryBuilder
{
    /// <summary>
    /// The dynamic connection data context.
    /// </summary>
    public class QueryBuilderDbContext : DbContext
    {
        #region Properties
        ///// <summary>
        ///// Gets or sets the <see cref="AppRole"/> as table.
        ///// </summary>
        //public DbSet<AppRole> AppRoles { get; set; }
        ///// <summary>
        ///// Gets or sets the <see cref="AppUser"/> as table.
        ///// </summary>
        //public DbSet<AppUser> AppUsers { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="ExecutionSource"/> as table.
        /// </summary>
        public DbSet<ExecutionSource> ExecutionSources { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="FieldDefinition"/> as table.
        /// </summary>
        public DbSet<FieldDefinition> FieldDefinitions { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="FieldDefinitionUi"/> as table.
        /// </summary>
        public DbSet<FieldDefinitionUi> FieldDefinitionUis { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="FieldGroup"/> as table.
        /// </summary>
        public DbSet<FieldGroup> FieldGroups { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="FieldGroupDetail"/> as table.
        /// </summary>
        public DbSet<FieldGroupDetail> FieldGroupDetails { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="FieldIncludeManySource"/> as table.
        /// </summary>
        public DbSet<FieldIncludeManySource> FieldIncludeManySources { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="FieldKeysRelationship"/> as table.
        /// </summary>
        public DbSet<FieldKeysRelationship> FieldKeysRelationships { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="FieldListSource"/> as table.
        /// </summary>
        public DbSet<FieldListSource> FieldListSources { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="TableAction"/> as table.
        /// </summary>
        public DbSet<TableAction> TableActions { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="TableConfiguration"/> as table.
        /// </summary>
        public DbSet<TableConfiguration> TableConfigurations { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="TableDefinition"/> as table.
        /// </summary>
        public DbSet<TableDefinition> TableDefinitions { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="TableDefinitionUi"/> as table.
        /// </summary>
        public DbSet<TableDefinitionUi> TableDefinitionUis { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="TableFieldValidation"/> as table.
        /// </summary>
        public DbSet<TableFieldValidation> TableFieldValidations { get; set; }

        /// <summary>
        /// Designed to store the schema.
        /// </summary>
        private string _Schema;
        /// <summary>
        /// The schema prefix.
        /// </summary>
        public string SchemaPrefix;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Microsoft.EntityFrameworkCore.DbContext class using the specified options.
        /// The Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)
        /// method will still be called to allow further configuration of the options.
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public QueryBuilderDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        #endregion

        /// <summary>
        /// Override this method to configure the database (and other options) to be used for this context. This method is called for each instance of the context that
        /// is created. The base implementation does nothing. In situations where an instance of Microsoft.EntityFrameworkCore.DbContextOptions may or may not have been
        /// passed to the constructor, you can use Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured to determine if the options have already been set,
        /// and skip some or all of the logic in Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder).
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions) typically define extension
        /// methods on this object that allow you to configure the context.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (optionsBuilder.Options.Extensions.Any(e => e.ToString().Contains("MySQL", System.StringComparison.OrdinalIgnoreCase)))
            {
                this._Schema = null;
                this.SchemaPrefix = string.Empty;
            }
            else
            {
                this._Schema = "Alessa";
                this.SchemaPrefix = this._Schema + ".";
            }

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types exposed in Microsoft.EntityFrameworkCore.DbSet`1
        /// properties on your derived context. The resulting model may be cached and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically define extension methods
        /// on this object that allow you to configure aspects of the model that are specific to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TableConfiguration>(table =>
            {

                table.SetTable("TableConfiguration", this._Schema)
                    .HasKey(f => f.TableConfigurationId);

                table.Property(f => f.ConnectionString).SetVarcharProperty(255);

                table
                    .HasMany(f => f.TableDefinitions)
                    .WithOne(f => f.TableConfiguration)
                    .HasForeignKey(f => f.TableConfigurationId);

                table
                    .HasMany(f => f.ExecutionSources)
                    .WithOne(f => f.TableConfiguration)
                    .HasForeignKey(f => f.TableConfigurationId);
            });

            modelBuilder.Entity<TableDefinition>(table =>
            {
                table.SetTable("TableDefinition", this._Schema)
                    .HasKey(f => f.TableDefinitionId);

                table.Property(f => f.TableName).SetVarcharProperty();
                table.Property(f => f.ItemName).SetVarcharProperty();
                table.Property(f => f.TableDefinitionType).SetEnumProperty();
                table.Property(f => f.IsEnabled).SetBitProperty();
                table.Property(f => f.TableConfigurationId).SetIntProperty();

                table
                    .HasOne(f => f.TableConfiguration)
                    .WithMany(f => f.TableDefinitions)
                    .HasForeignKey(f => f.TableConfigurationId);

                table
                    .HasOne(f => f.TableDefinitionUi)
                    .WithOne(f => f.TableDefinition);

                table
                    .HasMany(f => f.FieldDefinitions)
                    .WithOne(f => f.TableDefinition)
                    .HasForeignKey(f => f.TableDefinitionId);

                table
                    .HasMany(f => f.FieldListSources)
                    .WithOne(f => f.TableDefinition)
                    .HasForeignKey(f => f.TableDefinitionId);

            });

            modelBuilder.Entity<TableDefinitionUi>(table =>
            {
                table.SetTable("TableDefinitionUi", this._Schema)
                    .HasKey(f => f.TableDefinitionId);

                table.Property(f => f.ShowInEdit).SetBitProperty();
                table.Property(f => f.ShowInGrid).SetBitProperty();
                table.Property(f => f.ShowInDetails).SetBitProperty();
                table.Property(f => f.AllowExport).SetBitProperty();
                table.Property(f => f.AllowEditInDetail).SetBitProperty();
                table.Property(f => f.AllowSort).SetBitProperty();
                table.Property(f => f.AllowFilter).SetBitProperty();
                table.Property(f => f.IsReadOnly).SetBitProperty();
                table.Property(f => f.DetailFormat).SetVarcharProperty(255, false);

                table
                    .HasOne(f => f.TableDefinition)
                    .WithOne(f => f.TableDefinitionUi);
            });

            modelBuilder.Entity<TableAction>(table =>
            {
                table.SetTable("TableAction", this._Schema)
                    .HasKey(f => new { f.TableDefinitionId, f.ExecutionSourceId });

                table.Property(f => f.ExecutionSourceId).SetIntProperty();
                table.Property(f => f.TableDbEventType).SetEnumProperty();
                table.Property(f => f.ExecutionOrder).SetIntProperty();

                table
                    .HasOne(f => f.ExecutionSource)
                    .WithMany(f => f.TableActions)
                    .HasForeignKey(f => f.ExecutionSourceId)
                    .OnDelete(DeleteBehavior.Restrict);
                table
                    .HasOne(f => f.TableDefinition)
                    .WithMany(f => f.TableActions)
                    .HasForeignKey(f => f.TableDefinitionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FieldDefinition>(table =>
            {
                table.SetTable("FieldDefinition", this._Schema)
                    .HasKey(f => f.FieldDefinitionId);

                table.Property(f => f.TableDefinitionId).SetIntProperty();
                table.Property(f => f.ItemName).SetVarcharProperty();
                table.Property(f => f.IsKey).SetBitProperty();
                table.Property(f => f.FieldLength).SetIntProperty(false);
                table.Property(f => f.FieldType).SetEnumProperty();
                table.Property(f => f.IsEnabled).SetBitProperty();
                table.Property(f => f.IsIdentity).SetBitProperty();

                table
                    .HasOne(f => f.TableDefinition)
                    .WithMany(f => f.FieldDefinitions)
                    .HasForeignKey(f => f.TableDefinitionId);

                table
                    .HasOne(f => f.FieldDefinitionUi)
                    .WithOne(f => f.FieldDefinition);

                table
                    .HasMany(f => f.FieldListSources)
                    .WithOne(f => f.FieldDefinition)
                    .HasForeignKey(f => f.FieldDefinitionId);

                table
                   .HasMany(f => f.RequiredFieldListSources)
                   .WithOne(f => f.RequiredFieldDefinition)
                   .HasForeignKey(f => f.RequiredFieldDefinitionId);

                table
                   .HasMany(f => f.FieldIncludeManySources)
                   .WithOne(f => f.FieldDefinition)
                   .HasForeignKey(f => f.FieldDefinitionId);
            });

            modelBuilder.Entity<FieldDefinitionUi>(table =>
            {
                table.SetTable("FieldDefinitionUi", this._Schema)
                    .HasKey(f => f.FieldDefinitionId);

                table.Property(f => f.DisplayName).SetVarcharProperty(255);
                table.Property(f => f.DisplayOrder).SetIntProperty();
                table.Property(f => f.HelpText).SetVarcharProperty(1024, false);
                table.Property(f => f.DisplayType).SetEnumProperty();
                table.Property(f => f.ShowInEdit).SetBitProperty();
                table.Property(f => f.ShowInGrid).SetBitProperty();
                table.Property(f => f.ShowInDetails).SetBitProperty();
                table.Property(f => f.AllowExport).SetBitProperty();
                table.Property(f => f.AllowEditInDetail).SetBitProperty();
                table.Property(f => f.AllowSort).SetBitProperty();
                table.Property(f => f.AllowFilter).SetBitProperty();
                table.Property(f => f.IsReadOnly).SetBitProperty();
                table.Property(f => f.IsHidden).SetBitProperty();
                table.Property(f => f.IsRequired).SetBitProperty();
                table.Property(f => f.Regex).SetVarcharProperty(512, false);
                table.Property(f => f.MinLength).SetIntProperty(false);
                table.Property(f => f.MaxLength).SetIntProperty(false);
                table.Property(f => f.RangeMin).SetVarcharProperty(255, false);
                table.Property(f => f.RangeMax).SetVarcharProperty(255, false);
                table.Property(f => f.RequiredErrorMsg).SetVarcharProperty(255, false);
                table.Property(f => f.RegexErrorMsg).SetVarcharProperty(255, false);
                table.Property(f => f.MinLengthErrorMsg).SetVarcharProperty(255, false);
                table.Property(f => f.MaxLengthErrorMsg).SetVarcharProperty(255, false);
                table.Property(f => f.RangeErrorMsg).SetVarcharProperty(255, false);
                table.Property(f => f.FormatErrorMsg).SetVarcharProperty(255, false);
                table.Property(f => f.DisplayFormat).SetVarcharProperty(255, false);
                table.Property(f => f.GridWidth).SetIntProperty(false);
                table.Property(f => f.EditWidth).SetIntProperty(false);
                table.Property(f => f.FieldGroupDetailId).SetIntProperty(false);

                table
                    .HasOne(f => f.FieldDefinition)
                    .WithOne(f => f.FieldDefinitionUi);

                table
                  .HasOne(f => f.FieldGroupDetail)
                  .WithMany(f => f.FieldDefinitionUis)
                  .HasForeignKey(f => f.FieldGroupDetailId);
            });

            modelBuilder.Entity<FieldGroup>(table =>
            {
                table.SetTable("FieldGroup", this._Schema)
                    .HasKey(f => f.FieldGroupId);

                table.Property(f => f.ItemName).SetVarcharProperty(50);
                table.Property(f => f.DisplayName).SetVarcharProperty(255);
                table.Property(f => f.GroupType).SetEnumProperty();
                table.Property(f => f.IsEnabled).SetBitProperty();
                table.Property(f => f.GroupWidth).SetIntProperty();
                table.Property(f => f.IsReadOnly).SetBitProperty();

                table
                   .HasMany(f => f.FieldGroupDetails)
                   .WithOne(f => f.FieldGroup)
                   .HasForeignKey(f => f.FieldGroupId);
            });

            modelBuilder.Entity<FieldGroupDetail>(table =>
            {
                table.SetTable("FieldGroupDetail", this._Schema)
                    .HasKey(f => f.FieldGroupDetailId);

                table.Property(f => f.ItemName).SetVarcharProperty(50);
                table.Property(f => f.DisplayName).SetVarcharProperty();
                table.Property(f => f.GroupType).SetEnumProperty();
                table.Property(f => f.GroupWidth).SetIntProperty();
                table.Property(f => f.IsEnabled).SetBitProperty();
                table.Property(f => f.DisplayOrder).SetIntProperty();
                table.Property(f => f.IsReadOnly).SetBitProperty();

                table
                   .HasOne(f => f.FieldGroup)
                   .WithMany(f => f.FieldGroupDetails)
                   .HasForeignKey(f => f.FieldGroupId);

                table
                   .HasMany(f => f.FieldDefinitionUis)
                   .WithOne(f => f.FieldGroupDetail)
                   .HasForeignKey(f => f.FieldGroupDetailId);
            });

            modelBuilder.Entity<FieldListSource>(table =>
            {
                table.SetTable("FieldListSource", this._Schema)
                    .HasKey(f => new { f.FieldDefinitionId, f.ExecutionSourceId, f.RequiredFieldDefinitionId });

                table
                   .HasOne(f => f.FieldDefinition)
                   .WithMany(f => f.FieldListSources)
                   .HasForeignKey(f => f.FieldDefinitionId)
                   .OnDelete(DeleteBehavior.Restrict);

                table
                  .HasOne(f => f.RequiredFieldDefinition)
                  .WithMany(f => f.RequiredFieldListSources)
                  .HasForeignKey(f => f.RequiredFieldDefinitionId)
                  .OnDelete(DeleteBehavior.Restrict);

                table
                  .HasOne(f => f.ExecutionSource)
                  .WithMany(f => f.FieldListSources)
                  .HasForeignKey(f => f.ExecutionSourceId);

                table
                  .HasOne(f => f.TableDefinition)
                  .WithMany(f => f.FieldListSources)
                  .HasForeignKey(f => f.TableDefinitionId);
            });

            modelBuilder.Entity<FieldIncludeManySource>(table =>
            {
                table.SetTable("FieldIncludeManySource", this._Schema)
                    .HasKey(f => new { f.FieldDefinitionId, f.ExecutionSourceId });

                table.Property(f => f.ForeignKey).SetVarcharProperty();
                table.Property(f => f.LocalKey).SetVarcharProperty();

                table
                   .HasOne(f => f.FieldDefinition)
                   .WithMany(f => f.FieldIncludeManySources)
                   .HasForeignKey(f => f.FieldDefinitionId)
                   .OnDelete(DeleteBehavior.Restrict);

                table
                  .HasOne(f => f.ExecutionSource)
                  .WithMany(f => f.FieldIncludeManySources)
                  .HasForeignKey(f => f.ExecutionSourceId)
                  .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FieldKeysRelationship>(table =>
            {
                table.SetTable("FieldKeysRelationship", this._Schema)
                    .HasKey(f => new { f.FieldDefinitionId, f.FieldSaveTargetId, f.KeyFieldId });

                table
                   .HasOne(f => f.FieldDefinition)
                   .WithMany(f => f.FieldKeysRelationships)
                   .HasForeignKey(f => f.FieldDefinitionId)
                   .OnDelete(DeleteBehavior.Restrict);

                table
                  .HasOne(f => f.FieldSaveTarget)
                  .WithMany(f => f.FieldSaveTargetIds)
                  .HasForeignKey(f => f.FieldSaveTargetId)
                  .OnDelete(DeleteBehavior.Restrict);

                table
                    .HasOne(f => f.KeyField)
                    .WithMany(f => f.KeyFieldIds)
                    .HasForeignKey(f => f.KeyFieldId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TableFieldValidation>(table =>
            {
                table.SetTable("TableFieldValidation", this._Schema)
                    .HasKey(f => new { f.ChangeFieldDefinitionId, f.ExecutionSourceId });

                table.Property(f => f.ValidateOnClient).SetBitProperty();
                table.Property(f => f.IsEnabled).SetBitProperty();
                table.Property(f => f.ExecutionResultType).SetEnumProperty();

                table
                   .HasOne(f => f.ChangeFieldDefinition)
                   .WithMany(f => f.TableFieldValidations)
                   .HasForeignKey(f => f.ChangeFieldDefinitionId)
                   .OnDelete(DeleteBehavior.Restrict);

                table
                  .HasOne(f => f.ExecutionSource)
                  .WithMany(f => f.TableFieldValidations)
                  .HasForeignKey(f => f.ExecutionSourceId)
                  .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ExecutionSource>(table =>
            {
                table.SetTable("ExecutionSource", this._Schema)
                    .HasKey(f => f.ExecutionSourceId);

                table.Property(f => f.ExecutionDescription).SetVarcharProperty(512);
                table.Property(f => f.ExecutionText).SetVarcharProperty(8000);
                table.Property(f => f.AdditionalParameters).SetVarcharProperty(8000, false);
                table.Property(f => f.ExecutionType).SetEnumProperty();

                table
                   .HasMany(f => f.FieldIncludeManySources)
                   .WithOne(f => f.ExecutionSource)
                   .HasForeignKey(f => f.ExecutionSourceId);

                table
                   .HasMany(f => f.TableActions)
                   .WithOne(f => f.ExecutionSource)
                   .HasForeignKey(f => f.ExecutionSourceId);

                table
                   .HasMany(f => f.TableFieldValidations)
                   .WithOne(f => f.ExecutionSource)
                   .HasForeignKey(f => f.ExecutionSourceId);

                table
                    .HasOne(f => f.TableConfiguration)
                    .WithMany(f => f.ExecutionSources)
                    .HasForeignKey(f => f.TableConfigurationId);
            });


            modelBuilder.Entity<AppRole>(table =>
            {
                table.SetTable("AppRole", this._Schema)
                    .HasKey(f => f.AppRoleId);

                table.Property(f => f.RoleName).SetVarcharProperty(512);
                table.Property(f => f.IsEnabled).SetBitProperty();

                table
                   .HasMany(f => f.AppUsers)
                   .WithOne(f => f.AppRole)
                   .HasForeignKey(f => f.AppRoleId);
            });

            modelBuilder.Entity<AppUser>(table =>
            {
                table.SetTable("AppUser", this._Schema)
                    .HasKey(f => f.UserName);

                table.Property(f => f.UserName).SetVarcharProperty(512);
                table.Property(f => f.FirstName).SetVarcharProperty();
                table.Property(f => f.LastName).SetVarcharProperty();
                table.Property(f => f.LastName2).SetVarcharProperty(isRequired: false);
                table.Property(f => f.Email).SetVarcharProperty();
                table.Property(f => f.Position).SetVarcharProperty();
                table.Property(f => f.IsEnabled).SetBitProperty();

                table
                   .HasOne(f => f.AppRole)
                   .WithMany(f => f.AppUsers)
                   .HasForeignKey(f => f.AppRoleId);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
