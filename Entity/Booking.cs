using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public enum Status
    {
        Reserved,
        Canceled,
        Completed
    }

    public class Booking
    {
        public Booking()
        {
            if(tables == null)
            {
                TableCount = 0;
                return;
            }
            TableCount = tables.CountTable();
        }
        [Key]
        public int BookingId { get; set; }
        [NotMapped]
        public TableList? tables { get; set; }
        public int? TableCount { get; set; }

        public int PeopleCount { get; set; }

        public DateTime BookingDate { get; set; }
        public string GuestName { get; set; }
        public Status Status { get; set; }
        [Keyless]
        public class TableList
        {
            public IEnumerable<Table> tables { get; set; } = new List<Table>();
            public int CountTable()
            {
                int count = 0;

                foreach (var item in tables)
                {
                    count++;
                }
                return count;
            }

        }

    }
}
