using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates.Common
{
    [Table("Approval")]
    public class Approval
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ApprovalId { get; set; }
        public string ApprovalItemId { get; set; }
        public string TableName { get; set; }
        public string Text { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public int? RequestedBy { get; set; }
        public DateTime? RequestOn { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalUserType { get; set; }
    }

    public class ApprovalView
    {
        public long ApprovalId { get; set; }
        public string ApprovalItemId { get; set; }
        public string TableName { get; set; }
        public string Text { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public int? RequestedBy { get; set; }
        public string RequestedByName { get; set; }
        public DateTime? RequestOn { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? ApprovalUserType { get; set; }
        public int ApprovalUserGroupTypeName { get; set; }
    }
}