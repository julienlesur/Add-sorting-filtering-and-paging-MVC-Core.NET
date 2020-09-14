using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Recruiting.BL.Models;
using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.EfModels;
using Recruiting.Infrastructures.Configurations;
using Recruiting.Web.Infrastructures;
using Recruiting.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.Controllers
{
    public class JobsController : PagingSortingSearchingControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IHtmlHelper _htmlHelper;
        private readonly GridConfiguration _gridOptions;

        public override string _sortOrder => SortOrder??Job._DefaultSort;
        public JobsController(IJobService jobService,
                                IHtmlHelper htmlHelper,
                                IOptions<GridConfiguration> gridOptions)
        {
            _jobService = jobService;
            _htmlHelper = htmlHelper;
            _gridOptions = gridOptions.Value;
        }

        [PagingSortingSearching]
        public async Task<IActionResult> List()
        {
            (IEnumerable<Job> jobs, int numberOfItems) = await _jobService.GetListAsync(_searchText, _sortOrder, _indexPage, _gridOptions.ItemsPerPage);

            JobList jobList = new JobList { 
                Jobs = jobs,
                NumberOfItems = numberOfItems                
            };
            return View(jobList);
        }

        [PagingSortingSearching]
        public async Task<IActionResult> Details(int id)
        {
            Job job = await _jobService.FindByIdAsync(id);
            if (Job.IsEmpty(job))
            {
                return RedirectToAction(nameof(JobNotFound));
            }
            return View(new JobDetails { Job = job, Message = (TempData["Message"] ?? "").ToString() });
        }

        public IActionResult Add()  
        {
            return View("Edit", new JobEdit { Job = Job._EmptyJob, Types = _htmlHelper.GetEnumSelectList<JobType>().OrderBy(t => t.Text) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Title, Reference, Company, Type, Location, Description")] Job job)
        {
            if (ModelState.IsValid)
            {
                var newJob = await _jobService.AddAsync(job);
                TempData["Message"] = "The job has been succesfully added";
                return RedirectToAction(nameof(Details), new { id = newJob.JobId });
            }
            return View("Edit", new JobEdit { Job = job, Types = _htmlHelper.GetEnumSelectList<JobType>().OrderBy(t => t.Text) });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var job = await _jobService.FindByIdAsync(id);
            if (Job.IsEmpty(job))
            {
                return RedirectToAction(nameof(JobNotFound));
            }

            return View(new JobEdit { Job = job, Types = _htmlHelper.GetEnumSelectList<JobType>().OrderBy(t => t.Text) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId, Title, Reference, Company, Type, Location, Description")] Job job)
        {
            if (ModelState.IsValid)
            {
                var updatedJob = await _jobService.UpdateAsync(job);
                TempData["Message"] = "The job has been succesfully saved";
                return RedirectToAction(nameof(Details), new { id = updatedJob.JobId });
            }
            return View(new JobEdit { Job = job, Types = _htmlHelper.GetEnumSelectList<JobType>() }); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedJob = await _jobService.DeleteAsync(id);
            if (Job.IsEmpty(deletedJob))
            {
                return RedirectToAction(nameof(JobNotFound));
            }
            return RedirectToAction(nameof(List));
        }

        public IActionResult JobNotFound()
        {
            return View();
        }
    }
}
