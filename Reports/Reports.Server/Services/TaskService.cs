using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Reports.DAL.DTO;
using Reports.DAL.Entities;
using Reports.Server.Database;
using TaskStatus = Reports.DAL.Entities.TaskStatus;

namespace Reports.Server.Services
{
    public class TaskService : ITaskService
    {
        private readonly ReportsDatabaseContext _context;

        public TaskService(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<TaskModel> Create(TaskModel taskModel)
        {
            await _context.Tasks.AddAsync(taskModel);
            await _context.SaveChangesAsync();
            return taskModel;
        }

        public IEnumerable<TaskModel> GetEmployeesTasks()
        {
            return _context.Tasks.ToArray();
        }

        public async Task<TaskModel> Find(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public IEnumerable<TaskModel> Find(DateTime creationTime)
        {
            return _context.Tasks.Where(task => task.CreationTime == creationTime);
        }
        public IEnumerable<TaskModel> FindEmployeeTasks(Guid id)
        {
            return _context.Tasks.Where(task => task.EmployeeId == id);
        }
        
        public async Task<TaskModel> Update(Guid taskId, Guid newEmployeeId)
        {
            if (await _context.Employees.FindAsync(newEmployeeId) is null)
            {
                throw new ArgumentException("Employee with this guid doesnt exists");
            }
            
            TaskModel dbTaskModel = await _context.Tasks.FindAsync(taskId);
            if (dbTaskModel is null)
            {
                throw new ArgumentException("taskModel with this name does not exists");
            }

            dbTaskModel.EmployeeId = newEmployeeId;
            await _context.SaveChangesAsync();
            return dbTaskModel;
        }

        public async Task<TaskModel> Update(Guid taskId, string text)
        {
            TaskModel dbTaskModel = await _context.Tasks.FindAsync(taskId);
            if (dbTaskModel is null)
            {
                throw new ArgumentException("taskModel with this name does not exists");
            }

            dbTaskModel.Text = text;
            await _context.SaveChangesAsync();
            return dbTaskModel;
        }

        public async Task<TaskModel> Update(Guid taskId, TaskStatus taskStatus)
        {
            TaskModel dbTaskModel = await _context.Tasks.FindAsync(taskId);
            if (dbTaskModel is null)
            {
                throw new ArgumentException("taskModel with this name does not exists");
            }

            dbTaskModel.Status = taskStatus;
            await _context.SaveChangesAsync();
            return dbTaskModel;
        }
    }
}