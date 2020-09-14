using Recruiting.BL.Services.Interfaces;
using Recruiting.Data.EfRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Services
{
    public abstract class PagingSortingSearchingServiceBase<TDomain, TEntity> : ServiceBase<TDomain, TEntity>, IPagingSortAndSearchService<TDomain>
                                                                where TDomain : class where TEntity : class
    {
        protected string _defaultSort { get; set; }
        protected readonly Func<IEnumerable<TEntity>, IList<TDomain>> _mapListEntityToListDomain;
        public PagingSortingSearchingServiceBase(
                                IEfRepositoryBase<TEntity> efRepository,
                                IEfUnitRepository efUnitRepository,
                                Func<TDomain, TEntity> mapDomainToEntity,
                                Func<TEntity, TDomain> mapEntityToDomain,
                                Func<IEnumerable<TEntity>, IList<TDomain>> mapListEntityToListDomain,
                                string defaultSort)
            : base(efRepository, efUnitRepository, mapDomainToEntity, mapEntityToDomain)
        {
            _mapListEntityToListDomain = mapListEntityToListDomain;
            _defaultSort = defaultSort;
        }
        public virtual Func<TDomain, bool> GetFilter(string search)
        {
            return s => 1 == 1;
        }

        public async Task<(IEnumerable<TDomain>, int)> GetListAsync(string search, string sortOrder, int indexPage, int itemsPerPage)
        {
            IEnumerable<TEntity> efList = await _efRepository.ListAsync();

            return _mapListEntityToListDomain(efList)
                        .ToList()
                        .Where(GetFilter(search))
                        .SortList((sortOrder ?? _defaultSort).Contains("_desc"), GetSort((sortOrder??_defaultSort).Replace("_desc", "")))
                        .GetPageElements(indexPage, itemsPerPage);
        }

        public virtual Func<TDomain, string> GetSort(string sortOrder)
        {
            return s => s.ToString();
        }
    }
}
