using RapidFireLib.Lib.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidFireLib.Models
{
    [Context("DefaultMSSQLContext")]
    public class AuditTrail
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AuditId { get; set; }
        public string RecordId { get; set; }
        public string UserId { get; set; }
        public string ModelName { get; set; }
        public string OperationType { get; set; }
        public DateTime AuditDate { get; set; }
    }
}
