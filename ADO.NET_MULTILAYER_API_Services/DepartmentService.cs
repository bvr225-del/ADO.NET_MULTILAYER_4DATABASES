using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using AutoMapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly ILoggingFactory _loggerFactory;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, ILoggingFactory loggerFactory)
        {
            _departmentRepository = departmentRepository;
            this._mapper = mapper;
            _loggerFactory = loggerFactory;
        }
        public async Task<int> AddDepartment(DepartmentDto department)
        {
            Log.Information("DepartmentService:AddDepartment method execution started");
            await _loggerFactory.AddLoggingMessages("venkat","information", "DepartmentService:AddDepartment method execution started");
            Department dept = new Department();
            _mapper.Map(department, dept);
            var res= await _departmentRepository.AddDepartment(dept);
            Log.Information("DepartmentService:AddDepartment method execution completed");
            await _loggerFactory.AddLoggingMessages("venkat", "information", "DepartmentService:AddDepartment method execution completed");

            return res;

        }

        public async Task<bool> DeleteDepartment(int deptid)
        {
            Log.Information("DepartmentService: DeleteDepartment method execution started");
            await _loggerFactory.AddLoggingMessages("venkat", "information", "DepartmentService:DeleteDepartment method execution started");

            var result = await _departmentRepository.DeleteDepartment(deptid);
            Log.Information("DepartmentService:DeleteDepartment method execution ended");
            await _loggerFactory.AddLoggingMessages("venkat", "information", "DepartmentService:DeleteDepartment method execution ended");

            return result;

        }

        public async Task<DepartmentDto> GetDepartmentById(int deptid)
        {
            Log.Information("DepartmentService:GetDepartmentById method execution started");
            await _loggerFactory.AddLoggingMessages("venkat", "information", "DepartmentService:GetDepartmentById method execution started");

            var res = await _departmentRepository.GetDepartmentById(deptid);
            Log.Information("DepartmentService:GetDepartmentById method execution ended");
            await _loggerFactory.AddLoggingMessages("venkat", "information", "DepartmentService:GetDepartmentById method execution ended");

            return _mapper.Map<DepartmentDto>(res);

        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            Log.Information("DepartmentService:GetDepartment method execution started");
            await _loggerFactory.AddLoggingMessages("venkat", "information", "DepartmentService:GetDepartment method execution started");

            var res = await _departmentRepository.GetDepartments();
            Log.Information("DepartmentService:GetDepartment method execution ended");
            await _loggerFactory.AddLoggingMessages("venkat", "information", "DepartmentService:GetDepartment method execution ended");

            return _mapper.Map<List<DepartmentDto>>(res);
        }

        public async Task<bool> UpdateDepartment(DepartmentDto department)
        {
            Log.Information("DepartmentService:UpdateDepartment method execution started");
            await _loggerFactory.AddLoggingMessages("venkat", "information", "DepartmentService:UpdateDepartment method execution started");

            Department dept = new Department();
            _mapper.Map(department, dept);
            var result = await _departmentRepository.UpdateDepartment(dept);
            Log.Information("DepartmentService:UpdateDepartment method execution ended");
            await _loggerFactory.AddLoggingMessages("venkat", "information", "DepartmentService:UpdateDepartment method execution ended");

            return result;
        }
    }
}
