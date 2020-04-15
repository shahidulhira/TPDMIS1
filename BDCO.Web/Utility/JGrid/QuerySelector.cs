using Newtonsoft.Json;

namespace BDCO.Web.Utility.JGrid
{
    public class QuerySelector
    {
        string sql = "";
        public string getSelector(JGridRequest packet)
        {
            FilterData filter = new FilterData();
            if (packet.Data != null && packet.RequestType != "Update")
                filter = JsonConvert.DeserializeObject<FilterData>(packet.Data.ToString());

            switch (packet.ModelName)
            {
                case "MemberInfoView":
                    sql = string.Format(@"EXEC SearchMemberInfoData '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}'",
                        filter.FromDate, filter.ToDate, filter.BenId, filter.BenName,filter.BenType,
                        filter.BenUsers, filter.DistrictCode,filter.UpazilaCode, filter.UnionCode, filter.VillageCode,
                         filter.CenterId, filter.CampId, filter.BlockId,packet.PageSize,packet.PageNo);
                    break;
                case "MemberDetailView":
                    sql = string.Format(@"exec SearchMemberInfoDetailData '{0}'", packet.RecordId);
                    break;
                case "SessionInfoView":
                    sql = string.Format(@"EXEC SearchSessionInfoMasterData '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}'",
                        filter.FromDate, filter.ToDate, filter.CategoryId, filter.GroupId, filter.SessionAttendance,
                        filter.IndividualAttendance, filter.CenterId, filter.Users, filter.BenId,
                        filter.DistrictCode, filter.UpazilaCode, filter.UnionCode, filter.VillageCode, packet.PageSize, packet.PageNo, filter.BlockId);
                    break;
                case "SessionMemberInfoView":
                    sql = string.Format(@"EXEC SessionInfoDetails '{0}'", packet.RecordId);
                    break;
                case "SessionInfoSummaryView":
                    sql = string.Format(@"EXEC SessionInfoSummaryDetails '{0}'", packet.RecordId);
                    break;
                case "SessionInfoTopicView":
                    sql = string.Format(@"EXEC SearchSessionInfoTopicData '{0}'", packet.RecordId);
                    break;
                case "SessionMemberGuardiansView":
                    sql = string.Format(@"EXEC SearchSessionMemberGuardiansData '{0}'", packet.RecordId);
                    break;
                case "GmpView":
                    sql = $@"EXEC SearchGmpMaster '{filter.FromDate}','{filter.ToDate}','{filter.Users}','{filter.BenId}','{filter.BenName}','{filter.BenType}','{filter.BenStatus}','{filter.InCare}','{filter.DistrictCode}','{filter.UpazilaCode}','{filter.UnionCode}','{filter.VillageCode}','{filter.CenterId}','{packet.PageSize}','{packet.PageNo}'";
                    break;
                case "GmpDetailsView":
                    sql = $@"EXEC GmpDetailsByRecordId '{packet.RecordId}'";
                    break;
                case "AdmissionView":
                    ////sql = $@"EXEC SearchAdmission '{filter.FromDate}','{filter.ToDate}','{filter.Block}','{filter.ServicePointId}','{filter.Users}','{filter.BenId}','{filter.BenName}','{filter.DistrictCode}','{filter.UpazilaCode}','{filter.UnionCode}','{filter.VillageCode}','{packet.PageSize}','{packet.PageNo}'";
                    sql = $@"EXEC SearchAdmissionMaster '{filter.FromDate}','{filter.ToDate}','{filter.CenterId}','{filter.Users}','{filter.BenId}','{filter.BenName}','{filter.DistrictCode}','{filter.UpazilaCode}','{filter.UnionCode}','{filter.VillageCode}','{packet.PageSize}','{packet.PageNo}','{filter.BlockId}','{filter.AdmissionType}','{filter.DischargeType}'";
                    break;
                case "AdmissionDetailsView":
                    sql = $@"EXEC SearchAdmissionDetailsByRecordId '{packet.RecordId}'";
                    break;
                case "DischargeView":
                    sql = $@"EXEC SearchDischargeByRecordId '{packet.RecordId}'";
                    break;
                case "DistributionView":
                    sql = $@"EXEC SearchDistribution '{filter.FromDate}','{filter.ToDate}','{filter.Users}','{filter.BenId}','{filter.BenName}','{filter.BenType}','{filter.BenStatus}','{filter.InCare}','{filter.DistrictCode}','{filter.UpazilaCode}','{filter.UnionCode}','{filter.VillageCode}','{filter.CenterId}','{packet.PageSize}','{packet.PageNo}'";
                    break;
                case "DistributionDetailsView":
                    sql = $@"EXEC SearchDistributionByBenId '{packet.RecordId}'";
                    break;
                case "DailyProgressReportView":
                    sql = $@"EXEC Report_IndicatorInfo '{filter.Program}','{filter.FromDate}','{filter.ToDate}','{filter.BlockId}','{filter.DistrictCode}','{filter.UpazilaCode}','{filter.UnionCode}','{filter.VillageCode}'";
                    break;
                case "UserWiseServiceCountReportView":
                    sql = $@"EXEC UserWiseServiceCount '{filter.Users}','{filter.FromDate}','{filter.ToDate}','{filter.BlockId}','{filter.DistrictCode}','{filter.UpazilaCode}','{filter.UnionCode}','{filter.VillageCode}','{packet.PageNo}','{packet.PageSize}'";
                    break;
                case "DistributionPlanView":
                    sql = $@"EXEC Report_DistributionPlan '{filter.CampId}','{filter.StatusType}','{filter.FromDate}','{packet.PageSize}','{packet.PageNo}'";
                    break;
            }
            return sql;
        }
    }

    public class FilterData
    {
        public int? BlockId { get; set; }
        public string ID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string CenterId { get; set; }
        public string BenUsers { get; set; }
        public string Users { get; set; }
        public string BenName { get; set; }
        public int? PageSize { get; set; }
        public int? PageNo { get; set; }
        public int? RecordId { get; set; }
        public string GMPStatus { get; set; }
        public string AttendStatus { get; set; }
        public string Consistency { get; set; }
        public string Periodicity { get; set; }
        public string Practice { get; set; }
        public string BenId { get; set; }
        public int? CategoryId { get; set; }
        public int? GroupId { get; set; }
        public int? SessionAttendance { get; set; }
        public int? IndividualAttendance { get; set; }
        public int? DoseYear { get; set; }
        public int? Dose { get; set; }
        public string PLPCardCode { get; set; }
        public string MessageTypeId { get; set; }
        public string MessageId { get; set; }
        public string Program { get; set; }
        public string CampId { get; set; }
        public string StatusType { get; set; }
        public string AdmissionType { get; set; }
        public string DischargeType { get; set; }
        public string ModelName { get; set; }
        public string OverrideModel { get; set; }
        public bool? IsOverride { get; set; }
        public string Extension { get; set; }
        public string BenType { get;  set; }
        public string BenStatus { get;  set; }
        public string InCare { get;  set; }
        //public string CampCode { get; set; }
        public int? TrendType { get; set; }
    }
}