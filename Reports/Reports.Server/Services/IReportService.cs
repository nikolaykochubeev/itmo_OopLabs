using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        Task<ReportModel> Create(ReportModel reportModel);
        IEnumerable<ReportModel> GetEmployeesReports();
        Task<ReportModel> FindReportById(Guid id);
        Task<IEnumerable<ReportModel>> FindReportByEmployeeId(Guid id);
        IEnumerable<ReportModel> GetWrittenReports();
        IEnumerable<ReportModel> GetNotWrittenReports(); 
        Task<ReportModel> AddTask(Guid reportId, Guid taskId);
    }
}