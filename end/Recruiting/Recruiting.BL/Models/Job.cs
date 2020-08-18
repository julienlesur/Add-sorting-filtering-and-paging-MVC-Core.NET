using Recruiting.BL.Validation;
using Recruiting.Data.EfModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Recruiting.BL.Models
{
    public class Job : IEquatable<Job>
    {
        public int JobId { get; set; }
        [Required]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Required, StringLength(20)]
        [IsUniqueJobReference]
        public string Reference { get; set; }
        [Required]
        public JobType Type { get; set; }
        public string Location { get; set; }
        [Required]
        public string Company { get; set; }

        public int NumberOfApplications { get; set; }
        public IList<Application> Applications { get; set; }


        public static readonly Job _EmptyJob = new Job { JobId = 0 };
            public static bool IsEmpty(Job job)
                => job == _EmptyJob;

        public override bool Equals(object obj)
        {
            return Equals(obj as Job);
        }

        public bool Equals(Job other)
        {
            return other != null &&
                   JobId == other.JobId &&
                   Title == other.Title &&
                   Description == other.Description &&
                   Reference == other.Reference &&
                   Type == other.Type &&
                   Location == other.Location &&
                   Company == other.Company;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(JobId, Title, Description, Reference, Type, Location, Company);
        }

        public static bool operator ==(Job left, Job right)
        {
            return EqualityComparer<Job>.Default.Equals(left, right);
        }

        public static bool operator !=(Job left, Job right)
        {
            return !(left == right);
        }
    }
}
