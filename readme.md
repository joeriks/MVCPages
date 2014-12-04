##MVCPages


Easy map content to objects and route them

Create json documents, yaml documents or pocos in your MVC projects, specify url and type and then let MVCPages route it for you.

Included sample running at

[mvcpages.azurewebsites.net](http://mvcpages.azurewebsites.net/) 


For example, store index.yaml in a folder called yamlcontent:

	---
	  Url: "/news"
	  Type: "MVCPages.SampleWeb.Models.PageViewModel"
	  Content:
		Header: "YAML News"
		Introduction: "Intro"
		BodyText: >
		  Lorem ipsum
		  a lot of lines
		  of text

Add the following to your Application_Start():

	RouteTable.Routes.MapRoutesByFiles(Server.MapPath("~/yamlcontent"), "yaml/", "Page", "Index");
	
Add the PageController with Index action:

    public class PageController : Controller
    {
        public ActionResult Index(PageViewModel page)
        {
            return View("Page", page);
        }
    }

Add the model:

    public class PageViewModel
    {
        public string Header { get; set; }
        public string Introduction { get; set; }
        public string BodyText { get; set; }
    }

Add the view Page.cshtml

	@model PageViewModel
	
	<h1>@Model.Header</h1>
	<div class="intro">@Model.Introduction</div>
	<p>@Model.BodyText</p>
	
Done!

You can also use json for documents (similar to yaml), or C# Pocos with an attribute to specify url:

    [MVCPages.PageUrl("/news/new-software")]
    public class new_software : Models.PageViewModel
    {
        public new_software()
        {
            BodyText = "Lorem";
            Header = "New Software";
            Introduction = "Lorem ipsum";
        }
    }