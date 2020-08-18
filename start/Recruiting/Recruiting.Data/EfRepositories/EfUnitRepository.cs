using Recruiting.Data.Data;
using Recruiting.Data.EfRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.Data.EfRepositories
{
    public class EfUnitRepository : IEfUnitRepository
    {
        private readonly RecruitingContext _context;

        public EfUnitRepository(RecruitingContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync()
            => await _context.SaveChangesAsync();
    }
}
