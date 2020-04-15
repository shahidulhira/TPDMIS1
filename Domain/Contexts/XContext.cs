using Domain.Aggregates;
using Domain.Aggregates.Common;
using Domain.Aggregates.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RapidFireLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contexts
{
    public class XContext:DbContext
    {
        public DbSet<AspNetUser> AspNetUser { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }

        public DbSet<GroupMemberInfoE> GroupMemberInfoE { get; set; }
        public DbSet<GroupInfoE> GroupInfoE { get; set; }
        public DbSet<QuickSearch> QuickSearch { get; set; }

        //public DbSet<IdentityUserClaim<string>> Claims { get; set; }
        //public DbSet<IdentityUserLogin<string>> Logins { get; set; }
        //public DbSet<IdentityUserToken<string>> Tokens { get; set; }
        //public DbSet<TwoFactorRecoveryCode> RecoveryCodes { get; set; }



        //public DbSet<GroupInfoE> GroupInfoE { get; set; }
        public DbSet<AuditTrail> AuditTrail { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string conStr = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(conStr);
            
        }
    }
}
