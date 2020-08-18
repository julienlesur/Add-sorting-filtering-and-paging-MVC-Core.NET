using Recruiting.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.Models.ViewModels
{
    public class ApplicantList
    {
        public IEnumerable<Applicant> Applicants { get; set; }
        public string ListTitle { get; set; }
        public string JobColumnTitle { get; set; }
        public string CurrentSort { get; set; }
        public string FullNameSort { get; set; }
        public string EmailSort { get; set; }
        public string CitySort { get; set; }
        public string CountrySort { get; set; }
        public string ApplicationSort { get; set; }
    }
}
