using Microsoft.EntityFrameworkCore;
using Recruiting.Data.Data;
using Recruiting.Data.EfModels;
using Recruiting.Data.EfRepositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Data.EfRepositories
{
    public class EfApplicantRepository : EfRepositoryBase<EfApplicant>, IEfApplicantRepository
    {
        public EfApplicantRepository(RecruitingContext context) : base(context)
        {
        }

        public async Task<EfApplication> GetLastApplicationByApplicantId(int applicantId)
        {
            return await _context.Applications
                                    .Where(app => app.ApplicantId == applicantId)
                                    .OrderByDescending(app => app.ApplicationDate)
                                    .Include(app => app.Job)
                                    .Include(app => app.Applicant)
                                    .FirstOrDefaultAsync();
        }
    }
}
