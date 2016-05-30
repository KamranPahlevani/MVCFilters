using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure.Filters
{
    public class CustomActionFilterAndResultFilterIndex:ActionFilterAttribute
    {
        private Stopwatch timer;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            timer.Stop();
            if (filterContext.Exception == null)
                filterContext.Controller.ViewData.Model = timer.Elapsed.TotalSeconds.ToString();
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();
            if (filterContext.Exception == null)
                filterContext.HttpContext.Response.Write("result elapsed time: " + timer.Elapsed.TotalSeconds.ToString());
        }
    }
}