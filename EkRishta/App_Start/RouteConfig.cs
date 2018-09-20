using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EkRishta
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Area", "", new { area = "MobileApp", controller = "Login", action = "MobileIndex" },
                                      new[] { "EkRishta.Areas.MobileApp.Controllers" });
            //routes.MapRoute("Area", "", new { area = "WebApp", controller = "Login", action = "WebIndex" },
            //                         new[] { "EkRishta.Areas.WebApp.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
