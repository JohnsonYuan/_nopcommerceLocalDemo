using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Web.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //We reordered our routes so the most used ones are on top. It can improve performance.

            //home page
            routes.MapLocalizedRoute("HomePage",
                "",
                new { controller = "Home", action = "Index" },
                new[] { "Nop.Web.Controllers" });

            //authenticate topic AJAX link
            routes.MapLocalizedRoute("TopicAuthenticate",
                "topic/authenticate",
                new {controller = "Topic", action = "Authenticate" },
                new[] {"Nop.Web.Controllers"});

            //install
            routes.MapRoute("Install",
                "install",
                new { controller = "Install", action = "Index" },
                new[] { "Nop.Web.Controllers" });

            //page not found
            routes.MapLocalizedRoute("PageNotFound",
                            "page-not-found",
                            new { controller = "Common", action = "PageNotFound" },
                            new[] { "Nop.Web.Controllers" });
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}