using System;
using System.Collections.Generic;
using System.Text;

namespace Recruiting.BL.Services.Interfaces
{
    public interface ISortService<T>
    {
        public Func<T, string> GetSort(string sortOrder);
    }
}
