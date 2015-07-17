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
        // GET: /Menu/BlankMenuItemRow

        public ActionResult BlankMenuItemRow()
        {
            return PartialView("MenuItemRow", new EditMenuItemModel());
        }

        //
        // GET: /Menu/EditForRestaurant/5

        public ActionResult EditForRestaurant(int? restaurantId = null)
        {
            if (restaurantId == null)
                return HttpNotFound();

            Restaurant restaurant = db.Restaurants.Find(restaurantId);
            if (restaurant == null || (!User.IsInRole("Admin") && restaurant.UserId != WebSecurity.CurrentUserId))
                return HttpNotFound();

            return View(restaurant);
        }

        //
        // POST: /Menu/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditMenuModel model)
        {
            // Simple security to avoid trying to edit an non-existing menu
            if (!model.MenuId.HasValue)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                // Generate a list of MenuItems used by this Menu
                List<MenuItem> menuItems = new List<MenuItem>();
                if (model.MenuItemModels != null)
                {
                    Dictionary<EditMenuItemModel, string> menuItemWarnings = new Dictionary<EditMenuItemModel, string>();

                    foreach (EditMenuItemModel itemModel in model.MenuItemModels)
                    {
                        MenuItem item;
                        if (itemModel.MenuItemId.HasValue)
                        {
                            item = db.MenuItems.Find(itemModel.MenuItemId.Value);
                            item.Nom = itemModel.Nom;
                            item.Description = itemModel.Description;
                            item.Prix = Decimal.ToInt32(itemModel.Prix * 100);

                            // Indicate the item was modified:
                            db.Entry(item).State = EntityState.Modified;
                        }
                        else
                        {
                            item = new MenuItem()
                            {
                                Nom = itemModel.Nom,
                                Description = itemModel.Description,
                                Prix = Decimal.ToInt32(itemModel.Prix * 100)
                            };

                            db.MenuItems.Add(item);
                        }

                        if (String.IsNullOrEmpty(item.Description))
                        {
                            menuItemWarnings[itemModel] = Resources.Menu.DescriptionMissingWarning;
                        }

                        menuItems.Add(item);
                    }

                    ViewBag.MenuItemWarnings = menuItemWarnings;
                }

                Menu menu = db.Menus.Find(model.MenuId.Value);

                // Iterate through the items of the stored menu:
                foreach (MenuItem item in menu.MenuItems.ToList())
                {
                    // If the item does not exist in the presentation model, it has been removed.
                    // Remove from database if there was no CommandeItem associated to the Item.
                    if (!item.CommandeItems.Any() && !model.MenuItemModels.Any(itemModel =>
                        itemModel.MenuItemId.HasValue && itemModel.MenuItemId.Value == item.MenuItemId))
                    {
                        db.MenuItems.Remove(item);
                    }
                }

                menu.Nom = model.Nom;
                menu.MenuItems = menuItems;

                // Indicate the menu was modified:
                db.Entry(menu).State = EntityState.Modified;

                db.SaveChanges();
                ViewBag.SuccessMessage = Resources.Menu.MenuDefinitionSuccessMessage;
            }

            return PartialView("AddEditMenuPartial", model);
        }

        //
        // POST: /Menu/Add

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(EditMenuModel model)
        {
            if (ModelState.IsValid)
            {
                // Generate a list of MenuItems used by this Menu
                List<MenuItem> menuItems = new List<MenuItem>();
                if (model.MenuItemModels != null)
                {
                    Dictionary<EditMenuItemModel, string> menuItemWarnings = new Dictionary<EditMenuItemModel, string>();

                    foreach (EditMenuItemModel itemModel in model.MenuItemModels)
                    {
                        MenuItem item = new MenuItem()
                        {
                            Nom = itemModel.Nom,
                            Description = itemModel.Description,
                            Prix = Decimal.ToInt32(itemModel.Prix * 100)
                        };

                        db.MenuItems.Add(item);

                        if (String.IsNullOrEmpty(item.Description))
                        {
                            menuItemWarnings[itemModel] = Resources.Menu.DescriptionMissingWarning;
                        }

                        menuItems.Add(item);
                    }

                    ViewBag.MenuItemWarnings = menuItemWarnings;
                }

                // Create a new Menu with the information provided, and the MenuItems generated
                Menu menu = new Menu()
                {
                    Nom = model.Nom,
                    RestaurantId = model.RestaurantId,
                    MenuItems = menuItems
                };

                db.Menus.Add(menu);

                db.SaveChanges();
                ViewBag.SuccessMessage = Resources.Menu.MenuDefinitionSuccessMessage;
            }

            return PartialView("AddEditMenuPartial", model);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}