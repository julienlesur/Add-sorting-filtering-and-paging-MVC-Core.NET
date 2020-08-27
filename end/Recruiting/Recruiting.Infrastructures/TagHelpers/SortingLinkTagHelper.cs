using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.Infrastructures.TagHelpers
{

    [HtmlTargetElement("a", Attributes = "sort-order,current-sort,search-text")]
    public class SortingLinkTagHelper : TagHelper
    {
        public string SortOrder { get; set; }
        public string CurrentSort { get; set; }
        public string SearchText { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = output.Content.IsModified ? output.Content.GetContent() :
               (await output.GetChildContentAsync()).GetContent();
            var sortSymbol = GetSymbolSortName();
            if (!String.IsNullOrEmpty(sortSymbol))
            {
                output.Content.SetHtmlContent($@"{childContent} <i class=""fa {sortSymbol}""></i>");
            }

            string searchTextQuery = String.IsNullOrEmpty(SearchText) ? "" : $@"&SearchText={SearchText}";
            var currentHref = output.Attributes["href"]?.Value;
            var sortName = GetSortName();
            output.Attributes.SetAttribute("href",
                $@"{currentHref.ToString()}?sortOrder={sortName}{searchTextQuery}");
        }

        private string GetSortName()
        =>
            (CurrentSort.Replace("_desc", "") == SortOrder) 
                ? (CurrentSort??"").EndsWith("_desc")  ? SortOrder : SortOrder + "_desc"
                : SortOrder;

        private string GetSymbolSortName()
        =>
            (CurrentSort.Replace("_desc", "") == SortOrder)
                ? (CurrentSort ?? "").EndsWith("_desc") ? "fa-sort-down" : "fa-sort-up"
                : "";
    }
}
