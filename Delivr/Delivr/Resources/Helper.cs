using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivr.Resources
{
    public static class Helper
    {
        public static readonly ISet<String> AvailableCultures = new HashSet<String>()
        {
            "fr-CA",
            "en-CA"
        };
    }
}