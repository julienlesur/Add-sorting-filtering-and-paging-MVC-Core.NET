using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recruiting.Data.EfModels
{
    public class EfApplicant : EfModelBase
    {
        [Column("ApplicantId")]
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Adress1{ get; set; }
        public string Adress2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<EfApplication> Applications { get; set; }

    }
}
