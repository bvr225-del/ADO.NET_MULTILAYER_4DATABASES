using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NET_MULTILAYER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDto empdetail)
        {
            try
            {
                var empData = await _employeeService.AddEmployee(empdetail);
                if (empData == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, empData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee/{empid}")]
        public async Task<IActionResult> DeleteEmployee(int empid)
        {
            try
            {
                var result = await _employeeService.DeleteEmployee(empid);
                if (!result)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "Employee deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpGet]
        [Route("GetEmployeeById/{empid}")]
        public async Task<IActionResult> GetEmployeeById(int empid)
        {
            try
            {
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var empData = await _employeeService.GetEmployees();
                if (empData == null || empData.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, empData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDto empdetail)
        {
            try
            {
                var result = await _employeeService.UpdateEmployee(empdetail);
                if (!result)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "Employee updated successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}