using System.ComponentModel;

namespace DA_webRestaurant.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int TableId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BookingTime { get; set; }
        public int NumberOfGuests { get; set; }

        [DefaultValue(false)]
        public bool Status { get; set; }
        public Table Table { get; set; }
    }
}
