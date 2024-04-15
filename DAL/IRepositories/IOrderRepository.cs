using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IOrderRepository : IDisposable
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(int? tableId);
        void InsertOrder(Order Order);
        void UpdateOrder(Order Order);
        void DeleteOrder(int OrderId);
        Task<int> Save();
    }
}
