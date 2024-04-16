using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using Entity;
using DAL;
using DAL.Repositories;
using System.Linq.Expressions;

namespace DA_webRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RevenueReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;
        public RevenueReportsController(ApplicationDbContext context, UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        public async Task<IActionResult> Create([Bind("StartDate,EndDate,EmployeeEmail")] RevenueReport revenueReport)
        {
            DateTime startDate = revenueReport.StartDate;
            DateTime? endDate = revenueReport.EndDate;

            Expression<Func<Order, bool>> filter = order => order.OrderDate >= startDate && order.OrderDate <= endDate;

            var OrdersOfTheDay = _unitOfWork.OrderRepository.GetAll(filter: filter);

            revenueReport.TotalRevenue = CalculateRevenue((List<Order>)OrdersOfTheDay);

            if (ModelState.IsValid)
            {
                _unitOfWork.RevenueReportRepository.Add(revenueReport);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(revenueReport);
        }

        private float? CalculateRevenue(List<Order> order)
        {
            float? rev = 0;

            foreach (var item in order)
            {
                rev += item.TotalPrice;
            }

            return rev;
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
