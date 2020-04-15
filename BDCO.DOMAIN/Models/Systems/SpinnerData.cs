using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDCO.Domain.RequestParams;

namespace BDCO.Domain.Models.Systems
{
    public class SpinnerData
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        public List<SpinnerValue> APIGetSpinnerData(ForApiResponse apr, SpinnerRequest req)
        {
            string sql = "";
            if(req.where == "" || req.where == null)
            {
                sql = $@"SELECT ROW_NUMBER() over ( order by {req.valueText}) as RowNo,{req.displayText} as 'displayText', {req.valueText} as 'valueText' FROM  {req.modelName}  Order by RowNo asc OFFSET ({apr.PageSize}*({apr.PageNo}-1)) ROWS FETCH NEXT {apr.PageSize} ROWS ONLY";
            }
            else
            {
                sql = $@"SELECT ROW_NUMBER() over ( order by {req.valueText}) as RowNo,{req.displayText} as 'displayText', {req.valueText} as 'valueText' FROM  {req.modelName}  WHERE {req.where} Order by RowNo asc OFFSET ({apr.PageSize}*({apr.PageNo}-1)) ROWS FETCH NEXT {apr.PageSize} ROWS ONLY";
            }
            
            var list = unitOfWork.GenericRepositories<SpinnerValue>().GetRecordSet(sql).ToList();
            return list;
        }

        public int APIGetTotalRecord(ForApiResponse apr, SpinnerRequest req)
        {
            string sql = "";
            if (req.where == "" || req.where == null)
            {
                sql = $@"SELECT COUNT({req.valueText}) as Total FROM  {req.modelName} ";
            }
            else
            {
                sql = $@"SELECT COUNT({req.valueText}) as Total FROM  {req.modelName}  WHERE {req.where} ";
            }

            var total = unitOfWork.context.Database.SqlQuery<int>(sql).FirstOrDefault();
            return total;
        }

    }


    public class SpinnerRequest
    {
        public string modelName { get; set; }
        public string displayText { get; set; }
        public string valueText { get; set; }
        public string where { get; set; }
    }
    public class SpinnerValue
    {
        public string displayText { get; set; }
        public string valueText { get; set; }
    }
}
