using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SupplyManagement.Shared.Enums
{
    public enum CompanyStatus
    {
        [Display(Name = "Pending Admin Approval")]
        PendingAdminApproval = 1,
        [Display(Name = "Pending Manager Approval")]
        PendingManagerApproval = 2,
        Active = 3,
        Rejected = 4
    }
}
