using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.BuilderParameters
{
    /// <summary>
    /// The save data parameters class.
    /// </summary>
    public class SaveParameters : DataParameters
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SaveParameters"/>.
        /// </summary>
        public SaveParameters()
        {
            this.QueryType = EQueryType.EditView;
        }

        /// <summary>
        /// Gets or sets the save type.
        /// </summary>
        public virtual ESaveType SaveType { get; set; }
    }
}
