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

        public virtual Commande User { get; set; }
        public virtual MenuItem Restaurant { get; set; }
    }
}