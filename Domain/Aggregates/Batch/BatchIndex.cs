using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Aggregates
{
    [Table("BatchIndex")]
    public class BatchIndex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
        public string BatchCode { get; set; }
        public string BatchName { get; set; }
    }
}