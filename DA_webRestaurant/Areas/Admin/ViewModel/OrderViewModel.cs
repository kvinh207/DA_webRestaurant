using Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DA_webRestaurant.Areas.Admin.ViewModel
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<SelectListItem>? MenuItemOptions { get; set; }
        [Required(ErrorMessage = "Please select a menu item")]
        public int SelectedMenuItemId { get; set; }
    }
}
