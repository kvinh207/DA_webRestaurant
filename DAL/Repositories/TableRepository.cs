using DAL.Context;
using DAL.IRepositories;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TableRepository : ITableRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public TableRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteTable(int tableId)
        {
            var Table = _context.Tables.Find(tableId);
            _context.Tables.Remove(Table);
        }

        public async Task<Table> GetTableById(int? tableId)
        {
            return await _context.Tables.FindAsync(tableId);
        }

        public async Task<List<Table>> GetTables()
        {
            return _context.Tables.ToList();
        }

        public void InsertTable(Table Table)
        {
            _context.Tables.Add(Table);
        }

        public async Task<int> Save()
        {
            return _context.SaveChanges();
        }

        public void UpdateTable(Table Table)
        {
            _context.Entry(Table).State = EntityState.Modified;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
