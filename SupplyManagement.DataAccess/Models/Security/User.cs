using SupplyManagement.DataAccess.Bases;
using SupplyManagement.DataAccess.Models.Organization;
using SupplyManagement.Shared.Attributes;

namespace SupplyManagement.DataAccess.Models.Security
{
    /// <summary>
    /// Represents a system user and authentication-related information.
    /// <para><b>Important constraints:</b></para>
    /// <list type="bullet">
    ///   <item><description><see cref="Username"/> and <see cref="EmailAddress"/> must be unique and are required.</description></item>
    ///   <item><description><see cref="Password"/> is hashed before being stored.</description></item>
    ///   <item><description><see cref="RoleId"/> defines the user's assigned role.</description></item>
    ///   <item><description>Supports soft deletion via <see cref="EntityBase.RowStatus"/>.</description></item>
    /// </list>
    /// </summary>
    [DatabaseSchema("security")]
    public class User : EntityBase
    {
        /// <summary>
        /// Maximum length: 50 characters.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Maximum length: 50 characters.
        /// </summary>
        public required string Email{ get; set; }

        public string Password { get; private set; } = string.Empty;

        public void SetPassword(string hashedPassword)
        {
            Password = hashedPassword;
        }

        /// <summary>
        /// Maximum length: 50 characters.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Default Value: true
        /// </summary>
        public bool IsActive { get; set; } = true;

        public Guid RoleId { get; set; }
        public Guid CompanyId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Company Company { get; set; } = null!;

    }

}
