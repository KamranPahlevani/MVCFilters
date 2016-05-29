using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Filters.Infrastructure.Filters
{
    public class CustomExceptionAttribute:FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is NullReferenceException)
            {
                RouteValueDictionary rvd = new RouteValueDictionary();
                rvd.Add("controller", "FilterProcess");
                rvd.Add("action", "RedirectToError");
                filterContext.Result = new RedirectToRouteResult(rvd);
                filterContext.ExceptionHandled = true;

                if (!filterContext.Controller.TempData.Keys.Contains("HandleErrorInfo"))
                    filterContext.Controller.TempData.Add("HandleErrorInfo", new HandleErrorInfo(filterContext.Exception, filterContext.RequestContext.RouteData.Values["controller"].ToString(), filterContext.RequestContext.RouteData.Values["action"].ToString()));
            }
        }
    }
}