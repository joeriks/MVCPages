using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCPages.Core;

namespace MVCPages
{
    public class PageUrlAttributePopulator : NavigationPopulator
    {
        private Assembly assembly;
        public PageUrlAttributePopulator(Assembly assembly){
            this.assembly = assembly;
        }
        public override IEnumerable<NavigationPage<object>> PopulateNavigationPages()
        {
            foreach (var el in getTypesWithUrlAttribute(assembly).Select((type, i) => new { type, i }))
            {
                var page = new NavigationPage<object>();
                page.Content = Activator.CreateInstance(el.type);
                page.Order = el.i;
                page.Url = el.type.GetCustomAttribute<PageUrlAttribute>().Url;
                yield return page;
            }
        }

        private IEnumerable<Type> getTypesWithUrlAttribute(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(PageUrlAttribute), true).Length > 0)
                {
                    yield return type;
                }
            }
        }

    }
}
