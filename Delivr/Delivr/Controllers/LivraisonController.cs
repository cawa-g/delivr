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
    public class LivraisonController : Controller
    {
        private DelivrContext db = new DelivrContext();

        //
        // GET: /Commande/ListeLivraison
        public ActionResult ListeLivraison()
        {
            List<Commande> commandes = db.Commandes.Where(c => c.Statut == Commande.StatutCommande.Prete).OrderBy(c => c.Date).ToList();
            List<LivraisonModel> listeLivraison = new List<LivraisonModel>();
            foreach (Commande c in commandes)
            {
                Restaurant r = db.Restaurants.Find(c.RestaurantId);
                UserProfile u = db.UserProfiles.Find(c.UserId);
                Adresse clientAdd = db.Adresses.Find(c.AdresseId);
                Adresse add = new Adresse();
                add.NumeroCivique = r.NumeroCivique;
                add.CodePostale = r.CodePostale;
                add.Rue = r.Rue;
                LivraisonModel lm = new LivraisonModel(c.Date, add, clientAdd, r.nom, u.Prenom + " " + u.Nom, c.CommandeId);
                listeLivraison.Add(lm);
            }
            ListeLivraisonModel listeLM = new ListeLivraisonModel(listeLivraison);
            return View(listeLM);
        }

        //
        // Post: /Commande/ListeLivraison
        [HttpPost]
        public ActionResult ListeLivraison(ListeLivraisonModel listeLM)
        {
            Commande commande = db.Commandes.Find(listeLM.IdCommande);
            if (commande.Statut != Commande.StatutCommande.Prete)
            {
                return RedirectToAction("Message", "Livraison", new { chaine = "Un autre livreur a déjà accepté la commande sélectionnée" });
            }
            commande.Statut = Commande.StatutCommande.Livree;

            Livraison livraison = new Livraison();
            livraison.CommandeId = commande.CommandeId;
            livraison.Date = DateTime.Now;
            db.Livraisons.Add(livraison);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ValidateInput(false)]
        public ActionResult Message(string chaine)
        {
            ViewBag.Chaine = chaine;
            return View();
        }

        public ActionResult Index()
        {
            var livraisons = db.Livraisons.Include(l => l.Commande);
            return View(livraisons.ToList());
        }

        //
        // GET: /Livraison/Details/5

        public ActionResult Details(int id = 0)
        {
            Livraison livraison = db.Livraisons.Find(id);
            if (livraison == null)
            {
                return HttpNotFound();
            }
            return View(livraison);
        }

        //
        // GET: /Livraison/Create

        public ActionResult Create()
        {
            ViewBag.CommandeId = new SelectList(db.Commandes, "CommandeId", "Statut");
            return View();
        }

        //
        // POST: /Livraison/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Livraison livraison)
        {
            if (ModelState.IsValid)
            {
                db.Livraisons.Add(livraison);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommandeId = new SelectList(db.Commandes, "CommandeId", "Statut", livraison.CommandeId);
            return View(livraison);
        }

        //
        // GET: /Livraison/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Livraison livraison = db.Livraisons.Find(id);
            if (livraison == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommandeId = new SelectList(db.Commandes, "CommandeId", "Statut", livraison.CommandeId);
            return View(livraison);
        }

        //
        // POST: /Livraison/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Livraison livraison)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livraison).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommandeId = new SelectList(db.Commandes, "CommandeId", "Statut", livraison.CommandeId);
            return View(livraison);
        }

        //
        // GET: /Livraison/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Livraison livraison = db.Livraisons.Find(id);
            if (livraison == null)
            {
                return HttpNotFound();
            }
            return View(livraison);
        }

        //
        // POST: /Livraison/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livraison livraison = db.Livraisons.Find(id);
            db.Livraisons.Remove(livraison);
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