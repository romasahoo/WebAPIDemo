using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public EmployeeRepository(EmployeeContext _context)
        {
            context = _context;

        }
        public async Task<Employee> AddEmployee(EmployeeViewModel employeeModel)
        {

            if (string.IsNullOrWhiteSpace(employeeModel.EmployeeName) || (string.IsNullOrWhiteSpace(employeeModel.DepartmentName)))
            {

                throw new ArgumentNullException($"{nameof(AddEmployee)} entity must not be null");
            }
            else
            {
                try
                {
                    var department = context.Departments.FirstOrDefault(d => d.DepartmentName == employeeModel.DepartmentName);
                    var departmentId = department.Id;

                    Employee employee = new Employee
                    {
                        Id = employeeModel.Id,
                        EmployeeName = employeeModel.EmployeeName,
                        DepartmentId = departmentId,
                    };

                    await context.Employees.AddAsync(employee);
                    await context.SaveChangesAsync();

                    return employee;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{nameof(employeeModel)} could not be saved: {ex.Message}");
                }
            }

        }

        public async Task<int> DeleteEmployee(int? id)
        {
            if (context != null)
            {
                var employee = await context.Employees.FirstAsync(x => x.Id == id);

                if (employee != null)
                {
                    context.Employees.Remove(employee);

                    await context.SaveChangesAsync();
                }
            }

            return Convert.ToInt32(id);
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
            if (context != null)
            {
                return await (from e in context.Employees
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
