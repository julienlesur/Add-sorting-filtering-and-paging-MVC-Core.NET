using Recruiting.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface IApplicantService : IServiceBase<Applicant>, ISortAndSearchService<Applicant>
    {
        Task<IEnumerable<Applicant>> GetApplicantList(string jobReference, string search, string sortOrder);
        Task<Application> GetApplicantLastApplication(int applicantId);
        Task<Applicant> AddAsync(Applicant applicant, string jobReference);
    }
}
