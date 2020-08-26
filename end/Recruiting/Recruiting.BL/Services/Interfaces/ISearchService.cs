using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface ISearchService<T>
    {
        public Func<T, bool> GetFilter(string search);
    }
}
