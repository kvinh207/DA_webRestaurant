namespace DA_webRestaurant.Models
{
    public class Table
    {
        public int TableId { get; set; }

        public string TableType { get; set; }
        public int NumberOfSeats { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
