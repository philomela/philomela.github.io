using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Subtone.ru
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Confirm",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Confirm", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "LoginForm",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Admin", action = "LoginForm", id = UrlParameter.Optional }
           );
        }
    }
}
