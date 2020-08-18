using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface IServiceBase<T>
    {
        Task<T> FindByIdAsync(int id);
        Task<T> AddAsync(T newEntity);
        Task<T> UpdateAsync(T updatedEntity);
        Task<T> DeleteAsync(int id);
    }
}
