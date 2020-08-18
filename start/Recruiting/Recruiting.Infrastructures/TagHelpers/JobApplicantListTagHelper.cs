using Microsoft.AspNetCore.Razor.TagHelpers;
using Recruiting.BL.Services.Interfaces;
using System.Threading.Tasks;

namespace Recruiting.Infrastructures.TagHelpers
{
    public class JobApplicantListTagHelper : TagHelper
    {
        public string JobReference { get; set; }

        private readonly IJobService _jobService;

        public JobApplicantListTagHelper(IJobService jobService)
        {
            _jobService = jobService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var applicantsCount = await _jobService.GetNumberOfApplicationsByJobReference(JobReference);
            output.Content.SetHtmlContent($@"<a href=""/Applicants/List?jobreference={JobReference}"">
                                                {applicantsCount}
                                            </a>");
            
        }
    }
}
