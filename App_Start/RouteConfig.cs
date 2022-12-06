using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // order of results matter, looks for most specific to most generic
            //most common is to use teh overload with 3 parameters "name", "url", defaults
            // anonymous object is used
            //routes.MapRoute(
            //    "MoviesByReleaseDate", 
            //    "movies/released/{year}/{month}",
            //    new {controller = "Movies", action = "ByReleaseDate" },
            //    //contraint to require year to be 4 digits and month 2 digits
            //    //new { year = @"\d{4}", month =@"\d{2}"});
            //    // constraint for specific year
            //    new   { year = @"2015|2016", month = @"\d{2}" });

            //use attribute routing
            //1: enable attribute routes
            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
