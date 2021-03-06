﻿using Delivr.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Delivr
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.Add(
                name: "LocalizingRoute",
                item: new AutoLocalizingRoute(
                    url: "{lang}/{controller}/{action}/{id}",
                    defaults: new { id = UrlParameter.Optional },
                    constraints: new { lang = @"^[a-z]{2}$" }
                )
            );

            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new
                {
                    lang = UrlParameter.Optional,
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}