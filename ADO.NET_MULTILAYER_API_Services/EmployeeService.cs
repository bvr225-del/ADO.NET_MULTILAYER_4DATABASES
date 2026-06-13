using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace ADO.NET_MULTILAYER_API_Services
{
    public class EmployeeService : IEmployeeService
    {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMapper _mapper;
        private readonly ILoggingFactory _loggingFactory;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, ILoggingFactory loggingFactory   )
        {
            _employeeRepository = employeeRepository;
            this._mapper = mapper;
            this._loggingFactory = loggingFactory;
        }
        public async Task<int> AddEmployee(EmployeeDto empdetail)
        {
            Log.Information("EmployeeService: AddEmployee method execution started.");
            await _loggingFactory.AddLoggingMessages("venkat","information","EmployeeService: AddEmployee method execution started.");
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
            Log.Information("EmployeeService: AddEmployee method execution completed.");
                await _loggingFactory.AddLoggingMessages("venkat", "information", "EmployeeService: AddEmployee method execution completed.");
            return result;
            #endregion

        }

        public async Task<bool> DeleteEmployee(int empid)
        {
            Log.Information("EmployeeService: DeleteEmployee method execution started.");
                await _loggingFactory.AddLoggingMessages("venkat","information","EmployeeService: DeleteEmployee method execution started.");
            var result=await _employeeRepository.DeleteEmployee(empid);
            Log.Information("EmployeeService: DeleteEmployee method execution completed.");
                await _loggingFactory.AddLoggingMessages("venkat","information","EmployeeService: DeleteEmployee method execution completed.");
            return result;
        }

        public async Task<EmployeeDto> GetEmployeeById(int empid)
        {
            Log.Information("EmployeeService: GetEmployeeById method execution started.");
                await _loggingFactory.AddLoggingMessages("venkat","information","EmployeeService: GetEmployeeById method execution started.");
            //var emp = await _employeeRepository.GetEmployeeById(empid);
            //EmployeeDto empdetail = new EmployeeDto();
            //empdetail.empid = emp.empid;
            //empdetail.empname = emp.empname;
            //empdetail.empsalary = emp.empsalary;
            //return empdetail;
            #region AutoMapper code
            var res = await _employeeRepository.GetEmployeeById(empid);
            Log.Information("EmployeeService: GetEmployeeById method execution completed.");
                await _loggingFactory.AddLoggingMessages("venkat","information","EmployeeService: GetEmployeeById method execution completed.");
            return _mapper.Map<EmployeeDto>(res);
            #endregion
        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            Log.Information("EmployeeService: GetEmployees method execution started.");
                await _loggingFactory.AddLoggingMessages("venkat","information","EmployeeService: GetEmployees method execution started.");
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
            Log.Information("EmployeeService: GetEmployees method execution completed.");
                await _loggingFactory.AddLoggingMessages("venkat","information","EmployeeService: GetEmployees method execution completed.");
            return _mapper.Map<List<EmployeeDto>>(emp);
            #endregion
        }

        public async Task<bool> UpdateEmployee(EmployeeDto empdetail)
        {
            Log.Information("EmployeeService: UpdateEmployee method execution started.");
                await _loggingFactory.AddLoggingMessages("venkat","information","EmployeeService: UpdateEmployee method execution started.");
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
            Log.Information("EmployeeService: UpdateEmployee method execution completed.");
                await _loggingFactory.AddLoggingMessages("venkat","information","EmployeeService: UpdateEmployee method execution completed.");
            return result;
            #endregion

        }
    }
}
