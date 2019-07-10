using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// The detail list configuration model.
    /// </summary>
    public class DetailListConfigViewModel : TableConfigViewModel
    {
        /// <summary>
        /// Gets or sets the detail format.
        /// </summary>
        public virtual string DetailFormat { get; set; }
        /// <summary>
        /// The group models.
        /// </summary>
        public new ICollection<FieldConfigViewModel> Groups { get; set; }
    }
}
