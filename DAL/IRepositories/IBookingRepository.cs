using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IBookingRepository : IDisposable
    {
        Task<List<Booking>> GetBookings();
        Task<Booking> GetBookingById(int? tableId);
        void InsertBooking(Booking Booking);
        void UpdateBooking(Booking Booking);
        void DeleteBooking(int BookingId);
        Task<int> Save();
    }
}
