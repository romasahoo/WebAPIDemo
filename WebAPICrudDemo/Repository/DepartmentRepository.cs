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

        public async Task<List<DepartmentViewModel>> GetDepartments()
        {
            if (context != null)
            {
                return await (from d in context.Departments
                              select new DepartmentViewModel
                              {
                                  Id = d.Id,
                                  DepartmentName = d.DepartmentName

                              }).ToListAsync();
            }
            return null;
        }

        public async Task<DepartmentViewModel> GetDepartment(int? id)
        {
            if (context != null)
            {
                return await (from d in context.Departments
                              where d.Id == id
                              select new DepartmentViewModel
                              {
                                  Id = d.Id,
                                  DepartmentName = d.DepartmentName

                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<Department> AddDepartment(DepartmentViewModel departmentViewModel)
        {

            Department department = new Department
            {
                DepartmentName = departmentViewModel.DepartmentName
            };
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();

            return department;

        }


        public async Task<Department> UpdateDepartment(DepartmentViewModel departmentViewModel)
        {
            var dbDepartment = await context.Departments.FindAsync(departmentViewModel.Id);
            if (dbDepartment != null)
            {
                dbDepartment.Id = departmentViewModel.Id;
                dbDepartment.DepartmentName = departmentViewModel.DepartmentName;
            }
            await context.SaveChangesAsync();

            return dbDepartment;
        }

        public async Task<int> DeleteDepartment(int? id)
        {
            if (context != null)
            {
                var department = await context.Departments.FirstAsync(x => x.Id == id);

                if (department != null)
                {
                    context.Departments.Remove(department);

                    await context.SaveChangesAsync();
                }
            }

            return Convert.ToInt32(id);
        }


    }
}
