using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.DTO;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }
        [Route("/tasks/create")]
        [HttpPost]
        public async Task<TaskModel> Create([FromBody] TaskDto taskDto)
        {
            return await _service.Create(new TaskModel(taskDto));
        }
        
        [Route("/tasks/findTasks")]
        [HttpGet]
        public IActionResult Find([FromQuery] DateTime creationTime, [FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = _service.Find(id);
                if (result != null)
                {
                    return Ok(result.Result);
                }

                return NotFound();
            }

            if (creationTime != DateTime.MinValue)
            {
                var result = _service.Find(creationTime);
                if (result != null)
                {
                    return Ok(result);
                }
        
                return NotFound();
            }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        
        [Route("/tasks/findEmployeeTasks")]
        [HttpGet]
        public IActionResult FindEmployeeTasks([FromQuery] Guid id)
        {
            var tasks = _service.FindEmployeeTasks(id);
            if (tasks is null)
            {
                return NotFound();
            }
            return Ok(tasks);
        }

        [Route("/tasks/update")]
        [HttpPatch]
        public IActionResult Update([FromBody] TaskDto taskDto)
        {
            return Ok(_service.Update(taskDto.Id, taskDto.EmployeeId, taskDto.Text, taskDto.Status));
        }
    }
}