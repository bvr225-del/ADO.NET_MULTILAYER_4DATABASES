using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int empid);
        Task <int>AddEmployee(Employee empdetail);
        Task<bool> UpdateEmployee(Employee empdetail);
        Task<bool> DeleteEmployee(int empid);
    }
}
