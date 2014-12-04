using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlContent.Controllers
{
    public class DefaultPageController : Controller
    {
        public ActionResult Index(dynamic page, string view)
        {            
            if (string.IsNullOrEmpty(view))
                return View(page);
            else
                return View(view, page);
        }
    }
}