using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Web.Framework.Seo
{
    public static class GenericPathRouteExtensions
    {
        //Override for localized route
        public static Route MapGenericPathRoute(this RouteCollection routes, string name, string url)
        {
            return MapGenericPathRoute(routes, name, url, null /* defaults */, (object)null /* constraints */);
        }
        public static Route MapGenericPathRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return MapGenericPathRoute(routes, name, url, defaults, (object)null /* constraints */);
        }
        public static Route MapGenericPathRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            return MapGenericPathRoute(routes, name, url, defaults, constraints, null /* namespaces */);
        }
        public static Route MapGenericPathRoute(this RouteCollection routes, string name, string url, string[] namespaces)
        {
            return MapGenericPathRoute(routes, name, url, null /* defaults */, null /* constraints */, namespaces);
        }
        public static Route MapGenericPathRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
        {
            return MapGenericPathRoute(routes, name, url, defaults, null /* constraints */, namespaces);
        }

        /// <summary>
        /// Maps the specified URL route and sets default route values, constraints, and namespaces.
        /// </summary>
        /// <param name="routes">A collection of routes for the application.</param>
        /// <param name="name">The name of the route to map.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="constraints">A set of expressions that specify values for the url parameter.</param>
        /// <param name="namespaces">A set of namespaces for the application.</param>
        /// <returns>A reference to the mapped route.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// The routes or url parameter is null.所以开头要判断这两个值
        /// </exception>
        public static Route MapGenericPathRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            var route = new GenericPathRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(),
                Constraints = new RouteValueDictionary(),
                DataTokens = new RouteValueDictionary()
            };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens["namespaces"] = namespaces;
            }

            routes.Add(name, route);

            return route;
        }
    }
}
