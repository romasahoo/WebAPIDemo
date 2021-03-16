using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICrudDemo;
using WebAPICrudDemo.Models;
using WebAPICrudDemo.Repository;
using WebAPICrudDemo.ViewModel;

namespace WebAPICrudDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeeRepository repository;

        public EmployeesController(IEmployeeRepository _repository)
        {
            repository = _repository;
        }

        // GET: api/Employees
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await repository.GetEmployees();
                if (employees == null)
                {
                    return NotFound();
                }

                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Employees/5
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployee(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await repository.GetEmployee(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await repository.UpdateEmployee(id,employeeViewModel);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        // POST: api/Employees
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeObject = await repository.AddEmployee(employee);
                    
                        return Ok(employeeObject);
                }
                catch (Exception)
                {
                    if(employee.DepartmentName == null)
                    {
                        return StatusCode(400, " Department Name is not provided");
                    }
                    else if (employee.EmployeeName == null)
                    {
                        return StatusCode(400, " Employee Name is not provided");
                    }
                }
            }
            return BadRequest();
        }

        // DELETE: api/Employees/5
        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            int result;

            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                result = await repository.DeleteEmployee(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
