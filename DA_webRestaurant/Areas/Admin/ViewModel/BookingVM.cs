using Entity;

namespace DA_webRestaurant.Areas.Admin.ViewModel
{
    public class BookingVM
    {
        public Booking Booking { get; set; }
        public IEnumerable<Table> Table { get; set; }
    }
}
