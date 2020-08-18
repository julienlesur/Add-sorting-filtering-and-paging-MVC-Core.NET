using Microsoft.EntityFrameworkCore;
using Recruiting.Data.Data;
using Recruiting.Data.EfModels;
using Recruiting.Data.EfRepositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recruiting.Data.EfRepositories
{
    public abstract class EfRepositoryBase<T> : IEfRepositoryBase<T> where T : EfModelBase
    {
        protected readonly RecruitingContext _context;
        protected readonly DbSet<T> _dbSet;

        public EfRepositoryBase(RecruitingContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T newEntity)
        {
            await _context.AddAsync(newEntity);
            return newEntity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
            }
            return entity;
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public T Update(T updatedEntity)
        {
            var entity = _dbSet.Attach(updatedEntity);
            entity.State = EntityState.Modified;
            return updatedEntity;
        }
    }
}
