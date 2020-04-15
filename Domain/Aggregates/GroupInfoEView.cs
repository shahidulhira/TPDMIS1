using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public class GroupInfoEView : BaseViewModel
    {
        public Int64 RecordId { get; set; }
        public String TypeName { get; set; }
        public String CategoryName { get; set; }
        public String GroupName { get; set; }
        public string FormationDate { get; set; }
        public String ValidUntil { get; set; }
        public string Condition { get; set; }
        public string ConditionColor { get; set; }
        public int TotalMember { get; set; }
        public string RcNSchoolName { get; set; }
    }
    public class GroupMemberInfoEView
    {
        public long RowId { get; set; }
        public string BenId { get; set; }
        public string BenName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string EnrolledDate { get; set; }
        //public string DropOutDate { get; set; }
        //public string DropoutReason { get; set; }
        //public string MemberRemarks { get; set; }
        //public string DropOutDateColor { get; set; }
        public int? IsVerified { get; set; }
    }
}
