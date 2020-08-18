using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recruiting.Web.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Application>> Add([Bind("ApplicantId", "JobId")] Application application)
        =>
            await _applicationService.AddAsync(application);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int applicantId, int jobId)
        {
            await _applicationService.DeleteAsync(applicantId, jobId);
            return Ok();
        }

        [Route("{controller}/{action}/{applicantId}")]
        public async Task<IEnumerable<SelectListItem>> GetApplicantJobsWithoutApplication(int applicantId)
        {
            return await _applicationService.GetJobsWithoutApplication(applicantId);
        }
    }
}
