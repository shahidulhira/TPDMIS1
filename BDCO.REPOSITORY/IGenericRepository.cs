using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace BDCO.Repository
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {       
        bool IsExist(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();

        T GetById(object id);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void InsertAll(List<T> entities);
        void Update(T entity,bool hasToDeatch=true);
        void UpdateAll(List<T> entities);
        void Delete(object id);
        void Delete(int id);
        void Delete(long id);
        void Delete(T entity);
        void DeleteAll(List<T> entities);
        void SaveChange();
        object SaveChangeAndReturnObject();
        void SetContext(DbContext context);
        void ExeccuteRawQyery(string sql, SqlParameter[] _sqlParameter = null);
        List<T> GetRecordSetList(string sqlSyntex, [DataSourceRequest]DataSourceRequest request, SqlParameter[] param = null);
        DataSourceResult GetRecordSet(string sqlSyntex, [DataSourceRequest]DataSourceRequest request, SqlParameter[] param = null);
        DbRawSqlQuery<T> GetRecordSet(string sqlSyntex, SqlParameter[] param = null);
        string GetSingleValue(string sSQL);
        DataTable GetDataTable(string sqlSyntex);
        void DetachEntity(T detachableObject);
    }
}
