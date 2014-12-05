using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCPages
{

    // TODO: Very temporary

    public class Navigation
    {
        public List<NavigationPage<object>> NavigationPages { get; set; }

        public string CurrentUrl { get; set; }
        public string RootUrl { get; set; }

        public string CurrentUrlRelative()
        {
            // TODO: obviously

            return ("/" + ("/" + CurrentUrl).Substring(RootUrl.Length)).Replace("//", "/");
        }
        public NavigationPage<object> Current()
        {
            return NavigationPages.SingleOrDefault(t => t.Url == CurrentUrlRelative());
        }
        public IEnumerable<NavigationPage<object>> Siblings()
        {
            return NavigationPages.Where(t => t.ParentUrl == Current().ParentUrl);
        }
        public IEnumerable<NavigationPage<object>> Children()
        {
            return NavigationPages.Where(t => t.ParentUrl == CurrentUrlRelative());
        }
        public NavigationPage<object> Parent()
        {
            return NavigationPages.SingleOrDefault(t => t.Url == Current().ParentUrl);
        }

        public static Navigation GetCurrent()
        {
            if (System.Web.HttpContext.Current == null) return null;

            var routeData = System.Web.HttpContext.Current.Request.RequestContext.RouteData;

            var nav = (Navigation)routeData.Values["Navigation"];
            var currentUrl = ((System.Web.Routing.Route)(routeData.Route)).Url;
            nav.CurrentUrl = currentUrl;
            return nav;
        }
    }
}
