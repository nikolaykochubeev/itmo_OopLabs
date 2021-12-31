using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.DAL.DTO;

namespace Reports.DAL.Entities
{
    public class EmployeeModel
    {
        private EmployeeModel()
        {
        }
        public EmployeeModel(EmployeeDTO employeeDto)
        {
            if (string.IsNullOrWhiteSpace(employeeDto.Name))
            {
                throw new ArgumentNullException(nameof(employeeDto.Name), "Name is invalid");
            }

            Id = Guid.NewGuid();
            Name = employeeDto.Name;
            Status = employeeDto.Status;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EmployeeStatus Status { get; set; }
    }
}