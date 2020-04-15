using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates
{
    [Table("SurveyQuestion")]
    public class SurveyQuestion
    {
        public SurveyQuestion()
        {
            SurveyAnswers = new List<SurveyAnswer>();
        }
        [Key]
        public long QuestionId { get; set; }
        public long SurveyId { get; set; }
        public int PageNo { get; set; }
        public string QuestionText { get; set; }
        public int ViewType { get; set; }
        public string QuestionName { get; set; }
        public int UserId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual List<SurveyAnswer> SurveyAnswers { get;set;}
    }
}
