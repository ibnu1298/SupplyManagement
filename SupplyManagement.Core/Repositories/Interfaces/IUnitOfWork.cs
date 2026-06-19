using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
