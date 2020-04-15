//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using Kendo.Mvc.UI;

//namespace RapidFireLib.Lib.DB
//{
//    public class RepositoryOracle<T> : IRepository<T> where T : class
//    {
//        public RepositoryOracle(DbContext ctx)
//        {
//            //this.context = ctx;
//            //table = context.Set<T>();
//        }

//        public void Delete(object id)
//        {
//            throw new NotImplementedException();
//        }

//        public void Delete(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void Delete(long id)
//        {
//            throw new NotImplementedException();
//        }

//        public void Delete(T entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteAll(List<T> entities)
//        {
//            throw new NotImplementedException();
//        }

//        public void DetachEntity(T detachableObject)
//        {
//            throw new NotImplementedException();
//        }

//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }

//        public void ExeccuteRawQyery(string sql, SqlParameter[] _sqlParameter = null)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public IQueryable<T> GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        public T GetById(object id)
//        {
//            throw new NotImplementedException();
//        }

//        public DataTable GetDataTable(string sqlSyntex)
//        {
//            throw new NotImplementedException();
//        }

//        public DataSourceResult GetRecordSet(string sqlSyntex, [DataSourceRequest] DataSourceRequest request, SqlParameter[] param = null)
//        {
//            throw new NotImplementedException();
//        }

//        //public DbRawSqlQuery<T> GetRecordSet(string sqlSyntex, SqlParameter[] param = null)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        public List<T> GetRecordSetList(string sqlSyntex, [DataSourceRequest] DataSourceRequest request, SqlParameter[] param = null)
//        {
//            throw new NotImplementedException();
//        }

//        public string GetSingleValue(string sSQL)
//        {
//            throw new NotImplementedException();
//        }

//        public void Insert(T entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void InsertAll(List<T> entities)
//        {
//            throw new NotImplementedException();
//        }

//        public bool IsExist(Expression<Func<T, bool>> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public void SaveChange()
//        {
//            throw new NotImplementedException();
//        }

//        public object SaveChangeAndReturnObject()
//        {
//            throw new NotImplementedException();
//        }

//        public void SetContext(DbContext context)
//        {
//            throw new NotImplementedException();
//        }

//        public void Test()
//        {
//            throw new NotImplementedException();
//        }

//        public void Update(T entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void UpdateAll(List<T> entities)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
