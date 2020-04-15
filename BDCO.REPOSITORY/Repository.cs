using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.Entity.Infrastructure;

namespace BDCO.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private bool isSucessOperation = true;
        private DbContext context;
        private DbSet<T> table = null;
        private string UserName = "";

        private bool canRead = false;
        private bool canAdd = false;
        private bool canUpdate = false;
        private bool canDelete = false;
        private bool[] access = null;
        private string tableName = "";

        public bool IsSuccessOperation
        {
            get { return isSucessOperation; }
            set { isSucessOperation = value; }
        }
        public Repository()
        {

        }

        public Repository(DbContext ctx)
        {
            this.context = ctx;
            table = context.Set<T>();
            if (HttpContext.Current != null)
                UserName = HttpContext.Current.User.Identity.Name.ToString();

            Type objectType = typeof(T);
            tableName = objectType.Name;
            access = AccessPermission(tableName);
            canAdd = access[0];
            canRead = access[1];
            canUpdate = access[2];
            canDelete = access[3];
        }
        public void SetContext(DbContext ctx)
        {


            this.context = ctx;
            table = context.Set<T>();
            if (HttpContext.Current != null)
                UserName = HttpContext.Current.User.Identity.Name.ToString();

            Type objectType = typeof(T);
            tableName = objectType.Name;
            access = AccessPermission(tableName);
            canAdd = access[0];
            canRead = access[1];
            canUpdate = access[2];
            canDelete = access[3];
        }
        public bool IsExist(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return GetAll().Any(predicate);
        }
        public IQueryable<T> GetAll()
        {
            object result = null;
            IQueryable<T> queryResult = null;
            if (canRead)
            {
                queryResult = table;

            }
            else
            {
                isSucessOperation = false;
                result = "You do not have enough permission.";
            }

            return queryResult;
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
        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = context.Set<T>().Where(predicate);
            return query;
        }

        public string Insert(T entity)
        {
            string result = "";
            try
            {
                if (canAdd || tableName == "EventStore")
                    table.Add(entity);
                else {
                    isSucessOperation = false;
                    result = "You do not have enough permission.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string InsertAll(List<T> entities)
        {
            string result = "";
            try
            {
                if (canAdd)
                {
                    foreach (var item in entities)
                    {
                        table.Add(item);
                    }

                }

                else
                {
                    isSucessOperation = false;
                    result = "You do not have enough permission.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public void DetachEntity(T detachableObject)
        {
            var objectState = context.Entry(detachableObject).State;
            if (objectState != EntityState.Detached)
                context.Entry(detachableObject).State = EntityState.Detached;
        }
        public string Update(T entity)
        {
            string result = "";
            try
            {
                if (canUpdate)
                {
                    var entityStateObject = context.ChangeTracker.Entries<T>().FirstOrDefault()?.Entity;

                    if (entityStateObject != null)
                        DetachEntity(entityStateObject);
                    if (context.Entry(entity).State == EntityState.Detached)
                    {
                        table.Attach(entity);
                    }
                    context.Entry(entity).State = EntityState.Modified;
                }

                else
                {
                    isSucessOperation = false;
                    result = "You do not have enough permission.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;



        }


        public string UpdateAll(List<T> entities)
        {
            string result = "";
            try
            {
                if (canUpdate)
                    foreach (var item in entities)
                    {
                        table.Attach(item);
                        context.Entry(item).State = EntityState.Modified;
                    }

                else
                {
                    isSucessOperation = false;
                    result = "You do not have enough permission.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string Delete(T entity)
        {
            string result = "";
            try
            {
                if (canDelete)
                    context.Entry(entity).State = EntityState.Deleted;
                else
                {
                    isSucessOperation = false;
                    result = "You do not have enough permission.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string DeleteAll(List<T> entities)
        {
            string result = "";
            try
            {
                if (canDelete)
                    context.Set<T>().RemoveRange(entities);
                else
                {
                    isSucessOperation = false;
                    result = "You do not have enough permission.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string Delete(int id)
        {
            string result = "";
            try
            {
                if (canDelete)
                {
                    T existing = table.Find(id);
                    table.Remove(existing);
                }
                else
                {
                    isSucessOperation = false;
                    result = "You do not have enough permission.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        public string Delete(long id)
        {
            string result = "";
            try
            {
                if (canDelete)
                {

                    T existing = table.Find(id);
                    context.Entry(existing).State = EntityState.Deleted;
                }
                else
                {
                    isSucessOperation = false;
                    result = "You do not have enough permission.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
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
                result = ex.Message;
            }
            return result;
        }
        public object SaveChangeAndReturnObject()
        {
            object result;
            try
            {
                result = context.SaveChanges();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }


        public bool[] AccessPermission(string tableName)
        {
            string groupId = "-1";
            SqlConnection sqlCon = (SqlConnection)context.Database.Connection;
            bool[] granted = new bool[4];
            DataTable dt = new DataTable();

            string sqlQuery = "";

            sqlQuery = SqlSyntext.CheckTables();
            sqlQuery += " SELECT ";
            sqlQuery += "CASE WHEN SUM(CAST(ExecuteInsert as INT))>0 THEN 'True' ELSE 'False' END ExecuteInsert, ";
            sqlQuery += "CASE WHEN SUM(CAST(ExecuteRead as int))>0 THEN 'True' ELSE 'False' END ExecuteRead, ";
            sqlQuery += "CASE WHEN SUM(CAST(ExecuteUpdate as int))>0 THEN 'True' ELSE 'False' END ExecuteUpdate,  ";
            sqlQuery += "CASE WHEN SUM(CAST(ExecuteDelete as int))>0 THEN 'True' ELSE 'False' END ExecuteDelete ";
            sqlQuery += "FROM AppPermission ";
            sqlQuery += "WHERE ";
            sqlQuery += "AppResourceId = (SELECT TOP 1 Id FROM AppResource WHERE Name='" + tableName + "') ";
            sqlQuery += "AND ( UserId =ISNULL((select UserId from AspNetUsers where UserName ='" + UserName + "'),0)  OR GroupId = '" + groupId + "') ";

            SqlDataAdapter dAdapter = new SqlDataAdapter(sqlQuery, sqlCon);
            dAdapter.Fill(dt);
            object o = context.Database.ExecuteSqlCommand(sqlQuery);
            granted[0] = bool.Parse(dt.Rows[0]["ExecuteInsert"].ToString());
            granted[1] = bool.Parse(dt.Rows[0]["ExecuteRead"].ToString());
            granted[2] = bool.Parse(dt.Rows[0]["ExecuteUpdate"].ToString());
            granted[3] = bool.Parse(dt.Rows[0]["ExecuteDelete"].ToString());
            return granted;
        }

        public string ExeccuteRawQyery(string sql, SqlParameter[] _sqlParameter = null)
        {
            string result = "";
            try
            {
                if (canRead)
                {
                    if (_sqlParameter != null)
                        context.Database.ExecuteSqlCommand(sql, _sqlParameter);
                    else
                        context.Database.ExecuteSqlCommand(sql);
                }
                else
                    result = "You do not have enough permission.";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
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
        public DataSourceResult GetRecordSet(string sqlSyntex, [DataSourceRequest]DataSourceRequest request, SqlParameter[] param = null)
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
        public List<T> GetRecordSetList(string sqlSyntex, SqlParameter[] param = null)
        {
            DbRawSqlQuery<T> rows = null;
            if (param != null)
            {
                rows = context.Database.SqlQuery<T>(sqlSyntex, param);
            }
            else
            {
                rows = context.Database.SqlQuery<T>(sqlSyntex);
            }
            var _sqlData = rows.ToList();
            return (List<T>)_sqlData;
        }
        public List<T> GetRecordSetList(string sqlSyntex, [DataSourceRequest]DataSourceRequest request, SqlParameter[] param = null)
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
        public string GetSingleValue(string sSQL)
        {
            string strValue = context.Database.SqlQuery<string>(sSQL).FirstOrDefault();
            return strValue;
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

    }
}
