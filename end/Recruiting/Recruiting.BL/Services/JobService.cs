using Recruiting.BL.Mappers;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.EfModels;
using Recruiting.Data.EfRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Job>> GetJobs(string sortOrder)
        {
            IEnumerable<EfJob> efJobs = await _efJobRepository.ListAsync();
            efJobs = SortList(sortOrder, efJobs);
            return _mapListEntityToListDomain(efJobs);
        }

        private IEnumerable<EfJob> SortList(string sortOrder, IEnumerable<EfJob> efJobs)
        {
            switch (sortOrder)
            {
                case "title_desc":
                    return efJobs.OrderByDescending(job => job.Title);
                case "reference":
                    return efJobs.OrderBy(job => job.Reference);
                case "reference_desc":
                    return efJobs.OrderByDescending(job => job.Reference);
                case "location":
                    return efJobs.OrderBy(job => job.Location);
                case "location_desc":
                    return efJobs.OrderByDescending(job => job.Location);
                case "company":
                    return efJobs.OrderBy(job => job.Company);
                case "company_desc":
                    return efJobs.OrderByDescending(job => job.Company);
                default:
                    return efJobs.OrderBy(job => job.Title);
            }
        }

        public async Task<int> GetNumberOfApplicationsByJobReference(string reference)
            => await _efJobRepository.GetNumberOfApplicationsByJobReference(reference);

        public bool IsReferenceUnique(int id, string reference)
            => _efJobRepository.IsReferenceUnique(id, reference);
    }
}
