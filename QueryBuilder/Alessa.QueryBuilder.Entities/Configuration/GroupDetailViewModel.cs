using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// Group detail.
    /// </summary>
    public class GroupDetailViewModel
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
        /// Gets or sets the Display order.
        /// </summary>
        public virtual int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets the field definition models.
        /// </summary>
        public virtual ICollection<FieldConfigViewModel> Fields { get; set; }
    }
}
