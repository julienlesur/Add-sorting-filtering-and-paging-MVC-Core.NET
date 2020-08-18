using Recruiting.Data.EfModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.Data.EfRepositories.Interfaces
{
    public interface IEfApplicantRepository : IEfRepositoryBase<EfApplicant>
    {
        public Task<EfApplication> GetLastApplicationByApplicantId(int applicantId);
    }
}
