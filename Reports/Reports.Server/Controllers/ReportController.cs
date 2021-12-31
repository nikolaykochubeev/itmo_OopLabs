using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.DTO;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/reports")]
    public class ReportController
    {
        private readonly IReportService _service;
        public ReportController(IReportService service)
        {
            _service = service;
        } 
        [HttpPost]
        public async Task<ReportModel> Create([FromBody] ReportDto reportDto)
        {
            var taskModel = new ReportModel(reportDto);
            return await _service.Create(taskModel);
        }
    }
}