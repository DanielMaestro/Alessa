using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// The grid definition model.
    /// </summary>
    public class GridConfigViewModel : TableConfigViewModel
    {
        /// <summary>
        /// Whether can export or not.
        /// </summary>
        public virtual bool AllowExport { get; set; }
        /// <summary>
        /// The group models.
        /// </summary>
        public new  ICollection<GridGroupViewModel> Groups { get; set; }
    }
}
