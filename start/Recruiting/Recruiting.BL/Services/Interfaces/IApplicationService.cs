using Microsoft.AspNetCore.Mvc.Rendering;
using Recruiting.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetApplicationsByIdApplicant(int applicantId);
        Task<IEnumerable<SelectListItem>> GetJobsWithoutApplication(int applicantId);
        Task<Application> AddAsync(Application application);
        Task DeleteAsync(int applicantId, int jobId);
    }
}
