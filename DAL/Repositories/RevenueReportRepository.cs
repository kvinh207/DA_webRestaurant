using DAL.Context;
using DAL.IRepositories;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RevenueReportRepository : IRevenueReportRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public RevenueReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RevenueReport>> GetRevenueReports()
        {
            return await _context.RevenueReports.ToListAsync();
        }

        public async Task<RevenueReport> GetRevenueReportById(int? revenueReportId)
        {
            return await _context.RevenueReports.FindAsync(revenueReportId);
        }

        public void InsertRevenueReport(RevenueReport revenueReport)
        {
            _context.RevenueReports.Add(revenueReport);
        }

        public void UpdateRevenueReport(RevenueReport revenueReport)
        {
            _context.Entry(revenueReport).State = EntityState.Modified;
        }

        public void DeleteRevenueReport(int revenueReportId)
        {
            var revenueReport = _context.RevenueReports.Find(revenueReportId);
            if (revenueReport != null)
            {
                _context.RevenueReports.Remove(revenueReport);
            }
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
