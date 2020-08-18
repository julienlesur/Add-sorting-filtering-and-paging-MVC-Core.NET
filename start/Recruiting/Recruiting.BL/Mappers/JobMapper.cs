using Recruiting.BL.Models;
using Recruiting.Data.EfModels;
using System.Collections.Generic;

namespace Recruiting.BL.Mappers
{
    public static class JobMapper
    {
        public static Job MapEntityToDomain(EfJob entity)
        => 
            entity is null ?
            Job._EmptyJob :
            new Job
            {
                JobId = entity.Id,
                Reference = entity.Reference,
                Title = entity.Title,
                Company = entity.Company,
                Description = entity.Description,
                Location = entity.Location,
                Type = entity.Type
            };

        public static EfJob MapDomainToEntity(Job domain)
            => new EfJob
            {
                Id = domain.JobId,
                Reference = domain.Reference,
                Title = domain.Title,
                Company = domain.Company,
                Description = domain.Description,
                Location = domain.Location,
                Type = domain.Type
            };

        public static IList<Job> MapListEntityToListDomain(IEnumerable<EfJob> entities)
        {
            IList<Job> domainJobs = new List<Job>();
            foreach (EfJob entity in entities)
            {
                domainJobs.Add(MapEntityToDomain(entity));
            }
            return domainJobs;
        }
    }
}
