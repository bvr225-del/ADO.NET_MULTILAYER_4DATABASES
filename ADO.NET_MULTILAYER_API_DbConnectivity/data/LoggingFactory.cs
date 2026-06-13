using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ADO.NET_MULTILAYER_API_DbConnectivity.data
{
    public class LoggingFactory : ILoggingFactory
    {
        private readonly IConnectionFactory _connectionFactory;
        public LoggingFactory(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<bool> AddLoggingMessages(string username, string LogLevel, string MessageTemplate)
        {
            using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.AddLoggingMessages, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.Username, username);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.LogLevel, LogLevel);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.MessageTemplate, MessageTemplate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

        public async Task<bool> AddProjectLevelErrorLogAsync(string StatusCode, string ErrorMessage, string StackTraceError, string InnerExceptionError, string userName)
        {
            using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.AddProjectLevelErrorLog, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.UserName, userName);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.StatusCode, StatusCode);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.ErrorMessage, ErrorMessage);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.StackTraceError, StackTraceError);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.InnerExceptionError, InnerExceptionError);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
