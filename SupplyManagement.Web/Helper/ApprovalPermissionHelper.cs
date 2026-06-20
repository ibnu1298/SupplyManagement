using SupplyManagement.Shared.Enums;

namespace SupplyManagement.Web.Helper
{
    public static class ApprovalPermissionHelper
    {
        public static bool CanViewDetail(CompanyStatus status, string role)
        {
            return (status == CompanyStatus.PendingAdminApproval && role == "ADMIN")
                || (status == CompanyStatus.PendingManagerApproval && role == "MANAGER")
                || status == CompanyStatus.Active;
        }
    }
}
