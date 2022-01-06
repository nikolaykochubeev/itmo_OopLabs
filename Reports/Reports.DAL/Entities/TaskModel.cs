using System;
using System.ComponentModel.DataAnnotations;
using Reports.DAL.DTO;

namespace Reports.DAL.Entities
{
    public class TaskModel
    {
        private TaskModel()
        {
        }
        public TaskModel(TaskDto taskDto)
        {
            if (taskDto is null)
            {
                throw new ArgumentNullException(nameof(taskDto), "TaskDTO is invalid");
            }

            Id = Guid.NewGuid();
            Comment = taskDto.Comment;
            EmployeeId = taskDto.EmployeeId;
            Status = taskDto.Status;
            CreationTime = DateTime.Now;
            LastChangesTime = CreationTime;
        }
        
        public Guid Id { get; set; }
        public Comment Comment { get; set; }
        public Guid EmployeeId { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastChangesTime { get; set; }
    }
}