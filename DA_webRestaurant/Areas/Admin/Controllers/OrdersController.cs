using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using Entity;
using DA_webRestaurant.Areas.Admin.ViewModel;
using Newtonsoft.Json;
using DAL;

namespace DA_webRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public OrdersController(ApplicationDbContext context, UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

   
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

  
        public IActionResult Create()
        {
            var viewModel = new OrderViewModel
            {
                MenuItemOptions = _context.MenuItems
                .Select(mi => new SelectListItem
                {
                    Value = mi.MenuItemId.ToString(),
                    Text = mi.ItemName
                })
                .ToList()
            };

            // Retrieve menu items from the database and populate MenuItemOptions
            return View(viewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDate,OrderItems")] Order order, string registeredItems)
        {
            var itemlist = new List<MenuItem>();
            if (!string.IsNullOrEmpty(registeredItems))
            {
                List<int> selectedItems = JsonConvert.DeserializeObject<List<int>>(registeredItems);
                // Use selectedItems as needed
                foreach (var item in selectedItems)
                {
                    itemlist.Add(_unitOfWork.menuItemRepository.GetById(item));
                }
            }

            order.OrderItems = itemlist;


            if (ModelState.IsValid)
            {
                order.TotalPrice = CalculatePrice((List<MenuItem>)order.OrderItems);
                _unitOfWork.OrderRepository.Add(order);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        private float CalculatePrice(List<MenuItem> items)
        {
            float price = 0;
            foreach (var item in items)
            {
                price += item.Price;
            }
            return price;
        }

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,TotalPrice")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
