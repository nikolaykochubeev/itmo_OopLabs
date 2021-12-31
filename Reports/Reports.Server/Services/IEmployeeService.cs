using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.DTO;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeModel> Create(EmployeeModel employeeModel);
        Task<EmployeeModel> FindByName(string name);
        Task<EmployeeModel> FindById(Guid id);
        IEnumerable<EmployeeModel> GetEmployees();
        Task<EmployeeModel> Delete(Guid id);
        Task<EmployeeModel> Update(Guid employeeId, string name, EmployeeStatus employeeStatus); 
        
    }
}