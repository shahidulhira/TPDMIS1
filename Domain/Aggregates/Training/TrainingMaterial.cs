using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Aggregates
{
    [Table("TrainingMaterial")]
    public class TrainingMaterial
    {
        [Key]
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialNameEn { get; set; }
        public string MaterialType { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
    }
}