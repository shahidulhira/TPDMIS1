using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Aggregates
{
    [Table("TrainingMemberInfo")]
    public class TrainingMemberInfo
    {
        [Key]
        public long? RowId { get; set; }
        public string TrainingId { get; set; }
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public string UUID { get; set; }
        public string PresenceType { get; set; }
        public int? Present { get; set; }
        public string AttendanceRemarks { get; set; }
    }
}