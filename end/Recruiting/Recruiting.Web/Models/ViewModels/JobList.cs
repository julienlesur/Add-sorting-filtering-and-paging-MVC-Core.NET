using Recruiting.BL.Models;
using System.Collections.Generic;

namespace Recruiting.Web.Models.ViewModels
{
    public class JobList : SortSearchAndPagingViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }
    }
}
