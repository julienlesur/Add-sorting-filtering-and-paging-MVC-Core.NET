using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface ISortAndSearchService<T> : ISortService<T>, ISearchService<T>
    {
        public Task<IEnumerable<T>> GetListAsync(string search, string sortOrder);
    }
}
