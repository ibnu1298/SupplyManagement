using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Services.Dtos
{
    public class ApprovalRequestDto
    {
        public bool IsApproved { get; set; }

        public string? Remarks { get; set; }
    }
}
