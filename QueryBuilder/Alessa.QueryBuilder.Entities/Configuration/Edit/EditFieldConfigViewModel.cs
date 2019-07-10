using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Configuration
{
    /// <summary>
    /// The edit field configuration class.
    /// </summary>
    public class EditFieldConfigViewModel : FieldConfigViewModel
    {
        /// <summary>
        /// Whether can edit in the details screen or not.
        /// </summary>
        public virtual bool? AllowEditInDetail { get; set; }
        /// <summary>
        /// Gets or sets the grid width.
        /// </summary>
        public virtual int? EditWidth { get; set; }

        /// <summary>
        /// Gets or sets whether is required or not.
        /// </summary>
        public virtual bool? IsRequired { get; set; }
        /// <summary>
        /// Gets or sets a regular expression in order to validate this field.
        /// </summary>
        public virtual string Regex { get; set; }
        /// <summary>
        /// Gets or sets a minimun length in order to validate this field.
        /// </summary>
        public virtual int? MinLength { get; set; }
        /// <summary>
        /// Gets or sets a max length in order to validate this field.
        /// </summary>
        public virtual int? MaxLength { get; set; }
        /// <summary>
        /// Gets or sets a list of required fields.
        /// </summary>
        public virtual List<string> SourceList { get; set; }
        /// <summary>
        /// Gets or sets the field type.
        /// </summary>
        public virtual EFieldType FieldType { get; set; }
        /// <summary>
        /// Gets or sets the display type.
        /// </summary>
        public virtual EDisplayType DisplayType { get; set; }
        /// <summary>
        /// Gets or sets the is read only value.
        /// </summary>
        public virtual bool IsReadOnly { get; set; }
    }
}
