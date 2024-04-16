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
            CalculateRevenue();
        }
        [Key]
        public int RevenueReportId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float TotalRevenue { get; private set; }
        [NotMapped]
        public Orders? orders { get; private set; }
        public string EmployeeEmail { get; set; }

        public void CalculateRevenue()
        {
            TotalRevenue = orders.CalculateRevenue();
        }
        [Keyless]
        public class Orders
        {
            public IEnumerable<Order> OrderLists { get; set; } = new List<Order>();

            public float CalculateRevenue()
            {
                float totalRevenue = 0;
                foreach (Order item in OrderLists)
                {
                    totalRevenue += item.TotalPrice;
                }
                return CalculateRevenue();
            }

        }
    }
}
