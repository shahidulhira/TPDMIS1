using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace BDCO.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext context;
        private DbSet<T> table = null;
        public GenericRepository()
        {

        }

        public GenericRepository(DbContext ctx)
        {
            this.context = ctx;
            table = context.Set<T>();
        }
        public void Delete(int id)
        {
            try
            {
                T existing = table.Find(id);
                table.Remove(existing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long id)
        {
            try
            {
                T existing = table.Find(id);
                context.Entry(existing).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteByEntity(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void DeleteAll(List<T> entities)
        {
            try
            {
                context.Set<T>().RemoveRange(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }

        public void ExeccuteRawQyery(string sql, SqlParameter[] _sqlParameter = null)
        {
            try
            {
                if (_sqlParameter != null)
                    context.Database.ExecuteSqlCommand(sql, _sqlParameter);
                else
                    context.Database.ExecuteSqlCommand(sql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IEnumerable<T> query = context.Set<T>().Where(predicate);
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                IQueryable<T> queryResult = table;
                return queryResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetById(object id)
        {
            try
            {
                return table.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        public DbRawSqlQuery<T> GetRecordSet(string sqlSyntex, SqlParameter[] param = null)
        {
            try
            {
                DbRawSqlQuery<T> rows;
                if (param != null)
                    rows = context.Database.SqlQuery<T>(sqlSyntex, param);
                else
                    rows = context.Database.SqlQuery<T>(sqlSyntex);
                return rows;
            }
            catch (Exception ex) { throw ex; }
        }

        public DataSourceResult GetRecordSet(string sqlSyntex, [DataSourceRequest] DataSourceRequest request, SqlParameter[] param = null)
        {
            try
            {
                DbRawSqlQuery<T> rows = null;
                DataSourceResult results = null;
                if (param != null)
                {
                    rows = context.Database.SqlQuery<T>(sqlSyntex, param);
                    results = rows.ToDataSourceResult(request);
                }
                else
                {
                    rows = context.Database.SqlQuery<T>(sqlSyntex);
                    results = rows.ToDataSourceResult(request);
                }
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetRecordSetList(string sqlSyntex, [DataSourceRequest] DataSourceRequest request, SqlParameter[] param = null)
        {
            try
            {
                DbRawSqlQuery<T> rows = null;
                DataSourceResult results = null;
                if (param != null)
                {
                    rows = context.Database.SqlQuery<T>(sqlSyntex, param);
                    results = rows.ToDataSourceResult(request);
                }
                else
                {
                    rows = context.Database.SqlQuery<T>(sqlSyntex);
                    results = rows.ToDataSourceResult(request);
                }
                var _sqlData = rows.ToList();
                return (List<T>)_sqlData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSingleValue(string sSQL)
        {
            string strValue = context.Database.SqlQuery<string>(sSQL).FirstOrDefault();
            return strValue;
        }

        public void Insert(T entity)
        {
            try
            {
                table.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertAll(List<T> entities)
        {
            try
            {
                foreach (var item in entities)
                {
                    table.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool IsExist(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Any(predicate);
        }

        public void SaveChange()
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

        public object SaveChangeAndReturnObject()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetContext(DbContext ctx)
        {
            this.context = ctx;
            table = context.Set<T>();
        }

        public void Update(T entity,bool hasToDeatch = true)
        {
            try
            {
                if (hasToDeatch)
                {
                    var entityStateObject = context.ChangeTracker.Entries<T>().FirstOrDefault()?.Entity;
                    if (entityStateObject != null)
                        DetachEntity(entityStateObject);
                }
               
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    table.Attach(entity);
                }
                context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAll(List<T> entities)
        {
            try
            {
                var entityStateObject = context.ChangeTracker.Entries<T>().FirstOrDefault()?.Entity;
                if (entityStateObject != null)
                    DetachEntity(entityStateObject);

                foreach (var item in entities)
                {
                    table.Attach(item);
                    context.Entry(item).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DetachEntity(T detachableObject)
        {
            var objectState = context.Entry(detachableObject).State;
            if (objectState != EntityState.Detached)
                context.Entry(detachableObject).State = EntityState.Detached;
        }
    }
}
