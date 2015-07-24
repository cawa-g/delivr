using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Delivr.Models;
using WebMatrix.WebData;
using Delivr.Helpers;

namespace Delivr.Controllers
{
    public class CommandeController : Controller
    {
        private DelivrContext db = new DelivrContext();
        private TwilioHelper twilio = new TwilioHelper();


       


        public ActionResult Index()
        {
            var commandes = db.Commandes.Include(c => c.User).Include(c => c.Restaurant);
            return View(commandes.ToList());
        }

        //
        // GET: /Commande/Details/5

        public ActionResult Details(int id = 0)
        {
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        //
        // GET: /Commande/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "RestaurantId", "nom");
            return View();
        }

        //
        // POST: /Commande/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Commandes.Add(commande);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", commande.UserId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "RestaurantId", "nom", commande.RestaurantId);
            return View(commande);
        }

        //
        // GET: /Commande/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", commande.UserId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "RestaurantId", "nom", commande.RestaurantId);
            return View(commande);
        }

        //
        // POST: /Commande/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commande).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", commande.UserId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "RestaurantId", "nom", commande.RestaurantId);
            return View(commande);
        }

        //
        // GET: /Commande/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        //
        // POST: /Commande/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commande commande = db.Commandes.Find(id);
            db.Commandes.Remove(commande);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Commande/List

        public ActionResult List(int? restaurantId = null)
        {
            if (!restaurantId.HasValue)
                return HttpNotFound();

            Restaurant restaurant = db.Restaurants.Find(restaurantId);
            if (restaurant == null || restaurant.UserId != WebSecurity.CurrentUserId)
                return HttpNotFound();

            var commandes = restaurant.Commandes.Where(c => c.Statut != Commande.StatutCommande.EnLivraison && c.Statut != Commande.StatutCommande.Livree).OrderBy(c => c.Date);

            return View(commandes);
        }

        //
        // POST: /Commande/Statut

        public ActionResult Statut(SetStatutCommandeModel model)
        {
            if (model.Statut == null ||
                (model.Statut != Commande.StatutCommande.EnAttente
                && model.Statut != Commande.StatutCommande.EnPreparation
                && model.Statut != Commande.StatutCommande.Prete
                && model.Statut != Commande.StatutCommande.EnLivraison
                && model.Statut != Commande.StatutCommande.Livree))
            {
                return HttpNotFound();
            }

            Commande commande = db.Commandes.Find(model.Id);
            if (commande == null)
                return HttpNotFound();

            commande.Statut = model.Statut;
            db.Entry(commande).State = EntityState.Modified;
            db.SaveChanges();

            UserProfile user = db.UserProfiles.Find(commande.UserId);
            twilio.SendSMS("Votre commande #: " + commande.CommandeId + " est maintenant: " + commande.Statut, user.Telephone);

            return PartialView("StatutCommande", commande);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}