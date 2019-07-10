namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// The field config class for grid view.
    /// </summary>
    public class GridFieldConfigViewModel : FieldConfigViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this field can be sorted or not.
        /// </summary>
        public virtual bool AllowSort { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this field can be Filterred or not.
        /// </summary>
        public virtual bool AllowFilter { get; set; }
        /// <summary>
        /// Gets or sets the grid width.
        /// </summary>
        public virtual int? GridWidth { get; set; }
        /// <summary>
        /// Gets or sets the display type.
        /// </summary>
        public virtual EDisplayType DisplayType { get; set; }
        /// <summary>
        /// Gets or sets the is read only value.
        /// </summary>
        public virtual bool IsReadOnly { get; set; }
    }
}
