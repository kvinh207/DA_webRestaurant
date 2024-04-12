using System.ComponentModel.DataAnnotations;

namespace DA_webRestaurant.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
