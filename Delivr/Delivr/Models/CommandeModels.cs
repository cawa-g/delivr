using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Delivr.Models
{
    [Table("Commande")]
    public class Commande
    {
        public static class StatutCommande
        {
            public const string EnTraitement = "EnTraitement";
            public const string EnAttente = "EnAttente";
            public const string EnPreparation = "EnPreparation";
            public const string Prete = "Prete";
            public const string EnLivraison = "EnLivraison";
            public const string Livree = "Livree";
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CommandeId { get; set; }
        public string PaypalTransacId { get; set; }
        public DateTime Date { get; set; }
        public string Statut { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public int AdresseId { get; set; }
        public double Prix 
        { 
            get
            {
                double total = 0;
                foreach (var item in CommandeItems)
                {
                    total += item.SousTotal;
                }
                return total;
            }
        }

        public virtual UserProfile User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual Adresse Adresse { get; set; }
        public virtual ICollection<CommandeItem> CommandeItems { get; set; }

        public Commande()
        {
            this.CommandeItems = new List<CommandeItem>();
        }
    }

    public class CreateCommandeModel
    {
        public DateTime Date { get; set; }
        public Adresse NewAdresse { get; set; }
        public Adresse AdresseDefault { get; set; }
        public virtual IList<CommandeItem> CommandeItems { get; set; }
        public virtual IList<Adresse> Adresses { get; set; }
        public int AdresseId { get; set; }

        public CreateCommandeModel()
        {
            this.CommandeItems = new List<CommandeItem>();
            this.Adresses = new List<Adresse>();
        }

    }

    public class SetStatutCommandeModel
    {
        public int Id { get; set; }
        public string Statut { get; set; }
    }
}