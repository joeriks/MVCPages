using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPages
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                        System.AttributeTargets.Struct)
    ]
    public class PageUrlAttribute : System.Attribute
    {
        public string Url;

        public PageUrlAttribute(string url)
        {
            this.Url = url;
        }
    }

}