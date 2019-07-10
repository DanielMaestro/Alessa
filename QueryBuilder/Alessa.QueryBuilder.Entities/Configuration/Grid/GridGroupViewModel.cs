using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// TGhe grid configuration view model.
    /// </summary>
    public class GridGroupViewModel: GroupDetailViewModel
    {
        /// <summary>
        /// Gets or sets the field definition models.
        /// </summary>
        public new ICollection<GridFieldConfigViewModel> Fields { get; set; }
    }
}
