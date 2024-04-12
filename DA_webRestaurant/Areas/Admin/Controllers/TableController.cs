using DA_webRestaurant.Data;
using DA_webRestaurant.Models;
using DA_webRestaurant.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DA_webRestaurant.Areas.Admin.Controllers
{
    [Area(SD.Role_Admin)]
    [Authorize(Roles = SD.Role_Admin)]
    public class TableController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TableController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View(_context.Tables.ToList());
        }


        public IActionResult Details(int id)
        {
            var table = _context.Tables.Find(id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Table table)
        {

            _context.Tables.Add(table);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

       
        }


        public IActionResult Edit(int id)
        {
            var table = _context.Tables.Find(id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }


        [HttpPost]
        public IActionResult Edit(int id, Table table)
        {
            _context.Update(table);
            _context.SaveChanges();
			return RedirectToAction(nameof(Index));
		
        }


        public IActionResult Delete(int id)
        {
            var table = _context.Tables.Find(id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }


        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            var table = _context.Tables.Find(id);
            _context.Tables.Remove(table);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //public async Task<IActionResult> OrderMenuItem(int id)
        //{
        //    var table = await _context.Tables.FindAsync(id);
        //    if (table == null)
        //    {
        //        return NotFound();
        //    }

        //    if (table.Status)
        //    {
        //        return RedirectToAction("Create", "Order", new { tableId = id });
        //    }
        //    else
        //    {
        //        TempData["Message"] = "Bàn hiện không khả dụng để đặt món.";
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> ReserveTable(int? tableId, DateTime? bookingTime, string customerName, string phoneNumber)
        {
            if (tableId == null || bookingTime == null)
            {
                return View("Error");
            }
            var table = await _context.Tables.FindAsync(tableId);
            if (table == null)
            {
                return NotFound();
            }

           
                var booking = new Booking
                {
                    TableId = tableId.Value,
                    BookingTime = bookingTime.Value,
                    CustomerName = customerName,
                    PhoneNumber = phoneNumber,
                    Status = true
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            
              
        }

    }
}
