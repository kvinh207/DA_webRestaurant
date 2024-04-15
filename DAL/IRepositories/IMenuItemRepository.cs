using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IMenuItemRepository : IDisposable
    {
        Task<List<MenuItem>> GetMenuItems();
        Task<MenuItem> GetMenuItemById(int? tableId);
        void InsertMenuItem(MenuItem MenuItem);
        void UpdateMenuItem(MenuItem MenuItem);
        void DeleteMenuItem(int MenuItemId);
        Task<int> Save();
    }
}
