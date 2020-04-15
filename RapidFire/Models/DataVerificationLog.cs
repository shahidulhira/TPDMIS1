using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidFireLib.Models
{
    [Table("DataVerificationLog")]
    public class DataVerificationLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LogId { get; set; }
        public string TableName { get; set; }
        public int OperationType { get; set; }
        public string PrimaryKey { get; set; }
        public long Id { get; set; }
        public string UUID { get; set; }
        public int? RecordType { get; set; }
        public int? NotifiedTo { get; set; }
        public int NotifiedBy { get; set; }
        public int IsNotified { get; set; }
        public int? IsClientUpdated { get; set; }
        public DateTime? ClientUpdateDate { get; set; }
        public string ChildTables { get; set; }
    }
}
