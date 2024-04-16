using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public enum TableType
    {
        Ban_2_Nguoi,
        Ban_4_Nguoi,
        Ban_6_Nguoi,
        Ban_1_Nguoi,
        Ban_Couple,
        Ban_10_Nguoi
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
        public TableType? Type { get; set; }
        public int NumberOfSeats { get; set; }
        public bool IsReserved { get; set; }
        public TableStatus Status { get; set; }

        /*public Table(TableType type)
        {
            Type = type;
            Status = TableStatus.Empty;
        }
*/
        
       /* public int GetNumberOfSeats()
        {
            if (Type == null)
            {
                return 0; 
            }
            switch (Type)
            {
                case TableType.Ban_2_Nguoi:
                    return 2;
                case TableType.Ban_4_Nguoi:
                    return 4;
                case TableType.Ban_6_Nguoi:
                    return 6;
                case TableType.Ban_10_Nguoi:
                    return 10;
                case TableType.Ban_1_Nguoi:
                    return 6;
                case TableType.Ban_Couple:
                    return 2;
                // Add cases for other table types as needed
                default:
                    return 0;
            }
        }*/
    }
}
