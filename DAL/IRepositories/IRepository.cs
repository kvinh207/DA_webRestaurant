using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(object entityId);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    string includeProperties = "");
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(object entityId);
    }
}
