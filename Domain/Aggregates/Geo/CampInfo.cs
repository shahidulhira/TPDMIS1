using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Aggregates
{
    public class CampInfo
    {
        [Key]
        public int CampId { get; set; }
        public string CampName { get; set; }
        public string CampNameBn { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public int? IsActive { get; set; }
    }
}
