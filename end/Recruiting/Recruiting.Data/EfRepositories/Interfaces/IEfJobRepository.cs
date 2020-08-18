using Recruiting.Data.EfModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruiting.Data.EfRepositories.Interfaces
{
    public interface IEfJobRepository : IEfRepositoryBase<EfJob>
    {        
        public Task<int> GetNumberOfApplicationsByJobReference(string jobReference);
        public bool IsReferenceUnique(int jobId, string reference);
        public Task<int> GetJobIdByReference(string jobReference);
        public Task<EfJob> GetJobByReference(string jobReference);
        public Task<IEnumerable<EfJob>> GetJobsNotIn(List<int> idsJob);
    }
}
