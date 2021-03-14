using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICrudDemo.Models;
using WebAPICrudDemo.ViewModel;

namespace WebAPICrudDemo.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeViewModel>> GetEmployees();

        Task<EmployeeViewModel> GetEmployee(int? id);

        Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employee);

        Task<int> DeleteEmployee(int? id);

        Task UpdateEmployee(Employee employee);
    }
}
