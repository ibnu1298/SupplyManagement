using SupplyManagement.Core.Repositories.Interfaces;
using SupplyManagement.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
