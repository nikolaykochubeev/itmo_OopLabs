using System;
using Newtonsoft.Json;
using Reports.DAL.Entities;

namespace Reports.DAL.DTO
{
    public class EmployeeDTO
    {
        [JsonConstructor]
        public EmployeeDTO(Guid id, string name, EmployeeStatus status)
        {
            Id = id;
            Name = name;
            Status = status;
        }
        public Guid Id { get; }
        public string Name { get; }
        public EmployeeStatus Status { get; }
    }
}