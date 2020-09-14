using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Recruiting.Web.Controllers;
using Recruiting.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recruiting.Web.Infrastructures
{
    public class PagingSortingSearchingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller;
            if (controller == null || !(controller is PagingSortingSearchingControllerBase))
                return;

            var viewResult = context.Result;
            if (viewResult == null || !(viewResult is ViewResult))
                return;

            var model = ((ViewResult)viewResult).Model;
            if (model is ControllerActionSSPViewModel)
            {
                return;
            }
            if (model is SortSearchAndPagingViewModel)
            {
                ((SortSearchAndPagingViewModel)model).CurrentPage = ((PagingSortingSearchingControllerBase)controller)._indexPage;
                ((SortSearchAndPagingViewModel)model).CurrentSort = ((PagingSortingSearchingControllerBase)controller)._sortOrder;
                ((SortSearchAndPagingViewModel)model).SearchText = ((PagingSortingSearchingControllerBase)controller)._searchText;

            }
            base.OnActionExecuted(context);
        }
    }
}
