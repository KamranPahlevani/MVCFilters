using Filters.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Controllers
{
    public class FilterProcessController : Controller
    {
        //
        // GET: /FilterProcess/

        [CustomAuthorize("adam", "steve", "bob")]
        public ActionResult AuthorizeFilterIndex()
        {
            return View();
        }

        [CustomException]
        public ContentResult ExceptionFilterIndex()
        {
            List<decimal> list = null;
            list.Add(1);
            return Content("Success");
        }

        public ViewResult RedirectToError()        
        {
            if (TempData["HandleErrorInfo"] != null)
                ViewData.Model = TempData["HandleErrorInfo"];
            return View("ErrorViewer");
        }

        [CustomAction]
        public ViewResult ActionFilterIndex()
        {            
            for (int i = 0; i < 1000000; i++)
            {                
            }
            return View();
        }

        [CustomResult]
        public ViewResult ResultFilterIndex()
        {
            for (int i = 0; i < 1000000; i++)
            {
            }
            return View();
        }

        [CustomActionFilterAndResultFilterIndex]
        public ViewResult ActionFilterAndResultFilterIndex()
        {
            for (int i = 0; i < 1000000; i++)
            {
            }
            return View();
        }

        [RequireHttps]
        public ViewResult RequireHttpsFilterIndex()
        {
            return View();
        }

        public ViewResult Index()
        {
            ViewData.Model = DateTime.Now;
            return View();
        }

        [OutputCache(Duration=30)]
        public ViewResult OutputCacheFilterIndex()
        {
            ViewData.Model = DateTime.Now;
            return View();
        }


    }
}
