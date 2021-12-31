using System;
using Reports.DAL.Entities;

namespace Reports.DAL.DTO
{
    public class EmployeeDTO
    {
        public EmployeeDTO(string name, EmployeeStatus employeeStatus)
        {
            Name = name;
            Status = employeeStatus;
        }

        public EmployeeDTO(Guid id, string name, EmployeeStatus employeeStatus)
        {
            Id = id;
            Name = name;
            Status = employeeStatus;
        }
        public Guid Id { get; }
        public string Name { get; }
        public EmployeeStatus Status;
    }
}