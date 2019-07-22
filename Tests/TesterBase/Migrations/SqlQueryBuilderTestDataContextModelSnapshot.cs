﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TesterBase.DataContext;

namespace TesterBase.Migrations
{
    [DbContext(typeof(SqlQueryBuilderTestDataContext))]
    partial class SqlQueryBuilderTestDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.AppRole", b =>
                {
                    b.Property<int>("AppRoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .IsUnicode(false);

                    b.HasKey("AppRoleId");

                    b.ToTable("AppRole","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.AppUser", b =>
                {
                    b.Property<string>("UserName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(512)
                        .IsUnicode(false);

                    b.Property<int>("AppRoleId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("LastName2")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.HasKey("UserName");

                    b.HasIndex("AppRoleId");

                    b.ToTable("AppUser","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.ExecutionSource", b =>
                {
                    b.Property<int>("ExecutionSourceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalParameters")
                        .IsUnicode(false);

                    b.Property<string>("ExecutionDescription")
                        .IsRequired()
                        .HasMaxLength(512)
                        .IsUnicode(false);

                    b.Property<string>("ExecutionText")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<string>("ExecutionType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int>("TableConfigurationId");

                    b.HasKey("ExecutionSourceId");

                    b.HasIndex("TableConfigurationId");

                    b.ToTable("ExecutionSource","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldDefinition", b =>
                {
                    b.Property<int>("FieldDefinitionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FieldLength");

                    b.Property<string>("FieldType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<bool>("IsEnabled");

                    b.Property<bool>("IsIdentity");

                    b.Property<bool>("IsKey");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<int>("TableDefinitionId");

                    b.HasKey("FieldDefinitionId");

                    b.HasIndex("TableDefinitionId");

                    b.ToTable("FieldDefinition","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldDefinitionUi", b =>
                {
                    b.Property<int>("FieldDefinitionId");

                    b.Property<bool>("AllowEditInDetail");

                    b.Property<bool>("AllowExport");

                    b.Property<bool>("AllowFilter");

                    b.Property<bool>("AllowSort");

                    b.Property<string>("DisplayFormat")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<int>("DisplayOrder");

                    b.Property<string>("DisplayType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int?>("EditWidth");

                    b.Property<int?>("FieldGroupDetailId");

                    b.Property<string>("FormatErrorMsg")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<int?>("GridWidth");

                    b.Property<string>("HelpText")
                        .HasMaxLength(1024)
                        .IsUnicode(false);

                    b.Property<bool>("IsHidden");

                    b.Property<bool>("IsReadOnly");

                    b.Property<bool>("IsRequired");

                    b.Property<int?>("MaxLength");

                    b.Property<string>("MaxLengthErrorMsg")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<int?>("MinLength");

                    b.Property<string>("MinLengthErrorMsg")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("RangeErrorMsg")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("RangeMax")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("RangeMin")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Regex")
                        .HasMaxLength(512)
                        .IsUnicode(false);

                    b.Property<string>("RegexErrorMsg")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("RequiredErrorMsg")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<bool>("ShowInDetails");

                    b.Property<bool>("ShowInEdit");

                    b.Property<bool>("ShowInGrid");

                    b.HasKey("FieldDefinitionId");

                    b.HasIndex("FieldGroupDetailId");

                    b.ToTable("FieldDefinitionUi","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldGroup", b =>
                {
                    b.Property<int>("FieldGroupId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("GroupType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int>("GroupWidth");

                    b.Property<bool>("IsEnabled");

                    b.Property<bool>("IsReadOnly");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("FieldGroupId");

                    b.ToTable("FieldGroup","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldGroupDetail", b =>
                {
                    b.Property<int>("FieldGroupDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<int>("DisplayOrder");

                    b.Property<int>("FieldGroupId");

                    b.Property<string>("GroupType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int>("GroupWidth");

                    b.Property<bool>("IsEnabled");

                    b.Property<bool>("IsReadOnly");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("FieldGroupDetailId");

                    b.HasIndex("FieldGroupId");

                    b.ToTable("FieldGroupDetail","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldIncludeManySource", b =>
                {
                    b.Property<int>("FieldDefinitionId");

                    b.Property<int>("ExecutionSourceId");

                    b.Property<string>("ForeignKey")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("LocalKey")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.HasKey("FieldDefinitionId", "ExecutionSourceId");

                    b.HasIndex("ExecutionSourceId");

                    b.ToTable("FieldIncludeManySource","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldKeysRelationship", b =>
                {
                    b.Property<int>("FieldDefinitionId");

                    b.Property<int>("FieldSaveTargetId");

                    b.Property<int>("KeyFieldId");

                    b.HasKey("FieldDefinitionId", "FieldSaveTargetId", "KeyFieldId");

                    b.HasIndex("FieldSaveTargetId");

                    b.HasIndex("KeyFieldId");

                    b.ToTable("FieldKeysRelationship","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldListSource", b =>
                {
                    b.Property<int>("FieldDefinitionId");

                    b.Property<int>("ExecutionSourceId");

                    b.Property<int>("RequiredFieldDefinitionId");

                    b.Property<int?>("TableDefinitionId");

                    b.HasKey("FieldDefinitionId", "ExecutionSourceId", "RequiredFieldDefinitionId");

                    b.HasIndex("ExecutionSourceId");

                    b.HasIndex("RequiredFieldDefinitionId");

                    b.HasIndex("TableDefinitionId");

                    b.ToTable("FieldListSource","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.TableAction", b =>
                {
                    b.Property<int>("TableDefinitionId");

                    b.Property<int>("ExecutionSourceId");

                    b.Property<int>("ExecutionOrder");

                    b.Property<string>("TableDbEventType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("TableDefinitionId", "ExecutionSourceId");

                    b.HasIndex("ExecutionSourceId");

                    b.ToTable("TableAction","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.TableConfiguration", b =>
                {
                    b.Property<int>("TableConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConnectionString")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.HasKey("TableConfigurationId");

                    b.ToTable("TableConfiguration","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.TableDefinition", b =>
                {
                    b.Property<int>("TableDefinitionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<int>("TableConfigurationId");

                    b.Property<string>("TableDefinitionType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.HasKey("TableDefinitionId");

                    b.HasIndex("TableConfigurationId");

                    b.ToTable("TableDefinition","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.TableDefinitionUi", b =>
                {
                    b.Property<int>("TableDefinitionId");

                    b.Property<bool>("AllowCreate");

                    b.Property<bool>("AllowDelete");

                    b.Property<bool>("AllowEdit");

                    b.Property<bool>("AllowEditInDetail");

                    b.Property<bool>("AllowExport");

                    b.Property<bool>("AllowFilter");

                    b.Property<bool>("AllowSort");

                    b.Property<string>("DetailFormat")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("DisplayName");

                    b.Property<bool>("IsReadOnly");

                    b.Property<bool>("ShowInDetails");

                    b.Property<bool>("ShowInEdit");

                    b.Property<bool>("ShowInGrid");

                    b.HasKey("TableDefinitionId");

                    b.ToTable("TableDefinitionUi","Alessa");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.TableFieldValidation", b =>
                {
                    b.Property<int>("ChangeFieldDefinitionId");

                    b.Property<int>("ExecutionSourceId");

                    b.Property<string>("ExecutionResultType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<bool>("IsEnabled");

                    b.Property<bool>("ValidateOnClient");

                    b.HasKey("ChangeFieldDefinitionId", "ExecutionSourceId");

                    b.HasIndex("ExecutionSourceId");

                    b.ToTable("TableFieldValidation","Alessa");
                });

            modelBuilder.Entity("TesterBase.Entities.BasicColumnType", b =>
                {
                    b.Property<string>("BasicColumnTypeId")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<bool>("ColCheckbox");

                    b.Property<DateTime>("ColDate");

                    b.Property<DateTime>("ColDateTime");

                    b.Property<decimal>("ColDouble");

                    b.Property<int>("ColInteger");

                    b.Property<decimal>("ColMoney");

                    b.Property<string>("ColRichTextArea")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<string>("ColText")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("ColTextArea")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<TimeSpan>("ColTime");

                    b.HasKey("BasicColumnTypeId");

                    b.ToTable("BasicColumnType","Samples");
                });

            modelBuilder.Entity("TesterBase.Entities.CatalogType", b =>
                {
                    b.Property<int>("CatalogTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CatalogTypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("CatalogTypeText")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<bool>("IsEnabled");

                    b.HasKey("CatalogTypeId");

                    b.ToTable("CatalogType","Samples");
                });

            modelBuilder.Entity("TesterBase.Entities.CatalogValue", b =>
                {
                    b.Property<int>("CatalogValueId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CatalogTypeId");

                    b.Property<bool>("CatalogValueDisplayEnabled");

                    b.Property<string>("CatalogValueName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("CatalogValueText")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<bool>("IsEnabled");

                    b.HasKey("CatalogValueId");

                    b.HasIndex("CatalogTypeId");

                    b.ToTable("CatalogValue","Samples");
                });

            modelBuilder.Entity("TesterBase.Entities.CatalogsJoinSample", b =>
                {
                    b.Property<int>("JoinSampleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Comments")
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool>("IsCommited");

                    b.Property<bool>("IsEnabled");

                    b.Property<int?>("RecordTypeId");

                    b.HasKey("JoinSampleId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RecordTypeId");

                    b.ToTable("CatalogsJoinSample","Samples");
                });

            modelBuilder.Entity("TesterBase.Entities.HideEnableMultiselection", b =>
                {
                    b.Property<int>("CatalogValueId");

                    b.Property<string>("HideEnableSampleId")
                        .HasMaxLength(7)
                        .IsUnicode(false);

                    b.Property<bool>("IsEnabled");

                    b.HasKey("CatalogValueId", "HideEnableSampleId");

                    b.HasIndex("HideEnableSampleId");

                    b.ToTable("HideEnableMultiselection","Samples");
                });

            modelBuilder.Entity("TesterBase.Entities.HideEnableSample", b =>
                {
                    b.Property<string>("HideEnableSampleId")
                        .HasMaxLength(7)
                        .IsUnicode(false);

                    b.Property<bool?>("Checkbox");

                    b.Property<string>("EnableWhen5")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<decimal?>("GridList");

                    b.Property<int?>("HideWhen2OrMore");

                    b.Property<bool>("IsCommited");

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("ShowChkbox")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("ShowWhenBasic")
                        .HasMaxLength(13)
                        .IsUnicode(false);

                    b.HasKey("HideEnableSampleId");

                    b.ToTable("HideEnableSample","Samples");
                });

            modelBuilder.Entity("TesterBase.Entities.MultiSelectCheckbox", b =>
                {
                    b.Property<int>("CatalogValueId");

                    b.Property<int>("MultiSelectSampleId");

                    b.Property<bool>("IsEnabled");

                    b.HasKey("CatalogValueId", "MultiSelectSampleId");

                    b.HasIndex("MultiSelectSampleId");

                    b.ToTable("MultiSelectCheckbox","Samples");
                });

            modelBuilder.Entity("TesterBase.Entities.MultiSelectList", b =>
                {
                    b.Property<int>("CatalogValueId");

                    b.Property<int>("MultiSelectSampleId");

                    b.Property<bool>("IsEnabled");

                    b.HasKey("CatalogValueId", "MultiSelectSampleId");

                    b.HasIndex("MultiSelectSampleId");

                    b.ToTable("MultiSelectList","Samples");
                });

            modelBuilder.Entity("TesterBase.Entities.MultiSelectSample", b =>
                {
                    b.Property<int>("MultiSelectSampleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.HasKey("MultiSelectSampleId");

                    b.ToTable("MultiSelectSample","Samples");
                });

            modelBuilder.Entity("TesterBase.Entities.MultiSelectTable", b =>
                {
                    b.Property<string>("BasicColumnTypeId")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<int>("MultiSelectSampleId");

                    b.Property<bool>("IsEnabled");

                    b.HasKey("BasicColumnTypeId", "MultiSelectSampleId");

                    b.HasIndex("MultiSelectSampleId");

                    b.ToTable("MultiSelectTable","Samples");
                });

            modelBuilder.Entity("TesterBase.Entities.ValidationSample", b =>
                {
                    b.Property<string>("ValidationSampleId")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.Property<int>("AnythinButValue");

                    b.Property<DateTime>("NotBeforeTwoDays");

                    b.Property<int>("OnlyOneSupportedValue");

                    b.Property<int>("RangeNumber");

                    b.Property<string>("Regex")
                        .HasMaxLength(300)
                        .IsUnicode(false);

                    b.Property<string>("Required")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<DateTime>("RequiredIfBasic");

                    b.Property<string>("VariableLength")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.HasKey("ValidationSampleId");

                    b.ToTable("ValidationSample","Samples");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.AppUser", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.AppRole", "AppRole")
                        .WithMany("AppUsers")
                        .HasForeignKey("AppRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.ExecutionSource", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.TableConfiguration", "TableConfiguration")
                        .WithMany("ExecutionSources")
                        .HasForeignKey("TableConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldDefinition", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.TableDefinition", "TableDefinition")
                        .WithMany("FieldDefinitions")
                        .HasForeignKey("TableDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldDefinitionUi", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.FieldDefinition", "FieldDefinition")
                        .WithOne("FieldDefinitionUi")
                        .HasForeignKey("Alessa.QueryBuilder.Entities.Data.FieldDefinitionUi", "FieldDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Alessa.QueryBuilder.Entities.Data.FieldGroupDetail", "FieldGroupDetail")
                        .WithMany("FieldDefinitionUis")
                        .HasForeignKey("FieldGroupDetailId");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldGroupDetail", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.FieldGroup", "FieldGroup")
                        .WithMany("FieldGroupDetails")
                        .HasForeignKey("FieldGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldIncludeManySource", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.ExecutionSource", "ExecutionSource")
                        .WithMany("FieldIncludeManySources")
                        .HasForeignKey("ExecutionSourceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Alessa.QueryBuilder.Entities.Data.FieldDefinition", "FieldDefinition")
                        .WithMany("FieldIncludeManySources")
                        .HasForeignKey("FieldDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldKeysRelationship", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.FieldDefinition", "FieldDefinition")
                        .WithMany("FieldKeysRelationships")
                        .HasForeignKey("FieldDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Alessa.QueryBuilder.Entities.Data.FieldDefinition", "FieldSaveTarget")
                        .WithMany("FieldSaveTargetIds")
                        .HasForeignKey("FieldSaveTargetId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Alessa.QueryBuilder.Entities.Data.FieldDefinition", "KeyField")
                        .WithMany("KeyFieldIds")
                        .HasForeignKey("KeyFieldId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.FieldListSource", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.ExecutionSource", "ExecutionSource")
                        .WithMany("FieldListSources")
                        .HasForeignKey("ExecutionSourceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Alessa.QueryBuilder.Entities.Data.FieldDefinition", "FieldDefinition")
                        .WithMany("FieldListSources")
                        .HasForeignKey("FieldDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Alessa.QueryBuilder.Entities.Data.FieldDefinition", "RequiredFieldDefinition")
                        .WithMany("RequiredFieldListSources")
                        .HasForeignKey("RequiredFieldDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Alessa.QueryBuilder.Entities.Data.TableDefinition", "TableDefinition")
                        .WithMany("FieldListSources")
                        .HasForeignKey("TableDefinitionId");
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.TableAction", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.ExecutionSource", "ExecutionSource")
                        .WithMany("TableActions")
                        .HasForeignKey("ExecutionSourceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Alessa.QueryBuilder.Entities.Data.TableDefinition", "TableDefinition")
                        .WithMany("TableActions")
                        .HasForeignKey("TableDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.TableDefinition", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.TableConfiguration", "TableConfiguration")
                        .WithMany("TableDefinitions")
                        .HasForeignKey("TableConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.TableDefinitionUi", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.TableDefinition", "TableDefinition")
                        .WithOne("TableDefinitionUi")
                        .HasForeignKey("Alessa.QueryBuilder.Entities.Data.TableDefinitionUi", "TableDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alessa.QueryBuilder.Entities.Data.TableFieldValidation", b =>
                {
                    b.HasOne("Alessa.QueryBuilder.Entities.Data.FieldDefinition", "ChangeFieldDefinition")
                        .WithMany("TableFieldValidations")
                        .HasForeignKey("ChangeFieldDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Alessa.QueryBuilder.Entities.Data.ExecutionSource", "ExecutionSource")
                        .WithMany("TableFieldValidations")
                        .HasForeignKey("ExecutionSourceId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TesterBase.Entities.CatalogValue", b =>
                {
                    b.HasOne("TesterBase.Entities.CatalogType", "CatalogType")
                        .WithMany("CatalogValues")
                        .HasForeignKey("CatalogTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TesterBase.Entities.CatalogsJoinSample", b =>
                {
                    b.HasOne("TesterBase.Entities.CatalogValue", "Category")
                        .WithMany("Categories")
                        .HasForeignKey("CategoryId");

                    b.HasOne("TesterBase.Entities.CatalogValue", "RecordType")
                        .WithMany("RecordTypes")
                        .HasForeignKey("RecordTypeId");
                });

            modelBuilder.Entity("TesterBase.Entities.HideEnableMultiselection", b =>
                {
                    b.HasOne("TesterBase.Entities.CatalogValue", "CatalogValue")
                        .WithMany("HideEnableMultiselections")
                        .HasForeignKey("CatalogValueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TesterBase.Entities.HideEnableSample", "HideEnableSample")
                        .WithMany("HideEnableMultiselections")
                        .HasForeignKey("HideEnableSampleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TesterBase.Entities.MultiSelectCheckbox", b =>
                {
                    b.HasOne("TesterBase.Entities.CatalogValue", "CatalogValue")
                        .WithMany("MultiSelectCheckboxes")
                        .HasForeignKey("CatalogValueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TesterBase.Entities.MultiSelectSample", "MultiSelectSample")
                        .WithMany("MultiSelectCheckboxes")
                        .HasForeignKey("MultiSelectSampleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TesterBase.Entities.MultiSelectList", b =>
                {
                    b.HasOne("TesterBase.Entities.CatalogValue", "CatalogValue")
                        .WithMany("MultiSelectLists")
                        .HasForeignKey("CatalogValueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TesterBase.Entities.MultiSelectSample", "MultiSelectSample")
                        .WithMany("MultiSelectLists")
                        .HasForeignKey("MultiSelectSampleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TesterBase.Entities.MultiSelectTable", b =>
                {
                    b.HasOne("TesterBase.Entities.BasicColumnType", "BasicColumnType")
                        .WithMany("MultiSelectTables")
                        .HasForeignKey("BasicColumnTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TesterBase.Entities.MultiSelectSample", "MultiSelectSample")
                        .WithMany("MultiSelectTables")
                        .HasForeignKey("MultiSelectSampleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
