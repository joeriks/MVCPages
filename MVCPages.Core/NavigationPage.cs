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
        public string Controller { get; set; }
        public string Action { get; set; }
        public string View { get; set; }

        public T Content { get; set; }
        private string[] urlParts;
        public string[] UrlParts { get { if (urlParts == null) urlParts = Url.Split('/'); return urlParts; } }
        public string Name { get { return UrlParts.Last(); } }
        public string ParentUrl { get { return string.IsNullOrEmpty(Url)?"":(Url==""||Url=="/")?"":Url.Substring(0, Url.Length - Name.Length - 1); } }

        public DateTime? CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }        
        public int Order { get; set; }
        public bool? HideInNavigation { get; set; }
        public object Id { get; set; }
    }

}
