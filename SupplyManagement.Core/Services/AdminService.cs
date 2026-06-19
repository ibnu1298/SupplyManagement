using SupplyManagement.Core.Dtos;
using SupplyManagement.Core.Repositories.Interfaces;
using SupplyManagement.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyManagement.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;

        public AdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
       
    }
}
