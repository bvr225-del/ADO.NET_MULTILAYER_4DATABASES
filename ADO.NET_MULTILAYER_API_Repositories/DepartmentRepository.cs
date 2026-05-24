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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public DepartmentRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddDepartment(Department department)
        {
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
                return deptCount;
            }

        }

        public async Task<bool> DeleteDepartment(int deptid)
        {
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
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public async Task<Department> GetDepartmentById(int deptid)
        {
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
            return department;

        }

        public async Task<List<Department>> GetDepartments()
        {
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
                return listDepts;
            }
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
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
