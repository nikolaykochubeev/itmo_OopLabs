using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.DTO;
using Reports.DAL.Entities;
using TaskStatus = Reports.DAL.Entities.TaskStatus;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        Task<TaskModel> Create(TaskModel taskModel);
        IEnumerable<TaskModel> GetEmployeesTasks();
        Task<TaskModel> Find(Guid id);
        IEnumerable<TaskModel> Find(DateTime creationTime);
        IEnumerable<TaskModel> FindEmployeeTasks(Guid id);
        IEnumerable<Comment> GetAllTaskComments(Guid id);
        Task<TaskModel> Update(Guid taskId, Guid employeeId, Comment comment, TaskStatus taskStatus);
    }
}