using System;
using Reports.DAL.Entities;

namespace Reports.DAL.DTO
{
    public class TaskDto
    {
        
        public TaskDto(Guid Id, string text, Guid employeeId, TaskStatus status)
        {
            EmployeeId = employeeId;
            Text = text;
            Status = status;
        }
        public TaskDto(string text, Guid employeeId, TaskStatus status)
        {
            EmployeeId = employeeId;
            Text = text;
            Status = status;
        }
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Text { get; set; }
        public TaskStatus Status { get; set; }
    }
}