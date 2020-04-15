using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates
{
    [Table("SurveyAnswer")]
    public class SurveyAnswer
    {
        [Key]
        public int RecordId { get; set; }        
        public long SurveyId { get; set; }
        public long QuestionId { get; set; }
        public string AnswerText { get; set; }        
        public string AnswerId { get; set; }
        public string AnswerValue { get; set; }
    }
}
