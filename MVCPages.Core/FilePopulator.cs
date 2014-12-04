using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCPages.Core;
using YamlDotNet.Serialization;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Dynamic;

namespace MVCPages
{
    public class ContentPage
    {
        public string Url { get; set; }
        public string Type { get; set; }
        public object Content { get; set; }
    }
    public class FilePopulator : NavigationPopulator
    {
        private string rootPath;
        public FilePopulator(string rootPath)
        {
            this.rootPath = rootPath;
        }
        public override IEnumerable<NavigationPage<object>> PopulateNavigationPages()
        {
            var deserializer = new Deserializer(namingConvention: new PascalCaseNamingConvention());
            var serializer = new Serializer(SerializationOptions.JsonCompatible);

            foreach (var el in getFiles(this.rootPath).Select((file, i) => new { file, i }))
            {
                var page = new NavigationPage<object>();

                var jsonContent = "";
                var fileContent = System.IO.File.ReadAllText(el.file);
                if (new System.IO.FileInfo(el.file).Extension == ".yaml")
                {
                    var yamlContent = deserializer.Deserialize(new StringReader(fileContent));
                    var sw = new StringWriter();
                    serializer.Serialize(sw, yamlContent);                    
                    jsonContent = sw.ToString();
                }
                else
                {
                    jsonContent = fileContent;
                }

                var contentPage = Newtonsoft.Json.JsonConvert.DeserializeObject<ContentPage>(jsonContent);

                var pageContent = Newtonsoft.Json.JsonConvert.DeserializeObject(contentPage.Content.ToString(), GetType(contentPage.Type));

                page.Content = pageContent;
                page.Order = el.i;
                page.Url = contentPage.Url;// el.file.Substring(this.rootPath.Length);
                yield return page;
            }
        }

        private IEnumerable<string> getFiles(string path, string pattern = "*.*")
        {
            return System.IO.Directory.EnumerateFiles(path, pattern, System.IO.SearchOption.AllDirectories);
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
        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
    }
}
