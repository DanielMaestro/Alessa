namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// The field group class.
    /// </summary>
    public class FieldGroup
    {
        /// <summary>
        /// Gets or sets the <see cref="FieldGroup"/> identifier.
        /// </summary>
        public virtual int FieldGroupId { get; set; }
        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        public virtual string ItemName { get; set; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets the Group Type.
        /// </summary>
        public virtual EGroupType GroupType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this entity is enabled or not.
        /// </summary>
        public virtual bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets the group width.
        /// </summary>
        public virtual int GroupWidth { get; set; }
        /// <summary>
        /// Gets or sets the is read only value.
        /// </summary>
        public virtual bool IsReadOnly { get; set; }

        /// <summary>
        /// Navigation property for <see cref="FieldGroupDetail"/> table.
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FieldGroupDetail> FieldGroupDetails { get; set; }
    }
}
