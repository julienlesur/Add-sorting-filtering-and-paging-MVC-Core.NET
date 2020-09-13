using Recruiting.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface IApplicantService : IServiceBase<Applicant>, IPagingSortAndSearchService<Applicant>
    {
        Task<(IEnumerable<Applicant> applicants, int numberOfItems)> GetApplicantList(string jobReference, string search, string sortOrder, int indexPage, int itemsPerPage);
        Task<Application> GetApplicantLastApplication(int applicantId);
        Task<Applicant> AddAsync(Applicant applicant, string jobReference);
    }
}
