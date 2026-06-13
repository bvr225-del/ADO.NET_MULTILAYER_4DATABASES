using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository=rolesRepository;
        }
        public async Task<UserSignInResponse> RolesCreation(RolesDTO rolesObj)
        {
            Roles roles = new Roles();
            roles.RoleName = rolesObj.RoleName;
            roles.IsActive = rolesObj.IsActive;
            var result = await _rolesRepository.RolesCreation(roles);
            return result;

        }
    }
}
