using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Serilog;

using ADO.NET_MULTILAYER_API_BusinessEntities.Utils;
using ADO.NET_MULTILAYER_API_DbConnectivity.data;
using Microsoft.AspNetCore.Http;
namespace ADO.NET_MULTILAYER_API_Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmployeeRepository(IConnectionFactory connectionFactory, ILoggingFactory loggingFactory,IHttpContextAccessor httpContextAccessor)
        {
            _connectionFactory = connectionFactory;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddEmployee(Employee empdetail)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeRepository: AddEmployees method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages("UserName","information","EmployeeRepository: AddEmployee  method execution starts");
            using (SqlConnection con=_connectionFactory.hotelmanagement_UATsqlconnectionstring())
            { SqlCommand cmd=new SqlCommand(StoredProcedures.AddEmployee, con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeName, empdetail.empname);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.EmplyeeSalary, empdetail.empsalary);
                SqlParameter outputparam=new SqlParameter(StoredProcedureParameters.EmplyeeInsertvalue, SqlDbType.Int);
                outputparam.Direction=ParameterDirection.Output;
                cmd.Parameters.Add(outputparam);
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds=new DataSet();
                da.Fill(ds,"Employee");
                var employeeCount=(int)cmd.Parameters[StoredProcedureParameters.EmplyeeInsertvalue].Value;
                Log.Information("EmployeeRepository: AddEmployee  method execution completed successfully");
                await _loggingFactory.AddLoggingMessages("UserName", "information","EmployeeRepository: AddEmployee  method execution completed successfully");

                Log.Information($"EmployeeRepository:AddEmployee Method executed successfully with EmployeeInsertedId:{employeeCount}");
                await _loggingFactory.AddLoggingMessages("UserName","information",$"EmployeeRepository:AddEmployee Method executed successfully with EmployeeInsertedId:{employeeCount}");
                return employeeCount;

            }
            
        }

        public async Task<bool> DeleteEmployee(int empid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeRepository: DeleteEmployeeById method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages("UserName","information","EmployeeRepository: DeleteEmployee  method execution starts");
            var result = await GetEmployeeById(empid);
            if (result.empid > 0)
            {
                using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedures.DeleteEmployee, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeId, empid);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Employee");
                }
                Log.Information("EmployeeRepository: DeleteEmployee  method execution completed successfully");
                await _loggingFactory.AddLoggingMessages("UserName","information","EmployeeRepository: DeleteEmployee  method execution completed successfully");
                Log.Information($"EmployeeRepository: Employee with id {empid} deleted successfully");
                await _loggingFactory.AddLoggingMessages("UserName","information",$"EmployeeRepository: Employee with id {empid} deleted successfully");
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeRepository: GetEmployees method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages("UserName","information","EmployeeRepository: GetAllEmployees  method execution starts");
            using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.GetEmployees, con);
                cmd.CommandType=CommandType.StoredProcedure;
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds=new DataSet();
                da.Fill(ds,"Employee");
                List<Employee> employees=new List<Employee>();
                foreach(DataRow dr in ds.Tables["Employee"].Rows)
                {
                    Employee emp=new Employee();
                    emp.empid=Convert.ToInt32(dr["empid"]);
                    emp.empname=Convert.ToString(dr["empname"]);
                    emp.empsalary=Convert.ToInt32(dr["empsalary"]);
                    employees.Add(emp);
                }
                    Log.Information("EmployeeRepository: GetAllEmployees  method execution completed successfully");
                    await _loggingFactory.AddLoggingMessages("UserName","information","EmployeeRepository: GetAllEmployees  method execution completed successfully");
                return employees;
            }
        }

        public async Task<Employee> GetEmployeeById(int empid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeRepository: GetEmployeeById method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages("UserName", "information","EmployeeRepository: GetEmployeeById  method execution starts");
            Employee emp = new Employee();

            using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
            { SqlCommand cmd = new SqlCommand(StoredProcedures.GetEmployeeById, con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeId, empid);
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds=new DataSet();
                da.Fill(ds,"Employee");
                foreach(DataRow dr in ds.Tables["Employee"].Rows)
                {
                    emp.empid=Convert.ToInt32(dr["empid"]);
                    emp.empname=Convert.ToString(dr["empname"]);
                    emp.empsalary=Convert.ToInt32(dr["empsalary"]);
                   
                }
            }
            Log.Information("EmployeeRepository: GetEmployeeById  method execution completed successfully");
            await _loggingFactory.AddLoggingMessages("UserName", "information","EmployeeRepository: GetEmployeeById  method execution completed successfully");
            return emp;

        }

        public async Task<bool> UpdateEmployee(Employee empdetail)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            Log.Information($"EmployeeRepository:UpdateEmployee  method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages("UserName", "information","EmployeeRepository: UpdateEmployee  method execution starts");
            var result = await GetEmployeeById(empdetail.empid);
            if (result.empid > 0)
            {

                using (SqlConnection con = _connectionFactory.hotelmanagement_UATsqlconnectionstring())
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateEmployee, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeId, empdetail.empid);
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeName, empdetail.empname);
                    cmd.Parameters.AddWithValue(StoredProcedureParameters.EmplyeeSalary, empdetail.empsalary);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Employee");
                    Log.Information("EmployeeRepository: UpdateEmployee  method execution completed successfully");
                    await _loggingFactory.AddLoggingMessages("UserName", "information","EmployeeRepository: UpdateEmployee  method execution completed successfully");
                    return true;
                }
            }
            else
            {
                Log.Information($"EmployeeRepository: Employee with id {empdetail.empid} not found for update");
                await _loggingFactory.AddLoggingMessages("UserName", "information",$"EmployeeRepository: Employee with id {empdetail.empid} not found for update");
                return false;
            }


        }
    }
}
