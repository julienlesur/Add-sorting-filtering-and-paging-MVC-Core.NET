using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Recruiting.Infrastructures.Configurations;
using Recruiting.Web.Infrastructures;
using Recruiting.Web.Models.ViewModels;
using System;

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
            string controller, 
            string action = "List")
        {
            PaginationViewModel vm = new PaginationViewModel
            {
                NumberOfItems = numberOfItems,
                ItemsPerPage = _gridOptions.ItemsPerPage,
                CurrentPage = currentPage,
                NumberOfPage = (int)Math.Ceiling((double)numberOfItems / _gridOptions.ItemsPerPage),
                Controller= controller,
                Action= action
            };
            return View(vm);
        }
    }
}
