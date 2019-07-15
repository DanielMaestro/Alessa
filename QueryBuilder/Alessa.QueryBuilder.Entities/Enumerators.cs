namespace Alessa.QueryBuilder.Entities
{

    /// <summary>
    /// Enumerates the table definition type.
    /// </summary>
    public enum ETableDefinitionType
    {
        /// <summary>
        /// Table type.
        /// </summary>
        Table,
        /// <summary>
        /// View type.
        /// </summary>
        View,
        /// <summary>
        /// A table builded with only an ALex query.
        /// </summary>
        ALex
    }

    /// <summary>
    /// Enumerates the execution result type.
    /// </summary>
    public enum EExecutionResultType
    {
        /// <summary>
        /// It expects the validation result.
        /// </summary>
        ValidationResultArray,
        /// <summary>
        /// It expects the show result.
        /// </summary>
        ShowResultArray,
        /// <summary>
        /// A custom object.
        /// </summary>
        CustomObject
    }

    /// <summary>
    /// Enumerates the database events.
    /// </summary>
    public enum ETableDbEventType
    {
        /// <summary>
        /// Before creating a record.
        /// </summary>
        BeforeCreate,
        /// <summary>
        /// After create a record.
        /// </summary>
        AfterCreate,
        /// <summary>
        /// Before select (filter) records.
        /// </summary>
        BeforeSelect,
        /// <summary>
        /// After select (filter) records.
        /// </summary>
        AfterSelect,
        /// <summary>
        /// Before update a record.
        /// </summary>
        BeforeUpdate,
        /// <summary>
        /// After update a record.
        /// </summary>
        AfterUpdate,
        /// <summary>
        /// Before delete a record.
        /// </summary>
        BeforeDelete,
        /// <summary>
        /// After delet a record.
        /// </summary>
        AfterDelete,
        /// <summary>
        /// When creates or edits a record.
        /// </summary>
        BeforeCreateOrUpdate,
        /// <summary>
        /// After a record is edited or created.
        /// </summary>
        AfterCreateOrUpdate,
        /// <summary>
        /// Triggers when the user wants to create a record.
        /// </summary>
        OnEnterCreate,
        /// <summary>
        /// Triggers when the user wants to update a record.
        /// </summary>
        OnEnterUpdate,
        /// <summary>
        /// Triggers when the user wants to create or updates a record.
        /// </summary>
        OnEnterCreateOrUpdate,
        /// <summary>
        /// Triggers when the user wants to delete an item.
        /// </summary>
        OnEnterDelete,
    }

    /// <summary>
    /// Enumerates the group types.
    /// </summary>
    public enum EGroupType
    {
        /// <summary>
        /// A tab control group.
        /// </summary>
        Tab,
        /// <summary>
        /// A form group.
        /// </summary>
        Form,
        /// <summary>
        /// A vertical form.
        /// </summary>
        VerticalForm,
        /// <summary>
        /// An inline form.
        /// </summary>
        InlineForm,
        /// <summary>
        /// A data grid.
        /// </summary>
        DataGrid,
        /// <summary>
        /// A tab vertical form.
        /// </summary>
        TabVerticalForm,
        /// <summary>
        /// A Tab inline form.
        /// </summary>
        TabInlineForm

    }

    /// <summary>
    /// Enumerates the execution type.
    /// </summary>
    public enum EExecutionType
    {
        /// <summary>
        /// Enumerates an ALex execution.
        /// </summary>
        ALex,
        /// <summary>
        /// Enumerates a JavaScript execution.
        /// </summary>
        JavaScript
    }

    /// <summary>
    /// Enumerates the field types.
    /// </summary>
    public enum EFieldType
    {
        /// <summary>
        /// Text field.
        /// </summary>
        Text,
        /// <summary>
        /// Integer field.
        /// </summary>
        Integer,
        /// <summary>
        /// Decimal field.
        /// </summary>
        Decimal,
        /// <summary>
        /// A date field.
        /// </summary>
        Date,
        /// <summary>
        /// A time field.
        /// </summary>
        Time,
        /// <summary>
        /// A datetime field.
        /// </summary>
        DateTime,
        /// <summary>
        /// Bit field.
        /// </summary>
        Bit,
        /// <summary>
        /// A text area field.
        /// </summary>
        TextArea,
        /// <summary>
        /// A rich text area.
        /// </summary>
        RichText,
        /// <summary>
        /// A single selection field.
        /// </summary>
        SingleSelect,
        /// <summary>
        /// A radio button field.
        /// </summary>
        Radio,
        /// <summary>
        /// A multiple selection field.
        /// </summary>
        MultiselectList,
        /// <summary>
        /// A multiple checkboxes field.
        /// </summary>
        MultiselectCheckbox,
        /// <summary>
        /// A field that references a table.
        /// </summary>
        TableReference
    }

    /// <summary>
    /// Enumerates the Display type.
    /// </summary>
    public enum EDisplayType
    {
        /// <summary>
        /// A field with label inline.
        /// </summary>
        FieldWithLabelInline,
        /// <summary>
        /// Shows on ly the placeholder.
        /// </summary>
        OnlyPlaceHolder,
        /// <summary>
        /// Is a field with a label above.
        /// </summary>
        FieldWithLabelAbove,
        /// <summary>
        /// Is a field with a label below.
        /// </summary>
        FieldWithLabelBelow
    }

    /// <summary>
    /// Enumerates the source action types.
    /// </summary>
    public enum ESourceActionType
    {
        /// <summary>
        /// Shows a info message.
        /// </summary>
        ShowInfo,
        /// <summary>
        /// Shows a warning message.
        /// </summary>
        ShowWarning,
        /// <summary>
        /// Shows an error message.
        /// </summary>
        ShowError,
        /// <summary>
        /// Gets the source.
        /// </summary>
        GetSource
    }

    /// <summary>
    /// Enumerates the statement type.
    /// </summary>
    public enum EStatementType
    {
        /// <summary>
        /// An insert statement.
        /// </summary>
        Insert,
        /// <summary>
        /// An Update statement.
        /// </summary>
        Update,
        /// <summary>
        /// Adds a filter.
        /// </summary>
        AddFilter,
    }

    /// <summary>
    /// Enumerats the query source.
    /// </summary>
    public enum EQuerySource
    {
        /// <summary>
        /// The source is not set.
        /// </summary>
        Undefined,
        /// <summary>
        /// The query comes from the web app.
        /// </summary>
        WebApp,
        /// <summary>
        /// The query comes from a mobile app.
        /// </summary>
        MobileApp
    }

    /// <summary>
    /// Enumerates the query type.
    /// </summary>
    public enum EQueryType
    {
        /// <summary>
        /// The query type is not defined.
        /// </summary>
        Undefined,
        /// <summary>
        /// Is for grid view.
        /// </summary>
        GridView,
        /// <summary>
        /// Is for the editing view.
        /// </summary>
        EditView,
        /// <summary>
        /// Is for detail view.
        /// </summary>
        DetailListView
    }

    /// <summary>
    /// Enumerates the save type.
    /// </summary>
    public enum ESaveType
    {
        /// <summary>
        /// Creates a new record.
        /// </summary>
        Create,
        /// <summary>
        /// Updates the existing record.
        /// </summary>
        Update,
        /// <summary>
        /// Deletes the existing record.
        /// </summary>
        Delete
    }
}
