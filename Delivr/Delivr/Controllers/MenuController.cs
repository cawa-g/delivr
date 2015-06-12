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
    public class MenuController : Controller
    {
        private DelivrContext db = new DelivrContext();

        //
        // GET: /Menu/Details/5

        public ActionResult Details(int id = 0)
        {
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        
        //
        // GET: /Menu/Edit/5

        public ActionResult Edit(int? restaurantId = null)
        {
            if (restaurantId == null)
                return HttpNotFound();

            Menu menu = db.Menus.SingleOrDefault(m => m.RestaurantId == restaurantId);

            EditMenuModel model;
            if (menu != null)
            {
                model = new EditMenuModel(menu);
            }
            else
            {
                Restaurant restaurant = db.Restaurants.Find(restaurantId);

                if (restaurant != null) // TODO: Check for Restaurateur ID.
                {
                    model = new EditMenuModel(restaurantId.Value);
                }
                else
                {
                    return HttpNotFound();
                }
            }

            return View(model);
        }

        //
        // POST: /Menu/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditMenuModel model)
        {
            if (ModelState.IsValid)
            {
                /*db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");*/
            }

            return View(model);
        }

        //
        // GET: /Menu/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        //
        // POST: /Menu/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
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