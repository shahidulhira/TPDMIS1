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
    public interface IRepository<T> : IDisposable where T : class
    {
        bool IsSuccessOperation { get; set; }
        bool IsExist(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();

        T GetById(object id);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        string Insert(T entity);
        string InsertAll(List<T> entities);
        string Update(T entity);
        string UpdateAll(List<T> entities);
        string Delete(int id);
        string Delete(long id);
        string Delete(T entity);
        string DeleteAll(List<T> entities);
        string SaveChange();
        object SaveChangeAndReturnObject();
        void SetContext(DbContext context);
        string ExeccuteRawQyery(string sql, SqlParameter[] _sqlParameter = null);
        List<T> GetRecordSetList(string sqlSyntex, [DataSourceRequest]DataSourceRequest request, SqlParameter[] param = null);
        DataSourceResult GetRecordSet(string sqlSyntex, [DataSourceRequest]DataSourceRequest request, SqlParameter[] param = null);
        DbRawSqlQuery<T> GetRecordSet(string sqlSyntex, SqlParameter[] param = null);
        string GetSingleValue(string sSQL);
        DataTable GetDataTable(string sqlSyntex);
        

    }
}
