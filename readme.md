##MVCPages

Easy map content to objects and route them

Create json documents, yaml documents or pocos in your MVC projects, specify url and type and then let MVCPages route it for you.

**Status**: Working proof of concept, see [mvcpages.azurewebsites.net](http://mvcpages.azurewebsites.net/) 

###Simple example

1. Save some yaml files in a folder called yamlcontent

	_index.yaml
	about.yaml
	
	news/
		_index.yaml
		release-version.yaml
		friday-update.yaml

Add content in the files like this:

	---
	  View: "SomeView"
	  Content:
	    Header: "YAML News"
	    Introduction: "Intro"
	    BodyText: >
	      Lorem ipsum
	      a lot of lines
	      of text

Name one of them _index.yaml. That will be the start page for that path (index.html)

2. Add the following line to Global.asax.cs:

	RouteTable.Routes.MapRoutesByFiles(Server.MapPath("~/yamlcontent"));

3. Add a view called SomeView.cshtml to your Views/Shared folder

	<h1>@Model.Header</h1>
	<div><strong>@Model.Introduction</strong</div>
	<div>@Model.BodyText</div>

4. Done! The pages now opens on the relative paths from inside your yamlfolders folder, for example "/", "/about" and "/news/friday-update"

###Richer example
Store index.yaml in a folder called yamlcontent:

	---
	  Url: "/news"
	  Type: "MVCPages.SampleWeb.Models.PageViewModel"
      Controller: "Page"
	  Action: "Index"
	  
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
	
###Done, this page now opens on the url /news

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