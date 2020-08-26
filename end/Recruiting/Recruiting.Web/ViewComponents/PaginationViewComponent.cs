using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Recruiting.Infrastructures.Configuration;
using Recruiting.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        private readonly GridConfiguration _gridOptions;

        public PaginationViewComponent(IOptions<GridConfiguration> gridOptions)
        {
            _gridOptions = gridOptions.Value;
        }
        public IViewComponentResult Invoke(
            int numberOfItems, 
            int currentPage, 
            string search, 
            string sort, 
            string controller, 
            string action = "List")
        {
            ControllerActionSSPViewModel vm = new ControllerActionSSPViewModel
            {
                NumberOfItems = numberOfItems,
                ItemsPerPage = _gridOptions.ItemsPerPage,
                CurrentPage = currentPage,
                NumberOfPage = (int)Math.Ceiling((double)numberOfItems / _gridOptions.ItemsPerPage),
                Controller= controller,
                Action= action,
                SearchText = search,
                CurrentSort= sort
            };
            return View(vm);
        }
    }
}
