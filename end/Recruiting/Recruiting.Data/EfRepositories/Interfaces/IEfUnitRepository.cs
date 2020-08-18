using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.Data.EfRepositories.Interfaces
{
    public interface IEfUnitRepository
    {
        public Task<int> CommitAsync();
    }
}
