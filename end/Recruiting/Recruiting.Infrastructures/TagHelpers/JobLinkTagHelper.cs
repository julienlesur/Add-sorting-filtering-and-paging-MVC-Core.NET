using Microsoft.AspNetCore.Razor.TagHelpers;
using Recruiting.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.Infrastructures.TagHelpers
{
    public class JobLinkTagHelper : TagHelper
    {
        public string JobReference { get; set; }
        private readonly IJobService _jobService;

        public JobLinkTagHelper(IJobService jobService)
        {
            _jobService = jobService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (int id, string title)? idAndTitle = await _jobService.GetIdAndTitleByReference(JobReference);
            if (idAndTitle != null)
            {
                output.Content.SetHtmlContent($@"<a title=""Display job"" 
                                                href=""/Jobs/Details/{idAndTitle?.id}"">
                                                {idAndTitle?.title} - {JobReference}
                                                </a>");
            }
        }
    }
}
