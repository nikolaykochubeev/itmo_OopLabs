using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportsDatabaseContext _context;

        public ReportService(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<ReportModel> Create(ReportModel reportModel)
        {
            await _context.Reports.AddAsync(reportModel);
            await _context.SaveChangesAsync();
            return reportModel;
        }

        public IEnumerable<ReportModel> GetEmployeesReports()
        {
            return _context.Reports.ToArray();
        }

        public async Task<ReportModel> FindReportById(Guid id)
        {
            return await _context.Reports.FindAsync(id);
        }
        
        public async Task<IEnumerable<ReportModel>> FindReportByEmployeeId(Guid id)
        {
            return _context.Reports.Where(report => report.EmployeeId == id);
        }

        public IEnumerable<ReportModel> GetWrittenReports()
        {
            return _context.Reports.Where(report => report.Status == ReportStatus.Written);
        }

        public IEnumerable<ReportModel> GetNotWrittenReports()
        {
            return _context.Reports.Where(report => report.Status == ReportStatus.NotWritten);
        }

        public async Task<ReportModel> AddTask(Guid reportId, Guid taskId)
        {
            ReportModel report = await _context.Reports.FindAsync(reportId);
            if (report is null)
            {
                throw new ArgumentException("Report with this guid does not exists");
            }

            TaskModel task = await _context.Tasks.FindAsync(taskId);
            if (report is null)
            {
                throw new ArgumentException("Task with this guid does not exists");
            }

            if (report.Tasks.Contains(task))
            {
                throw new ArgumentException("Task already added in this report");
            }
            report.AddTask(task);
            await _context.SaveChangesAsync();
            return report;
        }
    }
}