using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
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
        public static IHtmlString OtherLanguageLink(this HtmlHelper htmlHelper)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var anchor = new TagBuilder("a");

            string otherLanguage = Resources.Helper.AvailableCultures.First(
                c => c != Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
            CultureInfo cultureInfo = new CultureInfo(otherLanguage);

            string displayName = null;
            if (!String.IsNullOrWhiteSpace(cultureInfo.NativeName))
            {
                displayName = cultureInfo.NativeName.Trim();
                displayName = char.ToUpper(displayName[0]) + displayName.Substring(1);
            }

            anchor.Attributes["href"] = urlHelper.LanguageUrl(otherLanguage);
            anchor.SetInnerText(displayName);

            return new HtmlString(anchor.ToString());
        }
    }
}