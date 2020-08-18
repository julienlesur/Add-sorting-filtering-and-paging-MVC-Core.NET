using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.EfRepositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Recruiting.BL.Services
{
    public abstract class ServiceBase<TDomain, TEntity> : IServiceBase<TDomain>
    {
        protected readonly Func<TDomain, TEntity> _mapDomainToEntity;
        protected readonly Func<TEntity, TDomain> _mapEntityToDomain;
        private readonly IEfRepositoryBase<TEntity> _efRepository;
        protected readonly IEfUnitRepository _efUnitRepository;

        protected ServiceBase(IEfRepositoryBase<TEntity> efRepository,
                                IEfUnitRepository efUnitRepository,
                                Func<TDomain, TEntity> mapDomainToEntity, 
                                Func<TEntity, TDomain> mapEntityToDomain)
        {
            _mapDomainToEntity = mapDomainToEntity;
            _mapEntityToDomain = mapEntityToDomain;
            _efRepository = efRepository;
            _efUnitRepository = efUnitRepository;
        }
        public async Task<TDomain> AddAsync(TDomain newDomainEntity)
        {
            TEntity entity = _mapDomainToEntity(newDomainEntity);
            await _efRepository.AddAsync(entity);
            await _efUnitRepository.CommitAsync();
            return _mapEntityToDomain(entity);
        }

        public async Task<TDomain> DeleteAsync(int id)
        {
            TEntity entity = await _efRepository.DeleteAsync(id);
            await _efUnitRepository.CommitAsync();
            return _mapEntityToDomain(entity);
        }

        public async Task<TDomain> FindByIdAsync(int id)
        {
            TEntity entity = await _efRepository.FindByIdAsync(id);
            return _mapEntityToDomain(entity);
        }

        public async Task<TDomain> UpdateAsync(TDomain updatedDomainEntity)
        {
            TEntity entity = _mapDomainToEntity(updatedDomainEntity);
            _efRepository.Update(entity);
            await _efUnitRepository.CommitAsync();
            return _mapEntityToDomain(entity);
        }
    }
}
