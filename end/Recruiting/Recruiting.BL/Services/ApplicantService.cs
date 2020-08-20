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
    public class ApplicantService : ServiceBase<Applicant, EfApplicant>, IApplicantService
    {
        private readonly IEfApplicantRepository _efApplicantRepository;
        private readonly IEfApplicationRepository _efApplicationRepository;
        private readonly IEfJobRepository _efJobRepository;
        private readonly Func<IEnumerable<EfApplicant>, IList<Applicant>> _mapListEntityToListDomain;
        private readonly Func<EfApplication, Application> _mapApplicationEntityToDomain;

        public ApplicantService(IEfApplicantRepository efApplicantRepository,
                                    IEfApplicationRepository efApplicationRepository,
                                    IEfJobRepository efJobRepository,
                                    IEfUnitRepository efUnitRepository)
            : base(efApplicantRepository, efUnitRepository, ApplicantMapper.MapDomainToEntity, ApplicantMapper.MapEntityToDomain)
        {
            _efApplicantRepository = efApplicantRepository;
            _efApplicationRepository = efApplicationRepository;
            _efJobRepository = efJobRepository;
            _mapListEntityToListDomain = ApplicantMapper.MapListEntityToListDomain;
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

        public async Task<IEnumerable<Applicant>> GetApplicantList(string jobReference, string sortOrder)
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
            applicants = SortList(sortOrder, applicants);
            return applicants;
        }

        private static IEnumerable<Applicant> SortList(string sortOrder, IEnumerable<Applicant> applicants)
        {
            switch (sortOrder)
            {
                case "email":
                    applicants = applicants.OrderBy(app => app.Email);
                    break;
                case "city":
                    applicants = applicants.OrderBy(app => app.City);
                    break;
                case "country":
                    applicants = applicants.OrderBy(app => app.Country);
                    break;
                case "application":
                    applicants = applicants.OrderBy(app => app.ApplicationTitleAndReference);
                    break;
                case "fullname_desc":
                    applicants = applicants.OrderByDescending(app => app.FulllName);
                    break;
                case "email_desc":
                    applicants = applicants.OrderByDescending(app => app.Email);
                    break;
                case "city_desc":
                    applicants = applicants.OrderByDescending(app => app.City);
                    break;
                case "country_desc":
                    applicants = applicants.OrderByDescending(app => app.Country);
                    break;
                case "application_desc":
                    applicants = applicants.OrderByDescending(app => app.ApplicationTitleAndReference);
                    break;
                default:
                    applicants = applicants.OrderBy(app => app.FulllName);
                    break;
            }

            return applicants;
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
    }
}
