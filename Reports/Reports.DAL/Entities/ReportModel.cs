using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.DAL.DTO;

namespace Reports.DAL.Entities
{
    public class ReportModel
    {
        private List<TaskModel> _tasks = new ();

        private ReportModel()
        {
        }

        public ReportModel(ReportDto reportDto)
        {
            Id = Guid.NewGuid();
            EmployeeId = reportDto.EmployeeId;
            Status = reportDto.Status;
        }

        public TaskModel AddTask(TaskModel task)
        {
            if (task is null)
            {
                throw new ArgumentException("Task can not be the null");
            }
            _tasks.Add(task);
            return task;
        }
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public ReportStatus Status { get; set; }
        public IReadOnlyList<TaskModel> Tasks => _tasks;
    }
}