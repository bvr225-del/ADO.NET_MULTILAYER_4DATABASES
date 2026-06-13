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
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<UserSignInResponse> UserResgistration(Users usersObj)
        {
            using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
            {
                var encryptText = EncryptionLibrary.EncryptText(usersObj.Password);

                SqlCommand cmd = new SqlCommand(StoredProcedures.Usp_UserResgistration, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", usersObj.UserName);
                cmd.Parameters.AddWithValue("@Password", encryptText);
                cmd.Parameters.AddWithValue("@EmailId", usersObj.EmailId);
                cmd.Parameters.AddWithValue("@PhoneNumber", usersObj.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", usersObj.Address);
                cmd.Parameters.AddWithValue("@IsActive", usersObj.IsActive);
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

        public async Task<UserSignInResponse> UserRolesMapping(UserRole userRoleObj)
        {
            using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.Usp_UserRolesMapping, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleId",userRoleObj.RoleId);
                cmd.Parameters.AddWithValue("@UserId",userRoleObj.UserId);
                SqlDataAdapter da= new SqlDataAdapter(cmd);
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