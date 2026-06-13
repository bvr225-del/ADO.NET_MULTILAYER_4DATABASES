using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ADO.NET_MULTILAYER_API_BusinessEntities.Utils;
namespace ADO.NET_MULTILAYER_API_Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public AuthenticateRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<UserRolesInformationResponse> GetUserRolesInformation(LoginDTO loginDTOObj)
        {
            using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.GetUserRolesInformation, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", loginDTOObj.UserName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                UserRolesInformationResponse response = null;

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    response = new UserRolesInformationResponse
                    {
                        UserName = Convert.ToString(row["UserName"]),
                        EmailId = Convert.ToString(row["EmailId"]),
                        PhoneNumber = Convert.ToString(row["PhoneNumber"]),
                        Address = Convert.ToString(row["Address"]),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        RoleName = Convert.ToString(row["RoleName"])
                    };
                }

                return response;
            }
        }
        public async Task<UserSignInResponse> UserSignIn(LoginDTO loginDTOObj)
        {
            using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
            {
                var encryptText = EncryptionLibrary.EncryptText(loginDTOObj.Password);

                SqlCommand cmd = new SqlCommand(StoredProcedures.SignIn,con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", loginDTOObj.UserName);
                cmd.Parameters.AddWithValue("Password", encryptText);
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