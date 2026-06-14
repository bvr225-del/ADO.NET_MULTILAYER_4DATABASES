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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartmentRepository(IConnectionFactory connectionFactory, ILoggingFactory loggingFactory,IHttpContextAccessor httpContextAccessor)
        {
            _connectionFactory = connectionFactory;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddDepartment(Department department)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentRepository: AddDepartment method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: AddDepartment method execution started");
            using (SqlConnection con = _connectionFactory.Northwind_DB_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.AddDepartment, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.DepartmentName, department.deptname);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.DepartmentLocation, department.deptlocation);
                SqlParameter outputParam = new SqlParameter(StoredProcedureParameters.DepartmentInsertValue, SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Department");
                var deptCount = (int)cmd.Parameters[StoredProcedureParameters.DepartmentInsertValue].Value;
                Log.Information("DepartmentRepository: AddDepartment method execution completed");
                await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: AddDepartment method execution completed");

                Log.Information($"DepartmentRepository: AddDepartment method execution completed with DepartmentInsert value: {deptCount}");
                await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentRepository: AddDepartment method execution completed with DepartmentInsert value: {deptCount}");
                return deptCount;
            }

        }

        public async Task<bool> DeleteDepartment(int deptid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentRepository: DeleteDepartment method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: DeleteDepartment method execution started");
            var result = await GetDepartmentById(deptid);
            if (result.deptid > 0)
            {
                using (SqlConnection con = _connectionFactory.Northwind_DB_UATsqlconnectionstring())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedures.DeleteDepartment, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.DepartmentId, deptid);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Department");

                    Log.Information($"DepartmentRepository: DeleteDepartment method execution completed with DepartmentId: {deptid}");
                    await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentRepository: DeleteDepartment method execution completed with DepartmentId: {deptid}");
                    return true;
                }
            }
            else
            {
                    Log.Information($"DepartmentRepository: DeleteDepartment method execution failed. Department not found with deptid: {deptid}");
                    await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentRepository: DeleteDepartment method execution failed. Department not found with deptid: {deptid}");
                return false;
            }
        }
        public async Task<Department> GetDepartmentById(int deptid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentRepository: GetDepartmentById method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName,"information", "DepartmentRepository: GetDepartmentById method execution started");
            Department department = new Department();

            using (SqlConnection con = _connectionFactory.Northwind_DB_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.GetDepartmentById, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.DepartmentId, deptid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Department");
                foreach (DataRow dr in ds.Tables["Department"].Rows)
                {
                    department.deptid = Convert.ToInt32(dr["deptid"]);
                    department.deptname = Convert.ToString(dr["deptname"]);
                    department.deptlocation = Convert.ToString(dr["deptlocation"]);
                }
            }
            Log.Information($"DepartmentRepository: GetDepartmentById method execution ended with DepartmentId:{deptid}");
            await _loggingFactory.AddLoggingMessages(userName,"information", $"DepartmentRepository: GetDepartmentById method ended with DepartmentId:{deptid}");
            return department;

        }

        public async Task<List<Department>> GetDepartments()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentRepository: GetDepartment method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName,"information", "DepartmentRepository: GetDepartment method execution started");
            using (SqlConnection con = _connectionFactory.Northwind_DB_UATsqlconnectionstring())
            {
                SqlCommand cmd = new SqlCommand(StoredProcedures.GetDepartments, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Department");
                List<Department> listDepts = new List<Department>();
                foreach (DataRow dr in ds.Tables["Department"].Rows)
                {
                    Department department = new Department();
                    department.deptid = Convert.ToInt32(dr["deptid"]);
                    department.deptname = Convert.ToString(dr["deptname"]);
                    department.deptlocation = Convert.ToString(dr["deptlocation"]);
                    listDepts.Add(department);
                }
                Log.Information("DepartmentRepository: GetDepartmentById method execution ended");
                await _loggingFactory.AddLoggingMessages(userName,"information", "DepartmentRepository: GetDepartment method execution ended");
                return listDepts;
            }
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentRepository: UpdateDepartment method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName,"information", "DepartmentRepository: UpdateGetDepartment method execution started");
            var result = await GetDepartmentById(department.deptid);
            if (result.deptid > 0)
            {
                using (SqlConnection con = _connectionFactory.Northwind_DB_UATsqlconnectionstring())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateDepartment, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.DepartmentId, department.deptid);
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.DepartmentName, department.deptname);
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.DepartmentLocation, department.deptlocation);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Department");

                    Log.Information("DepartmentRepository: UpdateDepartment method execution ended");
                    await _loggingFactory.AddLoggingMessages(userName,"information", "DepartmentRepository: UpdateDepartment method execution ended");
                    return true;
                }
            }
            else
            {
                Log.Information($"DepartmentRepository: UpdateDepartment method execution failed. Department not found with deptid: {department.deptid}");
                await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentRepository: UpdateDepartment method execution failed. Department not found with deptid: {department.deptid}");
                return false;
            }
        }
    }
}
