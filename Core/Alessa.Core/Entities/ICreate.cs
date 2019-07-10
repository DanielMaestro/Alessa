namespace Alessa.Core.Entities
{
    /// <summary>
    /// Provides an interface for an entity that have create fields.
    /// </summary>
    ///// <typeparam name="T">Type of field.</typeparam>
    public interface ICreate
    {
        /// <summary>
        /// Gets or sets the created by user.
        /// </summary>
        string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        System.DateTime CreatedDate { get; set; }
    }
}
