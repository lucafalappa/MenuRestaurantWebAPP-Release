using MenuRestaurantWebAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuRestaurantWebAPP.Contexts
{
    public class MenuRestaurantDbContext : DbContext
    {
        public DbSet<Pietanza> _pietanze { get; set; }
        public DbSet<Portata> _portate { get; set; }
        public MenuRestaurantDbContext(DbContextOptions<MenuRestaurantDbContext> options) : base(options)
        {
            try
            {
                var databaseCreator =
                    Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) { databaseCreator.Create(); }
                    if (!databaseCreator.HasTables()) { databaseCreator.CreateTables(); }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurazione chiave primaria di Portata
            modelBuilder.Entity<Portata>()
                .HasKey(p => p.Id);

            // Configurazione chiave primaria di Pietanza
            modelBuilder.Entity<Pietanza>()
                .HasKey(p => p.Id);

            // Configurazione della relazione tra Pietanza e Portata
            // Definizione comportamento a cascata
            modelBuilder.Entity<Pietanza>()
                .HasOne<Portata>()
                .WithMany()
                .HasForeignKey(p => p.PortataId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
