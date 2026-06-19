using SupplyManagement.DataAccess.Bases;
using SupplyManagement.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.DataAccess.Models.Security
{
    /// <summary>
    /// Represents a user role that defines permission access for a group of users.
    /// <para><b>Important constraints:</b></para>
    /// <list type="bullet">
    ///   <item><description><see cref="Code"/> is unique and required.</description></item>
    ///   <item><description>Supports soft deletion via <see cref="EntityBase.RowStatus"/>.</description></item>
    /// </list>
    /// </summary>
    [DatabaseSchema("security")]
    public class Role : EntityBase
    {
        /// <summary>
        /// Maximum length: 50 characters.
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Maximum length: 100 characters.
        /// </summary>
        public required string Name { get; set; }

        public virtual ICollection<User> Users { get; set; } = [];
    }
}
