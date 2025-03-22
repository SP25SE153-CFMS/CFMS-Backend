using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null,
            bool noTracking = false);

        T GetByID(object id);
        void Insert(T entity);
        bool Delete(object id);
        void Delete(T entityToDelete);
        void DeleteRange(IEnumerable<T> entities);
        bool Update(object id, T entityToUpdate);
        void Update(T entityToUpdate);
        bool InsertRange(IEnumerable<T> entities);
    }
}
