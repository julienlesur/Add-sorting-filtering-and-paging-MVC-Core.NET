using Recruiting.BL.Mappers;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.EfModels;
using Recruiting.Data.EfRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruiting.BL.Services
{
    public class JobService : ServiceBase<Job, EfJob>, IJobService
    {
        private readonly IEfJobRepository _efJobRepository;
        private readonly Func<IEnumerable<EfJob>, IList<Job>> _mapListEntityToListDomain;

        public JobService(IEfJobRepository efJobRepository,
                            IEfUnitRepository efUnitRepository)
            : base(efJobRepository, efUnitRepository, JobMapper.MapDomainToEntity, JobMapper.MapEntityToDomain)
        {
            _efJobRepository = efJobRepository;
            _mapListEntityToListDomain = JobMapper.MapListEntityToListDomain;
        }

        public async Task<(int Id, string Title)?> GetIdAndTitleByReference(string jobReference)
        {
            var job = await _efJobRepository.GetJobByReference(jobReference);
            if (job != null)
            {
                return (job.Id, job.Title);
            }
            return null;
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            IEnumerable<EfJob> efJobs = await _efJobRepository.ListAsync();
            return _mapListEntityToListDomain(efJobs);
        }

        public async Task<int> GetNumberOfApplicationsByJobReference(string reference)
            => await _efJobRepository.GetNumberOfApplicationsByJobReference(reference);

        public bool IsReferenceUnique(int id, string reference)
            => _efJobRepository.IsReferenceUnique(id, reference);
    }
}
