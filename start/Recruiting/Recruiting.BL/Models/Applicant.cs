using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Recruiting.BL.Models
{
    public class Applicant : IEquatable<Applicant>
    {
        public int ApplicantId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string FulllName { get { return (FirstName ?? "") + " " + (LastName ?? ""); } }
        [Required]
        //[EmailAddress]
        [RegularExpression(
                        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", 
                        ErrorMessage = "The email format is not valid")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Adress")]
        public string Adress1 { get; set; }
        [Display(Name ="Adress complement")]
        public string Adress2 { get; set; }
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        public string ApplicationReference { get; set; }

        public IList<Application> Applications { get; set; }


        public static readonly Applicant _EmptyApplicant = new Applicant { ApplicantId = 0 };
        public static bool IsEmpty(Applicant applicant)
            => applicant == _EmptyApplicant;

        public override bool Equals(object obj)
        {
            return Equals(obj as Applicant);
        }

        public bool Equals(Applicant other)
        {
            return other != null &&
                   ApplicantId == other.ApplicantId &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   FulllName == other.FulllName &&
                   Email == other.Email &&
                   Adress1 == other.Adress1 &&
                   Adress2 == other.Adress2 &&
                   ZipCode == other.ZipCode &&
                   City == other.City &&
                   Country == other.Country;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(ApplicantId);
            hash.Add(FirstName);
            hash.Add(LastName);
            hash.Add(FulllName);
            hash.Add(Email);
            hash.Add(Adress1);
            hash.Add(Adress2);
            hash.Add(ZipCode);
            hash.Add(City);
            hash.Add(Country);
            return hash.ToHashCode();
        }

        public static bool operator ==(Applicant left, Applicant right)
        {
            return EqualityComparer<Applicant>.Default.Equals(left, right);
        }

        public static bool operator !=(Applicant left, Applicant right)
        {
            return !(left == right);
        }
    }
}
