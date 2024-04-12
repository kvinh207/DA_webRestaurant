using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using DA_webRestaurant.Data;
using DA_webRestaurant.Models;
using DA_webRestaurant.Utility;
using Microsoft.AspNetCore.Identity;

namespace DA_webRestaurant.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

       
    }
}
