using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Delivr.Models;

namespace Delivr.Controllers
{
    public class RestaurantController : Controller
    {
        private DelivrContext db = new DelivrContext();

        //
        // GET: /Restaurant/

        public ActionResult Index()
        {
            return View(db.Restaurants.ToList());
        }

        //
        // GET: /Restaurant/Details/5

        public ActionResult Details(int id = 0)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        //
        // GET: /Restaurant/Create

        public ActionResult Create()
        {
            

             List<SelectListItem> restaurateurs = new List<SelectListItem>();
             restaurateurs.Add(new SelectListItem
             {
                 Value = null,
                 Text = "",
             });
            foreach (Restaurateur r in db.Restaurateurs.ToList())
            {
                restaurateurs.Add(new SelectListItem
                {
                    Value = r.UserId.ToString(),
                    Text = r.Nom,
                });
            }
            ViewBag.DropDownRestaurateurs = restaurateurs;

            return View();
        }

        //
        // POST: /Restaurant/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                if (restaurant.UserId == null)
                {
                    db.Restaurants.Add(restaurant);
                    db.SaveChanges();
                    return RedirectToAction("Message", "Restaurant","Le restaurant à été ajouté sans restaurateur");
                }
                restaurant.Restaurateur = db.Restaurateurs.Find(restaurant.UserId);
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        //
        // GET: /Restaurant/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        //
        // POST: /Restaurant/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        //
        // GET: /Restaurant/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        //
        // POST: /Restaurant/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Account/Message


        [ValidateInput(false)]
        public ActionResult Message(string chaine)
        {
            ViewBag.Chaine = chaine;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}