﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace MVCPages
{
    public static class RoutingExtensionMethods
    {
        public static void MapRoutesByUrlAttribute(this System.Web.Routing.RouteCollection routes, string rootUrl = "/", string controller = "Page", string action = "Index", string routePrefix = "UrlAttributeRoutes_")
        {
            var r = new PageUrlAttributePopulator(System.Reflection.Assembly.GetCallingAssembly());
            r.MapRoutes(routes, rootUrl, controller, action, routePrefix);
        }

        public static void MapRoutesByFiles(this System.Web.Routing.RouteCollection routes, string mapPath, string rootUrl = "/", string controller = "Page", string action = "Index", string routePrefix = "JsonRoutes_")
        {

            var r = new FilePopulator(mapPath);
            r.MapRoutes(routes, rootUrl, controller, action, routePrefix);
        }

    }
}
