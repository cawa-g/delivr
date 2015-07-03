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
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CommandeId { get; set; }
        public DateTime? Date { get; set; }
        public string Statut { get; set; }
        public string Rue { get; set; }
        public int? CodeCivique { get; set; }
        public string CodePostale { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }

        public virtual UserProfile User { get; set; }
        public virtual Restaurant Restaurant { get; set; }

    }
}