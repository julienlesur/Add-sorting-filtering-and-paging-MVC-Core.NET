using Microsoft.AspNetCore.Mvc.Rendering;
using Recruiting.BL.Mappers;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.EfModels;
using Recruiting.Data.EfRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IEfApplicationRepository _efApplicationRepository;
        private readonly IEfJobRepository _efJobRepository;
        private readonly IEfUnitRepository _efUnitRepository;
        private readonly Func<IEnumerable<EfApplication>, IEnumerable<Application>> _mapApplicationListEntityToListDomain;
        private readonly Func<EfApplication, Application> _mapApplicationEntityToDomain;
        private readonly Func<Application, EfApplication> _mapApplicationDomainToEntity;

        public ApplicationService(IEfApplicationRepository efApplicationRepository, 
                                    IEfJobRepository efJobRepository,
                                    IEfUnitRepository efUnitRepository)
        {
            _efApplicationRepository = efApplicationRepository;
            _efJobRepository = efJobRepository;
            _efUnitRepository = efUnitRepository;
            _mapApplicationListEntityToListDomain = ApplicationMapper.MapListEntityToListDomain;
            _mapApplicationEntityToDomain = ApplicationMapper.MapEntityToDomain;
            _mapApplicationDomainToEntity = ApplicationMapper.MapDomainToEntity;
        }

        public async Task<Application> AddAsync(Application application)
        {
            application.ApplicationDate = DateTime.Now.ToLongDateString();
            var entity = _mapApplicationDomainToEntity(application);
            await _efApplicationRepository.AddAsync(entity);
            await _efUnitRepository.CommitAsync();
            entity.Job = await _efJobRepository.FindByIdAsync(application.JobId);
            application = _mapApplicationEntityToDomain(entity);
            return application;
        }

        public async Task DeleteAsync(int applicantId, int jobId)
        {
            await _efApplicationRepository.DeleteAsync(applicantId, jobId);
            await _efUnitRepository.CommitAsync();
        }

        public async Task<IEnumerable<Application>> GetApplicationsByIdApplicant(int applicantId)
        {
            var efApplications = await _efApplicationRepository.GetListByIdApplicant(applicantId);
            var applications = _mapApplicationListEntityToListDomain(efApplications);
            return applications.OrderByDescending(app => app.ApplicationDate);
        }

        public async Task<IEnumerable<SelectListItem>> GetJobsWithoutApplication(int applicantId)
        {
            var idsJob = await _efApplicationRepository.GetListByIdApplicant(applicantId);
            var jobs = await _efJobRepository.GetJobsNotIn(idsJob.Select(j => j.JobId).ToList());

            return jobs
                    .Select(j => new SelectListItem(j.Title + " - " + j.Reference, j.Id.ToString()))
                    .ToList();
        }
    }
}
