using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TesterBase.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Alessa");

            migrationBuilder.EnsureSchema(
                name: "Samples");

            migrationBuilder.CreateTable(
                name: "AppRole",
                schema: "Alessa",
                columns: table => new
                {
                    AppRoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(unicode: false, maxLength: 512, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.AppRoleId);
                });

            migrationBuilder.CreateTable(
                name: "FieldGroup",
                schema: "Alessa",
                columns: table => new
                {
                    FieldGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    GroupType = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    GroupWidth = table.Column<int>(nullable: false),
                    IsReadOnly = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldGroup", x => x.FieldGroupId);
                });

            migrationBuilder.CreateTable(
                name: "TableConfiguration",
                schema: "Alessa",
                columns: table => new
                {
                    TableConfigurationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConnectionString = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableConfiguration", x => x.TableConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "BasicColumnType",
                schema: "Samples",
                columns: table => new
                {
                    BasicColumnTypeId = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    ColCheckbox = table.Column<bool>(nullable: false),
                    ColDate = table.Column<DateTime>(nullable: false),
                    ColDateTime = table.Column<DateTime>(nullable: false),
                    ColDouble = table.Column<decimal>(nullable: false),
                    ColInteger = table.Column<int>(nullable: false),
                    ColMoney = table.Column<decimal>(nullable: false),
                    ColRichTextArea = table.Column<string>(unicode: false, nullable: false),
                    ColText = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    ColTextArea = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    ColTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicColumnType", x => x.BasicColumnTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CatalogType",
                schema: "Samples",
                columns: table => new
                {
                    CatalogTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CatalogTypeName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CatalogTypeText = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogType", x => x.CatalogTypeId);
                });

            migrationBuilder.CreateTable(
                name: "HideEnableSample",
                schema: "Samples",
                columns: table => new
                {
                    HideEnableSampleId = table.Column<string>(unicode: false, maxLength: 7, nullable: false),
                    Checkbox = table.Column<bool>(nullable: true),
                    EnableWhen5 = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    HideWhen2OrMore = table.Column<int>(nullable: true),
                    GridList = table.Column<decimal>(nullable: true),
                    ShowChkbox = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
                    ShowWhenBasic = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false),
                    IsCommited = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HideEnableSample", x => x.HideEnableSampleId);
                });

            migrationBuilder.CreateTable(
                name: "MultiSelectSample",
                schema: "Samples",
                columns: table => new
                {
                    MultiSelectSampleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiSelectSample", x => x.MultiSelectSampleId);
                });

            migrationBuilder.CreateTable(
                name: "ValidationSample",
                schema: "Samples",
                columns: table => new
                {
                    ValidationSampleId = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    AnythinButValue = table.Column<int>(nullable: false),
                    NotBeforeTwoDays = table.Column<DateTime>(nullable: false),
                    OnlyOneSupportedValue = table.Column<int>(nullable: false),
                    RangeNumber = table.Column<int>(nullable: false),
                    Regex = table.Column<string>(unicode: false, maxLength: 300, nullable: true),
                    Required = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    RequiredIfBasic = table.Column<DateTime>(nullable: false),
                    VariableLength = table.Column<string>(unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationSample", x => x.ValidationSampleId);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                schema: "Alessa",
                columns: table => new
                {
                    UserName = table.Column<string>(unicode: false, maxLength: 512, nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    LastName = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    LastName2 = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    Position = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    AppRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.UserName);
                    table.ForeignKey(
                        name: "FK_AppUser_AppRole_AppRoleId",
                        column: x => x.AppRoleId,
                        principalSchema: "Alessa",
                        principalTable: "AppRole",
                        principalColumn: "AppRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldGroupDetail",
                schema: "Alessa",
                columns: table => new
                {
                    FieldGroupDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FieldGroupId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    GroupType = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    GroupWidth = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    IsReadOnly = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldGroupDetail", x => x.FieldGroupDetailId);
                    table.ForeignKey(
                        name: "FK_FieldGroupDetail_FieldGroup_FieldGroupId",
                        column: x => x.FieldGroupId,
                        principalSchema: "Alessa",
                        principalTable: "FieldGroup",
                        principalColumn: "FieldGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExecutionSource",
                schema: "Alessa",
                columns: table => new
                {
                    ExecutionSourceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExecutionType = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    ExecutionDescription = table.Column<string>(unicode: false, maxLength: 512, nullable: false),
                    ExecutionText = table.Column<string>(unicode: false, nullable: false),
                    TableConfigurationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionSource", x => x.ExecutionSourceId);
                    table.ForeignKey(
                        name: "FK_ExecutionSource_TableConfiguration_TableConfigurationId",
                        column: x => x.TableConfigurationId,
                        principalSchema: "Alessa",
                        principalTable: "TableConfiguration",
                        principalColumn: "TableConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableDefinition",
                schema: "Alessa",
                columns: table => new
                {
                    TableDefinitionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    TableName = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    TableDefinitionType = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    TableConfigurationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableDefinition", x => x.TableDefinitionId);
                    table.ForeignKey(
                        name: "FK_TableDefinition_TableConfiguration_TableConfigurationId",
                        column: x => x.TableConfigurationId,
                        principalSchema: "Alessa",
                        principalTable: "TableConfiguration",
                        principalColumn: "TableConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogValue",
                schema: "Samples",
                columns: table => new
                {
                    CatalogValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CatalogTypeId = table.Column<int>(nullable: false),
                    CatalogValueDisplayEnabled = table.Column<bool>(nullable: false),
                    CatalogValueName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CatalogValueText = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogValue", x => x.CatalogValueId);
                    table.ForeignKey(
                        name: "FK_CatalogValue_CatalogType_CatalogTypeId",
                        column: x => x.CatalogTypeId,
                        principalSchema: "Samples",
                        principalTable: "CatalogType",
                        principalColumn: "CatalogTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultiSelectTable",
                schema: "Samples",
                columns: table => new
                {
                    BasicColumnTypeId = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    MultiSelectSampleId = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiSelectTable", x => new { x.BasicColumnTypeId, x.MultiSelectSampleId });
                    table.ForeignKey(
                        name: "FK_MultiSelectTable_BasicColumnType_BasicColumnTypeId",
                        column: x => x.BasicColumnTypeId,
                        principalSchema: "Samples",
                        principalTable: "BasicColumnType",
                        principalColumn: "BasicColumnTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultiSelectTable_MultiSelectSample_MultiSelectSampleId",
                        column: x => x.MultiSelectSampleId,
                        principalSchema: "Samples",
                        principalTable: "MultiSelectSample",
                        principalColumn: "MultiSelectSampleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldDefinition",
                schema: "Alessa",
                columns: table => new
                {
                    FieldDefinitionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    IsKey = table.Column<bool>(nullable: false),
                    FieldLength = table.Column<int>(nullable: true),
                    FieldType = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    IsIdentity = table.Column<bool>(nullable: false),
                    TableDefinitionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDefinition", x => x.FieldDefinitionId);
                    table.ForeignKey(
                        name: "FK_FieldDefinition_TableDefinition_TableDefinitionId",
                        column: x => x.TableDefinitionId,
                        principalSchema: "Alessa",
                        principalTable: "TableDefinition",
                        principalColumn: "TableDefinitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableAction",
                schema: "Alessa",
                columns: table => new
                {
                    TableDefinitionId = table.Column<int>(nullable: false),
                    ExecutionSourceId = table.Column<int>(nullable: false),
                    TableDbEventType = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    ExecutionOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableAction", x => new { x.TableDefinitionId, x.ExecutionSourceId });
                    table.ForeignKey(
                        name: "FK_TableAction_ExecutionSource_ExecutionSourceId",
                        column: x => x.ExecutionSourceId,
                        principalSchema: "Alessa",
                        principalTable: "ExecutionSource",
                        principalColumn: "ExecutionSourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TableAction_TableDefinition_TableDefinitionId",
                        column: x => x.TableDefinitionId,
                        principalSchema: "Alessa",
                        principalTable: "TableDefinition",
                        principalColumn: "TableDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TableDefinitionUi",
                schema: "Alessa",
                columns: table => new
                {
                    TableDefinitionId = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    ShowInEdit = table.Column<bool>(nullable: false),
                    ShowInGrid = table.Column<bool>(nullable: false),
                    ShowInDetails = table.Column<bool>(nullable: false),
                    AllowExport = table.Column<bool>(nullable: false),
                    AllowEditInDetail = table.Column<bool>(nullable: false),
                    AllowCreate = table.Column<bool>(nullable: false),
                    AllowEdit = table.Column<bool>(nullable: false),
                    AllowDelete = table.Column<bool>(nullable: false),
                    AllowSort = table.Column<bool>(nullable: false),
                    AllowFilter = table.Column<bool>(nullable: false),
                    IsReadOnly = table.Column<bool>(nullable: false),
                    DetailFormat = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableDefinitionUi", x => x.TableDefinitionId);
                    table.ForeignKey(
                        name: "FK_TableDefinitionUi_TableDefinition_TableDefinitionId",
                        column: x => x.TableDefinitionId,
                        principalSchema: "Alessa",
                        principalTable: "TableDefinition",
                        principalColumn: "TableDefinitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogsJoinSample",
                schema: "Samples",
                columns: table => new
                {
                    JoinSampleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true),
                    Comments = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    RecordTypeId = table.Column<int>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false),
                    IsCommited = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogsJoinSample", x => x.JoinSampleId);
                    table.ForeignKey(
                        name: "FK_CatalogsJoinSample_CatalogValue_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Samples",
                        principalTable: "CatalogValue",
                        principalColumn: "CatalogValueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogsJoinSample_CatalogValue_RecordTypeId",
                        column: x => x.RecordTypeId,
                        principalSchema: "Samples",
                        principalTable: "CatalogValue",
                        principalColumn: "CatalogValueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HideEnableMultiselection",
                schema: "Samples",
                columns: table => new
                {
                    CatalogValueId = table.Column<int>(nullable: false),
                    HideEnableSampleId = table.Column<string>(unicode: false, maxLength: 7, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HideEnableMultiselection", x => new { x.CatalogValueId, x.HideEnableSampleId });
                    table.ForeignKey(
                        name: "FK_HideEnableMultiselection_CatalogValue_CatalogValueId",
                        column: x => x.CatalogValueId,
                        principalSchema: "Samples",
                        principalTable: "CatalogValue",
                        principalColumn: "CatalogValueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HideEnableMultiselection_HideEnableSample_HideEnableSampleId",
                        column: x => x.HideEnableSampleId,
                        principalSchema: "Samples",
                        principalTable: "HideEnableSample",
                        principalColumn: "HideEnableSampleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultiSelectCheckbox",
                schema: "Samples",
                columns: table => new
                {
                    CatalogValueId = table.Column<int>(nullable: false),
                    MultiSelectSampleId = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiSelectCheckbox", x => new { x.CatalogValueId, x.MultiSelectSampleId });
                    table.ForeignKey(
                        name: "FK_MultiSelectCheckbox_CatalogValue_CatalogValueId",
                        column: x => x.CatalogValueId,
                        principalSchema: "Samples",
                        principalTable: "CatalogValue",
                        principalColumn: "CatalogValueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultiSelectCheckbox_MultiSelectSample_MultiSelectSampleId",
                        column: x => x.MultiSelectSampleId,
                        principalSchema: "Samples",
                        principalTable: "MultiSelectSample",
                        principalColumn: "MultiSelectSampleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultiSelectList",
                schema: "Samples",
                columns: table => new
                {
                    CatalogValueId = table.Column<int>(nullable: false),
                    MultiSelectSampleId = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiSelectList", x => new { x.CatalogValueId, x.MultiSelectSampleId });
                    table.ForeignKey(
                        name: "FK_MultiSelectList_CatalogValue_CatalogValueId",
                        column: x => x.CatalogValueId,
                        principalSchema: "Samples",
                        principalTable: "CatalogValue",
                        principalColumn: "CatalogValueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultiSelectList_MultiSelectSample_MultiSelectSampleId",
                        column: x => x.MultiSelectSampleId,
                        principalSchema: "Samples",
                        principalTable: "MultiSelectSample",
                        principalColumn: "MultiSelectSampleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldDefinitionUi",
                schema: "Alessa",
                columns: table => new
                {
                    FieldDefinitionId = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    HelpText = table.Column<string>(unicode: false, maxLength: 1024, nullable: true),
                    DisplayType = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    ShowInEdit = table.Column<bool>(nullable: false),
                    ShowInGrid = table.Column<bool>(nullable: false),
                    ShowInDetails = table.Column<bool>(nullable: false),
                    AllowExport = table.Column<bool>(nullable: false),
                    AllowEditInDetail = table.Column<bool>(nullable: false),
                    AllowSort = table.Column<bool>(nullable: false),
                    AllowFilter = table.Column<bool>(nullable: false),
                    IsReadOnly = table.Column<bool>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    Regex = table.Column<string>(unicode: false, maxLength: 512, nullable: true),
                    MinLength = table.Column<int>(nullable: true),
                    MaxLength = table.Column<int>(nullable: true),
                    RangeMin = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    RangeMax = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    RequiredErrorMsg = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    RegexErrorMsg = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    MinLengthErrorMsg = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    MaxLengthErrorMsg = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    RangeErrorMsg = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    FormatErrorMsg = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    DisplayFormat = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    GridWidth = table.Column<int>(nullable: true),
                    EditWidth = table.Column<int>(nullable: true),
                    FieldGroupDetailId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDefinitionUi", x => x.FieldDefinitionId);
                    table.ForeignKey(
                        name: "FK_FieldDefinitionUi_FieldDefinition_FieldDefinitionId",
                        column: x => x.FieldDefinitionId,
                        principalSchema: "Alessa",
                        principalTable: "FieldDefinition",
                        principalColumn: "FieldDefinitionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldDefinitionUi_FieldGroupDetail_FieldGroupDetailId",
                        column: x => x.FieldGroupDetailId,
                        principalSchema: "Alessa",
                        principalTable: "FieldGroupDetail",
                        principalColumn: "FieldGroupDetailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldIncludeManySource",
                schema: "Alessa",
                columns: table => new
                {
                    FieldDefinitionId = table.Column<int>(nullable: false),
                    ExecutionSourceId = table.Column<int>(nullable: false),
                    ForeignKey = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    LocalKey = table.Column<string>(unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldIncludeManySource", x => new { x.FieldDefinitionId, x.ExecutionSourceId });
                    table.ForeignKey(
                        name: "FK_FieldIncludeManySource_ExecutionSource_ExecutionSourceId",
                        column: x => x.ExecutionSourceId,
                        principalSchema: "Alessa",
                        principalTable: "ExecutionSource",
                        principalColumn: "ExecutionSourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldIncludeManySource_FieldDefinition_FieldDefinitionId",
                        column: x => x.FieldDefinitionId,
                        principalSchema: "Alessa",
                        principalTable: "FieldDefinition",
                        principalColumn: "FieldDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldKeysRelationship",
                schema: "Alessa",
                columns: table => new
                {
                    FieldDefinitionId = table.Column<int>(nullable: false),
                    FieldSaveTargetId = table.Column<int>(nullable: false),
                    KeyFieldId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldKeysRelationship", x => new { x.FieldDefinitionId, x.FieldSaveTargetId, x.KeyFieldId });
                    table.ForeignKey(
                        name: "FK_FieldKeysRelationship_FieldDefinition_FieldDefinitionId",
                        column: x => x.FieldDefinitionId,
                        principalSchema: "Alessa",
                        principalTable: "FieldDefinition",
                        principalColumn: "FieldDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldKeysRelationship_FieldDefinition_FieldSaveTargetId",
                        column: x => x.FieldSaveTargetId,
                        principalSchema: "Alessa",
                        principalTable: "FieldDefinition",
                        principalColumn: "FieldDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldKeysRelationship_FieldDefinition_KeyFieldId",
                        column: x => x.KeyFieldId,
                        principalSchema: "Alessa",
                        principalTable: "FieldDefinition",
                        principalColumn: "FieldDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldListSource",
                schema: "Alessa",
                columns: table => new
                {
                    FieldDefinitionId = table.Column<int>(nullable: false),
                    ExecutionSourceId = table.Column<int>(nullable: false),
                    RequiredFieldDefinitionId = table.Column<int>(nullable: false),
                    TableDefinitionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldListSource", x => new { x.FieldDefinitionId, x.ExecutionSourceId, x.RequiredFieldDefinitionId });
                    table.ForeignKey(
                        name: "FK_FieldListSource_ExecutionSource_ExecutionSourceId",
                        column: x => x.ExecutionSourceId,
                        principalSchema: "Alessa",
                        principalTable: "ExecutionSource",
                        principalColumn: "ExecutionSourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldListSource_FieldDefinition_FieldDefinitionId",
                        column: x => x.FieldDefinitionId,
                        principalSchema: "Alessa",
                        principalTable: "FieldDefinition",
                        principalColumn: "FieldDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldListSource_FieldDefinition_RequiredFieldDefinitionId",
                        column: x => x.RequiredFieldDefinitionId,
                        principalSchema: "Alessa",
                        principalTable: "FieldDefinition",
                        principalColumn: "FieldDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldListSource_TableDefinition_TableDefinitionId",
                        column: x => x.TableDefinitionId,
                        principalSchema: "Alessa",
                        principalTable: "TableDefinition",
                        principalColumn: "TableDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TableFieldValidation",
                schema: "Alessa",
                columns: table => new
                {
                    ChangeFieldDefinitionId = table.Column<int>(nullable: false),
                    ExecutionSourceId = table.Column<int>(nullable: false),
                    ValidateOnClient = table.Column<bool>(nullable: false),
                    ExecutionResultType = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableFieldValidation", x => new { x.ChangeFieldDefinitionId, x.ExecutionSourceId });
                    table.ForeignKey(
                        name: "FK_TableFieldValidation_FieldDefinition_ChangeFieldDefinitionId",
                        column: x => x.ChangeFieldDefinitionId,
                        principalSchema: "Alessa",
                        principalTable: "FieldDefinition",
                        principalColumn: "FieldDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TableFieldValidation_ExecutionSource_ExecutionSourceId",
                        column: x => x.ExecutionSourceId,
                        principalSchema: "Alessa",
                        principalTable: "ExecutionSource",
                        principalColumn: "ExecutionSourceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_AppRoleId",
                schema: "Alessa",
                table: "AppUser",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionSource_TableConfigurationId",
                schema: "Alessa",
                table: "ExecutionSource",
                column: "TableConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldDefinition_TableDefinitionId",
                schema: "Alessa",
                table: "FieldDefinition",
                column: "TableDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldDefinitionUi_FieldGroupDetailId",
                schema: "Alessa",
                table: "FieldDefinitionUi",
                column: "FieldGroupDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldGroupDetail_FieldGroupId",
                schema: "Alessa",
                table: "FieldGroupDetail",
                column: "FieldGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldIncludeManySource_ExecutionSourceId",
                schema: "Alessa",
                table: "FieldIncludeManySource",
                column: "ExecutionSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldKeysRelationship_FieldSaveTargetId",
                schema: "Alessa",
                table: "FieldKeysRelationship",
                column: "FieldSaveTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldKeysRelationship_KeyFieldId",
                schema: "Alessa",
                table: "FieldKeysRelationship",
                column: "KeyFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldListSource_ExecutionSourceId",
                schema: "Alessa",
                table: "FieldListSource",
                column: "ExecutionSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldListSource_RequiredFieldDefinitionId",
                schema: "Alessa",
                table: "FieldListSource",
                column: "RequiredFieldDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldListSource_TableDefinitionId",
                schema: "Alessa",
                table: "FieldListSource",
                column: "TableDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TableAction_ExecutionSourceId",
                schema: "Alessa",
                table: "TableAction",
                column: "ExecutionSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_TableDefinition_TableConfigurationId",
                schema: "Alessa",
                table: "TableDefinition",
                column: "TableConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_TableFieldValidation_ExecutionSourceId",
                schema: "Alessa",
                table: "TableFieldValidation",
                column: "ExecutionSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogsJoinSample_CategoryId",
                schema: "Samples",
                table: "CatalogsJoinSample",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogsJoinSample_RecordTypeId",
                schema: "Samples",
                table: "CatalogsJoinSample",
                column: "RecordTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogValue_CatalogTypeId",
                schema: "Samples",
                table: "CatalogValue",
                column: "CatalogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HideEnableMultiselection_HideEnableSampleId",
                schema: "Samples",
                table: "HideEnableMultiselection",
                column: "HideEnableSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiSelectCheckbox_MultiSelectSampleId",
                schema: "Samples",
                table: "MultiSelectCheckbox",
                column: "MultiSelectSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiSelectList_MultiSelectSampleId",
                schema: "Samples",
                table: "MultiSelectList",
                column: "MultiSelectSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiSelectTable_MultiSelectSampleId",
                schema: "Samples",
                table: "MultiSelectTable",
                column: "MultiSelectSampleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUser",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "FieldDefinitionUi",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "FieldIncludeManySource",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "FieldKeysRelationship",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "FieldListSource",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "TableAction",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "TableDefinitionUi",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "TableFieldValidation",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "CatalogsJoinSample",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "HideEnableMultiselection",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "MultiSelectCheckbox",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "MultiSelectList",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "MultiSelectTable",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "ValidationSample",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "AppRole",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "FieldGroupDetail",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "FieldDefinition",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "ExecutionSource",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "HideEnableSample",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "CatalogValue",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "BasicColumnType",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "MultiSelectSample",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "FieldGroup",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "TableDefinition",
                schema: "Alessa");

            migrationBuilder.DropTable(
                name: "CatalogType",
                schema: "Samples");

            migrationBuilder.DropTable(
                name: "TableConfiguration",
                schema: "Alessa");
        }
    }
}
