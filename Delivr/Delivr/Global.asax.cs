using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace Delivr
{
    // Remarque : pour obtenir des instructions sur l'activation du mode classique IIS6 ou IIS7, 
    // visitez http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_AcquireRequestState()
        {
            RouteData routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(Context));
            string requestedLang = routeData.Values["lang"] as string;

            // If we cannot provide the requested language, we try providing
            // one of the languages from the user's list.
            if (!Resources.Helper.AvailableCultures.Contains(requestedLang))
            {
                requestedLang = String.Empty;
                if (Request.UserLanguages != null)
                {
                    foreach (var _lang in Request.UserLanguages)
                    {
                        string match = Resources.Helper.AvailableCultures.FirstOrDefault(c => c.Substring(0, 2) == _lang.Substring(0, 2));

                        if (match != null)
                        {
                            requestedLang = match;
                            break;
                        }
                    }
                }
            }

            try
            {
                var cultureInfo = new CultureInfo(requestedLang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            catch { }
        }
    }
}