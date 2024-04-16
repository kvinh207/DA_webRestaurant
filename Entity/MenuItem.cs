using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }
        public string ItemName { get; set; }
        [Range(15000, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 15000")]
        public float Price { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

    }
}
