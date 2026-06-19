using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SupplyManagement.Shared.Enums
{
    public enum CompanyStatus
    {
        PendingAdminApproval = 1,
        PendingManagerApproval = 2,
        Active = 3,
        Rejected = 4
    }
}
