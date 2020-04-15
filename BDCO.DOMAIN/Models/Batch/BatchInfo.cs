using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    [Table("BatchIndex")]
    public class BatchIndex
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordID { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
        public string BatchCode { get; set; }
        public string BatchName { get; set; }
    }

    [Table("BatchInfo")]
    public class BatchInfo :BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordID { get; set; }
        [Key]
        public string BatchId { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
        public string BatchCode { get; set; }
        public int? BatchYear { get; set; }
        public string FormationDate { get; set; }
        public int? Activeness { get; set; }
    }

    [Table("BatchMemberInfo")]
    public class BatchMemberInfo : BaseModel
    {
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
    }

}
