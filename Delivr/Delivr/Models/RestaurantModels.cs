using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Delivr.Models
{
    [Table("Restaurant")]
    public class Restaurant
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RestaurantId { get; set; }
        public string nom { get; set; }
        public int? UserId { get; set; }
        public string Telephone { get; set; }
        public string Rue { get; set; }
        public int? CodeCivique { get; set; }
        public string CodePostale { get; set; }

        public virtual UserProfile Restaurateur { get; set; }
        public virtual ICollection<Commande> Commandes { get; set; }
    }
}