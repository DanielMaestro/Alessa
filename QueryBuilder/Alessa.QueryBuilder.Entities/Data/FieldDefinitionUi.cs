namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// The field definition UI.
    /// </summary>
    public class FieldDefinitionUi
    {
        /// <summary>
        /// Gets or sets the <see cref="FieldDefinition"/> identifier.
        /// </summary>
        public virtual int FieldDefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        public virtual int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets a help text.
        /// </summary>
        public virtual string HelpText { get; set; }
        /// <summary>
        /// Gets or sets the display type.
        /// </summary>
        public virtual EDisplayType DisplayType { get; set; }
        /// <summary>
        /// Gets or sets the show in edit value.
        /// </summary>
        public virtual bool ShowInEdit { get; set; }
        /// <summary>
        /// Gets or sets the show in grid value.
        /// </summary>
        public virtual bool ShowInGrid { get; set; }
        /// <summary>
        /// Gets or sets the show in the details screen.
        /// </summary>
        public virtual bool ShowInDetails { get; set; }
        /// <summary>
        /// Whether can export or not.
        /// </summary>
        public virtual bool AllowExport { get; set; }
        /// <summary>
        /// Whether can edit in the details screen or not.
        /// </summary>
        public virtual bool AllowEditInDetail { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this field can be sorted or not.
        /// </summary>
        public virtual bool AllowSort { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this field can be Filterred or not.
        /// </summary>
        public virtual bool AllowFilter { get; set; }
        /// <summary>
        /// Gets or sets the is read only value.
        /// </summary>
        public virtual bool IsReadOnly { get; set; }
        /// <summary>
        /// Gets or sets whether is a hidden value or not.
        /// </summary>
        public virtual bool IsHidden { get; set; }
        /// <summary>
        /// Gets or sets whether is required or not.
        /// </summary>
        public virtual bool IsRequired { get; set; }
        /// <summary>
        /// Gets or sets a regular expression in order to validate this field.
        /// </summary>
        public virtual string Regex { get; set; }
        /// <summary>
        /// Gets or sets a minimun length in order to validate this field.
        /// </summary>
        public virtual int? MinLength { get; set; }
        /// <summary>
        /// Gets or sets a max length in order to validate this field.
        /// </summary>
        public virtual int? MaxLength { get; set; }
        /// <summary>
        /// Gets or sets a required error message.
        /// </summary>
        public virtual string RequiredErrorMsg { get; set; }
        /// <summary>
        /// Gets or sets a Regular Expression error message.
        /// </summary>
        public virtual string RegexErrorMsg { get; set; }
        /// <summary>
        /// Gets or sets a minimun error message.
        /// </summary>
        public virtual string MinLengthErrorMsg { get; set; }
        /// <summary>
        /// Gets or sets a maximun error message.
        /// </summary>
        public virtual string MaxLengthErrorMsg { get; set; }
        /// <summary>
        /// Gets or sets a displaying format in order to validate this field.
        /// </summary>
        public virtual string DisplayFormat { get; set; }
        /// <summary>
        /// Gets or sets the grid width.
        /// </summary>
        public virtual int? GridWidth { get; set; }
        /// <summary>
        /// Gets or sets the Edit width.
        /// </summary>
        public virtual int? EditWidth { get; set; }
        /// <summary>
        /// Gets or sets <see cref="FieldGroupDetail"/> identifier.
        /// </summary>
        public virtual int? FieldGroupDetailId { get; set; }

        /// <summary>
        /// Navigation property for <see cref="FieldDefinition"/> table.
        /// </summary>
        public virtual FieldDefinition FieldDefinition { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldGroupDetail"/> table.
        /// </summary>
        public virtual FieldGroupDetail FieldGroupDetail { get; set; }
    }
}
