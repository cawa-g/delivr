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
    public class CommandeController : Controller
    {
        private DelivrContext db = new DelivrContext();

        //
        // GET: /Commande/

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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}