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
    public class DepartmentsController : ControllerBase
    {
        IDepartmentRepository deptRepository;

        public DepartmentsController(IDepartmentRepository _deptRepository)
        {
            deptRepository = _deptRepository;
        }

        // GET: api/Departments
        [HttpGet]
        [Route("GetDepartments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            try
            {
                var departments = await deptRepository.GetDepartments();
                if (departments == null)
                {
                    return NotFound();
                }

                return Ok(departments);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Departments/5
        [HttpGet]
        [Route("GetDepartment")]
        public async Task<ActionResult<Department>> GetDepartment(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await deptRepository.GetDepartment(id);

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

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (departmentViewModel.Id > 0)
                    {
                        await deptRepository.UpdateDepartment(departmentViewModel);

                        return Ok();
                    }
                    else
                    {
                        return StatusCode(400, "Department Id is not provided");
                    }

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

        // POST: api/Departments
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentObject = await deptRepository.AddDepartment(departmentViewModel);

                    return Ok(departmentObject);
                }
                catch (Exception)
                {
                    if (departmentViewModel.DepartmentName == null)
                    {
                        return StatusCode(400, " Department Name is not provided");
                    }
                }
            }
            return BadRequest();
        }



        // DELETE: api/Departments/5
        [HttpDelete]
        [Route("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int? id)
        {
            int result;

            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                result = await deptRepository.DeleteDepartment(id);
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

        //private bool DepartmentExists(int id)
        //{
        //    return _context.Departments.Any(e => e.Id == id);
        //}
    }
}
