using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICrudDemo.Models;
using WebAPICrudDemo.ViewModel;

namespace WebAPICrudDemo.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeContext context;
        IDepartmentRepository deptRepository;

        public EmployeeRepository(EmployeeContext _context)
        {
            context = _context;
            
        }
        public async Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employeeModel)
        {

                if(string.IsNullOrWhiteSpace(employeeModel.EmployeeName) || (string.IsNullOrWhiteSpace(employeeModel.DepartmentName)))
                {
                   
                    throw new ArgumentNullException($"{nameof(AddEmployee)} entity must not be null");
                }
                else
                {

                try
                {
                    //if (employeeModel.DepartmentId == 0)
                    //{
                    //    Department department = new Department();
                    //    department.DepartmentName = employeeModel.DepartmentName;
                    //    await deptRepository.AddDepartment(department);
                    //}
                    Employee employee = new Employee
                    {
                        Id = employeeModel.Id,
                        EmployeeName = employeeModel.EmployeeName,
                        DepartmentId = employeeModel.DepartmentId
                    };

                    await context.Employees.AddAsync(employee);
                    await context.SaveChangesAsync();

                    return employeeModel;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{nameof(employeeModel)} could not be saved: {ex.Message}");
                }
            }

        }

        public async Task<int> DeleteEmployee(int? id)
        {
            int result = 0;

            if (context != null)
            {
                var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);

                if (employee != null)
                {
                    context.Employees.Remove(employee);
                   
                    result = await context.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<EmployeeViewModel> GetEmployee(int? id)
        {
            if (context != null)
            {
                return await (from e in context.Employees
                              from d in context.Departments
                              where e.Department.Id == id
                              select new EmployeeViewModel
                              {
                                  Id = e.Id,
                                  EmployeeName = e.EmployeeName,
                                  DepartmentId = e.Department.Id,
                                  DepartmentName = e.Department.DepartmentName

                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<EmployeeViewModel>> GetEmployees()
        {
            if(context != null)
            {
                return await(from e in context.Employees
                             select new EmployeeViewModel
                             {
                                 Id = e.Id,
                                 EmployeeName = e.EmployeeName,
                                 DepartmentId = e.DepartmentId,
                                 DepartmentName = e.Department.DepartmentName
                             }).ToListAsync();
            }
            return null;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            if (context != null)
            {
                context.Employees.Update(employee);
                await context.SaveChangesAsync();
            }
        }
    }
}
