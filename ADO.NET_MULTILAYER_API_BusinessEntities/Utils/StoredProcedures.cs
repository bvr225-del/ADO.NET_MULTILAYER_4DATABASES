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

        #region Orders Related Stored Procedures
        public static string GetOrders = "Usp_GetOrders";
        public static string GetOrderById = "Usp_GetOrderById";
        public static string AddOrder = "Usp_AddOrder";
        public static string UpdateOrder = "Usp_UpdateOrder";
        public static string DeleteOrder = "Usp_DeleteOrder";
        #endregion

        #region         #region restaurant storedprocedures
        public static string AddRestaurant = "Usp_AddRestaurant";
        public static string UpdateRestaurant = "Usp_UpdateRestaurant";
        public static string DeleteRestaurant = "Usp_DeleteRestaurant";
        public static string GetRestaurant = "Usp_GetRestaurant";
        public static string GetRestaurantById = "Usp_GetRestaurantById";
        #endregion

        #region ProjectLevelLog Stored Procedures
        public static string AddLoggingMessages = "Usp_ProjectLevelLog";
        #endregion

        #region ProjectLevelErrorLog Stored Procedures
        public static string AddProjectLevelErrorLog = "Usp_AddProjectLevelErrorlog";
        #endregion
    }
}
