using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_DbConnectivity.data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        public SqlConnection hotelmanagement_UATsqlconnectionstring()
        {
            var connectionString=Convert.ToString(_configuration.GetSection(ConnectionStringNames.HotelManagement_UATsqlconnectionstring).Value);
            SqlConnection con=new SqlConnection(connectionString);
            return con;

        }
    }
}
