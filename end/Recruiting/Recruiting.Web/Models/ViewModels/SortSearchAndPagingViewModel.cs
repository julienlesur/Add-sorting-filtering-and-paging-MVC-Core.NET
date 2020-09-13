
using System;

namespace Recruiting.Web.Models.ViewModels
{
    public abstract class SortSearchAndPagingViewModel
    {
        public string CurrentSort { get; set; }
        public string SearchText { get; set; }
        public int NumberOfItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int NumberOfPage { get; set; }
    }
}
