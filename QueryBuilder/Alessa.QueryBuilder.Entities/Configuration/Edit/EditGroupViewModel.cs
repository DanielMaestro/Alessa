using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// The group model class.
    /// </summary>
    public class EditGroupViewModel
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
        /// Gets or sets the Group Type.
        /// </summary>
        public virtual EGroupType GroupType { get; set; }
        /// <summary>
        /// Gets or sets the group width.
        /// </summary>
        public virtual int GroupWidth { get; set; }
        /// <summary>
        /// Gets or sets the group details.
        /// </summary>
        public virtual ICollection<EditGroupDetailViewModel> GroupDetails { get; set; }
    }
}
