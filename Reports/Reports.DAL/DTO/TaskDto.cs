using System;
using Reports.DAL.Entities;

namespace Reports.DAL.DTO
{
    public class TaskDto
    {
        public TaskDto(Guid id, Guid employeeId, Comment comment, TaskStatus status)
        {
            Id = id;
            EmployeeId = employeeId;
            Comment = comment;
            Status = status;
        }
        
        public Guid Id { get; }
        public Guid EmployeeId { get; }
        public Comment Comment { get; }
        public TaskStatus Status { get; }
    }
}