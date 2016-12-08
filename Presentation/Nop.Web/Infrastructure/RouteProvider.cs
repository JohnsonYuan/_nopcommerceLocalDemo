using System;
using System.Web.Routing;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Web.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //authenticate topic AJAX link
            routes.MapLocalizedRoute("TopicAuthenticate",
                "topic/authenticate",
                new {controller = "Topic", action = "Authenticate" },
                new[] {"Nop.Web.Controllers"});

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