using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

/*********************************************************************************************************************
 * The following code and every segment that uses it was taken from                                                  *
 * http://thatextramile.be/blog/2011/04/automatically-including-current-language-in-generated-urls-with-asp-net-mvc/ *
 *********************************************************************************************************************/

namespace Delivr.Helpers
{
    public class AutoLocalizingRoute : Route
    {
        public AutoLocalizingRoute(string url, object defaults, object constraints)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler()) { }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            // only set the culture if it's not present in the values dictionary yet
            // this check ensures that we can link to a specific language when we need to (fe: when picking your language)
            if (!values.ContainsKey("lang"))
            {
                values["lang"] = Thread.CurrentThread.CurrentCulture.Name;
            }

            return base.GetVirtualPath(requestContext, values);
        }
    }
}