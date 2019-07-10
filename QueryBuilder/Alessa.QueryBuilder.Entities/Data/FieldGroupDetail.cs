using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// The edit group detail.
    /// </summary>
    public class FieldGroupDetail
    {
        /// <summary>
        /// Gets or sets the <see cref="FieldGroupDetail"/> identifier.
        /// </summary>
        public virtual int FieldGroupDetailId { get; set; }
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
        /// Gets or sets the width size.
        /// </summary>
        public virtual int GroupWidth { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this entity is enabled or not.
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets the Display order.
        /// </summary>
        public virtual int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets the is read only value.
        /// </summary>
        public virtual bool IsReadOnly { get; set; }

        /// <summary>
        /// Navigation property for <see cref="FieldGroup"/> table.
        /// </summary>
        public virtual FieldGroup FieldGroup { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldDefinitionUi"/> table.
        /// </summary>
        public virtual ICollection<FieldDefinitionUi> FieldDefinitionUis { get; set; }
    }
}
