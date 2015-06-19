using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Delivr.Models;
using WebMatrix.WebData;
using System.Web.Security;

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
        // GET: /Restaurant/
         [ActionName("IndexForRestaurateur")]
        public ActionResult Index(int? id)
        {
            List<Restaurant> Restaurants = db.Restaurants.ToList();
            List<Restaurant> RestaurantsToDelete = new List<Restaurant>();
            foreach (Restaurant r in Restaurants)
            {
                if (r.Restaurateur.UserId != id)
                {
                    RestaurantsToDelete.Add(r);
                }
            }
             foreach (Restaurant r in RestaurantsToDelete)
            {
                Restaurants.Remove(r);
            }
            return View(Restaurants);
        }

        //
        // GET: /Restaurant/Details/5

        public ActionResult Details(int id = 0)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            ViewBag.Restaurateur = "";
            string email = "";
            if (restaurant.UserId != null)
            {
                email = db.UserProfiles.Find(restaurant.UserId).Email;
            }         
            if (email != null)
            {
                ViewBag.Restaurateur = email;
            }
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

            var roles = (SimpleRoleProvider)Roles.Provider;

            var allRoles = roles.GetAllRoles();
             List<SelectListItem> restaurateurs = new List<SelectListItem>();
             restaurateurs.Add(new SelectListItem
             {
                 Value = null,
                 Text = "",
             });
             foreach (UserProfile r in db.UserProfiles.ToList())
             {
                 if (roles.GetRolesForUser(r.UserName).Contains("Restaurateur"))
                 {
                     restaurateurs.Add(new SelectListItem
                     {
                         Value = r.UserId.ToString(),
                         Text = r.Nom,
                     });
                 }
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
                    return RedirectToAction("Message", "Restaurant", new { chaine = "Le restaurant à été ajouté sans restaurateur" });
                }
                restaurant.Restaurateur = db.UserProfiles.Find(restaurant.UserId);
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

            var roles = (SimpleRoleProvider)Roles.Provider;

            var allRoles = roles.GetAllRoles();

            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }


            List<SelectListItem> restaurateurs = new List<SelectListItem>();
            restaurateurs.Add(new SelectListItem
            {
                Value = null,
                Text = "",
            });
            foreach (UserProfile r in db.UserProfiles.ToList())
            {
                if (roles.GetRolesForUser(r.UserName).Contains("Restaurateur"))
                { 
                restaurateurs.Add(new SelectListItem
                {
                    Value = r.UserId.ToString(),
                    Text = r.Nom,
                });
                }
            }
            ViewBag.DropDownRestaurateurs = restaurateurs;


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