using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// The table definition model class.
    /// </summary>
    public class TableConfigViewModel
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
        /// The group models.
        /// </summary>
        public virtual ICollection<GroupDetailViewModel> Groups { get; set; }
    }
}
