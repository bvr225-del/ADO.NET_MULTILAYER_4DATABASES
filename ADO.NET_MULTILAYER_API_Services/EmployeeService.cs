using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_Services
{
    public class EmployeeService : IEmployeeService
    {
            private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<int> AddEmployee(EmployeeDto empdetail)
        {
            Employee emp = new Employee();
            emp.empid = empdetail.empid;
            emp.empname = empdetail.empname;
            emp.empsalary = empdetail.empsalary;
            var result = await _employeeRepository.AddEmployee(emp);
            return result;

        }

        public async Task<bool> DeleteEmployee(int empid)
        {
           var result=await _employeeRepository.DeleteEmployee(empid);
            return result;
        }

        public async Task<EmployeeDto> GetEmployeeById(int empid)
        {
            var emp = await _employeeRepository.GetEmployeeById(empid);
            EmployeeDto empdetail = new EmployeeDto();
            empdetail.empid = emp.empid;
            empdetail.empname = emp.empname;
            empdetail.empsalary = emp.empsalary;
            return empdetail;
        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            List<EmployeeDto> employeeDetails = new List<EmployeeDto>();
            foreach (var emp in employees)
            {
                EmployeeDto empdetail = new EmployeeDto();
                empdetail.empid = emp.empid;
                empdetail.empname = emp.empname;
                empdetail.empsalary = emp.empsalary;
                employeeDetails.Add(empdetail);
            }
            return employeeDetails;
        }

        public async Task<bool> UpdateEmployee(EmployeeDto empdetail)
        {
            Employee emp = new Employee();
            emp.empid = empdetail.empid;
            emp.empname = empdetail.empname;
            emp.empsalary = empdetail.empsalary;
            var result = await _employeeRepository.UpdateEmployee(emp);
            return result;

        }
    }
}
