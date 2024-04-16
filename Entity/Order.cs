using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Order
    {
        public Order()
        {
            OrderDate = DateTime.Now;
            OrderItems = new List<MenuItem>();
        }
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<MenuItem>? OrderItems { get; set; }
        public float? TotalPrice { get; set; }
    }
}
