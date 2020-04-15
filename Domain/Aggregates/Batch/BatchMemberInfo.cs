using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Aggregates
{
    [Table("BatchMemberInfo")]
    public class BatchMemberInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordID { get; set; }
        public long? RowId { get; set; }
        public string UUID { get; set; }
        public string BatchId { get; set; }
        public string ProfileId { get; set; }
        public string EnrolledDate { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string ExitDate { get; set; }
        public string ExitReason { get; set; }
        public string Remarks { get; set; }
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
    }
}