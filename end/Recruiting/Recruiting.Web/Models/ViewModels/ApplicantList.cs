using Recruiting.BL.Models;
using System.Collections.Generic;

namespace Recruiting.Web.Models.ViewModels
{
    public class ApplicantList : SortViewModel
    {
        public IEnumerable<Applicant> Applicants { get; set; }
        public string ListTitle { get; set; }
        public string JobColumnTitle { get; set; }
        public  string SearchText { get; set; }
    }
}
