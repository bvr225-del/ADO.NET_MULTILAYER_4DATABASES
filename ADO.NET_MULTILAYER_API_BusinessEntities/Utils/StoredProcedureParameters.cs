using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_BusinessEntities.Utils
{
    public static class StoredProcedureParameters
    {
        #region Employee Related Stored Procedure Parameters
        public static string EmployeeId = "@empid";
        public static string EmployeeName = "@empname";
        public static string EmplyeeSalary = "@empsalary";
        public static string EmplyeeInsertvalue ="@insertvalue";
        #endregion

        #region Department Related Stored Procedure Parameters
        public static string DepartmentId = "@deptid";
        public static string DepartmentName = "@deptname";
        public static string DepartmentLocation = "@deptlocation";
        public static string DepartmentInsertValue = "@insertvalue";
        #endregion

        #region Orders Related Stored Procedure Parameters
        public static string OrderId = "@orderid";
        public static string OrderName = "@ordername";
        public static string OrderLocation = "@orderlocation";
        public static string OrderInsertValue = "@insertvalue";
        #endregion

        #region Restaurant Parameters
        public static string ID = "@Id";
        public static string RestaurantName = "@RestaurantName";
        public static string RestaurantLocation = "@RestaurantLocation";
        public static string CreationDate = "@CreationDate";
        public static string RestaurantInsertValue = "@Insertvalue";
        #endregion

        #region ProjectLevelLog Parameters
        public static string Username = "@username";
        public static string LogLevel = "@LogLevel";
        public static string MessageTemplate = "@MessageTemplate";
        #endregion

        #region ProjectLevelErrorLog Parameters
        public static string StatusCode = "@StatusCode";
        public static string ErrorMessage = "@ErrorMessage";
        public static string StackTraceError = "@StackTraceError";
        public static string InnerExceptionError = "@InnerExceptionError";
        public static string UserName = "@UserName";
        #endregion

    }
}
