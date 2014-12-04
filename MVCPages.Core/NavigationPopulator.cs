using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
namespace MVCPages.Core
{
    public abstract class NavigationPopulator
    {
        public abstract IEnumerable<NavigationPage<object>> PopulateNavigationPages();

        public void MapRoutes(System.Web.Routing.RouteCollection routes, string rootUrl, string controller = "Page", string action = "Index", string routePrefix = "MVCPages_")
        {
            var pages = PopulateNavigationPages();
            foreach (var page in pages)
            {
                // rootUrl should be "" or end with /

                if (rootUrl != "")
                {
                    if (!rootUrl.EndsWith("/")) rootUrl += "/";
                    else if (rootUrl == "/") rootUrl = "";
                }

                // pageUrl should not start with /
                var pageUrl = page.Url.TrimStart('/');

                var url = rootUrl + pageUrl;

                if (!string.IsNullOrEmpty(page.Action)) action = page.Action;
                if (!string.IsNullOrEmpty(page.Controller)) controller = page.Controller;

                routes.MapRoute(
                    name: routePrefix + url,
                    url: url,
                    defaults: new { controller, action, page = page.Content, navigation = pages }
               );
            }
        }

    }
}
