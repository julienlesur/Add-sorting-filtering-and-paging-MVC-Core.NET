using Recruiting.Data.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recruiting.Data.Data
{
    public static class SeedRecruitingContext
    {

        public static void Initialize(RecruitingContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Jobs.Any())
            {
                return;   // DB has been seeded
            }

            var jobs = new EfJob[]
            {
                new EfJob{ Company = "Microsoft", Title="Senior Back-End Developer", Location= "Seattle", Type=JobType.Full_Time, CreationDate = DateTime.Now, LastUpdateDate=DateTime.Now, Reference = "MIC_BACKEND_SNR_DEV"},
                new EfJob{ Company = "Microsoft", Title="Junior Back-End Developer", Location= "Seattle", Type=JobType.Full_Time, CreationDate = DateTime.Now, LastUpdateDate=DateTime.Now, Reference = "MIC_BACKEND_JNR_DEV"},
                new EfJob{ Company = "Ikea", Title="Sales Manager", Location= "Stockholm", Type = JobType.Full_Time, CreationDate = DateTime.Now, LastUpdateDate=DateTime.Now, Reference = "IKE_SALES_MNGR"},
                new EfJob{ Company = "Ikea", Title="Cashier", Location= "Stockholm", Type = JobType.Part_Time, CreationDate = DateTime.Now, LastUpdateDate=DateTime.Now, Reference = "IKE_CASHIER"}
            };

            context.Jobs.AddRange(jobs);
            context.SaveChanges();

            var applicants = new EfApplicant[]
            {
                new EfApplicant{FirstName="Jennifer", LastName="White", Email="jennifer@white.com", Adress1="123 my street", ZipCode="34567", City="Seattle", Country="USA"},
                new EfApplicant{FirstName="John", LastName="Pink", Email="john@pink.com", Adress1="56 my street", ZipCode="34567", City="Seattle", Country="USA"},
                new EfApplicant{FirstName="Michelle", LastName="Yellow", Email="michelle@yellow.com", Adress1="28 my street", ZipCode="34567", City="Seattle", Country="USA"},
                new EfApplicant{FirstName="Arthur", LastName="Black", Email="arthur@black.com", Adress1="478 my street", ZipCode="34567", City="Seattle", Country="USA"}
            };

            context.Applicants.AddRange(applicants);
            context.SaveChanges();

            var applications = new EfApplication[]
            {
                new EfApplication{ApplicantId=1,JobId=1,UrlCV=@"\Applicants\1\Jennifer_White.pdf", ApplicationDate = DateTime.Now},
                new EfApplication{ApplicantId=2,JobId=1,UrlCV=@"\Applicants\2\John_Pink.pdf", ApplicationDate = DateTime.Now},
                new EfApplication{ApplicantId=3,JobId=1,UrlCV=@"\Applicants\3\Michelle_Yellow.docx", ApplicationDate = DateTime.Now},
                new EfApplication{ApplicantId=4,JobId=1,UrlCV=@"\Applicants\4\Arthur_Black.docx", ApplicationDate = DateTime.Now}
            };

            context.Applications.AddRange(applications);
            context.SaveChanges();
        }
    }
}
