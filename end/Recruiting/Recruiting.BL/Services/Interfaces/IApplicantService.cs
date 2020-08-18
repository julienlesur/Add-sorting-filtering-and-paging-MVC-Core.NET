using Recruiting.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface IApplicantService : IServiceBase<Applicant>
    {
        Task<Application> GetApplicantLastApplication(int applicantId);
        Task<IEnumerable<Applicant>> GetApplicantList(string jobReference);
        Task<Applicant> AddAsync(Applicant applicant, string jobReference);
    }
}
