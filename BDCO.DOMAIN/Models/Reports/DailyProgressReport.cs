using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Models.Reports
{
    public class DailyProgressReportView
    {
      
        public int? Id { get; set; }
        public int SLNO { get; set; }
        public string Program { get; set; }
        public string Category { get; set; }
        public string Indicator { get; set; }
        public string Boy_6_23 { get; set; }
        public string Girls_6_23 { get; set; }
        public string Boy_24_59 { get; set; }
        public string Girls_24_59 { get; set; }
        public string PLW { get; set; }
    }
}
