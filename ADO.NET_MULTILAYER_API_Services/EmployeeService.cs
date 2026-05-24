using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using AutoMapper;
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
            private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            this._mapper = mapper;
        }
        public async Task<int> AddEmployee(EmployeeDto empdetail)
        {
            //Employee emp = new Employee();
            //emp.empid = empdetail.empid;
            //emp.empname = empdetail.empname;
            //emp.empsalary = empdetail.empsalary;
            //var result = await _employeeRepository.AddEmployee(emp);
            //return result;
            #region AutoMapper code
            Employee emp = new Employee();
            _mapper.Map(empdetail, emp);
            var result = await _employeeRepository.AddEmployee(emp);
            return result;
            #endregion

        }

        public async Task<bool> DeleteEmployee(int empid)
        {
           var result=await _employeeRepository.DeleteEmployee(empid);
            return result;
        }

        public async Task<EmployeeDto> GetEmployeeById(int empid)
        {
            //var emp = await _employeeRepository.GetEmployeeById(empid);
            //EmployeeDto empdetail = new EmployeeDto();
            //empdetail.empid = emp.empid;
            //empdetail.empname = emp.empname;
            //empdetail.empsalary = emp.empsalary;
            //return empdetail;
            #region AutoMapper code
            var res = await _employeeRepository.GetEmployeeById(empid);
            return _mapper.Map<EmployeeDto>(res);
            #endregion
        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            //var employees = await _employeeRepository.GetAllEmployees();
            //List<EmployeeDto> employeeDetails = new List<EmployeeDto>();
            //foreach (var emp in employees)
            //{
            //    EmployeeDto empdetail = new EmployeeDto();
            //    empdetail.empid = emp.empid;
            //    empdetail.empname = emp.empname;
            //    empdetail.empsalary = emp.empsalary;
            //    employeeDetails.Add(empdetail);
            //}
            //return employeeDetails;
            #region AutoMapper code
            var emp = await _employeeRepository.GetAllEmployees();
            return _mapper.Map<List<EmployeeDto>>(emp);
            #endregion
        }

        public async Task<bool> UpdateEmployee(EmployeeDto empdetail)
        {
            //Employee emp = new Employee();
            //emp.empid = empdetail.empid;
            //emp.empname = empdetail.empname;
            //emp.empsalary = empdetail.empsalary;
            //var result = await _employeeRepository.UpdateEmployee(emp);
            //return result;
            #region AutoMapper code
            Employee emp = new Employee();
            _mapper.Map(empdetail, emp);
            var result = await _employeeRepository.UpdateEmployee(emp);
            return result;
            #endregion

        }
    }
}
