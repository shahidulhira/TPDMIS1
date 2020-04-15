using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RapidFireLib.Extensions;
using RapidFireLib.Lib.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;

namespace RapidFireLib.Lib.DB
{
    public class RepositoryMSSQL<T> : IRepository<T> where T : class //: IGenericRepository<T> where T : class
    {
        private DbContext context;
        private DbSet<T> table = null;
        public RepositoryMSSQL() { }

        public RepositoryMSSQL(DbContext ctx)
        {
            Debug.WriteLine(ctx.ToString());
            this.context = ctx;
            table = context.Set<T>();
        }
        public IPagedList<T> GetPagedList(string where = null,
                                                string orderBy = null,
                                                string include = null,
                                                int pageIndex = 0,
                                                int pageSize = 20)
        {
            IQueryable<T> query = null;
            var contextType = context.GetType();
            if (contextType.GetProperties().Any(x => x.PropertyType == typeof(DbSet<T>)))
                query =  context.Set<T>();
            else if (contextType.GetProperties().Any(x => x.PropertyType == typeof(DbQuery<T>)))
                query = new InternalDbQuery<T>(context);
            query = query.AsNoTracking();
            if (!string.IsNullOrEmpty(include))
                foreach (var item in include.Split(','))
                    query = query.Include(item.Trim());
            if (!string.IsNullOrEmpty(where))
                query = query.Where(where);
            if (!string.IsNullOrEmpty(orderBy))
                return query.OrderBy(orderBy).ToPagedList(pageIndex, pageSize);
            //else
            return query.ToPagedList(pageIndex, pageSize);
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
                context.Remove(entity);
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

        public void Delete(List<T> entities)
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
        public int Count(string where = "")
        {
            IQueryable<T> query = null;
            var contextType = context.GetType();
            if (contextType.GetProperties().Any(x => x.PropertyType == typeof(DbSet<T>)))
                query =  context.Set<T>();
            else if (contextType.GetProperties().Any(x => x.PropertyType == typeof(DbQuery<T>)))
                query = new InternalDbQuery<T>(context);
            if (!string.IsNullOrEmpty(where))
                return query.Where(where).Count();
            else return query.Count();
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IQueryable<T> query = context.Set<T>().Where(predicate);
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

        public List<T> GetAll(MulticastDelegate expression = null)
        {
            try
            {
                Func<T, bool> exp = (Func<T, bool>)expression;
                IQueryable<T> queryResult = table;
                var obj = queryResult.Where(exp).ToList();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> GetAll(string expression = null)
        {
            try
            {
                IQueryable<T> queryResult = table;
                var obj = queryResult.Where(expression);
                return obj;
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
                using (var conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
                using (var cmd = new SqlCommand(sqlSyntex, conn))
                {
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }
        private bool HasColumn(IDataReader dr, string columnName)
        {
            try
            {
                return dr.GetOrdinal(columnName) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
        private List<T> DataReaderMapToList(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (HasColumn(dr, prop.Name))
                        obj.SetPropertyValue(prop.Name, dr[prop.Name]);
                }
                list.Add(obj);
            }
            return list;
        }
        
        public List<T> GetRecordSet(string sqlSyntex, SqlParameter[] param = null)
        {

            try
            {
                var result = GetDataTable(sqlSyntex).DatatableToList<T>();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //try
            //{
            //var contextType = context.GetType();
            //if (contextType.GetProperties().Any(x => x.PropertyType == typeof(DbSet<T>)))
            //    return table.AsNoTracking().FromSql(sqlSyntex).ToList();
            //else if (contextType.GetProperties().Any(x => x.PropertyType == typeof(DbQuery<T>)))
            //    return context.Query<T>().AsNoTracking().FromSql(sqlSyntex).ToList();
            //else
            //    throw new Exception("Item is not registered to the context");
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //    //return table.AsNoTracking().FromSql(sqlSyntex).ToList();




            //return new List<T>();

            //var dbConnection = context.Database.GetDbConnection();
            //if (dbConnection.State == ConnectionState.Closed || dbConnection.State == ConnectionState.Connecting)
            //    dbConnection.Close();
            //dbConnection.Open();
            //using (var command = dbConnection.CreateCommand())
            //{
            //    command.CommandText = sqlSyntex;
            //    command.CommandType = CommandType.Text;
            //    if (param != null)
            //        command.Parameters.Add(param);
            //    //var dbConnection = context.Database.GetDbConnection();
            //    //context.Database.OpenConnection();
            //    using (var result = command.ExecuteReader())
            //    {
            //        return DataReaderMapToList(result);
            //    }
            //}
        }
        //public List<T> GetRecordSet(string sqlSyntex, SqlParameter[] param = null)
        //{
        //    try
        //    {
        //        context.Set<T>();
        //        List<T> rows;
        //        if (param != null)
        //            rows = table.FromSql(sqlSyntex,param).ToList();
        //        else
        //            rows = table.FromSql(sqlSyntex).ToList();
        //        return rows;
        //    }
        //    catch (Exception ex) { throw ex; }
        //}

        //public DataSourceResult GetRecordSet1(string sqlSyntex, DataSourceRequest request, SqlParameter[] param = null)
        //{
        //    try
        //    {
        //        DbRawSqlQuery<T> rows = null;
        //        DataSourceResult results = null;
        //        if (param != null)
        //        {
        //            rows = context.Database.SqlQuery<T>(sqlSyntex, param);
        //            results = rows.ToDataSourceResult(request);
        //        }
        //        else
        //        {
        //            rows = context.Database.SqlQuery<T>(sqlSyntex);
        //            results = rows.ToDataSourceResult(request);
        //        }
        //        return results;
        //    }
        //    catch (Exception ex) { throw ex; }
        //}

        //public List<T> GetRecordSetList(string sqlSyntex, DataSourceRequest request, SqlParameter[] param = null)
        //{
        //    try
        //    {
        //        DbRawSqlQuery<T> rows = null;
        //        DataSourceResult results = null;
        //        if (param != null)
        //        {
        //            rows = context.Database.SqlQuery<T>(sqlSyntex, param);
        //            results = rows.ToDataSourceResult(request);
        //        }
        //        else
        //        {
        //            rows = context.Database.SqlQuery<T>(sqlSyntex);
        //            results = rows.ToDataSourceResult(request);
        //        }
        //        var _sqlData = rows.ToList();
        //        return (List<T>)_sqlData;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string GetSingleValue(string sSQL)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                var strValue = "";
                command.CommandText = sSQL;
                command.CommandType = CommandType.Text;
                context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        strValue = result[0].ToString();
                        break;
                    }
                }
                return strValue;
            }
        }

        public void Insert(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Added;
                table.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(List<T> entities)
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

        public void Update(T entity, bool isDetatchable = true)
        {
            try
            {
                if (isDetatchable)
                {
                    var entityStateObject = context.ChangeTracker.Entries<T>().FirstOrDefault()?.Entity;
                    if (entityStateObject != null)
                        DetachEntity(entityStateObject);
                }
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    var entry = this.context.Entry(entity);
                    var property = entry.Entity.GetType().GetProperties()
                    .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
                    var key = property.GetValue(entry.Entity, null);
                    var currentEntry = this.table.Find(key);
                    if (currentEntry != null)
                    {
                        var attachedEntry = this.context.Entry(currentEntry);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        this.table.Attach(entity);
                        entry.State = EntityState.Modified;
                    }

                }
                //context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(List<T> entities, /*For Dummy Invocation*/ bool isDetatchable = true)
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
        //public void Update(List<object> entities, /*For Dummy Invocation*/ bool isDetatchable = true)
        //{
        //    try
        //    {
        //        var entityStateObject = context.ChangeTracker.Entries<T>().FirstOrDefault()?.Entity;
        //        if (entityStateObject != null)
        //            DetachEntity(entityStateObject);
        //        foreach (var item in entities)
        //        {
        //            table.Attach(item);
        //            context.Entry(item).State = EntityState.Modified;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void DetachEntity(T detachableObject)
        {
            var objectState = context.Entry(detachableObject).State;
            if (objectState != EntityState.Detached)
                context.Entry(detachableObject).State = EntityState.Detached;
        }

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }

        public void Test() //by me
        {
            throw new NotImplementedException();
        }
        //*******************************************************
        public void InsertAll(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateAll(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll(List<T> entities)
        {
            throw new NotImplementedException();
        }



        //public DataSourceResult GetRecordSet(string sqlSyntex, [DataSourceRequest] DataSourceRequest request, SqlParameter[] param = null)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
