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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            (IEnumerable<Applicant> applicants, int numbersOfItems) = await _applicantService.GetListAsync("","", 1, 3);
            
            return View(applicants);
        }
    }
}
