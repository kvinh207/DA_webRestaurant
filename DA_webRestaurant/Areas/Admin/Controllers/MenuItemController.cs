using DA_webRestaurant.Data;
using DA_webRestaurant.Models;
using DA_webRestaurant.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA_webRestaurant.Controllers
{
    [Area(SD.Role_Admin)]
    [Authorize(Roles = SD.Role_Admin)]
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuItemController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return View(menuItems);
        }


        public async Task<IActionResult> Details(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuItem menuItem, IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                var noImage = "/images/null.png";

                if (imageUrl != null)
                {
                    menuItem.ImageUrl = await SaveImage(imageUrl);
                }
                else
                {
                    menuItem.ImageUrl = noImage;
                }

                _context.MenuItems.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuItem);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItem menuItem, IFormFile imageUrl)
        {
            if (id != menuItem.MenuItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (imageUrl == null)
                {
                    menuItem.ImageUrl = _context.MenuItems.Find(id).ImageUrl;
                }
                else
                {
                    menuItem.ImageUrl = await SaveImage(imageUrl);
                }

                _context.Update(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuItem);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }


        [HttpPost, ActionName("DeleteConfirmed")]
   
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            if (image != null)
            {
                var savePath = Path.Combine("wwwroot/images", image.FileName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return "/images/" + image.FileName;
            }
            return "";
        }
    }
}
