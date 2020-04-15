using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregates.GroupInfo
{
    public class GroupInfoE
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }
        public long? ServerRecordId { get; set; }
        public string UUID { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
        public string GroupId { get; set; }
        public DateTime? GroupFormationDate { get; set; }
        public string ValidUntil { get; set; }
        public string GeoType { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string RcNSchoolCode { get; set; }
        public DateTime? DataCollectionDate { get; set; }
        public string DataCollectionTime { get; set; }
        public int? UserId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string VerificationNote { get; set; }
        public DateTime EntryDate { get; set; }
    }
    public class GroupMemberInfoE
    {
        public long RowId { get; set; }
        public long? RecordId { get; set; }
        public string UUID { get; set; }
        public string BenId { get; set; }
        public string UID { get; set; }
        public string BenName { get; set; }
        public string Gender { get; set; }
        public DateTime? EnrolledDate { get; set; }
        public DateTime? DropOutDate { get; set; }
        public string DropoutReason { get; set; }
        public string MemberRemarks { get; set; }
        public int? UserId { get; set; }
        public int? Status { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? IsSponsored { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string VerificationNote { get; set; }
        public DateTime? EntryDate { get; set; }
    }

}
