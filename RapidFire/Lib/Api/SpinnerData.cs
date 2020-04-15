using Microsoft.EntityFrameworkCore;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace RapidFireLib.Lib.Api
{
    public class SpinnerDataSql
    {
        public int TotalRow { get; set; }
        public string DisplayText { get; set; }
        public string ValueText { get; set; }
    }
    public class SpinnerData
    {
        Db db = null;
        public SpinnerData(IConfig config)
        {
            db = new Db(config);
        }

        public Tuple<int, List<SpinnerValue>> APIGetSpinnerDataFromSP(string storeProcedure, string displayText, string valueText, int pageSize, int pageNo, DbContext dbContext = null)
        {
            string sqlExtra = "";
            List<SpinnerValue> spinnerDatas = new List<SpinnerValue>();
            if (!storeProcedure.ToUpper().Contains("EXEC"))
            {
                string _select = "SELECT " + Regex.Replace(storeProcedure ?? "", "SELECT", "", RegexOptions.IgnoreCase);
                _select = _select.ToUpper().Replace(" FROM ", " ,COUNT(*) over()TotalRow FROM ");
                if (storeProcedure.ToUpper().Contains("ORDER BY"))
                    sqlExtra = $@"{_select} OFFSET ({pageSize}*({pageNo}-1)) ROWS FETCH NEXT {pageSize} ROWS ONLY";
                else
                    sqlExtra = $@"{_select} Order by 1 asc OFFSET ({pageSize}*({pageNo}-1)) ROWS FETCH NEXT {pageSize} ROWS ONLY";
                var datatable = db.GetDataTable(sqlExtra);
                for (int i = 0; i < datatable.Rows.Count; i++)
                    spinnerDatas.Add(new SpinnerValue() { DisplayText = datatable.Rows[i][displayText].ToString(), ValueText = datatable.Rows[i][valueText].ToString() });
                return new Tuple<int, List<SpinnerValue>>(datatable.Rows.Count == 0 ? 0 : Convert.ToInt32(datatable.Rows[0]["TotalRow"]), spinnerDatas);
            }
            else
            {
                var datatable = db.GetDataTable(storeProcedure);
                for (int i = 0; i < datatable.Rows.Count; i++)
                    spinnerDatas.Add(new SpinnerValue() { DisplayText = datatable.Rows[i][displayText].ToString(), ValueText = datatable.Rows[i][valueText].ToString() });
            }
            return new Tuple<int, List<SpinnerValue>>(spinnerDatas.Count(), spinnerDatas);
        }
        public List<SpinnerValue> APIGetSpinnerData(ForApiResponse apr, SpinnerRequest req, DbContext dbContext = null)
        {
            string sql = "";
            if (req.Where == "" || req.Where == null)
            {
                sql = $@"SELECT ROW_NUMBER() over ( order by {req.ValueText}) as RowNo,{req.DisplayText} as 'DisplayText', CAST({req.ValueText} as nvarchar) as 'ValueText' FROM  {req.ModelName}  Order by RowNo asc OFFSET ({apr.PageSize}*({apr.PageNo}-1)) ROWS FETCH NEXT {apr.PageSize} ROWS ONLY";
            }
            else
            {
                sql = $@"SELECT ROW_NUMBER() over ( order by {req.ValueText}) as RowNo,{req.DisplayText} as 'DisplayText', CAST({req.ValueText} as nvarchar) as 'ValueText' FROM  {req.ModelName}  WHERE {req.Where} Order by RowNo asc OFFSET ({apr.PageSize}*({apr.PageNo}-1)) ROWS FETCH NEXT {apr.PageSize} ROWS ONLY";
            }
            var list = db.Get<SpinnerValue>(sql, dbContext).ToList();
            return list;
        }

        public int APIGetTotalRecord(ForApiResponse apr, SpinnerRequest req, DbContext dbContext = null)
        {
            string sql = "";
            if (req.Where == "" || req.Where == null)
            {
                sql = $@"SELECT COUNT({req.ValueText}) as Total FROM  {req.ModelName} ";
            }
            else
            {
                sql = $@"SELECT COUNT({req.ValueText}) as Total FROM  {req.ModelName}  WHERE {req.Where} ";
            }
            return db.Get<TotalRecord>(sql, dbContext).FirstOrDefault().Total;
        }

        public class TotalRecord
        {
            public int Total { get; set; }
        }

    }
}
