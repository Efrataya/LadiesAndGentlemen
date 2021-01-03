using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LadiesAndGentlemen.Models;

namespace LadiesAndGentlemen.Data
{
    public class LadiesAndGentlemenContext : DbContext
    {
        public LadiesAndGentlemenContext(DbContextOptions<LadiesAndGentlemenContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Product>()
                .HasOne<Cart>(s => s.Cart)
                .WithMany(g => g.Products)
                .HasForeignKey(s => s.CartId);
        }


        public DbSet<LadiesAndGentlemen.Models.Address> Address { get; set; }

        public DbSet<LadiesAndGentlemen.Models.Cart> Cart { get; set; }

        public DbSet<LadiesAndGentlemen.Models.Category> Category { get; set; }

        public DbSet<LadiesAndGentlemen.Models.Client> Client { get; set; }

        public DbSet<LadiesAndGentlemen.Models.Order> Order { get; set; }

        public DbSet<LadiesAndGentlemen.Models.Product> Product { get; set; }

        public DbSet<LadiesAndGentlemen.Models.SubCategory> SubCategory { get; set; }
    }
}

