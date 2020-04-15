using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Aggregates
{
    [Table("TrainingTopic")]
    public class TrainingTopic
    {
        [Key]
        public string TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicNameEn { get; set; }
        public string TopicType { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
    }
}