using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Aggregates
{
    [Table("TrainingInfoTopic")]
    public class TrainingInfoTopic
    {
        [Key]
        public long? RowId { get; set; }
        public string TrainingId { get; set; }
        public string TopicId { get; set; }
        public string TopicType { get; set; }
    }
}