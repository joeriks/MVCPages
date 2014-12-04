using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MVCPages;
using MVCPages.SampleWeb.Models;

namespace UrlContent
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.MapRoutesByFiles(Server.MapPath("~/Pages_Json"), "/j", defaultController: "Page", defaultAction: "Index", defaultType: typeof(PageViewModel));

            RouteTable.Routes.MapRoutesByFiles(Server.MapPath("~/Pages_Yaml"), "/y");

            RouteTable.Routes.MapRoutesByUrlAttribute("/", "Page", "Index");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
