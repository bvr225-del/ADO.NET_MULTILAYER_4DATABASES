using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetDepartments();
        Task<DepartmentDto> GetDepartmentById(int deptid);
        Task<int> AddDepartment(DepartmentDto department);
        Task<bool> UpdateDepartment(DepartmentDto department);
        Task<bool> DeleteDepartment(int deptid);
    }
}
