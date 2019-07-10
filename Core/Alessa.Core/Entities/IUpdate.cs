namespace Alessa.Core.Entities
{
    /// <summary>
    /// Provides an interface for an entity that have update fields.
    /// </summary>
    public interface IUpdate
    {
        /// <summary>
        /// Gets or sets the created by user.
        /// </summary>
        string UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        System.DateTime UpdatedDate { get; set; }
    }
}
