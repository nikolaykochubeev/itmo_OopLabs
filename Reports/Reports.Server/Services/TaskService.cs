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
        
        public async Task<TaskModel> Update(Guid taskId, Guid employeeId, string text, TaskStatus taskStatus)
        {
            if (await _context.Employees.FindAsync(employeeId) is null)
            {
                throw new ArgumentException("Employee with this guid doesnt exists");
            }
            
            TaskModel dbTaskModel = await _context.Tasks.FindAsync(taskId);
            if (dbTaskModel is null)
            {
                throw new ArgumentException("taskModel with this name does not exists");
            }

            dbTaskModel.EmployeeId = employeeId;
            dbTaskModel.Text = text;
            dbTaskModel.Status = taskStatus;
            await _context.SaveChangesAsync();
            return dbTaskModel;
        }
    }
}