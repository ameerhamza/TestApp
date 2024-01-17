using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Repo.DataStores
{
    public interface IDataStore<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T item);
        Task<bool> UpdateAsync(int id, T updatedItem);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<T>> SearchAsync(Func<T, bool> predicate);
    }

}
