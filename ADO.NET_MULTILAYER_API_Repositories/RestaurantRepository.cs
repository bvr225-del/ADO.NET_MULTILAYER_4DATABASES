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
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RestaurantRepository(IConnectionFactory connectionFactory, ILoggingFactory loggingFactory,IHttpContextAccessor httpContextAccessor)
        {
            _connectionFactory = connectionFactory;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddRestaurant(Restaurant Objres)
        {//addorder is used add the data to database by using the stored procedure and the parameters of the stored procedure are passed as the parameters of the command object and the command type is set to stored procedure and then the data adapter is used to fill the dataset with the data from the database and then the dataset is returned to the caller.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Repository:AddRestaurant API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName,"information", "Restaurant Repository:AddRestaurant API method execution started");
            using (SqlConnection con = _connectionFactory.RestaurantDB_UATSqlConnectionString())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.AddRestaurant, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.RestaurantName, Objres.RestaurantName);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.RestaurantLocation, Objres.RestaurantLocation);
                //below code is used to store the stoedprocedure return value.
                SqlParameter outputParam = new SqlParameter(StoredProcedureParameters.RestaurantInsertValue, SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;//if stooredprocedure returns any output params value.by using this process we can return
                cmd.Parameters.Add(outputParam);//need to add output parameter to sqlcommand object.this is the rule.

                SqlDataAdapter Da = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                Da.Fill(dataSet, "Restaurant");//dataset is filled with the data from the database and the name of the datatable is Order
                                               //you can give any name for dataset but it is better to give the name of the table as the name of the datatable in the dataset for better understanding and readability of the code.
                var restaurantCount = (int)cmd.Parameters[StoredProcedureParameters.RestaurantInsertValue].Value;
                Log.Information($"Restaurant Repository:AddRestaurant API method execution ended with RestaurantInsertValue:insertedid ");
                await _loggingFactory.AddLoggingMessages(userName, "information", $"Restaurant Repository:AddRestaurant API method execution ended with RestaurantInsertValue:insertedid ");
                return restaurantCount;
            }
            //return true;
        }

        public async Task<bool> DeleteRestaurant(int Id)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Repository:DeleteRestaurant API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:DeleteRestaurant API method execution started");

            var result = await GetRestaurantById(Id);
            if (result.Id > 0)
            {
                using (SqlConnection con = _connectionFactory.RestaurantDB_UATSqlConnectionString())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedures.DeleteRestaurant, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.ID, Id);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    da.Fill(dataSet);

                }
                Log.Information("Restaurant Repository:DeleteRestaurant API method execution ended succefully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:DeleteRestaurant API method execution ended successfully");

                return true;
            }
            else
            {
                Log.Information($"Restaurant Repository:DeleteRestaurant API method execution failed with RestaurantId:{Id}");
                await _loggingFactory.AddLoggingMessages(userName, "information", $"Restaurant Repository:DeleteRestaurant API method execution failed with RestaurantId:{Id}");

                return false;
            }
        }

        public async Task<List<Restaurant>> GetallRestaurants()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Repository:GetallRestaurants API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:GetallRestaurants API method execution started");


            using (SqlConnection con = _connectionFactory.RestaurantDB_UATSqlConnectionString())
            {
                List<Restaurant> restaurantslist = new List<Restaurant>();
                SqlCommand cmd = new SqlCommand(StoredProcedures.GetRestaurant, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Restaurants");
                foreach (DataRow row in ds.Tables["Restaurants"].Rows)
                {
                    Restaurant res = new Restaurant();
                    res.Id = Convert.ToInt16(row["Id"]);//HERE CONVERTTHE DATA TO INT FORMAT 
                    res.RestaurantName = Convert.ToString(row["RestaurantName"]);//HERE CONVERT THE DATA TO STRING FORMAT
                    res.RestaurantLocation = Convert.ToString(row["RestaurantLocation"]);//HERE CONVERT THE DATA TO STRING FORMAT
                    res.CreationDate = Convert.ToString(row["CreationDate"]);
                    restaurantslist.Add(res);
                }
                Log.Information("Restaurant Repository:GetallRestaurants API method execution ended successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:GetallRestaurants API method execution ended successfully");

                return restaurantslist;
            }

        }

        public async Task<Restaurant> GetRestaurantById(int Id)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Repository:GetRestaurantById API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:GetRestaurantById API method execution started");

            Restaurant res = new Restaurant();
            using (SqlConnection con = _connectionFactory.RestaurantDB_UATSqlConnectionString())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.GetRestaurantById, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.ID, Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Restaurant");
                foreach (DataRow row in ds.Tables["Restaurant"].Rows)
                {

                    res.Id = Convert.ToInt16(row["Id"]);//HERE CONVERTTHE DATA TO INT FORMAT 
                    res.RestaurantName = Convert.ToString(row["RestaurantName"]);//HERE CONVERT THE DATA TO STRING FORMAT
                    res.RestaurantLocation = Convert.ToString(row["RestaurantLocation"]);//HERE CONVERT THE DATA TO STRING FORMAT
                    res.CreationDate = Convert.ToString(row["CreationDate"]);
                }
                Log.Information($"Restaurant Repository:GetRestaurantById API method execution ended with Restaurant id:{Id}");
                await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:GetRestaurantById API method execution ended with Restaurant Id:{Id}");
                return res;
            }

        }

        public async Task<bool> UpdateRestaurant(Restaurant Objres)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Repository: UpdateRestaurant API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository: UpdateRestaurant API method execution started");

            var result = await GetRestaurantById(Objres.Id);
            if (result.Id > 0)
            {

                using (SqlConnection con = _connectionFactory.RestaurantDB_UATSqlConnectionString())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateRestaurant, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.ID, Objres.Id);
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.RestaurantName, Objres.RestaurantName);
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.RestaurantLocation, Objres.RestaurantLocation);
                    SqlDataAdapter Da = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    Da.Fill(dataSet, "Restaurant");
                }
                Log.Information($"Restaurant Repository: UpdateRestaurant API method execution Ended successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository: UpdateRestaurant API method execution ended successfully");

                return true;
            }
            else
            {
                Log.Information($"Restaurant Repository: UpdateRestaurant API method execution failed with RestaurantId:{Objres.Id}");
                await _loggingFactory.AddLoggingMessages(userName, "information", $"Restaurant Repository: UpdateRestaurant API method execution failed with RestaurantId:{Objres.Id}");

                return false;
            }
        }
    }
}
