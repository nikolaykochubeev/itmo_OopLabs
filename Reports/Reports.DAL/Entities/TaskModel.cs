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
            if (string.IsNullOrWhiteSpace(taskDto.Text))
            {
                throw new ArgumentNullException(nameof(taskDto.Text), "Text is invalid");
            }

            Id = Guid.NewGuid();
            Text = taskDto.Text;
            EmployeeId = taskDto.EmployeeId;
            Status = taskDto.Status;
            CreationTime = DateTime.Now;
            LastChangesTime = CreationTime;
        }
        
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid EmployeeId { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastChangesTime { get; set; }
    }
}