using Microsoft.EntityFrameworkCore;
using Recruiting.Data.Data;
using Recruiting.Data.EfModels;
using Recruiting.Data.EfRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Data.EfRepositories
{
    public class EfApplicationRepository : IEfApplicationRepository
    {
        private readonly RecruitingContext _context;

        public EfApplicationRepository(RecruitingContext context)
        {
            _context = context;
        }

        public async Task<EfApplication> AddAsync(EfApplication newApplication)
        {
            await _context.AddAsync(newApplication);
            return newApplication;
        }

        public async Task DeleteAsync(int applicantId, int jobId)
        {
            var application = await _context.Applications.SingleOrDefaultAsync(app => app.ApplicantId == applicantId && app.JobId == jobId);
            if (application != null)
            {
                _context.Remove(application);
            }
        }

        public async Task<ICollection<EfApplication>> GetApplicationListByJobReference(string jobReference)
        =>
            await _context.Applications
                            .Include(app => app.Applicant)
                            .Include(app => app.Job)
                            .Where(app => app.Job.Reference == jobReference)
                            .ToListAsync();

        public async Task<ICollection<EfApplication>> GetListByIdApplicant(int applicantId)
        =>
            await _context.Applications
                            .Where(app => app.ApplicantId == applicantId)
                            .Include(app => app.Applicant)
                            .Include(app => app.Job)
                            .ToListAsync();
    }
}
