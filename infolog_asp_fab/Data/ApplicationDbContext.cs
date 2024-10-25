using infolog_asp_fab.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace infolog_asp_fab.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Produit> Produits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
             .SelectMany(t => t.GetProperties())
             .Where(p => p.GetColumnType() == "nvarchar(max)"))
            {
                property.SetColumnType("TEXT");
            }

            // Configure les noms de tables correspondants si nécessaire
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Produit>().ToTable("Produits");
            modelBuilder.Entity<Client>().HasKey(c => c.IdClients);
            modelBuilder.Entity<Produit>().HasKey(p => p.IdProduits);
        }
    }
}
