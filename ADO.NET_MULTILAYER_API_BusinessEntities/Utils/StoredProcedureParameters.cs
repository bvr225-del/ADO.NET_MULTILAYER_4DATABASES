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

    }
}
