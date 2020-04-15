using Microsoft.EntityFrameworkCore;
using RapidFireLib.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace RapidFireLib.Lib.DB
{
    public class UnitOfWork : IDisposable
    {
        Configuration Config = null;
        public UnitOfWork(IConfig config)
        {
            Config = new ConfigBuilder().Get(config);
        }
        

        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public void RegisterContext(DbContext context)
        {
            contexts = contexts ?? new Dictionary<Type, object>();
            if (!contexts.Keys.Contains(context.GetType())) contexts.Add(context.GetType(), context);
        }
        private Dictionary<Type, object> contexts = new Dictionary<Type, object>();

        public DbContext GetContext(DbContext dbContext)
        {
            if (contexts.Keys.Contains(dbContext.GetType())) return (DbContext)contexts[dbContext.GetType()];
            return null;
        }
        public DbContext GetContext(Type dbContextType)
        {
            if (contexts.Keys.Contains(dbContextType)) return (DbContext)contexts[dbContextType];
            return null;
        }
        public IRepository<TEntity> Repositories<TEntity>(DbContext dbContext = null) where TEntity : class//, new()
        {
            if (dbContext == null) dbContext = Config.DB.DefaultDbContext;
            if (!contexts.Keys.Contains(dbContext.GetType())) contexts.Add(dbContext.GetType(), dbContext);
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
                return (IRepository<TEntity>)repositories[typeof(TEntity)];
            IRepository<TEntity> tRepositoryObject = CreateRepository<TEntity>(dbContext);
            repositories.Add(typeof(TEntity), tRepositoryObject);
            return tRepositoryObject;
        }

        private IRepository<TEntity> CreateRepository<TEntity>(DbContext context) where TEntity : class
        {
            IRepository<TEntity> repository = null;
            string connectionType = context.Database.ProviderName;
            if (connectionType.Equals("SqlConnection")) repository = new RepositoryMSSQL<TEntity>(context);
            else if (connectionType.Equals("SqlConnection1")) repository = null;//new RepositoryOracle<TEntity>(context);
            return repository;
        }

        public void SaveChange()
        {
            try
            {
                if (contexts.Count == 0) throw new Exception("DB Context Not Set!");
                if (contexts.Count == 1) ((DbContext)contexts.FirstOrDefault().Value).SaveChanges();
                else
                {
                    contexts.ToList().ForEach(x => { ((DbContext)x.Value).SaveChanges(); });
                    //using (TransactionScope transaction = new TransactionScope())
                    //{
                    //    try
                    //    {
                    //        contexts.ToList().ForEach(x => { ((DbContext)x.Value).SaveChanges(); });
                    //        transaction.Complete();
                    //    }
                    //    catch (TransactionException ex)
                    //    {
                    //        throw ex;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            //contexts = null;
            GC.SuppressFinalize(this);
        }
    }
}
