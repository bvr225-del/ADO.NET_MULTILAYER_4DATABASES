using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployeeById(int empid);
        Task<int> AddEmployee(EmployeeDto empdetail);
        Task<bool> UpdateEmployee(EmployeeDto empdetail);
        Task<bool> DeleteEmployee(int empid);
    }
}
