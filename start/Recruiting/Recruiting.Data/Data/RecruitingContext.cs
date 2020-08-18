using Microsoft.EntityFrameworkCore;
using Recruiting.Data.EfModels;
using System.Runtime.CompilerServices;

namespace Recruiting.Data.Data
{
    public class RecruitingContext : DbContext
    {
        public RecruitingContext(DbContextOptions<RecruitingContext> options)
            : base(options)
        {
        }

        public DbSet<EfApplicant> Applicants { get; set; }
        public DbSet<EfJob> Jobs { get; set; }
        public DbSet<EfApplication> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EfApplicant>().ToTable("Applicant");
            modelBuilder.Entity<EfJob>().ToTable("Job");

            modelBuilder
                .Entity<EfApplication>()
                .ToTable("Application")
                .HasKey(sc => new { sc.ApplicantId, sc.JobId });

            modelBuilder.Entity<EfApplication>()
            .HasOne(pt => pt.Applicant)
            .WithMany(p => p.Applications)
            .HasForeignKey(pt => pt.ApplicantId);

            modelBuilder.Entity<EfApplication>()
                .HasOne(pt => pt.Job)
                .WithMany(t => t.Applications)
                .HasForeignKey(pt => pt.JobId);

        }
    }
}
