using Domain.SAAS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Domain.SAAS
{
    public class SaasDefaultContext : DbContext
    {
        public SaasDefaultContext() : base()
        {
            //Database.SetInitializer<DataContext>(null);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AspNetUsers> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SAASDefaultConnection"));
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}