using Recruiting.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.Models.ViewModels
{
    public class JobList
    {
        public IEnumerable<Job> Jobs { get; set; }
        public string CurrentSort { get; set; }
        public string TitleSort { get; set; }
        public string ReferenceSort { get; set; }
        public string LocationSort { get; set; }
        public string CompanySort { get; set; }
    }
}
