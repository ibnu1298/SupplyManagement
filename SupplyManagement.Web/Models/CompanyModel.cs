using SupplyManagement.Shared.Enums;

namespace SupplyManagement.Web.Models
{
    public class CompanyModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string BusinessField { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string CompanyType { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;

        public CompanyStatus Status { get; set; }
    }
}
