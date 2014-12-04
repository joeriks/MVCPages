using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCPages.SampleWeb.Models;

namespace MVCPages.SampleWeb
{
    public class PageController : Controller
    {
        public ActionResult Index(PageViewModel page)
        {
            return View(page);
        }

        public ActionResult Alternative(PageViewModel page)
        {
            return View(page);
        }
    }
}