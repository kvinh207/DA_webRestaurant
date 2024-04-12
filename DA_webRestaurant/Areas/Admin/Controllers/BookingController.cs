using DA_webRestaurant.Data;
using DA_webRestaurant.Models;
using DA_webRestaurant.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DA_webRestaurant.Areas.Admin.Controllers
{
    [Area(SD.Role_Admin)]
    [Authorize(Roles = SD.Role_Admin)]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Create(int tableId)
        {
            var booking = new Booking
            {
                TableId = tableId
            };
            return View(booking);
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            var bookings = _context.Bookings.Include(b => b.Table).ToList();
            return View(bookings);
        }

        public IActionResult CheckOut(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
  
            if (booking.Status)
            {
                return RedirectToAction(nameof(Index)); 
            }
            
            booking.Status = true;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); 
        }

    }
}
