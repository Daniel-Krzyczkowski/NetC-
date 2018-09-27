using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using EntityFrameworkCoreHints.Common;

namespace EntityFrameworkCoreHints.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = CoreConfigurationProvider.BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"), b => b.MigrationsAssembly("EntityFrameworkCoreHints.Data"));
            return new ApplicationDbContext(builder.Options);
        }
    }
}
