using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Reports.DAL.DTO;
using Reports.DAL.Entities;
using Reports.Server.Controllers;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportsDatabaseContext _context;

        public EmployeeService(ReportsDatabaseContext context) 
        {
            _context = context;
        }

        public async Task<EmployeeModel> Create(EmployeeModel employeeModel)
        {
            await _context.Employees.AddAsync(employeeModel);
            if (employeeModel.Status == EmployeeStatus.Teamlead && _context.Employees.FirstOrDefault(employee => employee.Status == EmployeeStatus.Teamlead) is not null)
            {
                throw new ArgumentException("Teamlead already exists");
            }
            await _context.SaveChangesAsync();
            return employeeModel;
        }

        public async Task<EmployeeModel> FindByName(string name)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<EmployeeModel> FindById(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public IEnumerable<EmployeeModel> GetEmployees()
        {
            return _context.Employees.ToArray();
        }

        public async Task<EmployeeModel> Delete(Guid id)
        {
            EmployeeModel dbEmployeeModel = await _context.Employees.FindAsync(id);
            await _context.Tasks.ForEachAsync(model =>
            {
                if (model.EmployeeId == id)
                {
                    model.EmployeeId = Guid.Empty;
                }
            });
            _context.Employees.Remove(dbEmployeeModel);
            await _context.SaveChangesAsync();
            return dbEmployeeModel;
        }

        public async Task<EmployeeModel> Update(Guid employeeId, string name, EmployeeStatus status)
        {
            EmployeeModel dbEmployeeModel = _context.Employees.Find(employeeId);
            if (dbEmployeeModel is null)
            {
                throw new ArgumentException("Employee with this id does not exists");
            }
            if (status == EmployeeStatus.Teamlead && _context.Employees.FirstOrDefault(employee => employee.Status == EmployeeStatus.Teamlead) is not null)
            {
                throw new ArgumentException("Teamlead already exists");
            }

            dbEmployeeModel.Name = name;
            dbEmployeeModel.Status = status;
            await _context.SaveChangesAsync();
            return dbEmployeeModel;
        }
    }
}