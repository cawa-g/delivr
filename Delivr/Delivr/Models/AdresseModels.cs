using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Delivr.Models
{

        [Table("Adresse")]
        public class Adresse
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int AdresseId { get; set; }
            public string Rue { get; set; }
            public int? CodeCivique { get; set; }
            public string CodePostale { get; set; }
            public int? UserId { get; set; }

            public virtual UserProfile User { get; set; }


        }
    
}