using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using ADO.NET_MULTILAYER_API_BusinessEntities.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ADO.NET_MULTILAYER_API_Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public RolesRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<UserSignInResponse> RolesCreation(Roles rolesObj)
        {
            using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.Usp_RolesResgistration, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleName", rolesObj.RoleName);
                cmd.Parameters.AddWithValue("@IsActive", rolesObj.IsActive);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                UserSignInResponse response = null;

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    response = new UserSignInResponse
                    {
                        StatusCode = Convert.ToString(row["StatusCode"]),
                        StatusMessage = row["StatusMessage"].ToString()
                    };
                }

                return response;
            }
        }
    }
}