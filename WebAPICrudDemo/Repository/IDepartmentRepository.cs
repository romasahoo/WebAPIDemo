using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICrudDemo.Models;
using WebAPICrudDemo.ViewModel;

namespace WebAPICrudDemo.Repository
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentViewModel>> GetDepartments();

        Task<DepartmentViewModel> GetDepartment(int? id);

        Task<Department> AddDepartment(Department department);

        Task<int> DeleteDepartment(int? id);

        Task UpdateDepartment(Department department);
    }
}
