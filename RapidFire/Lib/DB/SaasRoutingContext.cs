using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models;
using System;

namespace RapidFireLib.Lib.DB
{
    public class SaasRoutingContext : DbContext
    {
        public SaasRoutingContext() : base() {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string conStr = configuration.GetConnectionString("SaasRoutingConnection");

            optionsBuilder.UseSqlServer(conStr);
        }
        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<AuditTrail> AuditTrail { get; set; }
    }
}
