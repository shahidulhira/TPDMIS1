using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace RapidFireLib.Lib.DB
{
    public interface IRepository<T> : IDisposable where T : class
    {
        bool IsExist(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IPagedList<T> GetPagedList(string where = null,
                                                string orderBy = null,
                                                string include = null,
                                                int pageIndex = 0,
                                                int pageSize = 20);
        T GetById(object id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void InsertAll(List<T> entities);
        void Update(T entity);
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
        //List<T> GetRecordSetList(string sqlSyntex, [DataSourceRequest]DataSourceRequest request, SqlParameter[] param = null);
        //DataSourceResult GetRecordSet(string sqlSyntex, [DataSourceRequest]DataSourceRequest request, SqlParameter[] param = null);
        List<T> GetRecordSet(string sqlSyntex, SqlParameter[] param = null);
        string GetSingleValue(string sSQL);
        DataTable GetDataTable(string sqlSyntex);
        void DetachEntity(T detachableObject);
    }
}
