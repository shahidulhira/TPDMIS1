using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filter
{
    public class BaseFilter
    {
        public string ID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string RcNSchoolCode { get; set; }
        public string Users { get; set; }
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
        public string VisitTyp { get; set; }
        public string IsSponsored { get; set; }
        public string AttendanceInfo { get; set; }
        public int? Group { get; set; }
        public int? JobType { get; set; }
        public int? MigrationType { get; set; }
        public int? MigrationEntityType { get; set; }
        public int? Advised { get; set; }
        public int? ECCDSessionCategory { get; set; }
        public int? BESessionCategory { get; set; }
        public int? BESessionAttendanceCategory { get; set; }
        public int? ECCDSessionAttendanceCategory { get; set; }
        public int? TradeTypeId { get; set; }
        public int? InstitutionName { get; set; }
        public int? GoalTypeId { get; set; }
        public int? ADSessionCategory { get; set; }
        public int? ADSessionCategoryForGroup { get; set; }
        public int? SHNSessionCategory { get; set; }
        public int? SHNSessionCategoryForGroup { get; set; }
        public int? BESessionCategoryForGroup { get; set; }
        public int? ECCDSessionCategoryForGroup { get; set; }
        public string OverrideModel { get; set; }
        public string SixMonthComplete { get; set; }
        public string HomeNo { get; set; }
        public int? ImplementationStatusId { get; set; }

        // For Exprot 
        public string ModelName { get; set; }
        public string Extension { get; set; }
        //End
    }
}
