DECLARE @json NVARCHAR(MAX)
SET @json = '[
  {
    "ItemName": "Catalogs.CatalogType",
    "DisplayName": "Types of catalog",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "CatalogTypeId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true,
        "IsIdentity": true
      },
      {
        "ItemName": "CatalogTypeName",
        "DisplayName": "Catalog type name",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "CatalogTypeText",
        "DisplayName": "Show Text",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "IsEnabled",
        "DisplayName": "Is enabled?",
        "IsKey": false,
        "FieldType": "Bit",
        "IsEnabled": true
      }
    ]
  },
  {
    "ItemName": "Catalogs.CatalogValue",
    "DisplayName": "Values of catalog",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "CatalogTypeId",
        "DisplayName": "Catalog type identifier",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "CatalogValueDisplayEnabled",
        "DisplayName": "Time column",
        "IsKey": false,
        "FieldType": "Bit",
        "IsEnabled": true
      },
      {
        "ItemName": "CatalogValueId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true,
        "IsIdentity": true
      },
      {
        "ItemName": "CatalogValueName",
        "DisplayName": "Record name",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "CatalogValueText",
        "DisplayName": "Show Text",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "IsEnabled",
        "DisplayName": "Is enabled?",
        "IsKey": false,
        "FieldType": "Bit",
        "IsEnabled": true
      }
    ]
  },
  {
    "ItemName": "Sample.BasicColumnType",
    "DisplayName": "Basic columns Sample",
    "IsEnabled": true,
    "ShowInEdit": true,
    "ShowInGrid": true,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "BasicColumnTypeId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Text",
        "IsEnabled": true,
        "IsIdentity": false
      },
      {
        "ItemName": "ColCheckbox",
        "DisplayName": "Textbox column",
        "IsKey": false,
        "FieldType": "Bit",
        "IsEnabled": true
      },
      {
        "ItemName": "ColDate",
        "DisplayName": "Date column",
        "IsKey": false,
        "FieldType": "Date",
        "IsEnabled": true
      },
      {
        "ItemName": "ColDateTime",
        "DisplayName": "Date and time column",
        "IsKey": false,
        "FieldType": "DateTime",
        "IsEnabled": true
      },
      {
        "ItemName": "ColDouble",
        "DisplayName": "Double column",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "ColInteger",
        "DisplayName": "Integer column",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "ColMoney",
        "DisplayName": "Money column",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "ColRichTextArea",
        "DisplayName": "Text area field",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "ColText",
        "DisplayName": "Text column",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "ColTextArea",
        "DisplayName": "Text area field",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "ColTime",
        "DisplayName": "Time column",
        "IsKey": false,
        "FieldType": "Time",
        "IsEnabled": true
      }
    ]
  },
  {
    "ItemName": "Sample.CatalogsJoinSample",
    "DisplayName": "A catalogs join Sample",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "CategoryId",
        "DisplayName": "Category",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "Comments",
        "DisplayName": "Comments",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "CreatedDate",
        "DisplayName": "Date column",
        "IsKey": false,
        "FieldType": "DateTime",
        "IsEnabled": true
      },
      {
        "ItemName": "JoinSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true,
        "IsIdentity": true
      },
      {
        "ItemName": "RecordTypeId",
        "DisplayName": "Record type",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      }
    ]
  },
  {
    "ItemName": "Sample.CatalogsJoinSampleView",
    "DisplayName": "The catalog join Sample View",
    "IsEnabled": true,
    "ShowInEdit": true,
    "ShowInGrid": true,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "Category",
        "DisplayName": "Category",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "Comments",
        "DisplayName": "Comments",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "CreatedDate",
        "DisplayName": "Date column",
        "IsKey": false,
        "FieldType": "DateTime",
        "IsEnabled": true
      },
      {
        "ItemName": "JoinSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true,
        "IsIdentity": true
      },
      {
        "ItemName": "RecordType",
        "DisplayName": "Record type",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      }
    ]
  },
  {
    "ItemName": "Sample.HiddenEnableMultiselection",
    "DisplayName": "Multiselection",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "CatalogValueId",
        "DisplayName": "Multiselected",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "IsEnabled",
        "DisplayName": "Is Enabled?",
        "IsKey": false,
        "FieldType": "Bit",
        "IsEnabled": true
      },
      {
        "ItemName": "ValidationSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Text",
        "IsEnabled": true,
        "IsIdentity": false
      }
    ]
  },
  {
    "ItemName": "Sample.HideEnableMultiselect",
    "DisplayName": "Multiselection",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "Checkbox",
        "DisplayName": "Check\/Uncheck me",
        "IsKey": false,
        "FieldType": "Bit",
        "IsEnabled": true
      },
      {
        "ItemName": "EnableWhen5",
        "DisplayName": "Only enable when Select list is One or Three",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "HideWhen2OrMore",
        "DisplayName": "Hidden when Multiselect contains two or more selec",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "List",
        "DisplayName": "Select List",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "Multiselect",
        "DisplayName": "Multiselect",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "ShowChkbox",
        "DisplayName": "Shown when checkbox is checked",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "ShowWhenBasic",
        "DisplayName": "Shown when basic contains more than 4 records.",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "ValidationSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Text",
        "IsEnabled": true
      }
    ]
  },
  {
    "ItemName": "Sample.HideEnableSample",
    "DisplayName": "Hide\/Show\/Enable\/Disable Controls",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "Checkbox",
        "DisplayName": "Check\/Uncheck me",
        "IsKey": false,
        "FieldType": "Bit",
        "IsEnabled": true
      },
      {
        "ItemName": "EnableWhen5",
        "DisplayName": "Only enable when Select list is One or Three",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "HideWhen2OrMore",
        "DisplayName": "Hidden when Multiselect contains two or more selec",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "List",
        "DisplayName": "Select List",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "ShowChkbox",
        "DisplayName": "Shown when checkbox is checked",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "ShowWhenBasic",
        "DisplayName": "Shown when basic contains more than 4 records.",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "ValidationSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Text",
        "IsEnabled": true,
        "IsIdentity": false
      }
    ]
  },
  {
    "ItemName": "Sample.MultiSelectCheckbox",
    "DisplayName": "Checboxes",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "CatalogValueId",
        "DisplayName": "Checkbox selection",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "IsEnabled",
        "DisplayName": "Is Enabled?",
        "IsKey": false,
        "FieldType": "Bit",
        "IsEnabled": true
      },
      {
        "ItemName": "MultiSelectSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true
      }
    ]
  },
  {
    "ItemName": "Sample.MultiSelectList",
    "DisplayName": "Multiselection List",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "CatalogValueId",
        "DisplayName": "Multiselection",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "IsEnabled",
        "DisplayName": "Is Enabled?",
        "IsKey": false,
        "FieldType": "Bit",
        "IsEnabled": true
      },
      {
        "ItemName": "MultiSelectSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true
      }
    ]
  },
  {
    "ItemName": "Sample.MultiSelectSample",
    "DisplayName": "A multiselection sample",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "CreatedDate",
        "DisplayName": "Created",
        "IsKey": false,
        "FieldType": "DateTime",
        "IsEnabled": true
      },
      {
        "ItemName": "MultiSelectSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true,
        "IsIdentity": true
      }
    ]
  },
  {
    "ItemName": "Sample.MultiSelectSampleView",
    "DisplayName": "The multiselection sample view",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "CheckboxSelection",
        "DisplayName": "Checkbox selection",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "CreatedDate",
        "DisplayName": "Created",
        "IsKey": false,
        "FieldType": "DateTime",
        "IsEnabled": true
      },
      {
        "ItemName": "Multiselection",
        "DisplayName": "Multiselection",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "MultiSelectSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true,
        "IsIdentity": true
      },
      {
        "ItemName": "Records",
        "DisplayName": "Records",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      }
    ]
  },
  {
    "ItemName": "Sample.MultiSelectTable",
    "DisplayName": "Grid list",
    "IsEnabled": true,
    "ShowInEdit": false,
    "ShowInGrid": false,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "BasicColumnTypeId",
        "DisplayName": "Record",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "IsEnabled",
        "DisplayName": "Is Enabled?",
        "IsKey": false,
        "FieldType": "Bit",
        "IsEnabled": true
      },
      {
        "ItemName": "MultiSelectSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Number",
        "IsEnabled": true
      }
    ]
  },
  {
    "ItemName": "Sample.ValidationSample",
    "DisplayName": "The validations sample",
    "IsEnabled": true,
    "ShowInEdit": true,
    "ShowInGrid": true,
    "IsReadOnly": false,
    "FieldDefinitions": [
      {
        "ItemName": "AnythinButValue",
        "DisplayName": "Any number but 5",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "NotBeforeTwoDays",
        "DisplayName": "Do not select two days ago",
        "IsKey": false,
        "FieldType": "DateTime",
        "IsEnabled": true
      },
      {
        "ItemName": "OnlyOneSupportedValue",
        "DisplayName": "Only accepts number 5",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "RangeNumber",
        "DisplayName": "Range Number (between 1 and 10)",
        "IsKey": false,
        "FieldType": "Number",
        "IsEnabled": true
      },
      {
        "ItemName": "Regex",
        "DisplayName": "Regular Expression",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "Required",
        "DisplayName": "Required",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      },
      {
        "ItemName": "RequiredIfBasic",
        "DisplayName": "Required if BasicColumnType contains more than 5 r",
        "IsKey": false,
        "FieldType": "DateTime",
        "IsEnabled": true
      },
      {
        "ItemName": "ValidationSampleId",
        "DisplayName": "Key field",
        "IsKey": true,
        "FieldType": "Text",
        "IsEnabled": true,
        "IsIdentity": false
      },
      {
        "ItemName": "VariableLength",
        "DisplayName": "From 3 to 5 charachters",
        "IsKey": false,
        "FieldType": "Text",
        "IsEnabled": true
      }
    ]
  }
]'

SELECT *
FROM OPENJSON(@json)
WITH(
	ItemName VARCHAR(50) '$.ItemName'
	, DisplayName VARCHAR(50) '$.DisplayName'
	, IsEnabled BIT '$.IsEnabled'
	, ShowInEdit BIT '$.ShowInEdit'
	, ShowInGrid BIT '$.ShowInGrid'
	, IsReadOnly BIT '$.IsReadOnly'
,	FieldDefinitions NVARCHAR(MAX) '$.FieldDefinitions' AS JSON
) AS t
	OUTER APPLY OPENJSON(FieldDefinitions)
	WITH
	(
		 ItemName2 VARCHAR(50) '$.ItemName'
		, DisplayName2 VARCHAR(50) '$.DisplayName'
		, IsKey BIT '$.IsKey'
		, FieldType VARCHAR(50) '$.FieldType'
		, IsEnabled2 BIT '$.IsEnabled'
		, IsIdentity BIT '$.IsIdentity'
	) AS r
ORDER BY ItemName, ItemName2, FieldType
