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
    public class MenuItemRepository : IMenuItemRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public MenuItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetMenuItems()
        {
            return await _context.MenuItems.ToListAsync();
        }

        public async Task<MenuItem> GetMenuItemById(int? menuItemId)
        {
            return await _context.MenuItems.FindAsync(menuItemId);
        }

        public void InsertMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
        }

        public void UpdateMenuItem(MenuItem menuItem)
        {
            _context.Entry(menuItem).State = EntityState.Modified;
        }

        public void DeleteMenuItem(int menuItemId)
        {
            var menuItem = _context.MenuItems.Find(menuItemId);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
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
