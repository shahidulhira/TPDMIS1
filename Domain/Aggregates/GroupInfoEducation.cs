using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Aggregates
{
    [Table("GroupInfoE")]
    public class GroupInfoE 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }
        public long ServerRecordId { get; set; }
        public string UUID { get; set; }
        public String TypeId { get; set; }
        public String CategoryId { get; set; }
        public String GroupId { get; set; }
        public DateTime? GroupFormationDate { get; set; }
        public String ValidUntil { get; set; }
        public String GeoType { get; set; }
        public String DistrictCode { get; set; }
        public String UpazilaCode { get; set; }
        public String UnionCode { get; set; }
        public String VillageCode { get; set; }
        public String RcNSchoolCode { get; set; }
        public DateTime? DataCollectionDate { get; set; }
        public String DataCollectionTime { get; set; }
        public int UserId { get; set; }
        public String Latitude { get; set; }
        public String Longitude { get; set; }
        public int? Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public String VerificationNote { get; set; }

        public List<GroupMemberInfoE> GroupMemberInfoE { get; set; }
        
    }

    [Table("GroupMemberInfoE")]
    public class GroupMemberInfoE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RowId { get; set; }
        public long RecordId { get; set; }
        public String BenId { get; set; }
        public String UID { get; set; }
        public String UUID { get; set; }
        public String BenName { get; set; }
        public String Gender { get; set; }
        public DateTime? EnrolledDate { get; set; }
        public DateTime? DropOutDate { get; set; }
        public String DropoutReason { get; set; }
        public String MemberRemarks { get; set; }
        public int? Status { get; set; }
        public int? UserId { get; set; }
        public String Latitude { get; set; }
        public String Longitude { get; set; }
        public int? isSponsored { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public String VerificationNote { get; set; }

    }
    public class GroupInfoERequestParam
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string RcNSchoolCode { get; set; }
        public string UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public int RecordId { get; set; }
    }
    public class GroupInfoEStoreProcedureModel
    {
        public Int64 SN { get; set; }
        public Int64 RecordId { get; set; }
        public string UserName { get; set; }
        public string TypeId { get; set; }
        public string GroupId { get; set; }
        public string CategoryId { get; set; }
        public DateTime? GroupFormationDate { get; set; }

        public string GroupFormationDateToString => GroupFormationDate?.ToString("dd-MM-yyyy");
        public string ValidUntil { get; set; }

        //String But Start With IS
        public string IsGroupDeactivate { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string VerificationNote { get; set; }
        public int? IsVerified { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? VerifierId { get; set; }
        public string VerifierName { get; set; }
        public int TotalRecord { get; set; }
        public int TotalDetails { get; set; }

        public List<GroupMemberInfoE> GroupMemberInfos { get; set; }
    }
    [Table("GroupNameE")]
    public class GroupNameE
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
    }
    public class GroupInfoEViewModel
    {
        public List<GroupInfoEStoreProcedureModel> lstGroup = new List<GroupInfoEStoreProcedureModel>();
    }
}
