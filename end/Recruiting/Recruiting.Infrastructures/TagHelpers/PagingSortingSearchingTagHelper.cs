using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Recruiting.Infrastructures.TagHelpers
{
    [HtmlTargetElement("a",Attributes = "sort-search")]
    public class PagingSortingSearchingTagHelper : TagHelper
    {
        public string SortSearch { get; set; }
        protected IQueryCollection Query => viewContext.HttpContext.Request.Query;
        protected string SortQuery => SortSearch[0].Equals('1') &&  Query.ContainsKey("sortOrder") ? $"sortOrder={Query["sortOrder"].ToString()}" : "";
        protected string SearchQuery => SortSearch[1].Equals('1') && Query.ContainsKey("searchText") ? $"searchText={Query["searchText"].ToString()}" : "";

        [ViewContext]
        public ViewContext viewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentHref = output.Attributes["href"]?.Value;

            output.Attributes.SetAttribute("href",
                (currentHref.ToString())
                    .CompleteUri(SortQuery)
                    .CompleteUri(SearchQuery));
        }
    }
}
