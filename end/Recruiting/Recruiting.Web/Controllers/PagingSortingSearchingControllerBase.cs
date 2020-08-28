using Microsoft.AspNetCore.Mvc;

namespace Recruiting.Web.Controllers
{
    public abstract class PagingSortingSearchingControllerBase : Controller
    {
        [ModelBinder]
        public string SearchText { get; set; }
        [ModelBinder]
        public string SortOrder { get; set; }
        [ModelBinder]
        public int? IndexPage { get; set; }

        public string _searchText { get { return SearchText; } }
        public virtual string _sortOrder{ get { return SortOrder; } }
        public int _indexPage { get { return IndexPage ?? 1; } }
    }
}
