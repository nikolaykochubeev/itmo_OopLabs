using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.DAL.DTO
{
    public class ReportDto
    {
        public ReportDto(Guid employeeId, IEnumerable<TaskModel> tasks, ReportStatus reportStatus)
        {
            EmployeeId = employeeId;
            Tasks = tasks;
            Status = reportStatus;
        }
        public Guid EmployeeId { get; }
        public ReportStatus Status { get; }
        public IEnumerable<TaskModel> Tasks { get; }
    }
}