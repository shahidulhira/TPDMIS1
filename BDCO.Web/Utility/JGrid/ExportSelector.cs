using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDCO.Web.Utility.JGrid
{
    public class ExportSelector
    {
        string sql = "";
        public string getSelector(JGridRequest packet)
        {
            FilterData filter = new FilterData();
                filter = JsonConvert.DeserializeObject<FilterData>(packet.Data.ToString());

            packet.ModelName = GetSwitch(packet.ModelName);
            sql = string.Format(@"SELECT * FROM {0}", filter.ModelName);
            switch (packet.ModelName)
            {                
                case "DistributionPlanView":
                    sql = string.Format($@"EXEC Report_DistributionPlan '{filter.CampId}','','{filter.FromDate}','999999','1'");
                    break;
                case "UserWiseServiceCountReportView":
                    sql = $@"EXEC UserWiseServiceCount '{filter.Users}','{filter.FromDate}','{filter.ToDate}','{filter.BlockId}','{filter.DistrictCode}','{filter.UpazilaCode}','{filter.UnionCode}','{filter.VillageCode}','1','999999'";
                    break;
                case "DailyProgressReportView":
                    sql = $@"EXEC Report_IndicatorInfo '{filter.Program}','{filter.FromDate}','{filter.ToDate}','{filter.BlockId}','{filter.DistrictCode}','{filter.UpazilaCode}','{filter.UnionCode}','{filter.VillageCode}'";
                    break;
                case "MemberInfo":
                    sql = $@"EXEC ExportMemberInfo '{filter.FromDate}','{filter.ToDate}','{filter.BenId}','{filter.Users}','{filter.BlockId}','{filter.DistrictCode}','{filter.UpazilaCode}','{filter.UnionCode}','{filter.VillageCode}'";
                    break;
                    

            }
            return sql;
        }

        public string GetSwitch(string s)
        {
            string ret = s;            
            return ret;
        }
    }
}