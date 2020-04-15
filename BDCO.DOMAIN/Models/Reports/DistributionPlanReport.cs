using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Models.Reports
{
    public class DistributionPlanView
    {
        public int? RecordId { get; set; }
        //public long RowNo { get; set; }
        public string BenId { get; set; }
        public string BenName { get; set; }
        public string Block { get; set; }
        public string Age { get; set; }
        public string BenStatus { get; set; }
        public string BenType { get; set; }
        public string LastVisitDate { get; set; }
        public string NextVisitDate { get; set; }
        
        public decimal? WsbPlus { get; set; }
        public decimal? WsbPlusPlus { get; set; }
        public decimal? Rusf { get; set; }
        public decimal? VegetableOil { get; set; }
        public int? TotalRecord { get; set; }
    }
}
