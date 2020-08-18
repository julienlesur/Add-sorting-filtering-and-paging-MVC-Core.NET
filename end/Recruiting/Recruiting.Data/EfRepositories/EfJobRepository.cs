using Microsoft.EntityFrameworkCore;
using Recruiting.Data.Data;
using Recruiting.Data.EfModels;
using Recruiting.Data.EfRepositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Data.EfRepositories
{
    public class EfJobRepository : EfRepositoryBase<EfJob>, IEfJobRepository
    {
        public EfJobRepository(RecruitingContext context) : base(context)
        {
        }

        public async Task<EfJob> GetJobByReference(string jobReference)
        =>
             await _context.Jobs.SingleOrDefaultAsync(j => j.Reference == jobReference);

        public async Task<int> GetJobIdByReference(string jobReference)
        {
            var job = await GetJobByReference(jobReference);
            if (job != null)
            {
                return job.Id;
            }
            return 0;
        }

        public async Task<IEnumerable<EfJob>> GetJobsNotIn(List<int> idsJob)
        {
            var jobs = await _context.Jobs.Where(j => !idsJob.Contains(j.Id)).ToListAsync();
            return jobs;
        }

        public async Task<int> GetNumberOfApplicationsByJobReference(string jobReference)
        {
            var jobList = await _context.Applications
                            .Where(app => app.Job.Reference == jobReference)
                            .ToListAsync();
                            return jobList.Count();
        }

        public bool IsReferenceUnique(int jobId, string reference)
        {
            return !_dbSet.Any(job => job.Id != jobId && job.Reference == reference);
        }
    }
}
