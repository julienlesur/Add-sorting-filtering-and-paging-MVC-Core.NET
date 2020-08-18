using Recruiting.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.Models.ViewModels
{
    public class ApplicantDetails
    {
        public Applicant Applicant { get; internal set; }
        public string Message { get; internal set; }
    }
}
