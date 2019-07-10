
using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// The edit group detail view model class.
    /// </summary>
    public class EditGroupDetailViewModel : GroupDetailViewModel
    {
        /// <summary>
        /// Gets or sets the Group Type.
        /// </summary>
        public virtual EGroupType GroupType { get; set; }
        /// <summary>
        /// Gets or sets the group width.
        /// </summary>
        public virtual int GroupWidth { get; set; }
        /// <summary>
        /// Gets or sets the field definition models.
        /// </summary>
        public new ICollection<EditFieldConfigViewModel> Fields { get; set; }
    }
}
