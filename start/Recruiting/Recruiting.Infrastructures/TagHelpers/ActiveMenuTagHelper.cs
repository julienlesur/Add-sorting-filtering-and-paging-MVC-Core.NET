using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recruiting.Infrastructures.TagHelpers
{
    [HtmlTargetElement("li", Attributes = "active-url, comparer-mode")]
    public class ActiveMenuTagHelper : TagHelper
    {
        public string ActiveUrl {get; set;}
        public string ComparerMode { get; set; }
        private Func<bool> _urlComparer;

        private readonly IHttpContextAccessor _httpService;

        public ActiveMenuTagHelper(IHttpContextAccessor httpService)
        {
            _httpService = httpService;
        }
        public override void Init(TagHelperContext context)
        {
            if (ComparerMode?.ToLower() == "contains")
            {
                _urlComparer = DoesUrlContains;
            }
            else
            {
                _urlComparer = DoesUrlEndsWith;
            }
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_urlComparer())
            {
                var currentAttributes = output.Attributes["class"]?.Value;
                output.Attributes.SetAttribute("class", 
                    currentAttributes.ToString() + " active");
            }
        }

        private bool DoesUrlEndsWith()
        =>
            _httpService.HttpContext.Request.Path.ToString().EndsWith(ActiveUrl)
            || _httpService.HttpContext.Request.Path.ToString().Contains(ActiveUrl + "?");

        private bool DoesUrlContains()
        =>
            _httpService.HttpContext.Request.Path.ToString().Contains(ActiveUrl);
    }
}
