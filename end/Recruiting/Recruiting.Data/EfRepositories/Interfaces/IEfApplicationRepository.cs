using Recruiting.Data.EfModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruiting.Data.EfRepositories.Interfaces
{
    public interface IEfApplicationRepository
    {
        public Task<ICollection<EfApplication>> GetApplicationListByJobReference(string jobReference);
        public Task<EfApplication> AddAsync(EfApplication newApplication);
        public Task<EfApplication> AddFromJobReference(string jobReference);
        Task<ICollection<EfApplication>> GetListByIdApplicant(int applicantId);
        Task DeleteAsync(int applicantId, int jobId);
    }
}
