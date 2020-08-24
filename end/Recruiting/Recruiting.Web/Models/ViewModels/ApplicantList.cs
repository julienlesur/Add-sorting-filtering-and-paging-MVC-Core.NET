using Recruiting.BL.Models;
using System.Collections.Generic;

namespace Recruiting.Web.Models.ViewModels
{
    public class ApplicantList : SortAndSearchViewModel
    {
        public IEnumerable<Applicant> Applicants { get; set; }
        public string ListTitle { get; set; }
        public string JobColumnTitle { get; set; }
    }
}
