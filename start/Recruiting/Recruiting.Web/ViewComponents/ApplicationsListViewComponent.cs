using Microsoft.AspNetCore.Mvc;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruiting.Web.ViewComponents
{
    public class ApplicationsListViewComponent : ViewComponent
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsListViewComponent(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        public async Task<IViewComponentResult> InvokeAsync(
            int applicantId)
        {
            IEnumerable<Application> applications = await _applicationService.GetApplicationsByIdApplicant(applicantId);

            ApplicationList applicationList = new ApplicationList
            {
                Applications = applications,
                ApplicantId = applicantId
            };

            return View(applicationList);
        }
    }
}
