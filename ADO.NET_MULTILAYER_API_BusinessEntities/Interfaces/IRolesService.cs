using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces
{
    public interface IRolesService
    {
        Task<UserSignInResponse> RolesCreation(RolesDTO rolesObj);

    }
}
