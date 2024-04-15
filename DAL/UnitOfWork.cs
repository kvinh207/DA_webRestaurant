using DAL.Context;
using DAL.Repositories;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext _context;
        private BaseRepository<Table> _tableRepository;
        private BaseRepository<MenuItem> _menuItemRepository;
        private BaseRepository<Category> _categoryRepository;
        private BaseRepository<Order> _orderRepository;
        private BaseRepository<Booking> _bookingRepository;
        private BaseRepository<RevenueReport> _revenueReportRepository;

        private bool disposed = false;

        public UnitOfWork(DbContextOptions<ApplicationDbContext> options)
        {
            _context = new ApplicationDbContext(options);
        }

        public BaseRepository<Table> tableRepository
        {
            get
            {
                if(_tableRepository == null)
                {
                    _tableRepository = new BaseRepository<Table>(_context);
                }
                return tableRepository;
            }
        }

        public BaseRepository<MenuItem> menuItemRepository
        {
            get
            {
                if (_menuItemRepository == null)
                {
                    _menuItemRepository = new BaseRepository<MenuItem>(_context);
                }
                return menuItemRepository;
            }
        }
        public BaseRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new BaseRepository<Category>(_context);
                }
                return _categoryRepository;
            }
        }
        public BaseRepository<Order> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new BaseRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }
        public BaseRepository<Booking> BookingRepository
        {
            get
            {
                if (_bookingRepository == null)
                {
                    _bookingRepository = new BaseRepository<Booking>(_context);
                }
                return _bookingRepository;
            }
        }
        public BaseRepository<RevenueReport> RevenueReportRepository
        {
            get
            {
                if (_revenueReportRepository == null)
                {
                    _revenueReportRepository = new BaseRepository<RevenueReport>(_context);
                }
                return _revenueReportRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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
    }
}
