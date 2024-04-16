using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public enum StatusBooking
    {
        Reserved,
        Canceled,
        Completed
    }

    public class Booking
    {
        public Booking()
        {
            Status = StatusBooking.Reserved;
        }
        [Key]
        public int BookingId { get; set; }
        [NotMapped]
        public int? PeopleCount { get; set; }
        public DateTime BookingDate { get; set; }
        public string GuestName { get; set; }
        public string PhoneNumber { get; set; }
        public StatusBooking? Status { get; set; }
        public string Note {  get; set; }
    }
}
