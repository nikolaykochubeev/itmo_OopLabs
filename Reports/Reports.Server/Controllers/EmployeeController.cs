using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.DTO;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{   
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
        [Route("/employees/create")]
        [HttpPost]
        public async Task<EmployeeModel> Create([FromBody] EmployeeDTO employeeDto)
        {
            return await _service.Create(new EmployeeModel(employeeDto));
        }
        [Route("/employees/find")]
        [HttpGet]
         public IActionResult Find([FromQuery] string name, [FromQuery] Guid id)
         {
             if (!string.IsNullOrWhiteSpace(name))
             {
                 Task<EmployeeModel> result = _service.FindByName(name);
                 if (result != null)
                 {
                     return Ok(result.Result);
                 }
        
                 return NotFound();
             }

             if (id != Guid.Empty)
             {
                 Task<EmployeeModel> result = _service.FindById(id);
                 if (result != null)
                 {
                     return Ok(result.Result);
                 }

                 return NotFound();
             }

             return StatusCode((int)HttpStatusCode.BadRequest);
         }

         [Route("/employees/delete")]
         [HttpPost]
         public IActionResult Delete([FromQuery] Guid employeeId)
         {
             return Ok(_service.Delete(employeeId).Result);
         }
         
         [Route("/employees/getAllEmployees")]
         [HttpGet]
         public IEnumerable<EmployeeModel> GetAllEmployees()
         {
             return _service.GetEmployees();
         }

         [Route("/employees/update")]
         [HttpPatch]
         public IActionResult Update([FromBody] EmployeeDTO employeeDto)
         {
             return Ok(_service.Update(employeeDto.Id, employeeDto.Name, employeeDto.Status));
         }
         
    }
}