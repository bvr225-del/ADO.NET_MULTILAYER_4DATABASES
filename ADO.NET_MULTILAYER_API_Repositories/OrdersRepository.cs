using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using ADO.NET_MULTILAYER_API_BusinessEntities.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggerFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersRepository(IConnectionFactory connectionFactory,ILoggingFactory loggerFactory,IHttpContextAccessor httpContextAccessor)
        {
            _connectionFactory = connectionFactory;
            _loggerFactory = loggerFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddOrder(Orders order)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Orders Repository:AddOrder API method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName,"information", "Orders Repository:AddOrder API method execution started");

            using (SqlConnection con = _connectionFactory.MIDLAND_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.AddOrder, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.OrderName, order.ordername);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.OrderLocation, order.orderlocation);
                SqlParameter outputIdParam = new SqlParameter(StoredProcedureParameters.OrderInsertValue, SqlDbType.Int);
                outputIdParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputIdParam);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Orders");
                var res = (int)cmd.Parameters[StoredProcedureParameters.OrderInsertValue].Value;
                Log.Information($"Orders Repository:AddOrder API method execution ended with OrdersInsertValue:insertvalue");
                await _loggerFactory.AddLoggingMessages(userName, "information", $"Orders Repository:AddOrder API method execution ended with OrdersInsertValue:insertvalue");
                return res;
            }
        }
        public async Task<bool> DeleteOrder(int orderid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Orders Repository:DeleteOrder API method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:DeleteOrder API method execution started");
            var result = await GetOrderById(orderid);
            if (result.orderid > 0)
            {
                using (SqlConnection con = _connectionFactory.MIDLAND_UATsqlconnectionstring())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedures.DeleteOrder, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.OrderId, orderid);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Orders");
                    Log.Information("Orders Repository:DeleteOrder API method execution ended");
                    await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:DeleteOrder API method execution ended");
                    return true;
                }
            }
            else
            {

                return false;
            }
        }
        public async Task<Orders> GetOrderById(int orderid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Orders Repository:GetOrderById API method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:GetOrderById API method execution started");
            Orders order = new Orders();
            using (SqlConnection con = _connectionFactory.MIDLAND_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.GetOrderById, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.OrderId, orderid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Orders");
                foreach (DataRow dr in ds.Tables["Orders"].Rows)
                {
                    order.orderid = Convert.ToInt32(dr["orderid"]);
                    order.ordername = Convert.ToString(dr["ordername"]);
                    order.orderlocation = Convert.ToString(dr["orderlocation"]);
                }
            }
            Log.Information("Orders Repository:GetOrderById API method execution ended");
            await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:GetOrderById API method execution completed");

            return order;
        }
        public async Task<List<Orders>> GetOrders()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Orders Repository:GetOrders API method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:GetOrders API method execution started");

            using (SqlConnection con = _connectionFactory.MIDLAND_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.GetOrders, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Orders");
                List<Orders> ordersList = new List<Orders>();
                foreach (DataRow dr in ds.Tables["Orders"].Rows)
                {
                    Orders order = new Orders();
                    order.orderid = Convert.ToInt32(dr["orderid"]);
                    order.ordername = Convert.ToString(dr["ordername"]);
                    order.orderlocation = Convert.ToString(dr["orderlocation"]);
                    ordersList.Add(order);
                }
                Log.Information("Orders Repository:GetOrders API method execution completed");
                await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:GetOrders API method execution completed");

                return ordersList;
            }
        }

        public async Task<bool> UpdateOrder(Orders order)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Orders Repository:UpdateOrder API method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:UpdateOrder API method execution started");

            var result = await GetOrderById(order.orderid);
            if (result.orderid > 0)
            {
                using (SqlConnection con = _connectionFactory.MIDLAND_UATsqlconnectionstring())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateOrder, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.OrderId, order.orderid);
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.OrderName, order.ordername);
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.OrderLocation, order.orderlocation);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Orders");
                    Log.Information("Orders Repository:UpdateOrder API method execution completed");
                    await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:UpdateOrder API method execution completed");

                    return true;
                }
            }
            else
            {
                return false;
            }


        }
    }
}
