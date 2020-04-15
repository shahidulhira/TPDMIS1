using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Aggregates
{
    [Table("TrainingInfoMaterial")]
    public class TrainingInfoMaterial
    {
        [Key]
        public long? RowId { get; set; }
        public string TrainingId { get; set; }
        public string MaterialId { get; set; }
        public string MaterialType { get; set; }
    }
}