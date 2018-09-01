using EntityFrameworkCoreJumpStart.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreJumpStart.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne<Owner>()
                .WithMany()
                .HasForeignKey(a => a.OwnerId);

            modelBuilder.Entity<Car>()
                .Property(b => b.RegistrationNumber)
                .HasMaxLength(7);
        }
    }
}
