using System;
using System.Collections.Generic;
using System.Text;

namespace Recruiting.BL.Services.Interfaces
{
    public interface IPagingService<T>
    {
        public (IEnumerable<T>, int) GetPageElements(int indexPage, int itemsPerPage, IEnumerable<T> entities);
    }
}
