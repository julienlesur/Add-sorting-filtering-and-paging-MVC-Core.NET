using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recruiting.Infrastructures.ActionFilters
{
    public class JobReferenceAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("jobReference", out var jobReference) && !String.IsNullOrEmpty(jobReference?.ToString()))
            {
                var controller = context.Controller as Controller;
                controller.ViewData["jobReference"] = jobReference.ToString();
            }
        }
    }
}
