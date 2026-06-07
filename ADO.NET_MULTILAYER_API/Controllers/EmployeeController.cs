using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ADO.NET_MULTILAYER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILoggingFactory _loggingFactory;
        public EmployeeController(IEmployeeService employeeService, ILoggingFactory loggingFactory)
        {
            _employeeService = employeeService;
            _loggingFactory = loggingFactory;
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDto empdetail)
        {
            #region Serilog
            Log.Information("EmployeeController:Employee PosT API method Execution started");
            Log.Information($"EmployeeController:input parameter EmployeeName: {empdetail.empname}");
            Log.Information($"EmployeeController:input parameter EmployeeSalary: {empdetail.empsalary}");
            #endregion
            #region databaselog
            await _loggingFactory.AddLoggingMessages("venkat", "information", "EmployeeController:Employee PosT API method Execution started");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"EmployeeController:input parameter EmployeeName: {empdetail.empname}");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"EmployeeController:input parameter EmployeeSalary: {empdetail.empsalary}");
            #endregion

            throw new Exception("Custom Exception:Employee Controller:Post API method failed");
            var empData = await _employeeService.AddEmployee(empdetail);
            if (empData == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            else
            {
                Log.Information("EmployeeController:Employee PosT API method Execution completed successfully");
                await _loggingFactory.AddLoggingMessages("venkat", "information", "EmployeeController:Employee PosT API method Execution completed successfully");
                return StatusCode(StatusCodes.Status200OK, empData);
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee/{empid}")]
        public async Task<IActionResult> DeleteEmployee(int empid)
        {
            #region serilog
            Log.Information("Employee Controller:Delete API method Execution Started");
            Log.Information($"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion

            #region databaselog
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Employee Controller:Delete API method Execution Started");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion
            var result = await _employeeService.DeleteEmployee(empid);
            if (!result)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Data not found");
            }
            else
            {
                Log.Information("Employee Controller:Delete API method Execution completed successfully");
                await _loggingFactory.AddLoggingMessages("venkat", "information", "Employee Controller:Delete API method Execution completed successfully");
                return StatusCode(StatusCodes.Status200OK, "Employee deleted successfully");
            }
        }
        [HttpGet]
        [Route("GetEmployeeById/{empid}")]
        public async Task<IActionResult> GetEmployeeById(int empid)
        {
            #region serilog
            Log.Information("Employee Controller:GetEmployeeById API method Execution Started");
            Log.Information($"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion

            #region databaselog
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Employee Controller:GetEmployeeById API method Execution Started");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion
            var empData = await _employeeService.GetEmployeeById(empid);
            if (empData == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Data not found");
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, empData);
            }
        }
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            #region serilog
            Log.Information("Employee Controller:GetEmployees API method Execution Started");
            #endregion
            #region databaselog
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Employee Controller:GetEmployees API method Execution Started");
            #endregion
            throw new Exception("Custom Exception:Employee Controller:GetEmployees API method failed");
            var empData = await _employeeService.GetEmployees();
            if (empData == null || empData.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Data not found");
            }
            else
            {
                Log.Information("Employee Controller:GetEmployees API method Execution completed successfully");
                await _loggingFactory.AddLoggingMessages("venkat", "information", "Employee Controller:GetEmployees API method Execution completed successfully");
                return StatusCode(StatusCodes.Status200OK, empData);
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDto empdetail)
        {
            #region serilog
            Log.Information("Employee Controller:UpdateEmployee API method Execution Started");
            Log.Information($"EmployeeController:input parameter EmployeeId: {empdetail.empid}");
            Log.Information($"EmployeeController:input parameter EmployeeName: {empdetail.empname}");
            Log.Information($"EmployeeController:input parameter EmployeeSalary: {empdetail.empsalary}");
            #endregion
            #region databaselog
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Employee Controller:UpdateEmployee API method Execution Started");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"EmployeeController:input parameter EmployeeId: {empdetail.empid}");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"EmployeeController:input parameter EmployeeName: {empdetail.empname}");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"EmployeeController:input parameter EmployeeSalary: {empdetail.empsalary}");
            #endregion
                var result = await _employeeService.UpdateEmployee(empdetail);
                if (!result)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Data not found");
                }
                else
                {
                    Log.Information("Employee Controller:UpdateEmployee API method Execution completed successfully");
                    await _loggingFactory.AddLoggingMessages("venkat", "information", "Employee Controller:UpdateEmployee API method Execution completed successfully");
                    return StatusCode(StatusCodes.Status200OK, "Employee updated successfully");
                }
            }
    }
}