using Microsoft.AspNetCore.Mvc;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.ViewComponents
{
    public class JobsShortListviewComponent : ViewComponent
    {
        private readonly IJobService _jobService;

        public JobsShortListviewComponent(IJobService jobService)
        {
            _jobService = jobService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            (IEnumerable<Job> jobs, int numberOfITems) = await _jobService.GetListAsync("","", 1, 3);

            return View(jobs);
        }
    }
}
