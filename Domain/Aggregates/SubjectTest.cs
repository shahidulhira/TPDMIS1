using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregates
{
    public class SubjectTest
    {
        [Key]
        public int Id { get; set; }
        public int StudentId{ get; set; }
        public string Subject{ get; set; }

    }
}
