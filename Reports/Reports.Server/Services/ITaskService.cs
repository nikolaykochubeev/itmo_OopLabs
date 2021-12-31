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
        Task<TaskModel> Update(Guid taskId, Guid newEmployeeId);
        Task<TaskModel> Update(Guid taskId, string text);
        Task<TaskModel> Update(Guid taskId, TaskStatus taskStatus);
    }
}