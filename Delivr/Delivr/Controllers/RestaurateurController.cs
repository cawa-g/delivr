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
            return View();
        }

        //
        // POST: /Restaurateur/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurateur restaurateur)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(restaurateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurateur);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}