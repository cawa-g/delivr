using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Delivr.Models
{
    public class DelivrDbContext: DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}