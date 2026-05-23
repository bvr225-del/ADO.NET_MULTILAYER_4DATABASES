using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public EmployeeRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddEmployee(Employee empdetail)
        {
            using(SqlConnection con=_connectionFactory.hotelmanagement_UATsqlconnectionstring())
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
                return employeeCount;



            }
            
        }

        public async Task<bool> DeleteEmployee(int empid)
        {
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
                return true;
            }
            else
            {
                return false;
            }


        }

        public async Task<List<Employee>> GetAllEmployees()
        {
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
                return employees;
            }



        }

        public async Task<Employee> GetEmployeeById(int empid)
        {
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
            return emp;



        }

        public async Task<bool> UpdateEmployee(Employee empdetail)
        {
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
