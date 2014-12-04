using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPages
{
    public class NavigationPage<T>
    {
        public string Url { get; set; }
        public T Content { get; set; }
        private string[] urlParts;
        public string[] UrlParts { get { if (urlParts == null) urlParts = Url.Split('/'); return urlParts; } }
        public string Name { get { return UrlParts.Last(); } }
        public string ParentUrl { get { return Url.Substring(0, Url.Length - Name.Length - 1); } }
        public int Order { get; set; }
        public bool Hidden { get; set; }
        
    }

}
