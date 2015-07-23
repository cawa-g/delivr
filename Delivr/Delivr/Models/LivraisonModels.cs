using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Delivr.Models
{
    [Table("Livraison")]
    public class Livraison
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int LivraisonId { get; set; }
        public DateTime Date { get; set; }
        public int? CommandeId { get; set; }

        public virtual Commande Commande { get; set; }
    }

    public class LivraisonModel
    {
        public DateTime DateLivraison { get; set; }
        public Adresse RestaurantAdresse { get; set; }
        public Adresse ClientAdresse { get; set; }
        public string NomRestaurant { get; set; }
        public string NomClient { get; set; }
        public int CommandeId { get; set; }

        public LivraisonModel()
        {
        }

        public LivraisonModel(DateTime DateLivraison, Adresse RestaurantAdresse, Adresse ClientAdresse, string NomRestaurant, string NomClient, int CommandeId)
        {
            this.DateLivraison = DateLivraison;
            this.RestaurantAdresse = RestaurantAdresse;
            this.ClientAdresse = ClientAdresse;
            this.NomRestaurant = NomRestaurant;
            this.NomClient = NomClient;
            this.CommandeId = CommandeId;
        }

    }

    public class ListeLivraisonModel
    {
        public List<LivraisonModel> Livraisons;
        public int IdCommande;

        public ListeLivraisonModel()
        {
        }

        public ListeLivraisonModel(List<LivraisonModel> Livraisons)
        {
            this.Livraisons = Livraisons;
        }


    }
}