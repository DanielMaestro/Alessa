namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// The application user class.
    /// </summary>
    public class AppUser
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// Gets or sets the First name.
        /// </summary>
        public virtual string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the Last name.
        /// </summary>
        public virtual string LastName { get; set; }
        /// <summary>
        /// Gets or sets the Last name.
        /// </summary>
        public virtual string LastName2 { get; set; }
        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public virtual string Position { get; set; }
        /// <summary>
        /// Is enabled?.
        /// </summary>
        public virtual bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="AppRole"/> identifier.
        /// </summary>
        public virtual int AppRoleId { get; set; }

        /// <summary>
        /// Navigation property for <see cref="AppRole"/>.
        /// </summary>
        public virtual AppRole AppRole { get; set; }
    }
}
