using Microsoft.EntityFrameworkCore;
using OdeToFoodCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFoodCore.Data
{
    public class OdeToFoodDbContext : DbContext
    {
        public OdeToFoodDbContext(DbContextOptions options) 
            : base(options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
