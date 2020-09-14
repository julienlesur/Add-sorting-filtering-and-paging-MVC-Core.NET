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
    public class ApplicantService : SortSearchAndPagingServiceBase<Applicant, EfApplicant>, IApplicantService, IPagingSortAndSearchService<Applicant>
    {
        private readonly IEfApplicantRepository _efApplicantRepository;
        private readonly IEfApplicationRepository _efApplicationRepository;
        private readonly Func<EfApplication, Application> _mapApplicationEntityToDomain;

        public ApplicantService(IEfApplicantRepository efApplicantRepository,
                                    IEfApplicationRepository efApplicationRepository,
                                    IEfUnitRepository efUnitRepository)
            : base(efApplicantRepository, efUnitRepository, ApplicantMapper.MapDomainToEntity, ApplicantMapper.MapEntityToDomain, ApplicantMapper.MapListEntityToListDomain, Applicant._DefaultSort)
        {
            _efApplicantRepository = efApplicantRepository;
            _efApplicationRepository = efApplicationRepository;
            _mapApplicationEntityToDomain = ApplicationMapper.MapEntityToDomain;

        }

        public async Task<Applicant> AddAsync(Applicant applicant, string jobReference)
        {
            var addedApplicant = await _efApplicantRepository
                                            .AddAsync(_mapDomainToEntity(applicant));

            if (!String.IsNullOrEmpty(jobReference))
            {
                await AddApplicationFromJobReference(jobReference, addedApplicant);
            }

            await _efUnitRepository.CommitAsync();

            return _mapEntityToDomain(addedApplicant);
        }

        private async Task AddApplicationFromJobReference(string jobReference, EfApplicant addedApplicant)
        {
            var application = await _efApplicationRepository
                                            .AddFromJobReference(jobReference);
            if (application != null)
            {
                (addedApplicant.Applications ?? new List<EfApplication>())
                    .Add(application);
            }
        }

        public async Task<IList<Applicant>> DomainListAsync()
        {
            IEnumerable<EfApplicant> efApplicants = await _efApplicantRepository.ListAsync();
            return _mapListEntityToListDomain(efApplicants);
        }
        public async Task<Application> GetApplicantLastApplication(int applicantId)
        {
            var efLastApplication = await _efApplicantRepository.GetLastApplicationByApplicantId(applicantId);
            return _mapApplicationEntityToDomain(efLastApplication);
        }

        public override Func<Applicant, bool> GetFilter(string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return s => 1 == 1;
            }
            else
            {
                return app => app.FulllName.ToLower().Contains(search.ToLower()) || app.Email.ToLower().Contains(search);
            }
        }

        public override Func<Applicant, string> GetSort(string sortOrder)
        {
            switch (sortOrder)
            {
                case "email":
                    return (app => app.Email);
                case "city":
                    return (app => app.City);
                case "country":
                    return (app => app.Country);
                case "application":
                    return (app => app.ApplicationTitleAndReference);
                default:
                    return (app => app.FulllName);
            }

        }

        private async Task<IEnumerable<Applicant>> GetApplicantsByJobReference(string jobReference)
        {
            IList<Applicant> applicants = new List<Applicant>();
            var applications = await _efApplicationRepository.GetApplicationListByJobReference(jobReference);
            foreach(var application in applications)
            {
                var applicant = _mapEntityToDomain(application.Applicant);
                applicant.ApplicationReference = application.Job.Reference;
                applicant.ApplicationTitleAndReference = application.Job.Title + " - " + application.Job.Reference;
                applicants.Add(applicant);
            }

            return applicants;
        }

        private async Task<IEnumerable<Applicant>> GetApplicantsWithLastApplication()
        {
            var applicants = await DomainListAsync();
            foreach (var applicant in applicants)
            {
                var lastApplication = await GetApplicantLastApplication(applicant.ApplicantId);
                applicant.ApplicationReference = lastApplication.JobReference;
                applicant.ApplicationTitleAndReference = lastApplication.JobTitleAndRef;
            }
            return applicants;
        }

        public async Task<(IEnumerable<Applicant> applicants, int numberOfItems)> GetApplicantList(string jobReference, string search, string sortOrder, int indexPage, int itemsPerPage)
        {
            IEnumerable<Applicant> applicants;
            if (String.IsNullOrEmpty(jobReference))
            {
                applicants = await GetApplicantsWithLastApplication();
            }
            else
            {
                applicants = await GetApplicantsByJobReference(jobReference);
            }

            return applicants
                    .Where(GetFilter(search))
                    .SortList(sortOrder.Contains("_desc"), GetSort(sortOrder.Replace("_desc", "")))
                    .GetPageElements(indexPage, itemsPerPage);
        }

    }
}
