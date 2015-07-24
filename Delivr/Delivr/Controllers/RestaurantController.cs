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
using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;

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
        // GET: /Restaurant/MenuNotFound
        public ActionResult MenuNotFound()
        {
            return View();
        }

        //
        // GET: /Restaurant/

        public ActionResult MenuCommande(int id)
        {
            Restaurant resto = db.Restaurants.Find(id);
            Menu menu;
            if (db.Menus.Where(c => c.RestaurantId == resto.RestaurantId).FirstOrDefault() != null)
            {
                menu = db.Menus.Where(c => c.RestaurantId == resto.RestaurantId).First();
            } else {
                return RedirectToAction("MenuNotFound");
            }
            List<MenuItem> menuItems = db.MenuItems.Where(c => c.MenuId == menu.MenuId).ToList();
            List<CreateCommandeItemModel> createCommandeItems = new List<CreateCommandeItemModel>();
            foreach (MenuItem mi in menuItems)
            { 
                CreateCommandeItemModel model = new CreateCommandeItemModel(0,mi.MenuItemId,mi.Nom,mi.Prix,id);
                createCommandeItems.Add(model);
            }

            return View(createCommandeItems);
        }

        [HttpPost]
        public ActionResult MenuCommande(IList<Delivr.Models.CreateCommandeItemModel> createCommandeItems)
        {
            

            List<CommandeItem> items = new List<CommandeItem>();
            foreach (CreateCommandeItemModel c in createCommandeItems)
            {
                if (c.Quantite != 0)
                { 
                    CommandeItem cItem = new CommandeItem()
                    {
                        MenuItemId = c.MenuItemId,
                        Quantite = c.Quantite,
                        SousTotal = c.Quantite*c.Prix
                        
                    };

                    items.Add(cItem);
                }
            }
            TempData["list"] = items;
            return RedirectToAction("CreateCommande");
        }



        //
        // GET: /Restaurant/CreateCommande

        public ActionResult CreateCommande()
        {
            
            var items = TempData["list"] as List<CommandeItem>;
            if (items == null)
                return RedirectToAction("Liste");
            CreateCommandeModel createCommande = new CreateCommandeModel();
            MenuItem firstMenuItem = db.MenuItems.Find(items.First().MenuItemId);
            Menu menu = db.Menus.Find(firstMenuItem.MenuId);
            Restaurant resto = db.Restaurants.Find(menu.RestaurantId);
            UserProfile user = db.UserProfiles.Find(WebSecurity.CurrentUserId);
            List<MenuItem> menuItems = db.MenuItems.Where(c => c.MenuId == menu.MenuId).ToList();
            List<CommandeItem> commandeItems = new List<CommandeItem>();
            foreach (CommandeItem ci in items)
            {
                firstMenuItem = db.MenuItems.Find(ci.MenuItemId);
                ci.MenuItem = firstMenuItem;
                createCommande.CommandeItems.Add(ci);
            }

            foreach (Adresse ad in user.Adresses)
            {
                if (user.AdresseDefaultId != ad.AdresseId)
                    createCommande.Adresses.Add(ad);
                else
                    createCommande.AdresseDefault = ad;
            }

            createCommande.Date = DateTime.Now;
            return View(createCommande);
        }

        //
        // GET: /Restaurant/CreateCommande

        [HttpPost]
        public ActionResult CreateCommande(CreateCommandeModel createCommande)
        {
            Commande commande;

            UserProfile user = db.UserProfiles.Find(WebSecurity.CurrentUserId);
            MenuItem firstMenuItem = db.MenuItems.Find(createCommande.CommandeItems.First().MenuItemId);
            Menu menu = db.Menus.Find(firstMenuItem.MenuId);
            Restaurant resto = db.Restaurants.Find(menu.RestaurantId);

            int AdresseId = createCommande.AdresseId;
            if ((createCommande.NewAdresse.Rue == null || createCommande.NewAdresse.Rue == ""))
            {
                AdresseId = createCommande.AdresseId;
                user.AdresseDefaultId = AdresseId;
            }
            else
            {
                Adresse add = new Adresse();
                add.CodeCivique = createCommande.NewAdresse.CodeCivique;
                add.CodePostale = createCommande.NewAdresse.CodePostale;
                add.Rue = createCommande.NewAdresse.Rue;
                add.User = user;
                db.Adresses.Add(add);
                db.SaveChanges();
                user.Adresses.Add(add);
                AdresseId = add.AdresseId;
                user.AdresseDefaultId = add.AdresseId;
            }


            commande = new Commande()
            {
                AdresseId = AdresseId,
                RestaurantId = resto.RestaurantId,
                UserId = user.UserId,
                Date = createCommande.Date,
                Statut = Commande.StatutCommande.EnTraitement, //sera mis a jour sur réception du paiement
                PaypalTransacId = null //sera inséré par la suite
            };

            commande.Adresse = db.Adresses.Find(AdresseId);
            foreach (CommandeItem c in createCommande.CommandeItems)
            {
                db.CommandeItems.Add(c);
            }

            commande.CommandeItems = createCommande.CommandeItems;
            commande.Adresse = db.Adresses.Find(AdresseId);
            db.Commandes.Add(commande);
            db.SaveChanges();

            string items = Environment.NewLine + "Items: ";
            string totalString = "Total: ";
            int total = 0;
            foreach (CommandeItem c in commande.CommandeItems)
            {
                MenuItem mi = db.MenuItems.Find(c.MenuItemId);
                items += mi.Nom + " x" + c.Quantite.ToString() + "" + Environment.NewLine;
                total = total + c.SousTotal;
            }
            totalString += total.ToString();
            SendMail("Confirmation de commande Delivr (Processing)", 
                        "Numéro de confirmation: " + commande.CommandeId + Environment.NewLine + "Adresse: " + commande.Adresse.CodeCivique + " " + 
                        commande.Adresse.Rue + " " + commande.Adresse.CodePostale + Environment.NewLine + "Date et heure:" + 
                        commande.Date.ToString("MM/dd/yyyy HH:mm:ss.fff") + " " + items + totalString, user.UserName);

            return Content(commande.CommandeId.ToString());

            //-----------------------------------------------------------------------------------------
        }

        //
        // GET: /Restaurant/ConfirmationCommande

        public ActionResult ConfirmationCommande()
        {
            // Receive IPN request from PayPal and parse all the variables returned
            var formVals = new Dictionary<string, string>();
            formVals.Add("cmd", "_notify-synch");
            formVals.Add("at", "ZjUQz8PtolJAdGtMs2OnKyzlPXJa0-oFywH2fbntTO2SXpVFKFrUl2sRuJC"); //identity-token
            formVals.Add("tx", Request["tx"].ToUpper());

            //true for sandbox
            string response = GetPayPalResponse(formVals, true);
            string[] args = response.Split(new string[] { "\n" }, StringSplitOptions.None);
            string orderId = args.First(x => x.Contains("custom")).Split('=')[1];
               
            Commande order = db.Commandes.Find(Int32.Parse(orderId));

            if (response.Contains("SUCCESS"))
            {
                string transactionID = GetPDTValue(response, "txn_id"); // txn_id //d
                string sAmountPaid = GetPDTValue(response, "mc_gross"); // d
                string deviceID = GetPDTValue(response, "custom"); // d
                string payerEmail = GetPDTValue(response, "payer_email"); // d
                string Item = GetPDTValue(response, "item_name");

                Decimal amountPaid = 0;
                Decimal.TryParse(sAmountPaid, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out amountPaid);

                if (amountPaid == (decimal)order.Prix/100)  // you might want to have a bigger than or equal to sign here!
                {
                    ViewBag.Result = "SUCCESS";
                    order.PaypalTransacId = transactionID;
                    order.Statut = Commande.StatutCommande.EnAttente;
                    TryUpdateModel(order);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Result = "WRONG_AMOUNT";
                }

            }
            else
            {
                //Payment failure
                ViewBag.Result = "PAYMENT_FAILURE";
            }

            return View(order);
        }

        //------------------------PAYPAL UTILS----------------------------------------------
        string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {

            string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr"
                : "https://www.paypal.com/cgi-bin/webscr";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (string key in formVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, formVals[key]);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {

                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            return response;
        }
        string GetPDTValue(string pdt, string key)
        {

            string[] keys = pdt.Split('\n');
            string thisVal = "";
            string thisKey = "";
            foreach (string s in keys)
            {
                string[] bits = s.Split('=');
                if (bits.Length > 1)
                {
                    thisVal = bits[1];
                    thisKey = bits[0];
                    if (thisKey.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                        break;
                }
            }
            return thisVal;

        }
        //--------------------------------------------------------------------------------------------------------

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
         // GET: /Restaurant/
         public ActionResult Liste()
         {
             List<Restaurant> Restaurants = db.Restaurants.ToList();
             return View(Restaurants);
         }

        //
        // GET: /Restaurant/Details/5

         public ActionResult Details(int? id)
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
        // GET: /Restaurant/Message


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
        protected void SendMail(string sujet, string message,string destinataire)
        {
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

                msg.Subject = sujet;
                msg.Body = message;
                msg.From = new MailAddress("delivrmail@gmail.com");
                msg.To.Add(destinataire);//mdp user deliveruser1123
                msg.IsBodyHtml = true;
                client.Host = "smtp.gmail.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("delivrmail@gmail.com", "delivrmail123");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
            

        }
    }
}