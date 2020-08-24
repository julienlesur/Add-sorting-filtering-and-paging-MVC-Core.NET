using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.Models.ViewModels
{
    public class SearchInListViewModel : SortAndSearchViewModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
