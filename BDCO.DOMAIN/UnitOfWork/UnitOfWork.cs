using BDCO.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace BDCO.Domain
{
    public class UnitOfWork : IDisposable
    {
        public DatabaseContext context = new DatabaseContext();
        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public Repository<TEntity> Repositories<TEntity>() where TEntity : class//, new()
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
                return (Repository<TEntity>)repositories[typeof(TEntity)];
            Repository<TEntity> tRepositoryObject = new Repository<TEntity>(context);
            repositories.Add(typeof(TEntity), tRepositoryObject);
            return tRepositoryObject;
        }
        public GenericRepository<TEntity> GenericRepositories<TEntity>() where TEntity : class//, new()
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
                return (GenericRepository<TEntity>)repositories[typeof(TEntity)];
            GenericRepository<TEntity> tRepositoryObject = new GenericRepository<TEntity>(context);
            repositories.Add(typeof(TEntity), tRepositoryObject);
            return tRepositoryObject;
        }
        public void DeleteItems<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, new()
        {
            var items = GenericRepositories<TEntity>().FindBy(filter).ToList();
            foreach (TEntity item in items)
            {
                GenericRepositories<TEntity>().Delete(item);
            }
        }

        public List<TEntity> RawSqlQuery<TEntity>(string sqlStr) where TEntity : class => context.Database.SqlQuery<TEntity>(sqlStr).ToList();
        public void RawSqlQuery(string sqlStr) => context.Database.ExecuteSqlCommand(sqlStr);


        public DataTable GetDataTable(string sqlSyntex)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection sqlConn = (SqlConnection)context.Database.Connection;
                SqlDataAdapter da = new SqlDataAdapter(sqlSyntex, sqlConn);
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }

        public DataSet GetDataSet(string sqlQuery)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt = GetDataTable(sqlQuery);
            ds.Tables.Add(dt);
            return ds;
        }
        public string SaveChange()
        {
            string result = "";
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //Rollback();
                result = ex.Message;
            }
            return result;
        }
        public void SaveChangeJGrid()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Rollback()
        {
            context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
