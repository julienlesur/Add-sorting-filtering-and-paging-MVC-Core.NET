using System;
using System.Collections.Generic;
using System.Text;

namespace Recruiting.Data.EfModels
{
    public class EfApplication
    {
        public int JobId { get; set; }
        public EfJob Job { get; set; }
        public int ApplicantId { get; set; }
        public EfApplicant Applicant { get; set; }

        public DateTime ApplicationDate { get; set; }

        public string UrlCV { get; set; }
    }
}
