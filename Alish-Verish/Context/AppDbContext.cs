using Alish_Verish.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alish_Verish.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; } 
        public DbSet<Products> Products { get; set; }
        public DbSet<Baskets> Baskets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Alish-Verish;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
