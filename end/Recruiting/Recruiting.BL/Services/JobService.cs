using Recruiting.BL.Mappers;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.EfModels;
using Recruiting.Data.EfRepositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Recruiting.BL.Services
{
    public class JobService : SortSearchAndPagingServiceBase<Job, EfJob>, IJobService
    {
        private readonly IEfJobRepository _efJobRepository;

        public JobService(IEfJobRepository efJobRepository,
                            IEfUnitRepository efUnitRepository)
            : base(efJobRepository, efUnitRepository, JobMapper.MapDomainToEntity, JobMapper.MapEntityToDomain, JobMapper.MapListEntityToListDomain, Job._DefaultSort)
        {
            _efJobRepository = efJobRepository;
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

        public override Func<Job, bool> GetFilter(string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return s => 1 == 1;
            }
            else
            {
                return job => job.Title.ToLower().Contains(search.ToLower()) || job.Reference.ToLower().Contains(search.ToLower());
            }
        }
        public override Func<Job, string> GetSort(string sortOrder)
        {
            switch (sortOrder)
            {
                case "reference":
                    return (job => job.Reference);
                case "location":
                    return (job => job.Location);
                case "company":
                    return (job => job.Company);
                default:
                    return (job => job.Title);
            }
        }

        public async Task<int> GetNumberOfApplicationsByJobReference(string reference)
            => await _efJobRepository.GetNumberOfApplicationsByJobReference(reference);

        public bool IsReferenceUnique(int id, string reference)
            => _efJobRepository.IsReferenceUnique(id, reference);
    }
}
