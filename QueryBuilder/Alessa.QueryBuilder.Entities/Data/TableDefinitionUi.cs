namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// The table definition to show in the UI.
    /// </summary>
    public class TableDefinitionUi
    {
        /// <summary>
        /// Gets or sets the <see cref="TableDefinition"/> identifier.
        /// </summary>
        public virtual int TableDefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public virtual string DisplayName { get; set; }
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
        /// Whether can create a new record or not.
        /// </summary>
        public virtual bool AllowCreate { get; set; }
        /// <summary>
        /// Whether can edit the current record or not.
        /// </summary>
        public virtual bool AllowEdit { get; set; }
        /// <summary>
        /// Whether can delete the current record or not.
        /// </summary>
        public virtual bool AllowDelete { get; set; }
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
        /// Gets or sets the detail format.
        /// </summary>
        public virtual string DetailFormat { get; set; }

        /// <summary>
        /// Navigatino property for <see cref="TableDefinition"/>
        /// </summary>
        public virtual TableDefinition TableDefinition { get; set; }
    }
}
