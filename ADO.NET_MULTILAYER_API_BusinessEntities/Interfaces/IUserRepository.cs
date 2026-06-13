using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces
{
    public interface IUserRepository
    {
        Task<UserSignInResponse> UserResgistration(Users usersObj);
        Task<UserSignInResponse> UserRolesMapping(UserRole userRoleObj);

    }
}
