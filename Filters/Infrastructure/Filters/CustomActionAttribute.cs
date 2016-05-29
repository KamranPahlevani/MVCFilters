using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure.Filters
{
    public class CustomActionAttribute:FilterAttribute,IActionFilter
    {
        private Stopwatch timer;

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            timer.Stop();
            if (filterContext.Exception == null)
                filterContext.Controller.ViewData.Model = timer.Elapsed.TotalSeconds;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }
    }
}