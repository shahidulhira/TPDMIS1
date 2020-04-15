using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Aggregates
{
    [Table("TrainingCategory")]
    public class TrainingCategory
    {
        [Key]
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeNameEn { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameEn { get; set; }
    }
}