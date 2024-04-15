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
            OrderItems = new OrderItemsList();
            CalculateTotalPrice();
        }
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        [NotMapped]
        public OrderItemsList OrderItems { get; set; }
        public float TotalPrice { get; private set; }

        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.CalculatePrice();
        }
        [Keyless]

        public class OrderItemsList
        {
            public IEnumerable<MenuItem> Items { get; set; } = new List<MenuItem>();

            public float CalculatePrice()
            {
                float totalPrice = 0;

                foreach (var item in Items)
                {
                    totalPrice += item.Price;
                }

                return totalPrice;
            }
        }
    }
}
