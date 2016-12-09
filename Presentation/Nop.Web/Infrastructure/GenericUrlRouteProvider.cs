using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Seo;

namespace Nop.Web.Infrastructure
{
    public class GenericUrlRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //generic URLs
            routes.MapGenericPathRoute("GenericUrl",
                "{generic_se_name}",
                new { controller = "Common", action = "GenericUrl" },
                new[] { "Nop.Web.Controllers" });

            routes.MapLocalizedRoute("Topic",
                "{SeName}",
                new { controller = "Topic", action = "TopicDetails" },
                new object[] { "Nop.Web.Controllers" });
            //the last route. it's used when none of registered routes could be used for the current request
            //but in this case we cannot process non-registered routes (/controller/action)
            //routes.MapLocalizedRoute(
            //    "PageNotFound-Wildchar",
            //    "{*url}",
            //    new { controller = "Common", action = "PageNotFound" },
            //    new[] { "Nop.Web.Controllers" });
        }

        public int Priority
        {
            get
            {
                //it should be the last route
                //we do not set it to -int.MaxValue so it could be overridden (if required)
                return -1000000;
            }
        }
    }
}