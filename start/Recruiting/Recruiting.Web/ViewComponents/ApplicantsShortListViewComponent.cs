using Microsoft.AspNetCore.Mvc;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Infrastructures.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.ViewComponents
{
    public class ApplicantsShortListViewComponent : ViewComponent
    {
        private readonly IApplicantService _applicantService;

        public ApplicantsShortListViewComponent(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [JobReference]
        public async Task<IViewComponentResult> InvokeAsync(
            int applicantId, string jobReference)
        {
            IEnumerable<Applicant> applicants = await _applicantService.GetApplicantList(jobReference);
            
            return View(applicants);
        }
    }
}
