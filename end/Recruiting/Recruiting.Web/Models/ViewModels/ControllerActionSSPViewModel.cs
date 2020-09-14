using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.Models.ViewModels
{
    public abstract class ControllerActionSSPViewModel : SortSearchAndPagingViewModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
