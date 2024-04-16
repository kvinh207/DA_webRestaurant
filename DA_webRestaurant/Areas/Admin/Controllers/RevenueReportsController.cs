using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using Entity;

namespace DA_webRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RevenueReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RevenueReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/RevenueReports
        public async Task<IActionResult> Index()
        {
            return View(await _context.RevenueReports.ToListAsync());
        }

        // GET: Admin/RevenueReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revenueReport = await _context.RevenueReports
                .FirstOrDefaultAsync(m => m.RevenueReportId == id);
            if (revenueReport == null)
            {
                return NotFound();
            }

            return View(revenueReport);
        }

        // GET: Admin/RevenueReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/RevenueReports/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RevenueReportId,StartDate,EndDate,TotalRevenue,EmployeeEmail")] RevenueReport revenueReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revenueReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(revenueReport);
        }

        // GET: Admin/RevenueReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revenueReport = await _context.RevenueReports.FindAsync(id);
            if (revenueReport == null)
            {
                return NotFound();
            }
            return View(revenueReport);
        }

        // POST: Admin/RevenueReports/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RevenueReportId,StartDate,EndDate,TotalRevenue,EmployeeEmail")] RevenueReport revenueReport)
        {
            if (id != revenueReport.RevenueReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revenueReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevenueReportExists(revenueReport.RevenueReportId))
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
            return View(revenueReport);
        }

        // GET: Admin/RevenueReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revenueReport = await _context.RevenueReports
                .FirstOrDefaultAsync(m => m.RevenueReportId == id);
            if (revenueReport == null)
            {
                return NotFound();
            }

            return View(revenueReport);
        }

        // POST: Admin/RevenueReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var revenueReport = await _context.RevenueReports.FindAsync(id);
            if (revenueReport != null)
            {
                _context.RevenueReports.Remove(revenueReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevenueReportExists(int id)
        {
            return _context.RevenueReports.Any(e => e.RevenueReportId == id);
        }
    }
}
