DECLARE @Temp TABLE (	TableDefinitionId INT ,	ItemName VARCHAR(8000) ,	DisplayName VARCHAR(8000) ,	TableDefinitionType VARCHAR(8000) ,	IsEnabled BIT ,	ShowInEdit BIT ,	ShowInGrid BIT ,	IsReadOnly BIT ,	FieldDefinitionId INT ,	TableDefinitionId2 INT ,	ItemName2 VARCHAR(8000) ,	DisplayName2 VARCHAR(8000) ,	IsKey BIT ,	FieldType VARCHAR(8000) ,	IsEnabled2 BIT ,	IsIdentity BIT 	)
INSERT INTO @Temp VALUES																	
(	1,	'Catalogs.CatalogType',	'Types of catalog',	'Table',	1,	0,	0,	0,	1,	1,	'CatalogTypeId',	'Key field',	1,	'Number',	1,	1	),
(	1,	'Catalogs.CatalogType',	'Types of catalog',	'Table',	1,	0,	0,	0,	2,	1,	'CatalogTypeName',	'Catalog type name',	0,	'Text',	1,	NULL	),
(	1,	'Catalogs.CatalogType',	'Types of catalog',	'Table',	1,	0,	0,	0,	3,	1,	'CatalogTypeText',	'Show Text',	0,	'Text',	1,	NULL	),
(	1,	'Catalogs.CatalogType',	'Types of catalog',	'Table',	1,	0,	0,	0,	4,	1,	'IsEnabled',	'Is enabled?',	0,	'Bit',	1,	NULL	),
(	1,	'Catalogs.CatalogValue',	'Values of catalog',	'Table',	1,	0,	0,	0,	5,	1,	'CatalogTypeId',	'Catalog type identifier',	0,	'Number',	1,	NULL	),
(	1,	'Catalogs.CatalogValue',	'Values of catalog',	'Table',	1,	0,	0,	0,	6,	1,	'CatalogValueDisplayEnabled',	'Time column',	0,	'Bit',	1,	NULL	),
(	1,	'Catalogs.CatalogValue',	'Values of catalog',	'Table',	1,	0,	0,	0,	7,	1,	'CatalogValueId',	'Key field',	1,	'Number',	1,	1	),
(	1,	'Catalogs.CatalogValue',	'Values of catalog',	'Table',	1,	0,	0,	0,	8,	1,	'CatalogValueName',	'Record name',	0,	'Text',	1,	NULL	),
(	1,	'Catalogs.CatalogValue',	'Values of catalog',	'Table',	1,	0,	0,	0,	9,	1,	'CatalogValueText',	'Show Text',	0,	'Text',	1,	NULL	),
(	1,	'Catalogs.CatalogValue',	'Values of catalog',	'Table',	1,	0,	0,	0,	10,	1,	'IsEnabled',	'Is enabled?',	0,	'Bit',	1,	NULL	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	11,	2,	'BasicColumnTypeId',	'Key field',	1,	'Text',	1,	0	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	12,	2,	'ColCheckbox',	'Textbox column',	0,	'Bit',	1,	NULL	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	13,	2,	'ColDate',	'Date column',	0,	'Date',	1,	NULL	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	14,	2,	'ColDateTime',	'Date and time column',	0,	'DateTime',	1,	NULL	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	15,	2,	'ColDouble',	'Double column',	0,	'Number',	1,	NULL	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	16,	2,	'ColInteger',	'Integer column',	0,	'Number',	1,	NULL	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	17,	2,	'ColMoney',	'Money column',	0,	'Number',	1,	NULL	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	18,	2,	'ColRichTextArea',	'Text area field',	0,	'Text',	1,	NULL	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	19,	2,	'ColText',	'Text column',	0,	'Text',	1,	NULL	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	20,	2,	'ColTextArea',	'Text area field',	0,	'Text',	1,	NULL	),
(	2,	'Sample.BasicColumnType',	'Basic columns Sample',	'Table',	1,	1,	1,	0,	21,	2,	'ColTime',	'Time column',	0,	'Time',	1,	NULL	),
(	3,	'Sample.CatalogsJoinSample',	'A catalogs join Sample',	'Table',	1,	0,	0,	0,	22,	3,	'CategoryId',	'Category',	0,	'Number',	1,	NULL	),
(	3,	'Sample.CatalogsJoinSample',	'A catalogs join Sample',	'Table',	1,	0,	0,	0,	23,	3,	'Comments',	'Comments',	0,	'Text',	1,	NULL	),
(	3,	'Sample.CatalogsJoinSample',	'A catalogs join Sample',	'Table',	1,	0,	0,	0,	24,	3,	'CreatedDate',	'Date column',	0,	'DateTime',	1,	NULL	),
(	3,	'Sample.CatalogsJoinSample',	'A catalogs join Sample',	'Table',	1,	0,	0,	0,	25,	3,	'JoinSampleId',	'Key field',	1,	'Number',	1,	1	),
(	3,	'Sample.CatalogsJoinSample',	'A catalogs join Sample',	'Table',	1,	0,	0,	0,	26,	3,	'RecordTypeId',	'Record type',	0,	'Number',	1,	NULL	),
(	4,	'Sample.CatalogsJoinSampleView',	'The catalog join Sample View',	'View',	1,	1,	1,	0,	27,	4,	'Category',	'Category',	0,	'Text',	1,	NULL	),
(	4,	'Sample.CatalogsJoinSampleView',	'The catalog join Sample View',	'View',	1,	1,	1,	0,	28,	4,	'Comments',	'Comments',	0,	'Text',	1,	NULL	),
(	4,	'Sample.CatalogsJoinSampleView',	'The catalog join Sample View',	'View',	1,	1,	1,	0,	29,	4,	'CreatedDate',	'Date column',	0,	'DateTime',	1,	NULL	),
(	4,	'Sample.CatalogsJoinSampleView',	'The catalog join Sample View',	'View',	1,	1,	1,	0,	30,	4,	'JoinSampleId',	'Key field',	1,	'Number',	1,	1	),
(	4,	'Sample.CatalogsJoinSampleView',	'The catalog join Sample View',	'View',	1,	1,	1,	0,	31,	4,	'RecordType',	'Record type',	0,	'Text',	1,	NULL	),
(	5,	'Sample.HiddenEnableMultiselection',	'Multiselection',	'Table',	1,	0,	0,	0,	32,	5,	'CatalogValueId',	'Multiselected',	0,	'Number',	1,	NULL	),
(	5,	'Sample.HiddenEnableMultiselection',	'Multiselection',	'Table',	1,	0,	0,	0,	33,	5,	'IsEnabled',	'Is Enabled?',	0,	'Bit',	1,	NULL	),
(	5,	'Sample.HiddenEnableMultiselection',	'Multiselection',	'Table',	1,	0,	0,	0,	34,	5,	'ValidationSampleId',	'Key field',	1,	'Text',	1,	0	),
(	6,	'Sample.HideEnableMultiselect',	'Multiselection',	'Table',	1,	0,	0,	0,	35,	6,	'Checkbox',	'Check/Uncheck me',	0,	'Bit',	1,	NULL	),
(	6,	'Sample.HideEnableMultiselect',	'Multiselection',	'Table',	1,	0,	0,	0,	36,	6,	'EnableWhen5',	'Only enable when Select list is One or Three',	0,	'Text',	1,	NULL	),
(	6,	'Sample.HideEnableMultiselect',	'Multiselection',	'Table',	1,	0,	0,	0,	37,	6,	'HideWhen2OrMore',	'Hidden when Multiselect contains two or more selec',	0,	'Number',	1,	NULL	),
(	6,	'Sample.HideEnableMultiselect',	'Multiselection',	'Table',	1,	0,	0,	0,	38,	6,	'List',	'Select List',	0,	'Number',	1,	NULL	),
(	6,	'Sample.HideEnableMultiselect',	'Multiselection',	'Table',	1,	0,	0,	0,	39,	6,	'Multiselect',	'Multiselect',	0,	'Text',	1,	NULL	),
(	6,	'Sample.HideEnableMultiselect',	'Multiselection',	'Table',	1,	0,	0,	0,	40,	6,	'ShowChkbox',	'Shown when checkbox is checked',	0,	'Text',	1,	NULL	),
(	6,	'Sample.HideEnableMultiselect',	'Multiselection',	'Table',	1,	0,	0,	0,	41,	6,	'ShowWhenBasic',	'Shown when basic contains more than 4 records.',	0,	'Text',	1,	NULL	),
(	6,	'Sample.HideEnableMultiselect',	'Multiselection',	'Table',	1,	0,	0,	0,	42,	6,	'ValidationSampleId',	'Key field',	1,	'Text',	1,	NULL	),
(	7,	'Sample.HideEnableSample',	'Hide/Show/Enable/Disable Controls',	'Table',	1,	0,	0,	0,	43,	7,	'Checkbox',	'Check/Uncheck me',	0,	'Bit',	1,	NULL	),
(	7,	'Sample.HideEnableSample',	'Hide/Show/Enable/Disable Controls',	'Table',	1,	0,	0,	0,	44,	7,	'EnableWhen5',	'Only enable when Select list is One or Three',	0,	'Text',	1,	NULL	),
(	7,	'Sample.HideEnableSample',	'Hide/Show/Enable/Disable Controls',	'Table',	1,	0,	0,	0,	45,	7,	'HideWhen2OrMore',	'Hidden when Multiselect contains two or more selec',	0,	'Number',	1,	NULL	),
(	7,	'Sample.HideEnableSample',	'Hide/Show/Enable/Disable Controls',	'Table',	1,	0,	0,	0,	46,	7,	'List',	'Select List',	0,	'Number',	1,	NULL	),
(	7,	'Sample.HideEnableSample',	'Hide/Show/Enable/Disable Controls',	'Table',	1,	0,	0,	0,	47,	7,	'ShowChkbox',	'Shown when checkbox is checked',	0,	'Text',	1,	NULL	),
(	7,	'Sample.HideEnableSample',	'Hide/Show/Enable/Disable Controls',	'Table',	1,	0,	0,	0,	48,	7,	'ShowWhenBasic',	'Shown when basic contains more than 4 records.',	0,	'Text',	1,	NULL	),
(	7,	'Sample.HideEnableSample',	'Hide/Show/Enable/Disable Controls',	'Table',	1,	0,	0,	0,	49,	7,	'ValidationSampleId',	'Key field',	1,	'Text',	1,	0	),
(	8,	'Sample.MultiSelectCheckbox',	'Checboxes',	'Table',	1,	0,	0,	0,	50,	8,	'CatalogValueId',	'Checkbox selection',	1,	'Number',	1,	NULL	),
(	8,	'Sample.MultiSelectCheckbox',	'Checboxes',	'Table',	1,	0,	0,	0,	51,	8,	'IsEnabled',	'Is Enabled?',	0,	'Bit',	1,	NULL	),
(	8,	'Sample.MultiSelectCheckbox',	'Checboxes',	'Table',	1,	0,	0,	0,	52,	8,	'MultiSelectSampleId',	'Key field',	1,	'Number',	1,	NULL	),
(	9,	'Sample.MultiSelectList',	'Multiselection List',	'Table',	1,	0,	0,	0,	53,	9,	'CatalogValueId',	'Multiselection',	1,	'Number',	1,	NULL	),
(	9,	'Sample.MultiSelectList',	'Multiselection List',	'Table',	1,	0,	0,	0,	54,	9,	'IsEnabled',	'Is Enabled?',	0,	'Bit',	1,	NULL	),
(	9,	'Sample.MultiSelectList',	'Multiselection List',	'Table',	1,	0,	0,	0,	55,	9,	'MultiSelectSampleId',	'Key field',	1,	'Number',	1,	NULL	),
(	10,	'Sample.MultiSelectSample',	'A multiselection sample',	'Table',	1,	0,	0,	0,	56,	10,	'CreatedDate',	'Created',	0,	'DateTime',	1,	NULL	),
(	10,	'Sample.MultiSelectSample',	'A multiselection sample',	'Table',	1,	0,	0,	0,	57,	10,	'MultiSelectSampleId',	'Key field',	1,	'Number',	1,	1	),
(	11,	'Sample.MultiSelectSampleView',	'The multiselection sample view',	'View',	1,	0,	0,	0,	58,	11,	'CheckboxSelection',	'Checkbox selection',	0,	'Text',	1,	NULL	),
(	11,	'Sample.MultiSelectSampleView',	'The multiselection sample view',	'View',	1,	0,	0,	0,	59,	11,	'CreatedDate',	'Created',	0,	'DateTime',	1,	NULL	),
(	11,	'Sample.MultiSelectSampleView',	'The multiselection sample view',	'View',	1,	0,	0,	0,	60,	11,	'Multiselection',	'Multiselection',	0,	'Text',	1,	NULL	),
(	11,	'Sample.MultiSelectSampleView',	'The multiselection sample view',	'View',	1,	0,	0,	0,	61,	11,	'MultiSelectSampleId',	'Key field',	1,	'Number',	1,	1	),
(	11,	'Sample.MultiSelectSampleView',	'The multiselection sample view',	'View',	1,	0,	0,	0,	62,	11,	'Records',	'Records',	0,	'Text',	1,	NULL	),
(	12,	'Sample.MultiSelectTable',	'Grid list',	'Table',	1,	0,	0,	0,	63,	12,	'BasicColumnTypeId',	'Record',	1,	'Number',	1,	NULL	),
(	12,	'Sample.MultiSelectTable',	'Grid list',	'Table',	1,	0,	0,	0,	64,	12,	'IsEnabled',	'Is Enabled?',	0,	'Bit',	1,	NULL	),
(	12,	'Sample.MultiSelectTable',	'Grid list',	'Table',	1,	0,	0,	0,	65,	12,	'MultiSelectSampleId',	'Key field',	1,	'Number',	1,	NULL	),
(	13,	'Sample.ValidationSample',	'The validations sample',	'Table',	1,	1,	1,	0,	66,	13,	'AnythinButValue',	'Any number but 5',	0,	'Number',	1,	NULL	),
(	13,	'Sample.ValidationSample',	'The validations sample',	'Table',	1,	1,	1,	0,	67,	13,	'NotBeforeTwoDays',	'Do not select two days ago',	0,	'DateTime',	1,	NULL	),
(	13,	'Sample.ValidationSample',	'The validations sample',	'Table',	1,	1,	1,	0,	68,	13,	'OnlyOneSupportedValue',	'Only accepts number 5',	0,	'Number',	1,	NULL	),
(	13,	'Sample.ValidationSample',	'The validations sample',	'Table',	1,	1,	1,	0,	69,	13,	'RangeNumber',	'Range Number (between 1 and 10)',	0,	'Number',	1,	NULL	),
(	13,	'Sample.ValidationSample',	'The validations sample',	'Table',	1,	1,	1,	0,	70,	13,	'Regex',	'Regular Expression',	0,	'Text',	1,	NULL	),
(	13,	'Sample.ValidationSample',	'The validations sample',	'Table',	1,	1,	1,	0,	71,	13,	'Required',	'Required',	0,	'Text',	1,	NULL	),
(	13,	'Sample.ValidationSample',	'The validations sample',	'Table',	1,	1,	1,	0,	72,	13,	'RequiredIfBasic',	'Required if BasicColumnType contains more than 5 r',	0,	'DateTime',	1,	NULL	),
(	13,	'Sample.ValidationSample',	'The validations sample',	'Table',	1,	1,	1,	0,	73,	13,	'ValidationSampleId',	'Key field',	1,	'Text',	1,	0	),
(	13,	'Sample.ValidationSample',	'The validations sample',	'Table',	1,	1,	1,	0,	74,	13,	'VariableLength',	'From 3 to 5 charachters',	0,	'Text',	1,	NULL	)




SELECT DISTINCT
	TableDefinitionId
	, ItemName
	, DisplayName
	, TableDefinitionType
	, IsEnabled
	, ShowInEdit
	, ShowInGrid
	, IsReadOnly
	,
	(
		SELECT
			FieldDefinitionId
			, TableDefinitionId2 AS TableDefinitionId
			, ItemName2 AS ItemName
			, DisplayName2 AS DisplayName
			, IsKey
			, FieldType
			, IsEnabled2 AS IsEnabled
			, IsIdentity
		FROM @Temp T2
		WHERE T1.ItemName = T2.ItemName 
		FOR JSON PATH
	) AS FieldDefinitions
FROM @Temp T1
FOR JSON PATH