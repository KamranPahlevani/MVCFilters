using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure.Filters
{
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        private string[] allowedUsers;

        public CustomAuthorizeAttribute(params string[] users)
        {
            allowedUsers = users;
        }

        /// <summary>
        /// this method use for user Authorize and user Role and user Access Level
        /// this method dont know any thing about request and relative request objects for example RouteData
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthenticated = (httpContext.Request.IsAuthenticated && allowedUsers.Contains(httpContext.User.Identity.Name, StringComparer.InvariantCultureIgnoreCase) || httpContext.Request.IsLocal);
            return isAuthenticated;
        }

        /// <summary>
        /// this method use for logging and session management
        /// this method access to request object
        /// AuthorizationContext is concrete of ControllerContext
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            ///if dont set result mvc assume that this action method is authorized
            ///but if set result mvc prevent action process
            //filterContext.Result = new HttpUnauthorizedResult();

            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new JsonResult()
                {
                    Data = new
                    {
                        Error = "NotAuthorized",
                        LogOnUrl = urlHelper.Action("LogOn", "Account")
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };


            }
            else
                base.HandleUnauthorizedRequest(filterContext);
        }

    }
}