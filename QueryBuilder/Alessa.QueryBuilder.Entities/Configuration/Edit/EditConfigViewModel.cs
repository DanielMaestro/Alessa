using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// The edit configuration view model.
    /// </summary>
    public class EditConfigViewModel : TableConfigViewModel
    {
        /// <summary>
        /// Gets or sets the detail format.
        /// </summary>
        public virtual string DetailFormat { get; set; }
        /// <summary>
        /// Whether can export or not.
        /// </summary>
        public virtual bool AllowExport { get; set; }
        /// <summary>
        /// The group models.
        /// </summary>
        public new ICollection<EditGroupViewModel> Groups { get; set; }
        /// <summary>
        /// Gets or sets a list of data validations.
        /// </summary>
        public virtual ICollection<DataValidation> Validations { get; set; }
    }
}
