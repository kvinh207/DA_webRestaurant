using Entity;

namespace DAL.IRepositories
{
    public interface ITableRepository : IDisposable
    {
        Task<List<Table>> GetTables();
        Task<Table> GetTableById(int? tableId);
        void InsertTable(Table Table);
        void UpdateTable(Table Table);
        void DeleteTable(int tableId);
        Task<int> Save();
    }
}
