using EntityFrameworkCoreHints.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreHints.Data.Configuration
{
    public class CarEntityConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .HasOne<Owner>()
                .WithMany()
                .HasForeignKey(a => a.OwnerId);

            builder
               .Property(b => b.RegistrationNumber)
               .HasMaxLength(7);
        }
    }
}
