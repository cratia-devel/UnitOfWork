using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Models
{
    public class SystemContext : DbContext
    {
        public DbSet<Waste> Wastes { get; set; }
        public DbSet<WasteType> TypesOfWaste { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Person> Persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=WasteDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Waste>()
                .HasOne(w => w.WasteType)
                .WithMany(wt => wt.Wastes)
                ;

            modelBuilder.Entity<Partner>()
                .HasOne(pt => pt.Person)
                .WithMany(p => p.Business)
                ;

            modelBuilder.Entity<Person>()
                .HasIndex(x => new { x.FirstName, x.LastName })
                .IsUnique();

            modelBuilder.Entity<WasteType>()
                .HasIndex(x => x.Description)
                .IsUnique()
                ;
        }
    }
}
