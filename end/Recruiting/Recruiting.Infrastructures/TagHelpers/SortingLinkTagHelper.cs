using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.Infrastructures.TagHelpers
{

    [HtmlTargetElement("a", Attributes = "sort-order,current-sort")]
    public class SortingLinkTagHelper : TagHelper
    {
        public string SortOrder { get; set; }
        public string CurrentSort { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var sortSymbol = GetSymbolSortName();
            if (!String.IsNullOrEmpty(sortSymbol))
            {
                var childContent = output.Content.IsModified ? output.Content.GetContent() :
                   (await output.GetChildContentAsync()).GetContent();
                output.Content.SetHtmlContent($@"{childContent} <i class=""fa {sortSymbol}""></i>");
            }

            var currentHref = output.Attributes["href"]?.Value;
            var sortName = GetSortName();
            output.Attributes.SetAttribute("href",
                $@"{currentHref.ToString()}?sortOrder={sortName}");
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
