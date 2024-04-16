using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class RevenueReport
    {
        public RevenueReport(string employeeEmail)
        {
            StartDate = DateTime.Now;
            EndDate = null;
            EmployeeEmail = employeeEmail;
            Orders = new List<Order>();
        }
        [Key]
        public int RevenueReportId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float? TotalRevenue { get; set; }
        public string EmployeeEmail { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
