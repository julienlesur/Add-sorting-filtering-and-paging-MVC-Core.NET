using System;
using System.Collections.Generic;
using System.Text;

namespace Recruiting.BL.Models
{
    public class Application
    {
        public int JobId { get; set; }
        public string JobReference { get; set; }
        public string JobTitle { get; set; }
        public string JobTitleAndRef { get { return (JobTitle ?? "") + " - " + (JobReference ?? ""); } }
        public int ApplicantId { get; set; }
        public string ApplicantFullName { get; set; }
        public string ApplicationDate { get; set; }

        public static readonly Application _EmptyApplication = new Application { ApplicantId = 0, ApplicantFullName = String.Empty, JobId = 0, JobTitle = String.Empty, JobReference = String.Empty };
    }
}
