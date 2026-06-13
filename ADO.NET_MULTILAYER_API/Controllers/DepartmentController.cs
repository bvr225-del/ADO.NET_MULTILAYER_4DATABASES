using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ADO.NET_MULTILAYER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILoggingFactory _loggingFactory;
        public DepartmentController(IDepartmentService departmentService, ILoggingFactory loggingFactory)
        {
            _departmentService = departmentService;
            _loggingFactory = loggingFactory;
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> AddDepartment(DepartmentDto department)
        {
            #region serilog
            Log.Information("DepartmentController: AddDepartment method Execution starts");
            Log.Information($"DepartmentController: AddDepartment method input parameter DepartmentName: {department.deptname}");
            Log.Information($"DepartmentController: AddDepartment method input parameter DepartmentLocation: {department.deptlocation}");
            #endregion
            #region database log
            await _loggingFactory.AddLoggingMessages("venkat","information","DepartmentController: AddDepartment method Execution starts");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"DepartmentController: AddDepartment method input parameter DepartmentName: {department.deptname}");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"DepartmentController: AddDepartment method input parameter DepartmentLocation: {department.deptlocation}");
            #endregion

            //throw new Exception("Custom Exception:Employee Controller:Post API method failed");

            var res = await _departmentService.AddDepartment(department);
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                Log.Information("DepartmentController: AddDepartment method Execution ended successfully");
                await _loggingFactory.AddLoggingMessages("venkat", "information", "DepartmentController: AddDepartment method Execution ended successfully");

                return StatusCode(StatusCodes.Status200OK, "created successfully");
                }
            }
        [HttpDelete]
        [Route("DeleteDepartment/{deptid}")]
        public async Task<IActionResult> DeleteDepartment(int deptid)
        {
            #region serilog
            Log.Information("DepartmentController: DeleteDepartment method Execution starts");
            Log.Information($"DepartmentController: DeleteDepartment method input parameter DepartmentId: {deptid}");
            #endregion

            #region database log
            await _loggingFactory.AddLoggingMessages("venkat", "information", "DepartmentController: DeleteDepartment method Execution starts");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"DepartmentController: DeleteDepartment method input parameter DepartmentId: {deptid}");
            #endregion
            //throw new Exception("Custom Exception:Employee Controller:Post API method failed");
            var res = await _departmentService.DeleteDepartment(deptid);
                if (res == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                Log.Information("DepartmentController: DeleteDepartment method Execution ended successfully");
                await _loggingFactory.AddLoggingMessages("venkat", "information", "DepartmentController: DeleteDepartment method Execution ended successfully");

                return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
        [HttpGet]
        [Route("GetDepartmentById/{deptid}")]
        public async Task<IActionResult> GetDepartmentById(int deptid)
        {
            #region serilog
            Log.Information("DepartmentController: GetDepartmentById method Execution starts");
            Log.Information($"DepartmentController: GetDepartmentById method input parameter DepartmentId: {deptid}");
            #endregion

            #region database log
            await _loggingFactory.AddLoggingMessages("venkat", "information", "DepartmentController: GetDepartmentById method Execution starts");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"DepartmentController: GetDepartmentById method input parameter DepartmentId: {deptid}");
            #endregion

            //throw new Exception("Custom Exception:Employee Controller:Post API method failed");

            var res = await _departmentService.GetDepartmentById(deptid);
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                Log.Information("DepartmentController: GetDepartmentById method Execution ended successfully");
                await _loggingFactory.AddLoggingMessages("venkat", "information", "DepartmentController: GetDepartmentById method Execution ended successfully");
                return StatusCode(StatusCodes.Status200OK, res);
                }
        }
        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            #region serilog
            Log.Information("DepartmentController: GetDepartments method Execution starts");
            #endregion

            #region database log
            await _loggingFactory.AddLoggingMessages("venkat", "information", "DepartmentController: GetDepartments method Execution starts");
            #endregion

            throw new Exception("Custom Exception:Employee Controller:Post API method failed");

            var res = await _departmentService.GetDepartments();
                if (res == null || res.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                Log.Information("DepartmentController: GetDepartments method Execution ended successfully");
                await _loggingFactory.AddLoggingMessages("venkat", "information", "DepartmentController: GetDepartments method Execution ended successfully");
                return StatusCode(StatusCodes.Status200OK, res);
                }
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DepartmentDto department)
        {
            #region serilog
            Log.Information("DepartmentController: UpdateDepartment method Execution starts");
            Log.Information($"DepartmentController: UpdateDepartment method input parameter DepartmentId: {department.deptid}");
            Log.Information($"DepartmentController: UpdateDepartment method input parameter DepartmentName: {department.deptname}");
            Log.Information($"DepartmentController: UpdateDepartment method input parameter DepartmentLocation: {department.deptlocation}");
            #endregion

            #region database log
            await _loggingFactory.AddLoggingMessages("venkat", "information", "DepartmentController: UpdateDepartment method Execution starts");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"DepartmentController: UpdateDepartment method input parameter DepartmentId: {department.deptid}");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"DepartmentController: UpdateDepartment method input parameter DepartmentName: {department.deptname}");
            await _loggingFactory.AddLoggingMessages("venkat", "information", $"DepartmentController: UpdateDepartment method input parameter DepartmentLocation: {department.deptlocation}");
            #endregion

            throw new Exception("Custom Exception:Employee Controller:Post API method failed");
            var res = await _departmentService.UpdateDepartment(department);
                if (res == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                Log.Information("DepartmentController: UpdateDepartment method Execution ended successfully");
                await _loggingFactory.AddLoggingMessages("venkat", "information", "DepartmentController: UpdateDepartment method Execution ended successfully");
                return StatusCode(StatusCodes.Status200OK, "updated successfully");
                }
        }
    }
}