using Recruiting.BL.Models;
using Recruiting.Data.EfModels;
using System.Collections.Generic;

namespace Recruiting.BL.Mappers
{
    public static class ApplicantMapper
    {
        public static Applicant MapEntityToDomain(EfApplicant entity)
        => 
            entity is null ?
            Applicant._EmptyApplicant :
            new Applicant
            {
                ApplicantId = entity.Id,
                FirstName = entity?.FirstName,
                LastName = entity?.LastName,
                Email = entity?.Email,
                Adress1 = entity?.Adress1,
                Adress2 = entity?.Adress2,
                ZipCode = entity?.ZipCode,
                City = entity?.City,
                Country = entity?.Country
            };

        public static EfApplicant MapDomainToEntity(Applicant domain)
            => new EfApplicant
            {
                Id = domain.ApplicantId,
                FirstName = domain?.FirstName,
                LastName = domain?.LastName,
                Email = domain?.Email,
                Adress1 = domain?.Adress1,
                Adress2 = domain?.Adress2,
                ZipCode = domain?.ZipCode,
                City = domain?.City,
                Country = domain?.Country
            };


        public static IList<Applicant> MapListEntityToListDomain(IEnumerable<EfApplicant> entities)
        {
            IList<Applicant> domainApplicants = new List<Applicant>();
            foreach (EfApplicant entity in entities)
            {
                domainApplicants.Add(MapEntityToDomain(entity));
            }
            return domainApplicants;
        }
    }
}
