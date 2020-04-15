using Domain.Filter;
using RapidFireLib.Lib.Core;
using RapidFireLib.UX.JGrid;

namespace Domain.Selectors.Student
{
    public class StudentSelector : IQuerySelector
    {
        public string Select(JGridRequest packet)
        {
            string sql = "";
            BaseFilter filter = Tools.GetFilter<BaseFilter>(packet);
            packet.ModelName = GetSwitch(packet.ModelName);
            if (packet.PageNo == 0)
            {
                packet.PageNo = 1;
            }
            if (packet.PageSize == 0)
            {
                packet.PageSize = int.MaxValue;
            }
            switch (packet.ModelName)
            {
                case "StudentView":
                    sql = "SELECT * FROM Student";
                    break;
                case "GmpView":
                    sql = string.Format(@"EXEC SearchGmpMasterData '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}'",
                        filter.Users, filter.RcNSchoolCode, filter.FromDate, filter.ToDate, filter.DistrictCode,
                        filter.UpazilaCode, filter.UnionCode, filter.VillageCode, filter.GMPStatus,
                        filter.AttendStatus, filter.BenId, packet.PageSize, packet.PageNo);
                    break;
                case "GmpDetailsView":
                    sql = string.Format(@"exec JGridGmpDetail '{0}'", packet.RecordId);
                    break;
                case "ESMessageView":
                    sql = string.Format(@"select GC.RowId,Gm.MessageText as Message from GmpCounselling  GC
                                        INNER JOIN GmpCounsellingMessage GM ON GM.MessageId=GC.MessageId
                                        where GC.RecordId='{0}'", packet.RecordId);
                    break;
                case "GmpMaterialView":
                    sql = string.Format(@"select GM.RowId,M.MaterialText as MaterialText from GmpMaterial  GM
                                        INNER JOIN GmpMaterialReference M ON M.MaterialId=GM.MaterialId
                                        where GM.RecordId='{0}'", packet.RecordId);
                    break;
                case "GroupInfoEView":
                    sql = string.Format(@"EXEC SearchGroupInfoEMasterData '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}'",
                        filter.FromDate, filter.ToDate, filter.ECCDSessionCategoryForGroup ?? filter.BESessionCategoryForGroup, filter.Group,
                        filter.RcNSchoolCode, filter.Users, filter.BenId,
                        filter.DistrictCode, filter.UpazilaCode, filter.UnionCode, filter.VillageCode, packet.PageSize, packet.PageNo);
                    break;
                case "GroupMemberInfoEView":
                    sql = string.Format(@"EXEC SearchGroupMemberInfoEData '{0}'", packet.RecordId);
                    break;
            }

            return sql;
        }

        public string GetSwitch(string s)
        {
            string ret = s;
            if (s.Contains("MCheckList"))
            {
                if (s.Contains("QView")) ret = "QBQ";
                else if (s.Contains("StepsView")) ret = "QBSteps";
                else ret = "QB";
            }
            return ret;
        }
    }
}
