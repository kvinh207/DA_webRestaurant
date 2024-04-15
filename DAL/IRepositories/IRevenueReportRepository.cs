using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IRevenueReportRepository : IDisposable
    {
        Task<List<RevenueReport>> GetRevenueReports();
        Task<RevenueReport> GetRevenueReportById(int? tableId);
        void InsertRevenueReport(RevenueReport RevenueReport);
        void UpdateRevenueReport(RevenueReport RevenueReport);
        void DeleteRevenueReport(int RevenueReportId);
        Task<int> Save();
    }
}
