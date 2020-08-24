
using System;

namespace Recruiting.Web.Models.ViewModels
{
    public abstract class SortAndSearchViewModel
    {
        public string CurrentSort { get; set; }
        public string SearchText { get; set; }
    }
}
