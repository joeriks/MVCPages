using MVCPages.SampleWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlContent.Controllers
{
    public class NamesController : Controller
    {
        // GET: Alt
        public ActionResult Index(NamesViewModel page)
        {            
            return View(page);
        }
    }
}