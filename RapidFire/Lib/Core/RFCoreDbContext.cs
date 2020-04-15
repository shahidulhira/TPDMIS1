using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using RapidFireLib.Lib.DB;
using RapidFireLib.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace RapidFireLib.Lib.Core
{
    public class RFCoreDbContext : DbContext
    {
        private readonly string connectionStirng;
        internal string CurrentTenant { get; set; }
        CookieManager cookieManager = new CookieManager();
        SAASType sAASType;

        public RFCoreDbContext(string connectionStirng, SAASType sAASType)
        {
            this.sAASType = sAASType;
            this.connectionStirng = connectionStirng;
            SetTenantStatus(sAASType);
        }

        // what about localhost or default site address .. saas index page??
        public void SetTenantStatus(SAASType sAASType)
        {
            if (!sAASType.Equals(SAASType.NoSaas))
            {
                RFDB db = new RFDB();
                string siteURL = new Http().HttpContext.Request.Host.Value.Split('.')[0];

                if (!string.IsNullOrEmpty(siteURL)) // why i need this at least i will get localhost
                {
                    Tenant tenant = new Tenant();
                    var cookieTenant = cookieManager.Get("_tenant");

                    if (!string.IsNullOrEmpty(cookieTenant)) tenant.SubdomainPrefix = cookieTenant;
                    else tenant = db.Get<Tenant>(exp => exp.SubdomainPrefix == siteURL, new SaasRoutingContext()).FirstOrDefault();

                    if (tenant != null)
                    {
                        cookieManager.Set("_tenant", tenant.SubdomainPrefix);
                        CurrentTenant = tenant.SubdomainPrefix;
                        if (tenant.IsPendingUpdate) db.ExecuteSQLFile(tenant.UpdateSQL, this, siteURL);
                    }
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string conStr = configuration.GetConnectionString(connectionStirng);
            if (sAASType.Equals(SAASType.Database))
                optionsBuilder.UseSqlServer(TenantString(conStr, CurrentTenant)).ReplaceService<IModelCacheKeyFactory, SassModelCacheKeyFactory>();
            else
                optionsBuilder.UseSqlServer(conStr).ReplaceService<IModelCacheKeyFactory, SassModelCacheKeyFactory>();
        }
        //UseSqlServer ?? what about oracle  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (sAASType.Equals(SAASType.Schema)) modelBuilder.HasDefaultSchema(CurrentTenant);
        }

        private string TenantString(string baseConnectionString, string currentTenant)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(baseConnectionString);
            return $"Data Source={builder.DataSource};Initial Catalog={currentTenant};user id={builder.UserID}; password={builder.Password}";
        }
    }

    class SassModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context) => new SassModelCacheKey(context);
    }

    class SassModelCacheKey : ModelCacheKey
    {
        string _schema;

        public SassModelCacheKey(DbContext context)
            : base(context)
        {
            _schema = (context as RFCoreDbContext)?.CurrentTenant;
        }

        protected override bool Equals(ModelCacheKey other)
            => base.Equals(other)
                && (other as SassModelCacheKey)?._schema == _schema;

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

    class RFDB
    {
        Dynamic Dynamic { get { return new Dynamic(); } }
        public List<T> Get<T>(Expression<Func<T, bool>> expression, DbContext dbContext = null) where T : class, new()
        {
            var repository = Dynamic.GetInstance(Dynamic.GetRepositoryBase(dbContext), typeof(T), dbContext);
            var record = (IQueryable<T>)Dynamic.InvokeMethod("FindBy", repository, typeof(T), new[] { typeof(Expression<Func<T, bool>>) }, expression);
            return record.ToList();
        }

        public void ExecuteSQLFile(string filePath, DbContext context, string databaseName)
        {

            var connectionString = context.Database.GetDbConnection().ConnectionString;
            connectionString = GetConnectionString(connectionString, databaseName);
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                FileInfo file = new FileInfo(filePath);
                string script = file.OpenText().ReadToEnd();
                script = script.Replace("GO", "");
                SqlCommand cmd = new SqlCommand(script, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }

        }

        private string GetConnectionString(string baseConnectionString, string currentTenant)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(baseConnectionString);
            return $"Data Source={builder.DataSource};Initial Catalog={currentTenant};user id={builder.UserID}; password={builder.Password}";
        }
    }
}
