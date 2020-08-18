using Microsoft.AspNetCore.Mvc.Rendering;
using Recruiting.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruiting.Web.Models.ViewModels
{
    public class JobEdit
    {
        public Job Job { get; internal set; }
        public IEnumerable<SelectListItem> Types { get; internal set; }
    }
}
