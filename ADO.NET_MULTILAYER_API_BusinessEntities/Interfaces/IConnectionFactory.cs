using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces
{
    public interface IConnectionFactory
    {
        SqlConnection hotelmanagement_UATsqlconnectionstring();
        SqlConnection Northwind_DB_UATsqlconnectionstring();

        SqlConnection MIDLAND_UATsqlconnectionstring();
    }
}
