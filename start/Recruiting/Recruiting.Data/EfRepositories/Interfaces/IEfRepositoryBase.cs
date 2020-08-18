using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruiting.Data.EfRepositories.Interfaces
{
    public interface IEfRepositoryBase<T>
    {
        Task<IEnumerable<T>> ListAsync();
        Task<T> FindByIdAsync(int id);
        Task<T> AddAsync(T newEntity);
        T Update(T updatedEntity);
        Task<T> DeleteAsync(int id);
    }
}
