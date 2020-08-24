using Recruiting.BL.Models;
using System.Collections.Generic;

namespace Recruiting.Web.Models.ViewModels
{
    public class JobList : SortAndSearchViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }
    }
}
