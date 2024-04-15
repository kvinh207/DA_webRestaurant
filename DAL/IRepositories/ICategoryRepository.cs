using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface ICategoryRepository : IDisposable
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int? tableId);
        void InsertCategory(Category Category);
        void UpdateCategory(Category Category);
        void DeleteCategory(int CategoryId);
        Task<int> Save();
    }
}
