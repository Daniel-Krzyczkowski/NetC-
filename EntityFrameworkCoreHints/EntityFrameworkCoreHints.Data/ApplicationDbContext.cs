using EntityFrameworkCoreHints.Data.Configuration;
using EntityFrameworkCoreHints.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkCoreHints.Data
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
            //modelBuilder.Entity<Car>()
            //    .HasOne<Owner>()
            //    .WithMany()
            //    .HasForeignKey(a => a.OwnerId);

            //modelBuilder.Entity<Car>()
            //    .Property(b => b.RegistrationNumber)
            //    .HasMaxLength(7);

            modelBuilder.ApplyConfiguration(new CarEntityConfiguration());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var addedAuditedEntities = ChangeTracker.Entries<IEntity>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IEntity>()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
