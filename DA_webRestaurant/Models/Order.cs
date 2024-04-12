namespace DA_webRestaurant.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public int TableId { get; set; }
        public DateTime OrderTime { get; set; }

        public Employee Employee { get; set; }
        public Table Table { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
