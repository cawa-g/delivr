using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Delivr.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Livraison sur commande";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Do no contact";

            return View();
        }
    }
}
