using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Delivr.Models;
using WebMatrix.WebData;


namespace Delivr.Controllers
{
    public class RestaurateurController : Controller
    {
        private DelivrContext db = new DelivrContext();

        //
        // GET: /Restaurateur/

        public ActionResult Index()
        {
            return View(db.UserProfiles.OfType<Restaurateur>().ToList());
        }

        //
        // GET: /Restaurateur/Details/5

        public ActionResult Details(int id = 0)
        {
            Restaurateur restaurateur = db.UserProfiles.OfType<Restaurateur>().First(u => u.UserId == id);
            if (restaurateur == null)
            {
                return HttpNotFound();
            }
            return View(restaurateur);
        }

        //
        // GET: /Restaurateur/Create

        public ActionResult Create()
        {
            List<SelectListItem> restaurants = new List<SelectListItem>();
            restaurants.Add(new SelectListItem
            {
                Value = null,
                Text = "",
            });
            foreach (Restaurant r in db.Restaurants.ToList())
            {
             
                restaurants.Add(new SelectListItem
                {
                    Value = r.RestaurantId.ToString(),
                    Text = r.nom,
                });
            }
            ViewBag.DropDownRestaurants = restaurants;
            
            return View();
        }

        //
        // POST: /Restaurateur/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateModel model)
        {
            Restaurateur restaurateur = new Restaurateur();
            //if (ModelState.IsValid)
            //{
                //restaurateur.CodeCivique = model.CodeCivique;
                //restaurateur.CodePostale = model.CodePostale;
                //restaurateur.DateNaissance = model.DateNaissance;
                //restaurateur.Email = model.Email;
                //restaurateur.Nom = model.Nom;
                //restaurateur.Prenom = model.Prenom;
                //restaurateur.Rue = model.Rue;
                //restaurateur.Telephone = model.Telephone;
                //restaurateur.UserName = model.Email;
                if (model.restaurantId == null)
                {
                    db.UserProfiles.Add(restaurateur);
                    db.SaveChanges();
                    return RedirectToAction("Message", "Restaurateur", new { chaine = "Le restaurateur à été ajouté sans restaurant" });
                }
                restaurateur.Restaurants.Add(db.Restaurants.Find(model.restaurantId));
                db.UserProfiles.Add(restaurateur);
                db.SaveChanges();   

                return RedirectToAction("Index");
            //}

        }

        //
        // GET: /Restaurateur/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Restaurateur restaurateur = db.UserProfiles.OfType<Restaurateur>().First(u => u.UserId == id);
            if (restaurateur == null)
            {
                return HttpNotFound();
            }
            return View(restaurateur);
        }

        //
        // POST: /Restaurateur/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurateur restaurateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurateur);
        }

        //
        // GET: /Restaurateur/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Restaurateur restaurateur = db.UserProfiles.OfType<Restaurateur>().First(u => u.UserId == id);
            if (restaurateur == null)
            {
                return HttpNotFound();
            }
            return View(restaurateur);
        }

        //
        // POST: /Restaurateur/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurateur restaurateur = db.UserProfiles.OfType<Restaurateur>().First(u => u.UserId == id);
            db.UserProfiles.Remove(restaurateur);
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