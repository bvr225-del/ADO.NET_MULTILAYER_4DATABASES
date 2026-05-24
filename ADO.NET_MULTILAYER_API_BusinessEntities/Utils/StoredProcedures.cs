using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_BusinessEntities.Utils
{
    public static class StoredProcedures
    {
        #region Employee Related Stored Procedures
        public static string GetEmployees = "Usp_GetEmployee";
        public static string GetEmployeeById = "Usp_GetEmployeeById";
        public static string AddEmployee = "Usp_AddEmployee";
        public static string UpdateEmployee = "Usp_UpdateEmployee";
        public static string DeleteEmployee = "Usp_DeleteEmployee";
        #endregion
        #region Department Related StoredProcedures
        public static string AddDepartment = "Usp_AddDepartment";
        public static string GetDepartments = "Usp_GetDepartments";
        public static string GetDepartmentById = "Usp_GetDepartmentById";
        public static string UpdateDepartment = "Usp_UpdateDepartment";
        public static string DeleteDepartment = "Usp_DeleteDepartment";
        #endregion


    }
}
