using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Models.Reports
{
    public class DistributionReport
    {
        public Distribution_Report_Top Distribution_Report_Top { get; set; }
        public List<Distribution_BenInfo> Distribution_BenInfo = new List<Distribution_BenInfo>();
    }
    public class Distribution_Report_Top
    {
        public decimal? TotalQuantityWsbPlusPlus { get; set; }
        public decimal? TotalQuantityRUSF { get; set; }
        public decimal? TotalQuantityWsbPlus { get; set; }
        public decimal? TotalQuantityVegetableOil { get; set; }
        public int? BSFPTotal { get; set; } 
        public int? TSFPTotal { get; set; }
        public int? PLWTotal { get; set; }
        public string DistrictName { get; set; }
        public string UpazilaName { get; set; }
        public string CenterName { get; set; }
        public string CampName { get; set; }
        public string DistributionDate { get; set; }

    }
    public class Distribution_BenInfo
    {
        public Int64 SLNO { get; set; }
        public string BlockName { get; set; }
        public string BenId { get; set; }
        public string ScopeId { get; set; }
        public string BenName { get; set; }
        public string BenType { get; set; }
        public string BenStatus { get; set; }
        public string Gender { get; set; }        
        public string Signature { get; set; }

       
    }
    //public class Distribution_Report_Ben
    //{
    //    public Int64 SLNO { get; set; }
    //    public string BlockName { get; set; }
    //    public string BenName { get; set; }
    //    public string Gender_1 { get; set; }
    //    public string Gender_2 { get; set; }
    //    public string Signature { get; set; }
    //}
}
