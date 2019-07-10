using System;

namespace Alessa.Core.Entities.QueryModels
{
    /// <summary>
    /// A sorting name class.
    /// </summary>
    public class SortingName
    {
        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        public string ItemName { get; set; }
        private string _Order;

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public string Order
        {
            get { return _Order; }
            set
            {
                if (value?.Equals("DESC", StringComparison.OrdinalIgnoreCase) == true || value?.Equals("ASC", StringComparison.OrdinalIgnoreCase) == true)
                    _Order = value;
                else
                    _Order = "ASC";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(this.ItemName, " ", this.Order);
        }
    }
}
