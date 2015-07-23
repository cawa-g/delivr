using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/*******************************************************************************************************
 * The following code and every segment that uses it was taken from                                    *
 * http://stackoverflow.com/questions/9807585/mvc3-actionlink-change-language-and-keep-same-parameters *
 *******************************************************************************************************/

namespace Delivr.Helpers
{
    public static class HtmlExtensions
    {
        public static IHtmlString LanguageLink(this HtmlHelper htmlHelper, string linkText, string lang)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var anchor = new TagBuilder("a");
            anchor.Attributes["href"] = urlHelper.LanguageUrl(lang);
            anchor.SetInnerText(linkText);
            return new HtmlString(anchor.ToString());
        }
    }
}