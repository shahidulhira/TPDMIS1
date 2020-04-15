using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RapidFireLib.Lib.DB;
using RapidFireLib.Lib.Extension;
using RapidFireLib.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RapidFireLib.Lib.Core
{
    public class DbResultAfter
    {
        object Model;
        public DbResultAfter(object Model)
        {
            this.Model = Model;
        }
        public Result Result { get; set; }
        public Result After(IPostDbOperationHandler handler, params IPostDbOperationHandler[] handlers)
        {
            Result result = null;
            result = handler.Handle(Model);
            for (int i = 0; i < handlers.Length; i++) result = handlers[i].Handle(Model);
            return result;
        }
    }

    public enum DBOperations
    {
        INSERT = 0,
        UPDATE = 1,
        DELETE = 2,
        SELECT = 3
    }

    public interface IPreDbOperationHandler
    {
        Object Handle(Object model);
    };

    public interface IPostDbOperationHandler
    {
        Result Handle(Object model);
    };

    public class Db
    {

        #region Initialization Properites
        Configuration Config = null;
        DbContext CurrentContext; //Swapable Context
        UnitOfWork unit;
        Dynamic Dynamic { get { return new Dynamic(); } }
        private Type DefaultContextType = null;
        private readonly DbContext DefaultContext = null; //Default can not change value after ctor end
        #endregion


        public Db(IConfig config)
        {
            Config = new ConfigBuilder().Get(config);

            unit = new UnitOfWork(config);

            DefaultContext = Config.DB.DefaultDbContext;
            CurrentContext = DefaultContext;
            DefaultContextType = CurrentContext.GetType();
            unit.RegisterContext(DefaultContext);
        }

        public Db(DbContext dbContext, IConfig config)
        {
            Config = new ConfigBuilder().Get(config);

            unit = new UnitOfWork(config);

            DefaultContext = dbContext;
            CurrentContext = dbContext;
            DefaultContextType = dbContext.GetType();
            unit.RegisterContext(dbContext);
        }

        public Result Commit()
        {
            Result result = null;
            try
            {
                unit.SaveChange();
                result = new Result(true, am.db.Success);
            }
            catch (Exception ex)
            {
                result = new Result(false, ex.Message, 0, null, ex);
            }

            return result;
        }

        private void SetContext(object model, DbContext dbContext = null)
        {
            CurrentContext = GetAnnotationDbContext(model) ?? dbContext ?? unit.GetContext(DefaultContextType) ?? DefaultContext;
            var registeredContext = unit.GetContext(CurrentContext);
            if (registeredContext == null)
                unit.RegisterContext(CurrentContext);
            else
                CurrentContext = registeredContext;
        }

        private DbContext GetContext(object model, DbContext _dbContext = null)
        {
            return GetAnnotationDbContext(model) ?? _dbContext ?? DefaultContext;
        }

        #region Without DbContext

        public DbResultAfter Save(object model, params IPreDbOperationHandler[] handlers)
        {
            SetContext(model);
            return ProcessInsertOrUpdate(model, null, handlers);
        }

        public DbResultAfter Save(object model, MulticastDelegate expression, params IPreDbOperationHandler[] handlers)
        {
            SetContext(model);
            return ProcessInsertOrUpdate(model, expression, handlers);
        }

        public DbResultAfter Save(object model, string sql, params IPreDbOperationHandler[] handlers)
        {
            SetContext(model);
            return ProcessInsertOrUpdate(model, sql, handlers);
        }

        #endregion


        #region With DbContext

        public DbResultAfter Save(object model, object dbContext, params IPreDbOperationHandler[] handlers)
        {
            SetContext(model, (DbContext)dbContext);
            return ProcessInsertOrUpdate(model, null, handlers);
        }

        public DbResultAfter Save(object model, object dbContext, MulticastDelegate expression,
            params IPreDbOperationHandler[] handlers)
        {
            SetContext(model, (DbContext)dbContext);
            return ProcessInsertOrUpdate(model, handlers);
        }

        public DbResultAfter Save(object model, object dbContext, string sql, params IPreDbOperationHandler[] handlers)
        {
            SetContext(model, (DbContext)dbContext);
            return ProcessInsertOrUpdate(model, sql, handlers);
        }

        #endregion

        private DbResultAfter ProcessInsertOrUpdate(object model, object expressionOrSql, params IPreDbOperationHandler[] handlers)
        {
            DbResultAfter dbResultAfter = null;
            if (model.GetType().IsGenericType)
            {
                foreach (var item in (IList)model)
                    dbResultAfter = InsertOrUpdate(item, expressionOrSql, handlers);
                return dbResultAfter;
            }
            else
                dbResultAfter = InsertOrUpdate(model, expressionOrSql, handlers);
            return dbResultAfter;
        }

        private DbResultAfter InsertOrUpdate(object model, object expressionOrSql, params IPreDbOperationHandler[] handlers)
        {
            Result result = null;
            bool isExists = false;
            var pkName = GetPrimaryKeyInfo(model).Name;
            object rec = new object();
            if (expressionOrSql == null)
            {
                rec = GetById(model, Dynamic.GetPropValue(model, pkName));
                isExists = rec != null;
            }
            else
            {
                if (expressionOrSql != null)
                {
                    rec = expressionOrSql.GetType() == typeof(String) ? GetAll(model, expressionOrSql.ToString(), true) : GetAll(model, (MulticastDelegate)expressionOrSql);
                    isExists = (int)rec.GetPropertyValue("Count") != 0;
                    if (isExists) rec = ((IList)rec)[0];
                }
            }
            if (!isExists) result = Archive(model, handlers, DBOperations.INSERT, new[] { model.GetType() }, model);
            else
            {
                var recordId = Dynamic.GetPropValue(model, pkName);
                var record = ((IQueryable<object>)GetAll(model, $"{pkName} = \"{recordId}\""));
                //This is not recursive need to do later
                foreach (var item in GetChildTableList(model, GetContext(model))) //dbContext ??
                {
                    record = record.Include(item);
                }

                model.SetPropertyValue(pkName, rec.GetPropertyValue(pkName));
                result = Archive(model, handlers, DBOperations.UPDATE, new[] { model.GetType(), typeof(bool) }, model, false);
                ManualUpdateMapping(model, expressionOrSql, handlers);
            }

            if (result.Success == false) throw result.Exception;
            return new DbResultAfter(model) { Result = result };
        }
        private DbResultAfter ManualUpdateMapping(object model, object expressionOrSql, params IPreDbOperationHandler[] handlers)
        {
            DbResultAfter dbResultAfter = null;
            GetChildTableList(model, GetContext(model)).ForEach(x =>
            {
                var getChildValues = (IList)model.GetPropertyValue(x);
                foreach (var childItems in getChildValues)
                    dbResultAfter = InsertOrUpdate(childItems, expressionOrSql, handlers);
            });
            return dbResultAfter;
        }

        // both object, List<object>
        // Impleement Multicontext, Set DbContext
        public Result Insert(Object model, params IPreDbOperationHandler[] handler)
        {
            return Archive(model, handler, DBOperations.INSERT, new[] { model.GetType() }, model);
        }

        // both object, List<object>
        // Impleement Multicontext, Set DbContext
        public Result Update(Object model, params IPreDbOperationHandler[] handler)
        {
            SetContext(model);
            return Archive(model, handler, DBOperations.UPDATE, new[] { model.GetType(), typeof(bool) }, model, true);
        }
        public Result Update(Object model, DbContext dbContext, params IPreDbOperationHandler[] handler)
        {
            SetContext(model, (DbContext)dbContext);
            return Archive(model, handler, DBOperations.UPDATE, new[] { model.GetType(), typeof(bool) }, model, true);
        }

        // Impleement Multicontext, Set DbContext

        public Result Delete(object model, params IPreDbOperationHandler[] handler)
        {
            return Delete(model, GetContext(model), handler);
        }

        public Result Delete(object model, DbContext dbContext, params IPreDbOperationHandler[] handler)
        {
            SetContext(model, dbContext);
            Result result = null;
            var pk = GetPrimaryKeyInfo(model).Name;
            var recordId = Dynamic.GetPropValue(model, pk);
            var record = ((IQueryable<object>)GetAll(model, $"{pk} = \"{recordId}\"")).AsNoTracking();
            foreach (var item in GetChildTableList(model, CurrentContext))
                record = record.Include(item);
            object excutedRecord = record.ToList().FirstOrDefault();
            result = Archive(excutedRecord, handler, DBOperations.DELETE, new[] { model.GetType() }, model);
            return result;
        }

        // Impleement Multicontext, Set DbContext
        //public Result DeleteAll(Object model, DbContext dbContext = null, params IPreDbOperationHandler[] handler)
        //{
        //    SetContext((DbContext)dbContext);
        //    unit.RegisterContext((DbContext)dbContext);
        //    return Archive(model, handler, DBOperations.DELETE, new[] { model.GetType() }, model);
        //}
        private void ExceptionBindToResult(Result result, Exception ex)
        {
            result.Message = ex.Message;
            result.Success = false;
            result.Exception = ex;
        }
        // Result Conversion
        private Result Archive(object model, IPreDbOperationHandler[] handlers, DBOperations dBOperations, Type[] methodParameterType = null, params object[] methodParameters)
        {
            Result result = new Result();
            try
            {
                if (Tools.HasAccess(model.GetType().Name, dBOperations, ConfigLib.CheckTablePermission))
                {
                    for (int i = 0; i < handlers.Length; i++) model = handlers[i].Handle(model);
                    Dynamic.Repository(model, CurrentContext, dBOperations, methodParameterType, methodParameters);
                    result = new Result(true, $"{dBOperations.ToString()} has been {am.db.Successful}", 0, model);
                    var auditTrailModel = Tools.LogAuditTrail(model, dBOperations);
                    if (result.Success) Dynamic.Repository(auditTrailModel, DefaultContext, DBOperations.INSERT, new Type[] { typeof(AuditTrail) }, auditTrailModel);
                }
                else result.Set(false, am.db.NoPermission);
            }
            catch (SqlException ex)
            {
                ExceptionBindToResult(result, ex);
            }
            catch (DbException ex)
            {
                ExceptionBindToResult(result, ex);
            }
            catch (Exception ex)
            {
                ExceptionBindToResult(result, ex);
            }
            return result;

        }

        public object GetAll(object model, string expression, bool isSql = false, DbContext dbContext = null)
        {
            object record;
            DbContext localContext = GetContext(model, dbContext);
            var repository = Dynamic.GetInstance(Dynamic.GetRepositoryBase(localContext), model.GetType(), localContext);
            if (!isSql)
                record = Dynamic.InvokeMethod("GetAll", repository, model.GetType(), new[] { typeof(String) }, expression);
            else
                record = Convert.ChangeType(Dynamic.InvokeMethod("GetRecordSet", repository, model.GetType(),
                    new[] { typeof(string), typeof(SqlParameter[]) }, expression, null), typeof(List<>).MakeGenericType(model.GetType()));
            return record;
        }

        public object GetAll(object model, MulticastDelegate expression, DbContext dbContext = null)
        {
            DbContext localContext = GetContext(model, dbContext);
            var repository = Dynamic.GetInstance(Dynamic.GetRepositoryBase(localContext), model.GetType(), localContext);
            var record = Dynamic.InvokeMethod("GetAll", repository, model.GetType(), new[] { typeof(MulticastDelegate) }, expression);
            return record;
        }

        public object GetById(object model, object id, bool includeChild = true, DbContext dbContext = null)
        {
            DbContext localContext = GetContext(model, dbContext);
            var repository = Dynamic.GetInstance(Dynamic.GetRepositoryBase(localContext), model.GetType(), localContext);
            var pkInfo = this.GetPrimaryKeyInfo(model);
            var pkValueCusted = Convert.ChangeType(id, pkInfo.PropertyType);
            var record = Dynamic.InvokeMethod("GetById", repository, model.GetType(), new Type[] { pkInfo.GetType() }, pkValueCusted);
            return record;
        }

        public object GetPrimaryKeyValue(object model) => model.GetPropertyValue(GetPrimaryKeyInfo(model).Name);

        public List<T> Get<T>(bool includeChild = true, DbContext dbContext = null) where T : class, new()
        {
            DbContext localContext = GetContext(new T(), dbContext);
            var repository = Dynamic.GetInstance(Dynamic.GetRepositoryBase(localContext), typeof(T), localContext);
            var record = (IQueryable<T>)Dynamic.InvokeMethod("GetAll", repository, typeof(T));
            if (includeChild) GetChildTableList(new T(), localContext).ForEach(item => record.Include(item));
            return record.ToList();
        }

        public T Get<T>(int Id, bool includeChild = true, DbContext dbContext = null) where T : class, new()
        {
            DbContext localContext = GetContext(new T(), dbContext);
            var repository = Dynamic.GetInstance(Dynamic.GetRepositoryBase(localContext), typeof(T), localContext);
            var record = Dynamic.InvokeMethod("GetById", repository, typeof(T), new[] { typeof(string) }, Id);
            var result = Get<T>(x => x == (T)record, true).FirstOrDefault();
            return result;
        }

        public List<T> Get<T>(Expression<Func<T, bool>> expression, bool includeChild = true, DbContext dbContext = null) where T : class, new()
        {
            DbContext localContext = GetContext(new T(), dbContext);
            var repository = Dynamic.GetInstance(Dynamic.GetRepositoryBase(localContext), typeof(T), localContext);
            var record = (IQueryable<T>)Dynamic.InvokeMethod("FindBy", repository, typeof(T), new[] { typeof(Expression<Func<T, bool>>) }, expression);
            if (includeChild)
            {
                foreach (var item in GetChildTableList(new T(), localContext))
                    record = record.Include(item.Trim());
            }
            return record.ToList();
        }

        public List<T> Get<T>(string sql, DbContext dbContext = null) where T : class, new()
        {
            DbContext localContext = GetContext(new T(), dbContext);
            var repository = Dynamic.GetInstance(Dynamic.GetRepositoryBase(localContext), typeof(T), localContext);
            var record = ((List<T>)Dynamic.InvokeMethod("GetRecordSet", repository, typeof(T), new[] { typeof(string), typeof(SqlParameter[]) }, sql, null)).AsQueryable();
            return record.ToList();
        }

        public SingleValue GetSingleValue(string select, string where, string orderBy)
        {
            var isSP = select.ToUpper().Contains("EXEC");
            var dt = GetDataTable(isSP ? select : SqlQueryBuilder(select, where, orderBy, 1, 1));
            return new SingleValue()
            {
                Value = dt.Rows[0][0].ToString()
            };
        }

        public PropertyInfo GetPrimaryKeyInfo(object model)
        {
            if (model.GetType().Name == "String") model = Dynamic.GetInstance(Dynamic.GetFullyQualifiedPath(model.ToString()));

            PropertyInfo[] properties = model.GetType().GetProperties();//typeof(T).GetProperties();
            foreach (PropertyInfo pI in properties)
            {
                System.Object[] attributes = pI.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute.GetType().Name == "KeyAttribute")
                    {
                        return pI;
                    }

                }
            }
            return null;
        }
        public DataTable GetDataTable(string sql, DbContext dbContext = null)
        {
            DbContext localContext = GetContext(null, dbContext);
            var repository = Dynamic.GetInstance(Dynamic.GetRepositoryBase(localContext), typeof(DataTable), localContext);
            return (DataTable)Dynamic.InvokeMethod("GetDataTable", repository, null, new Type[] { typeof(string) }, sql);
        }

        public List<string> GetChildTableList(object model, DbContext context)
        {
            List<string> arr = new List<string>();
            PropertyInfo[] properties = model.GetType().GetProperties();
            var propertyList = properties.Where(p => p.PropertyType.Name.Contains("List"));

            foreach (var item in propertyList)
            {
                PropertyInfo[] property = context.GetType().GetProperties();
                foreach (var citem in property)
                {
                    if (citem.Name == item.Name)
                        arr.Add(citem.Name);
                }
            }

            return arr;
        }

        public IEnumerable<object> SelectWithPaging(object dynamicModel, string select, string where, string orderBy, bool hasChildIncluded, int pageNo, int pageSize, DbContext dbContext = null)
        {
            try
            {
                DbContext localContext = GetContext(dynamicModel, dbContext);
                if (string.IsNullOrEmpty(select))
                {
                    return LinqData(dynamicModel, where, orderBy, hasChildIncluded, (pageNo - 1), pageSize, localContext);
                }
                string sql = SqlQueryBuilder(select, where, orderBy, pageNo, pageSize);
                var res = (IList)GetAll(dynamicModel, sql, true, localContext);
                IEnumerable<object> list = res.Cast<object>();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private IEnumerable<object> LinqData(object dynamicModel, string where, string orderBy, bool hasChildIncluded, int pageNo, int pageSize, DbContext dbContext)
        {
            string _where = Regex.Replace(where, "WHERE", "", RegexOptions.IgnoreCase);
            where = Regex.Replace(_where ?? "", " AND ", " && ", RegexOptions.IgnoreCase);
            _where = Regex.Replace(where ?? "", " OR ", " || ", RegexOptions.IgnoreCase);
            where = Regex.Replace(_where ?? "", " <> ", " != ", RegexOptions.IgnoreCase);
            _where = Regex.Replace(where ?? "", "'", "\"", RegexOptions.IgnoreCase);

            orderBy = Regex.Replace(orderBy ?? "", "order by", "", RegexOptions.IgnoreCase);

            var childTableList = hasChildIncluded ? GetChildTableList(dynamicModel, GetContext(dynamicModel, dbContext)).Join(",") : "";
            var types = new Type[] { typeof(string), typeof(string), typeof(string), typeof(int), typeof(int) };
            //var objs = new object[] { where, orderBy, childTableList, pageNo, pageSize };
            var repositoryBaseObject = Dynamic.GetInstance(Dynamic.GetRepositoryBase(dbContext), dynamicModel.GetType(), dbContext);
            var result = Dynamic.InvokeMethod("GetPagedList", repositoryBaseObject, dynamicModel.GetType(), types, _where, orderBy, childTableList, pageNo, pageSize);
            var data = (IList)result.GetPropertyValue("Items");
            var items = ((IList)result.GetPropertyValue("Items")).Cast<object>();
            return items;
        }
        private string SqlQueryBuilder(string select, string where, string orderBy, int pageNo, int pageSize)
        {
            string _select = "SELECT " + Regex.Replace(select ?? "", "SELECT", "", RegexOptions.IgnoreCase);
            string _where = string.IsNullOrEmpty(where) ? "" :
                " WHERE " + Regex.Replace(where ?? "", "WHERE", "", RegexOptions.IgnoreCase);
            string _orderBy = string.IsNullOrEmpty(orderBy) ? " Order By 1 asc " :
                " ORDER BY " + orderBy.Substring((orderBy ?? "").ToUpper().IndexOf("BY") + 2, orderBy.Length - (orderBy.ToUpper().IndexOf("BY") + 2));
            return $@"{_select} {_where} {_orderBy}
            OFFSET ({pageSize} * ({pageNo} - 1))  ROWS
                FETCH NEXT {pageSize} ROWS ONLY; ";
        }

        private int LinqCount(object dynamicModel, string where, DbContext dbContext)
        {
            DbContext localContext = GetContext(dynamicModel, dbContext);
            string _where = Regex.Replace(where, "WHERE", "", RegexOptions.IgnoreCase);
            where = Regex.Replace(_where, " AND ", " && ", RegexOptions.IgnoreCase);
            _where = Regex.Replace(where, " OR ", " || ", RegexOptions.IgnoreCase);
            where = Regex.Replace(_where, " <> ", " != ", RegexOptions.IgnoreCase);
            _where = Regex.Replace(where, "'", "\"", RegexOptions.IgnoreCase);
            var repositoryBaseObject = Dynamic.GetInstance(Dynamic.GetRepositoryBase(localContext), dynamicModel.GetType(), localContext);
            var result = Dynamic.InvokeMethod("Count", repositoryBaseObject, null, new Type[] { typeof(string) }, _where);
            return (int)result;
        }
        public int SelectTotalRecordCount(object dynamicModel, string select, string where, string orderBy, DbContext dbContext = null)
        {
            if (string.IsNullOrEmpty(select))
                return LinqCount(dynamicModel, where, dbContext);
            string _select = "SELECT " + Regex.Replace(select ?? "", "SELECT", "", RegexOptions.IgnoreCase);
            string _where = string.IsNullOrEmpty((where ?? "").Trim()) ? "" : " WHERE " + Regex.Replace(where ?? "", "WHERE", "", RegexOptions.IgnoreCase);
            int fromIndex = (_select ?? "").ToUpper().IndexOf(" FROM ");
            _select = "SELECT COUNT(*) Total " + _select.Substring(fromIndex, _select.Length - fromIndex);
            var sql = $@"{_select} {_where}";
            return Get<TotalRecord>(sql, dbContext).FirstOrDefault().Total;
        }

        internal class TotalRecord
        {
            public int Total { get; set; }
        }

        public DbContext GetAnnotationDbContext(object model)
        {
            if (model == null) return null;
            var customAttributes = (ContextAttribute[])model.GetType().GetCustomAttributes(typeof(ContextAttribute), true);
            if (customAttributes.Length > 0)
            {
                var attribute = customAttributes[0];
                string contextName = attribute.GetPropertyValue("DbContextName").ToString();
                var claz = (DbContext)Dynamic.GetInstance(Dynamic.GetFullyQualifiedPath(contextName),null,SAASType.NoSaas);
                return claz;
            }
            return null;
        }

        
        public void ExecuteSQLFile(string filePath, DbContext context,string databaseName)
        {
           
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            connectionString = GetConnectionString(connectionString,databaseName);
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

        public bool IsDatabaseExist(string databaseName)
        {
            var isDbExist = GetSingleValue($"SELECT DB_ID('{databaseName}')", "", "");
            return !string.IsNullOrEmpty(isDbExist.Value);
        }

        public void CreateDatabase(string subdomainPrefix)
        {
            var databaseCreateScript = $"Create database {subdomainPrefix}";
            string sqlConnectionString = "Data Source=10.12.1.2;Initial Catalog=master;user id=sa; password=bdco";
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(databaseCreateScript, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        private string GetConnectionString(string baseConnectionString, string currentTenant)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(baseConnectionString);
            return $"Data Source={builder.DataSource};Initial Catalog={currentTenant};user id={builder.UserID}; password={builder.Password}";
        }
    }
}