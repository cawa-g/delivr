using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Delivr.Models
{
    [Table("Restaurateur")]
    public class Restaurateur : UserProfile
    { 
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}