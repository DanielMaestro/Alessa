using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// Teh table definition entity.
    /// </summary>
    public class TableDefinition
    {
        /// <summary>
        /// The table definition identifier.
        /// </summary>
        public virtual int TableDefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        public virtual string ItemName { get; set; }
        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        public virtual string TableName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this entity is enabled or not.
        /// </summary>
        public virtual bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets the tabe definition type.
        /// </summary>
        public ETableDefinitionType TableDefinitionType { get; set; }
        /// <summary>
        /// The <see cref="TableConfiguration"/> identifier.
        /// </summary>
        public virtual int TableConfigurationId { get; set; }


        /// <summary>
        /// Navigation property for <see cref="FieldDefinition"/> table.
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FieldDefinition> FieldDefinitions { get; set; }
        /// <summary>
        /// Navigation property for <see cref="TableDefinitionUi"/> table.
        /// </summary>
        public virtual TableDefinitionUi TableDefinitionUi { get; set; }
        /// <summary>
        /// Navigation property for <see cref="TableConfiguration"/> table.
        /// </summary>
        public virtual TableConfiguration TableConfiguration { get; set; }
        /// <summary>
        /// Navigation property for <see cref="FieldListSource"/> table.
        /// </summary>
        public virtual ICollection<FieldListSource> FieldListSources { get; set; }
        /// <summary>
        /// Navigation property for <see cref="TableAction"/> table.
        /// </summary>
        public virtual ICollection<TableAction> TableActions { get; set; }

    }
}
