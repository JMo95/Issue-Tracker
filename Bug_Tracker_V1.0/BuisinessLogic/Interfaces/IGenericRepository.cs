using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Interfaces
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<T> where T : class
        {
            Task<IEnumerable<T>> GetAll();
            Task<T> FindOne(int id);
            Task<int> Delete(int id);
            Task<int> Update(T entity);
            Task<int> Create(T entity);
            Task SaveChanges();
        }
    }
}
