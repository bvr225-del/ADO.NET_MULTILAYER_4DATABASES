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
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public OrdersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddOrder(Orders order)
        {
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
                return res;
            }

        }

        public async Task<bool> DeleteOrder(int orderid)
        {
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
            return order;


        }

        public async Task<List<Orders>> GetOrders()
        {
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
                return ordersList;
            }



        }

        public async Task<bool> UpdateOrder(Orders order)
        {
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
