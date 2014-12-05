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
        public string Action { get; set; }
        public string Controller { get; set; }
        public string View { get; set; }

        public string Url { get; set; }
        public string Type { get; set; }
        public object Content { get; set; }

        public DateTime? CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

        public int? Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? HideInNavigation { get; set; }
        public object Id { get; set; }

    }
    public class FilePopulator : NavigationPopulator
    {
        private string rootPath;
        private Type defaultType;
        public FilePopulator(string rootPath, Type defaultType = null)
        {
            this.rootPath = rootPath;
            if (defaultType != null) this.defaultType = defaultType;
        }

        private object getPropertyValueWithReflection(object obj, string propertyName){

            var type = obj.GetType();
            var prop = type.GetProperty(propertyName);

            if (prop==null) return null;
            
            return prop.GetValue(obj);

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

                if (contentPage.Type == null && defaultType != null) page.Content = Newtonsoft.Json.JsonConvert.DeserializeObject(contentPage.Content.ToString(), defaultType);
                else if (contentPage.Type == "Dynamic" || (contentPage.Type==null && defaultType == null)) page.Content = Newtonsoft.Json.JsonConvert.DeserializeObject(contentPage.Content.ToString());
                else
                {
                    var type = GetType(contentPage.Type);
                    if (type == null) throw new Exception("Could not find type " + contentPage.Type);
                    page.Content = Newtonsoft.Json.JsonConvert.DeserializeObject(contentPage.Content.ToString(), type);
                }


                page.Action = contentPage.Action;
                page.Controller = contentPage.Controller;
                page.View = contentPage.View;

                page.Order = contentPage.Order??el.i;
                page.Id = contentPage.Id;
                page.Title = contentPage.Title;
                page.Description = contentPage.Description;
                page.HideInNavigation = contentPage.HideInNavigation;

                page.CreatedDateTime = contentPage.CreatedDateTime;
                page.UpdatedDateTime = contentPage.UpdatedDateTime;

                page.Url = new Func<string>(() =>
                {

                    if (!string.IsNullOrEmpty(contentPage.Url)) return contentPage.Url;

                    //new Action(() =>
                    //{

                    //    var x = "c://foo//bar";
                    //    var y = "c://foo";



                    //});
                    
                    

                    var thisPart = el.file.Substring(this.rootPath.Length);
                    var withoutExtension = System.IO.Path.ChangeExtension(thisPart, null);
                    var withCorrectDashes = withoutExtension.Replace("\\", "/");
                    var withoutEndingIndex = withCorrectDashes.Replace("/_index", "");

                    return withoutEndingIndex;

                })();

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
