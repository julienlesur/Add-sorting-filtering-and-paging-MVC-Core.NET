using Recruiting.BL.Models;
using Recruiting.Data.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recruiting.BL.Mappers
{
    public static class ApplicationMapper
    {
        public static Application MapEntityToDomain(EfApplication entity)
         => 
            entity == null ?
            Application._EmptyApplication : 
            new Application
            {
                ApplicantId = entity.ApplicantId,
                ApplicantFullName = entity.Applicant != null ? ApplicantMapper.MapEntityToDomain(entity.Applicant).FulllName : String.Empty,
                JobId = entity.JobId,
                JobReference = entity.Job?.Reference ?? String.Empty,
                JobTitle = entity.Job?.Title ?? String.Empty,
                ApplicationDate = entity.ApplicationDate.ToShortDateString()
            };
        public static IEnumerable<Application> MapListEntityToListDomain(IEnumerable<EfApplication> entities)
        {
            ICollection<Application> applications = new List<Application>();
            foreach(var entity in entities)
            {
                applications.Add(MapEntityToDomain(entity));
            }
            return applications;
        }

        public static EfApplication MapDomainToEntity(Application domain)
        {
            DateTime.TryParse(domain.ApplicationDate, out DateTime appDate);
            return new EfApplication
            {
                ApplicantId = domain.ApplicantId,
                JobId = domain.JobId,
                ApplicationDate = appDate
            };
        }
        
    }
}
