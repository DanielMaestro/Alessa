using System.Collections.Generic;

namespace Alessa.QueryBuilder.Entities.Data
{
    /// <summary>
    /// Application role table.
    /// </summary>
    public class AppRole
    {
        /// <summary>
        /// Gets or sets the <see cref="AppRole"/> identifier.
        /// </summary>
        public virtual int AppRoleId { get; set; }
        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        public virtual string RoleName { get; set; }
        /// <summary>
        /// Is enabled?
        /// </summary>
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// Navigation property for <see cref="AppUser"/> table.
        /// </summary>
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
