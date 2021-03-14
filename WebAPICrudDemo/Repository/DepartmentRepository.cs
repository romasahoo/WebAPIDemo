using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICrudDemo.Models;
using WebAPICrudDemo.ViewModel;

namespace WebAPICrudDemo.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        EmployeeContext context;
        public DepartmentRepository(EmployeeContext _context)
        {
            context = _context;
        }
        public async Task<Department> AddDepartment(Department department)
        {
            if (context != null)
            {
                await context.Departments.AddAsync(department);
                await context.SaveChangesAsync();

                return department;
            }
            return department;
        }

        public async Task<DepartmentViewModel> GetDepartment(int? id)
        {
            if (context != null)
            {
                return await(from d in context.Departments
                             where d.Id == id
                             select new DepartmentViewModel
                             {
                                 Id = d.Id,
                                 DepartmentName = d.DepartmentName

                             }).FirstOrDefaultAsync();
            }

            return null;
        }

        public Task<List<DepartmentViewModel>> GetDepartments()
        {
            throw new NotImplementedException();
        }

        public Task UpdateDepartment(Department department)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteDepartment(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
