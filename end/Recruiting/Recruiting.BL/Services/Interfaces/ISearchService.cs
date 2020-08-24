using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface ISearchService<T>
    {
        public IEnumerable<T> FilterList(string searchText, IEnumerable<T> entities);
    }
}
