using SupplyManagement.DataAccess.Bases;
using SupplyManagement.DataAccess.Models.Security;
using SupplyManagement.Shared.Attributes;
using SupplyManagement.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.DataAccess.Models.Organization
{
    /// <summary>
    /// Represents a company with basic contact information.
    /// <para><b>Important constraints:</b></para>
    /// <list type="bullet">
    ///   <item><description><c>Code</c> is unique and required.</description></item>
    ///   <item><description><c>Name</c> is required.</description></item>
    ///   <item><description>Supports soft deletion via <see cref="EntityBase.RowStatus"/>.</description></item>
    /// </list>
    /// </summary>
    [DatabaseSchema("organization")]
    public class Company : EntityBase
    {
        /// <summary>
        /// Maximum length: 50 characters.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Maximum length: 150 characters.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Maximum length: 15 characters.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Maximum length: 50 characters.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Maximum length: 50 characters.
        /// </summary>
        public string Website { get; set; } = string.Empty;

        /// <summary>
        /// Maximum length: 150 characters.
        /// </summary>
        public string Remarks { get; set; } = string.Empty;

        public required CompanyStatus Status { get; set; }
        /// <summary>
        /// Maximum length: 150 characters.
        /// </summary>
        public string BusinessField { get; set; } = string.Empty;
        /// <summary>
        /// Maximum length: 150 characters.
        /// </summary>
        public string CompanyType { get; set; } = string.Empty;

        public virtual User User { get; set; } = null!;
    }
}
