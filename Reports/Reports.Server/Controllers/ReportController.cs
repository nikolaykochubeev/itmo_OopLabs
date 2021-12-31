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
    [Route("/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;
        public ReportController(IReportService service)
        {
            _service = service;
        } 
        [Route("/reports/create")]
        [HttpPost]
        public async Task<ReportModel> Create([FromBody] ReportDto reportDto)
        {
            return await _service.Create(new ReportModel(reportDto));
        }

        [Route("/reports/getEmployeesReports")]
        [HttpGet]
        public IActionResult GetEmployeesReports()
        {
            return Ok(_service.GetEmployeesReports());
        }

        [Route("/reports/find")]
        [HttpGet]
        public IActionResult Find([FromQuery] Guid employeeId, [FromQuery] Guid reportId)
        {
            if (employeeId != Guid.Empty)
            {
                var result = _service.FindReportByEmployeeId(employeeId);
                if (result != null)
                {
                    return Ok(result.Result);
                }
                return NotFound();
            }
            if (reportId != Guid.Empty)
            {
                var result = _service.FindReportById(reportId);
                if (result != null)
                {
                    return Ok(result.Result);
                }

                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [Route("/reports/getWrittenReports")]
        [HttpGet]
        public IActionResult GetWrittenReports()
        {
            var result = _service.GetWrittenReports();
            return Ok(result);
        }
        
        [Route("/reports/getNotWrittenReports")]
        [HttpGet]
        public IActionResult GetNotWrittenReports()
        {
            var result = _service.GetNotWrittenReports();
            return Ok(result);
        }

        [Route("/reports/AddTask")]
        [HttpPatch]
        public IActionResult AddTask(Guid reportId, Guid taskId)
        {
            return Ok(_service.AddTask(reportId, taskId));
        }
    }
}