using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recruiting.Data.EfModels
{
    public class EfJob : EfModelBase
    {
        [Column("JobId")]
        public override int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public JobType Type { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }

        public ICollection<EfApplication> Applications { get; set; }
    }

    public enum JobType
    {
        Full_Time,
        Contract,
        Part_Time,
        Temporary,
        Internship,
        Commission
    }
}
