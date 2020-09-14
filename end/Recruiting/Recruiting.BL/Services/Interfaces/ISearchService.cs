using System;

namespace Recruiting.BL.Services.Interfaces
{
    public interface ISearchService<T>
    {
        public Func<T, bool> GetFilter(string search);
    }
}
