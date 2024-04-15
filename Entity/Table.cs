using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public enum TableType
    {
        Ban_2_Nguoi,
        Ban_4_Nguoi,
        Ban_6_Nguoi,
        Ban_1_Nguoi,
        Ban_Couple
    }

    public enum TableStatus
    {
        Reserved,
        Empty
    }

    public class Table
    {
        public Table()
        {
            Status = TableStatus.Empty;
        }
        [Key]
        public int TableId { get; set; }
        public TableType Type { get; set; }
        public int NumberOfSeats { get; set; }
        public ICollection<Order> Orders { get; set; }
        public bool IsReserved { get; set; }
        public TableStatus Status { get; set; }
    }
}
