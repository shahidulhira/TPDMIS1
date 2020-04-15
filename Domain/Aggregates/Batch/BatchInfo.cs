using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Aggregates
{
    [Table("BatchInfo")]
    public class BatchInfo
    {
        public BatchInfo()
        {
            BatchMemberInfo = new List<BatchMemberInfo>();
        }
        public long RecordID { get; set; }
        [Key]
        public string BatchId { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
        public string BatchCode { get; set; }
        public int? BatchYear { get; set; }
        public string FormationDate { get; set; }
        public int? Activeness { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string CenterCode { get; set; }
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
        public List<BatchMemberInfo> BatchMemberInfo { get; set; }
    }
}