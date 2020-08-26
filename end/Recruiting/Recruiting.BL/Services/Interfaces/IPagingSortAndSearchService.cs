using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface IPagingSortAndSearchService<T> : ISortService<T>, ISearchService<T>
    {
        public Task<(IEnumerable<T>, int)> GetListAsync(string search, string sortOrder, int indexPage, int itemsPerPage);
    }
}
