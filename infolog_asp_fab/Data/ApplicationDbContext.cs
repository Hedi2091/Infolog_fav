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

        public DbSet<Client> Client { get; set; }
        public DbSet<Produit> Produit { get; set; }

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
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Produit>().ToTable("Produit");
            modelBuilder.Entity<Client>().HasKey(c => c.IdClients);
            modelBuilder.Entity<Produit>().HasKey(p => p.IdProduits);
        }
    }
}
