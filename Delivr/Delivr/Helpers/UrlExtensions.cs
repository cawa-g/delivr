using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

/*******************************************************************************************************
 * The following code and every segment that uses it was taken from                                    *
 * http://stackoverflow.com/questions/9807585/mvc3-actionlink-change-language-and-keep-same-parameters *
 *******************************************************************************************************/

namespace Delivr.Helpers
{
    public static class UrlExtensions
    {
        public static string LanguageUrl(this UrlHelper urlHelper, string lang)
        {
            var rd = urlHelper.RequestContext.RouteData;
            var request = urlHelper.RequestContext.HttpContext.Request;
            var values = new RouteValueDictionary(rd.Values);
            foreach (string key in request.QueryString.Keys)
            {
                values[key] = request.QueryString[key];
            }
            values["lang"] = lang;
            return urlHelper.RouteUrl(values);
        }
    }
}