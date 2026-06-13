using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace ADO.NET_MULTILAYER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeController(IEmployeeService employeeService, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {
            _employeeService = employeeService;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDto empdetail)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region Serilog
            Log.Information($"EmployeeController: Post Api method Excution Starts and Current Loggedin username:{userName}");
            Log.Information($"EmployeeController:input parameter EmployeeName: {empdetail.empname}");
            Log.Information($"EmployeeController:input parameter EmployeeSalary: {empdetail.empsalary}");
            #endregion
            #region databaselog
            await _loggingFactory.AddLoggingMessages("UserName", "information", "EmployeeController:Employee PosT API method Execution started");
            await _loggingFactory.AddLoggingMessages("UserName", "information", $"EmployeeController:input parameter EmployeeName: {empdetail.empname}");
            await _loggingFactory.AddLoggingMessages("UserName", "information", $"EmployeeController:input parameter EmployeeSalary: {empdetail.empsalary}");
            #endregion

            throw new Exception("Custom Exception:Employee Controller:Post API method failed");
            var empData = await _employeeService.AddEmployee(empdetail);
            if (empData == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "data not found");
            }
            else
            {
                Log.Information("EmployeeController:Employee PosT API method Execution completed successfully");
                await _loggingFactory.AddLoggingMessages("UserName", "information", "EmployeeController:Employee PosT API method Execution completed successfully");
                return StatusCode(StatusCodes.Status200OK, empData);
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee/{empid}")]
        public async Task<IActionResult> DeleteEmployee(int empid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"EmployeeController: delete Api method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            Log.Information($"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion

            #region databaselog
            await _loggingFactory.AddLoggingMessages("UserName", "information", "Employee Controller:Delete API method Execution Started");
            await _loggingFactory.AddLoggingMessages("UserName", "information", $"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion
            var result = await _employeeService.DeleteEmployee(empid);
            if (!result)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Data not found");
            }
            else
            {
                Log.Information("Employee Controller:Delete API method Execution completed successfully");
                await _loggingFactory.AddLoggingMessages("UserName", "information", "Employee Controller:Delete API method Execution completed successfully");
                return StatusCode(StatusCodes.Status200OK, "Employee deleted successfully");
            }
        }
        [HttpGet]
        [Route("GetEmployeeById/{empid}")]
        public async Task<IActionResult> GetEmployeeById(int empid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"EmployeeController: Get Api method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            Log.Information($"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion

            #region databaselog
            await _loggingFactory.AddLoggingMessages("UserName", "information", "Employee Controller:GetEmployeeById API method Execution Started");
            await _loggingFactory.AddLoggingMessages("UserName", "information", $"EmployeeController:passed Input Parameter EmployeeId:{empid}");
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
            //To read  the token from postman/react/angular/mobile application we used below code 
            //===================First way of read token properties(Just understanding purpose)=================================================
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authHeader.Replace("Bearer ", "");


            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(token);
            Dictionary<string, string> obj = new Dictionary<string, string>();
            foreach (var claim in jwtToken.Claims)
            {
                obj.Add(claim.Type, claim.Value);
            }

            var totaldata = obj;

            //====================Second way of read token properties(Just  understanding purpose)===================
            var userName1 = User.FindFirst("UserName")?.Value;

            var email = User.FindFirst("EmailId")?.Value;

            var phone = User.FindFirst("PhoneNumber")?.Value;

            var address = User.FindFirst("Address")?.Value;

            var isActive = User.FindFirst("IsActive")?.Value;

            var role = User.FindFirst("Roles")?.Value;
            //=================================
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"EmployeeController: GetEmployees Api method Excution Starts and Current Loggedin username:{userName}");
            #endregion
            #region databaselog
            await _loggingFactory.AddLoggingMessages("UserName", "information", "Employee Controller:GetEmployees API method Execution Started");
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
                await _loggingFactory.AddLoggingMessages("UserName", "information", "Employee Controller:GetEmployees API method Execution completed successfully");
                return StatusCode(StatusCodes.Status200OK, empData);
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDto empdetail)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"EmployeeController: put Api method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            Log.Information($"EmployeeController:input parameter EmployeeId: {empdetail.empid}");
            Log.Information($"EmployeeController:input parameter EmployeeName: {empdetail.empname}");
            Log.Information($"EmployeeController:input parameter EmployeeSalary: {empdetail.empsalary}");
            #endregion
            #region databaselog
            await _loggingFactory.AddLoggingMessages("UserName", "information", "Employee Controller:UpdateEmployee API method Execution Started");
            await _loggingFactory.AddLoggingMessages("UserName", "information", $"EmployeeController:input parameter EmployeeId: {empdetail.empid}");
            await _loggingFactory.AddLoggingMessages("UserName", "information", $"EmployeeController:input parameter EmployeeName: {empdetail.empname}");
            await _loggingFactory.AddLoggingMessages("UserName", "information", $"EmployeeController:input parameter EmployeeSalary: {empdetail.empsalary}");
            #endregion
                var result = await _employeeService.UpdateEmployee(empdetail);
                if (!result)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Data not found");
                }
                else
                {
                    Log.Information("Employee Controller:UpdateEmployee API method Execution completed successfully");
                    await _loggingFactory.AddLoggingMessages("UserName", "information", "Employee Controller:UpdateEmployee API method Execution completed successfully");
                    return StatusCode(StatusCodes.Status200OK, "Employee updated successfully");
                }
            }
    }
}