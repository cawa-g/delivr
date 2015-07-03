using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Delivr.Models
{
    [Table("CommandeItem")]
    public class CommandeItem
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CommandeItemId { get; set; }
        public int Quantite { get; set; }
        public int CommandeId { get; set; }
        public int MenuItemId { get; set; }

        public virtual Commande Commande { get; set; }
        public virtual MenuItem MenuItem { get; set; }
    }

    public class CreateCommandeItemModel
    {
        public int Quantite { get; set; }
        public int MenuItemId { get; set; }
        public string Nom { get; set; }
        [DisplayFormat(DataFormatString = "${0:C}", ApplyFormatInEditMode = true)]
        public int Prix {  get; set; }
        public int RestaurantId { get; set; }

        public virtual MenuItem MenuItem { get; set; }

        public CreateCommandeItemModel(int quantite, int menuItemId, string nom, int prix, int RestaurantId)
        {
            Quantite = quantite;
            MenuItemId = menuItemId;
            this.Nom = nom;
            this.Prix = prix;
            this.RestaurantId = RestaurantId;
        }

        public CreateCommandeItemModel()
        { }
    }
}