namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// The field definition model class.
    /// </summary>
    public class FieldConfigViewModel
    {
        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        public virtual string ItemName { get; set; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        public virtual int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets whether is a kay value or not.
        /// </summary>
        public virtual bool IsKey { get; set; }
        /// <summary>
        /// Gets or sets whether is a hidden value or not.
        /// </summary>
        public virtual bool IsHidden { get; set; }
        /// <summary>
        /// Gets or sets a displaying format in order to validate this field.
        /// </summary>
        public virtual string DisplayFormat { get; set; }
    }
}
