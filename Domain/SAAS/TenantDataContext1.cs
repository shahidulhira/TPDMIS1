using Domain.SAAS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using RapidFireLib.Models;
using System;
using System.Data;
using System.Text.RegularExpressions;

namespace Domain.SAAS
{
    public class TenantDataCtx1 : DbContext
    {
        public TenantDataCtx1(string schema)
        {
            Schema = schema;
            IsDb = false;
        }

        public string Schema { get; }
        public bool IsDb { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            if (IsDb)
            {
                optionsBuilder.UseSqlServer(ChangeDatabase()).
                ReplaceService<IModelCacheKeyFactory, MyModelCacheKeyFactory>();
            }
            else
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("SAASClientConnection")).
                ReplaceService<IModelCacheKeyFactory, MyModelCacheKeyFactory>();
            }


        }
        public void ChangeDatabase(string database)
        {
            var connection = this.Database.GetDbConnection();
            var dbName = database;
            if (connection.State.HasFlag(ConnectionState.Open))
            {
                connection.ChangeDatabase(database);
            }
            else
            {
                var connectionString = Regex.Replace(connection.ConnectionString.Replace(" ", ""), @"(?<=[Dd]atabase=)\w+(?=;)", database, RegexOptions.Singleline);
                connection.ConnectionString = connectionString;
            }
            //// Following code only working for mysql.
            //var items = _context.Model.GetEntityTypes();
            //foreach (var item in items)
            //{
            //    if (item.Relational() is RelationalEntityTypeAnnotations extensions)
            //    {
            //        extensions.Schema = database;
            //    }
            //}
        }
        private string ChangeDatabase()
        {
            return $"Data Source=10.12.1.2;Initial Catalog={Schema};user id=sa; password=bdco";
        }

        public DbSet<Job> Job { get { return this.Set<Job>(); } }
        public DbSet<AuditTrail> AuditTrail { get { return this.Set<AuditTrail>(); } }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if(!IsDb)
                modelBuilder.HasDefaultSchema(Schema);
        }


    }
    class MyModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context)
            => new MyModelCacheKey(context);
    }

    class MyModelCacheKey : ModelCacheKey
    {
        string _schema;

        public MyModelCacheKey(DbContext context)
            : base(context)
        {
            _schema = (context as TenantDataCtx1)?.Schema;
        }

        protected override bool Equals(ModelCacheKey other)
            => base.Equals(other)
                && (other as MyModelCacheKey)?._schema == _schema;

        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode() * 397;
            if (_schema != null)
            {
                hashCode ^= _schema.GetHashCode();
            }

            return hashCode;
        }
    }
}